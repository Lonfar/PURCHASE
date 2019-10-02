using System;
using DataEntity;
using System.Data;
using System.Collections;

namespace Business.WareHouseManagment
{
	/// <summary>
	/// 收料业务逻辑类 Liujun Add at 2007-6-22
	/// </summary>
	public class BUSDirectArrivalMaterial : BUSBase
	{
		/// <summary>
		/// 收料业务逻辑类
		/// </summary>
		public BUSDirectArrivalMaterial()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 获得订单编号的筛选

		/// <summary>
		/// 获得订单编号的筛选条件
		/// </summary>
		/// <returns></returns>
		public string GetPOIDFilter ()
		{
			// 设置PO状态为Open的.
			return " PurchaseOrder.Status = "+ (int)POReceiveState.State_Open;
		}

		#endregion

		#region 获得库房ID的筛选条件

		/// <summary>
		/// 获得库房ID的筛选条件
		/// </summary>
		/// <returns></returns>
		public string GetWHIDFilter ()
		{
			// 库位数据实体类
			DataEntity.DAEBIBIN dAEBIBIN = new DAEBIBIN();
			string sFilter = string.Empty;

			// 查询
			string sWHIDs = dAEBIBIN.GetWHHasBIN();
			if ( sWHIDs.Length > 0 )
			{
				sFilter = " WH_BI_WareHouse.WHID IN ( " + sWHIDs + ")";
			}

			return sFilter;
		}

		#endregion

		#region 获得库位的筛选条件

		/// <summary>
		/// 根据库房获得对应库位的筛选条件
		/// </summary>
		/// <param name="sWHID">WHID</param>
		/// <returns></returns>
		public string GetBINIDFilter ( string sWHID )
		{
			// 库位数据实体类
			DataEntity.DAEBIBIN dAEBIBIN = new DAEBIBIN();
			string sFilter = string.Empty;

			// 查询
			string sBINIDs = dAEBIBIN.GetAllBINIDByWHID ( sWHID );
			if ( sBINIDs.Length > 0 )
			{
				sFilter = "WH_BI_BIN.BINID IN ( "+sBINIDs+" )";
			}

			return sFilter;
		}

		#endregion

		#region 校验实收数量的累加是否大于可收数量(防止一个物品放在多个库位上而发生数量错误)

		/// <summary>
		/// 校验实收数量的累加是否大于可收数量
		/// </summary>
		/// <param name="dtReceiveMaterial">物资子表</param>
		/// <returns></returns>
		private bool CheckFactReceiveQuantityTotal ( DataTable dtReceiveMaterial )
		{
			ArrayList list = new ArrayList();
			bool bRetValue = true;

			// 获得物资(物资编码及可收数量)
			foreach ( DataRow dr in dtReceiveMaterial.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					bool bIsExist = false;

					for ( int i = 0 ; i < list.Count ; i ++ )
					{
						ReceiveMaterialEntity entity = list[i] as ReceiveMaterialEntity;
						if ( entity != null )
						{
							if ( entity.ItemCode == dr["WH_ReceiveMaterial.ItemCode"].ToString() ) 
							{
								entity.FactReceiveQuantity += Convert.ToDouble( dr["WH_ReceiveMaterial.FactReceivedQuantity"] );

								if ( entity.FactReceiveQuantity > entity.CanReceivedQuantity )
								{
									return false;
								}

								bIsExist = true;
								break;
							}
						}
					}

					if ( ! bIsExist )
					{
						ReceiveMaterialEntity receiveMaterial = new ReceiveMaterialEntity();

						receiveMaterial.ItemCode = dr["WH_ReceiveMaterial.ItemCode"].ToString();
						receiveMaterial.CanReceivedQuantity = Convert.ToDouble( dr["WH_ReceiveMaterial.CanReceivedQuantity"] );
						receiveMaterial.FactReceiveQuantity = Convert.ToDouble( dr["WH_ReceiveMaterial.FactReceivedQuantity"] );

						if ( receiveMaterial.FactReceiveQuantity > receiveMaterial.CanReceivedQuantity )
						{
							return false;
						}

						list.Add ( receiveMaterial );
					}
				}
			}
			return bRetValue;
		}


		#endregion

		#region 校验数据逻辑

		/// <summary>
		/// 校验数据逻辑
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// 错误信息
			string sErrorMsg = string.Empty;	
			sErrorMsg = CheckChildRows(dt);
			if ( sErrorMsg.Trim().Length == 0 )
			{
				// 物资子表逻辑校验
				bool bCheck = CheckFactReceiveQuantityTotal ( dt );
				if ( !bCheck ) 
				{
					sErrorMsg = "FactQuantityAboveCanReceiveQuantity"; 
				}
			}

			return sErrorMsg;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt">Edit表</param>
		/// <returns>sErrMsg</returns>
		private string CheckChildRows(DataTable dtChild)
		{
			foreach(DataRow dr in dtChild.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoMaterialSelected" ;
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtEdit"></param>
		/// <param name="dtChild"></param>
		/// <param name="sWHID"></param>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild,DataTable dtIssue,string sWHID)
		{
            // 实收数量总额
            decimal mTotal = 0;
            decimal mTotalPriceNaturalER = 0;
            decimal mTotalPriceStandardlER = 0;

            // 如果子表中存在实收数量则更新其总计
            foreach (DataRow dr in dtChild.Rows)
            {
                //------------------add by  wud ---------------2007-8-8-------------
                //对本位币汇率
                decimal dTotalPriceNaturalER = Convert.ToDecimal(dtEdit.Rows[0]["WH_Receive.TotalPriceNaturalER"].ToString());
                //对核算币汇率
                decimal dTotalPriceStandardlER = Convert.ToDecimal(dtEdit.Rows[0]["WH_Receive.TotalPriceStandardlER"].ToString());
                if (dr.RowState != DataRowState.Deleted)
                {
                    dr["WH_ReceiveMaterial.UnitPriceNatural"] = Convert.ToDecimal(dr["WH_ReceiveMaterial.UnitPrice"]) * dTotalPriceNaturalER; //Convert.ToDecimal ( dr["WH_ReceiveMaterial.UnitPrice"] ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() ) * dTotalPriceNaturalER;
                    dr["WH_ReceiveMaterial.UnitPriceStandard"] = Convert.ToDecimal(dr["WH_ReceiveMaterial.UnitPrice"]) * dTotalPriceStandardlER; //Convert.ToDecimal ( dr["WH_ReceiveMaterial.UnitPrice"] ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() ) * dTotalPriceStandardlER;
                    dr["WH_ReceiveMaterial.SumPrice"] = Convert.ToDecimal(dr["WH_ReceiveMaterial.UnitPrice"].ToString()) * Convert.ToDecimal(dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString());
                    mTotal += Convert.ToDecimal(dr["WH_ReceiveMaterial.SumPrice"].ToString());
                    mTotalPriceNaturalER += Convert.ToDecimal(dr["WH_ReceiveMaterial.UnitPrice"]) * Convert.ToDecimal(dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString()) * dTotalPriceNaturalER; ;// Convert.ToDecimal(dr["WH_ReceiveMaterial.UnitPriceNatural"].ToString());
                    mTotalPriceStandardlER += Convert.ToDecimal(dr["WH_ReceiveMaterial.UnitPrice"]) * Convert.ToDecimal(dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString()) * dTotalPriceStandardlER; //Convert.ToDecimal(dr["WH_ReceiveMaterial.UnitPriceStandard"].ToString());
                }
            }
            dtEdit.Rows[0]["WH_Receive.TotalPrice"] = mTotal;
            dtEdit.Rows[0]["WH_Receive.TotalPriceStandarCUR"] = mTotalPriceStandardlER;
            dtEdit.Rows[0]["WH_Receive.TotalPriceNaturalCUR"] = mTotalPriceNaturalER;
            dtIssue.Rows[0]["WH_Issue.TotalPriceNatural"] = mTotalPriceNaturalER;
            dtIssue.Rows[0]["WH_Issue.TotalPriceStandard"] = mTotalPriceStandardlER;
		}		
		
	}
}
