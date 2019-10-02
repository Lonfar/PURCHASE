using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAERejectEdit ��ժҪ˵����
	/// </summary>
	public class DAERejectEdit : DAEBase
	{
		public DAERejectEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public void UpdateRejectMaterial(DataTable dtRejectMaterial,PriceType enWHPriceType)
		{
			foreach ( DataRow drRejectMaterial in dtRejectMaterial.Rows )
			{
				if(drRejectMaterial.RowState != DataRowState.Deleted)
				{
					string sSql = @"SELECT WH_InStoreMaterialDetail.*,Material.MaterialName,MaterialUOM.MaterialUomID ,
								ISNULL(WH_InStoreMaterialDetail.QuantityInBin,0) - ISNULL(WH_InStoreMaterialDetail.PreserveQuantity,0 ) as CanReject 
								From WH_InStoreMaterialDetail Left JOIN Material ON 
								Material.ItemCode = WH_InStoreMaterialDetail.ItemCode LEFT JOIN  MaterialUOM 
								on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID  AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1
								WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+drRejectMaterial["InStockMaterialID"].ToString()+"'";
					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{					
						//�������	
						drRejectMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ; 
						drRejectMaterial["WH_RejectMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ; 
						//���
						drRejectMaterial["QuantityInBin"] = dtTempInfo.Rows[0]["CanReject"] ;
						//					drRejectMaterial["QuantityInBin"] = dtTempInfo.Rows[0]["QuantityInBin"] ;
						//��λ	
						drRejectMaterial["BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						drRejectMaterial["WH_RejectMaterial__BINID"] = dtTempInfo.Rows[0]["BINID"] ;

						drRejectMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;
						//��λ	
						drRejectMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drRejectMaterial["WH_RejectMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UomID"] ;
						if(enWHPriceType == PriceType.TYPE_PO)
						{
							//������λ����(��)	
							drRejectMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"] ;
							//������λ����(��)	
							drRejectMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"] ;	
						}
						else if(enWHPriceType == PriceType.TYPE_Average)
						{
							//������λ����(��)
							drRejectMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"] ;
							//������λ����(��)	
							drRejectMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["AveragePriceStandard"] ;

						}
						//��������
						drRejectMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ;
					}
				}
			}
		}

		public void CalTotalAmount(DataTable dtEdit ,DataTable dtChild)
		{
			decimal decTotalAmountStandard = 0 ;
			decimal decTotalAmountNatural = 0 ;
			foreach(DataRow drRejectMaterial in dtChild.Rows)
			{
				if(drRejectMaterial.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(drRejectMaterial["WH_RejectMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(drRejectMaterial["WH_RejectMaterial.UnitPriceNatural"].ToString());
					decimal decQuantityReject =  Convert.ToDecimal(drRejectMaterial["WH_RejectMaterial.QuantityReject"].ToString());
					decTotalAmountStandard += decUnitPriceStandard * decQuantityReject;
					decTotalAmountNatural += decUnitPriceNatural * decQuantityReject ;
				}
			}
			dtEdit.Rows[0]["WH_Reject.TotalPriceStandard"] = decTotalAmountStandard ;
			dtEdit.Rows[0]["WH_Reject.TotalPriceNatural"] = decTotalAmountNatural ;
		}

		#region ���ύ���ʱ����У��( ���������Ƿ���ڿ������ )

		/// <summary>
		/// ���ύ���ʱ����У��
		/// </summary>
		/// <param name="sReceiveID">ת�ϵ����</param>
		/// <returns></returns>
		public string CheckNum ( string sRejectID )
		{
			string sErrorMsg = string.Empty;

			string sSelectRejectMaterial = @"select WH_InStoreMaterialDetail.ItemCode,WH_InStoreMaterialDetail.InStockMaterialID,QuantityReject,
														WH_InStoreMaterialDetail.QuantityInBin 
														from WH_RejectMaterial 
														left join WH_InStoreMaterialDetail on WH_RejectMaterial.InStockMaterialID 
														= WH_InStoreMaterialDetail.InStockMaterialID
														where 
														WH_InStoreMaterialDetail.QuantityInBin - WH_RejectMaterial.QuantityReject < 0 AND RejectID = '"+sRejectID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectRejectMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ 
				sErrorMsg = dt.Rows[0]["ItemCode"].ToString();
			}

			return sErrorMsg;
		}

		#endregion

		#region ���±��ϵ���״̬

		public string UpdateRejectState ( string sRejectID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_Reject SET Status = "+iState.ToString()+" WHERE RejectID = '"+sRejectID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			if(sErrorMsg.Length == 0)
			{			
				if(state == ApproveState.State_Approved)
				{
					string sSql = "SELECT a.* , b.WHID,b.RejectNO,b.CreateBy FROM WH_RejectMaterial a left join WH_Reject b on a.RejectID = b.RejectID WHERE a.RejectID = '"+sRejectID+"'";
					DataTable dtRejectMaterial = this.BaseDataAccess.GetDataTable ( sSql );
					
					foreach(DataRow drRejectMaterial in dtRejectMaterial.Rows)
					{
						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
						//����
						CInStoreMaterialDetail pInStore = new CInStoreMaterialDetail();
						pInStore.StoreOperateType = STOREOPERATETYPE.TYPE_OUT ;
						pInStore.OperateHistory = true;
						pInStore.InStockMaterialID = drRejectMaterial["InStockMaterialID"].ToString() ;
						pInStore.QuantityInBinSet  = decimal.Parse(drRejectMaterial["QuantityReject"].ToString()) ;
						pInStoreMaterialDetailAccess.OperateStore(pInStore);	
				
						//������д�����ӿ�
						CInterfaceOfFinanceAccess����pInterfaceOfFinanceAccess = new CInterfaceOfFinanceAccess() ;
						CInterfaceOfFinance pInterfaceOfFinance = new CInterfaceOfFinance();
						//�ⷿ
						pInterfaceOfFinance.Location = drRejectMaterial["WHID"].ToString() ;
						//���ʱ���
						pInterfaceOfFinance.ItemCode = drRejectMaterial["ItemCode"].ToString() ;
						//��λ
						pInterfaceOfFinance.BinNo = drRejectMaterial["BINID"].ToString() ;
						//���ݺ�
						pInterfaceOfFinance.BillNo = drRejectMaterial["RejectNO"].ToString() ;
						//������
						pInterfaceOfFinance.Operater = drRejectMaterial["CreateBy"].ToString() ;
						//�ǳ��⻹�����
						pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_OUT ;
						//��������
						pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_Reject) ;
						//������λ������
						pInterfaceOfFinance.Quantity =  decimal.Parse(drRejectMaterial["QuantityReject"].ToString()) ;
						//���㵥��
						pInterfaceOfFinance.UnitPriceStandard =  decimal.Parse(drRejectMaterial["UnitPriceStandard"].ToString()) ;

						pInterfaceOfFinanceAccess.OperateInterface(pInterfaceOfFinance) ;
					}
				}
			}
			return sErrorMsg;
		}

		#endregion
	}
}
