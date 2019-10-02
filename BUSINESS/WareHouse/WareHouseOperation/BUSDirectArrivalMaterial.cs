using System;
using DataEntity;
using System.Data;
using System.Collections;

namespace Business.WareHouseManagment
{
	/// <summary>
	/// ����ҵ���߼��� Liujun Add at 2007-6-22
	/// </summary>
	public class BUSDirectArrivalMaterial : BUSBase
	{
		/// <summary>
		/// ����ҵ���߼���
		/// </summary>
		public BUSDirectArrivalMaterial()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��ö�����ŵ�ɸѡ

		/// <summary>
		/// ��ö�����ŵ�ɸѡ����
		/// </summary>
		/// <returns></returns>
		public string GetPOIDFilter ()
		{
			// ����PO״̬ΪOpen��.
			return " PurchaseOrder.Status = "+ (int)POReceiveState.State_Open;
		}

		#endregion

		#region ��ÿⷿID��ɸѡ����

		/// <summary>
		/// ��ÿⷿID��ɸѡ����
		/// </summary>
		/// <returns></returns>
		public string GetWHIDFilter ()
		{
			// ��λ����ʵ����
			DataEntity.DAEBIBIN dAEBIBIN = new DAEBIBIN();
			string sFilter = string.Empty;

			// ��ѯ
			string sWHIDs = dAEBIBIN.GetWHHasBIN();
			if ( sWHIDs.Length > 0 )
			{
				sFilter = " WH_BI_WareHouse.WHID IN ( " + sWHIDs + ")";
			}

			return sFilter;
		}

		#endregion

		#region ��ÿ�λ��ɸѡ����

		/// <summary>
		/// ���ݿⷿ��ö�Ӧ��λ��ɸѡ����
		/// </summary>
		/// <param name="sWHID">WHID</param>
		/// <returns></returns>
		public string GetBINIDFilter ( string sWHID )
		{
			// ��λ����ʵ����
			DataEntity.DAEBIBIN dAEBIBIN = new DAEBIBIN();
			string sFilter = string.Empty;

			// ��ѯ
			string sBINIDs = dAEBIBIN.GetAllBINIDByWHID ( sWHID );
			if ( sBINIDs.Length > 0 )
			{
				sFilter = "WH_BI_BIN.BINID IN ( "+sBINIDs+" )";
			}

			return sFilter;
		}

		#endregion

		#region У��ʵ���������ۼ��Ƿ���ڿ�������(��ֹһ����Ʒ���ڶ����λ�϶�������������)

		/// <summary>
		/// У��ʵ���������ۼ��Ƿ���ڿ�������
		/// </summary>
		/// <param name="dtReceiveMaterial">�����ӱ�</param>
		/// <returns></returns>
		private bool CheckFactReceiveQuantityTotal ( DataTable dtReceiveMaterial )
		{
			ArrayList list = new ArrayList();
			bool bRetValue = true;

			// �������(���ʱ��뼰��������)
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

		#region У�������߼�

		/// <summary>
		/// У�������߼�
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// ������Ϣ
			string sErrorMsg = string.Empty;	
			sErrorMsg = CheckChildRows(dt);
			if ( sErrorMsg.Trim().Length == 0 )
			{
				// �����ӱ��߼�У��
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
		/// <param name="dt">Edit��</param>
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
            // ʵ�������ܶ�
            decimal mTotal = 0;
            decimal mTotalPriceNaturalER = 0;
            decimal mTotalPriceStandardlER = 0;

            // ����ӱ��д���ʵ��������������ܼ�
            foreach (DataRow dr in dtChild.Rows)
            {
                //------------------add by  wud ---------------2007-8-8-------------
                //�Ա�λ�һ���
                decimal dTotalPriceNaturalER = Convert.ToDecimal(dtEdit.Rows[0]["WH_Receive.TotalPriceNaturalER"].ToString());
                //�Ժ���һ���
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
