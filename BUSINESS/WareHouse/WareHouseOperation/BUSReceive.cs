using System;
using DataEntity;
using System.Data;
using System.Collections;

namespace Business.WareHouseManagment
{
	/// <summary>
	/// 收料业务逻辑类 Liujun Add at 2007-6-22
	/// </summary>
	public class BUSReceive : BUSBase
	{
		/// <summary>
		/// 收料业务逻辑类
		/// </summary>
		public BUSReceive()
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

		#region 校验数据逻辑

		#region 校验实收数量的累加是否大于可收数量(防止一个物品放在多个库位上而发生数量错误)

		/// <summary>
		/// 校验实收数量的累加是否大于可收数量
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dt)
		{
			string sErrMsg = "";
			foreach(DataRow row in dt.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					//实收数量
					decimal decFactReceivedQuantity = Convert.ToDecimal(row["WH_ReceiveMaterial.FactReceivedQuantity"].ToString());
					//可收数量
					decimal decCanReceivedQuantity = Convert.ToDecimal(row["WH_ReceiveMaterial.CanReceivedQuantity"].ToString());

					//实收数量应小于可收数量
					if(decFactReceivedQuantity > decCanReceivedQuantity)
					{
						sErrMsg ="FactQuantityAboveCanReceiveQuantity";
						break;

					}
				}
			}	
			return sErrMsg;
		}


		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtChild)
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
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			// 实收数量总额
			decimal mTotal = 0;
			decimal mTotalPriceNaturalER = 0;
			decimal mTotalPriceStandardlER = 0;
			
			// 如果子表中存在实收数量则更新其总计
			foreach ( DataRow dr in dtChild.Rows )
			{
				//对本位币汇率
				decimal dTotalPriceNaturalER = Convert.ToDecimal ( dtEdit.Rows[0]["WH_Receive.TotalPriceNaturalER"].ToString() ) ;
				//对核算币汇率
				decimal dTotalPriceStandardlER = Convert.ToDecimal ( dtEdit.Rows[0]["WH_Receive.TotalPriceStandardlER"].ToString() ) ;
				if(dr.RowState != DataRowState.Deleted)
				{
					dr["WH_ReceiveMaterial.UnitPriceNatural"] = Convert.ToDecimal ( dr["WH_ReceiveMaterial.UnitPrice"] ) * dTotalPriceNaturalER;
					dr["WH_ReceiveMaterial.UnitPriceStandard"] = Convert.ToDecimal ( dr["WH_ReceiveMaterial.UnitPrice"] )  * dTotalPriceStandardlER;
					dr["WH_ReceiveMaterial.SumPrice"] = Convert.ToDecimal( dr["WH_ReceiveMaterial.UnitPrice"].ToString() ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() );
					mTotal += Convert.ToDecimal( dr["WH_ReceiveMaterial.SumPrice"].ToString() );
					mTotalPriceNaturalER += Convert.ToDecimal( dr["WH_ReceiveMaterial.UnitPriceNatural"].ToString() ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() ) ;
					mTotalPriceStandardlER += Convert.ToDecimal( dr["WH_ReceiveMaterial.UnitPriceStandard"].ToString() ) * Convert.ToDecimal ( dr["WH_ReceiveMaterial.FactReceivedQuantity"].ToString() ) ;
				}
			}
			dtEdit.Rows[0]["WH_Receive.TotalPrice"] = mTotal ;
			dtEdit.Rows[0]["WH_Receive.TotalPriceStandarCUR"] = mTotalPriceStandardlER ;
			dtEdit.Rows[0]["WH_Receive.TotalPriceNaturalCUR"] = mTotalPriceNaturalER ;			
		}		

	}

	#region 收料实体

	/// <summary>
	///  收料实体
	/// </summary>
	public class ReceiveMaterialEntity
	{
		private string _ItemCode;

		/// <summary>
		/// 物资编码
		/// </summary>
		public string ItemCode
		{
			set { this._ItemCode = value; }
			get { return this._ItemCode; }
		}

		private double _CanReceivedQuantity;

		/// <summary>
		/// 可收数量
		/// </summary>
		public double CanReceivedQuantity
		{
			set { this._CanReceivedQuantity = value; }
			get { return this._CanReceivedQuantity; }
		}

		private double _FactReceiveQuantity;

		/// <summary>
		/// 实收数量
		/// </summary>
		public double FactReceiveQuantity
		{
			set { this._FactReceiveQuantity = value; }
			get { return this._FactReceiveQuantity; }
		}
	}

	#endregion
}
