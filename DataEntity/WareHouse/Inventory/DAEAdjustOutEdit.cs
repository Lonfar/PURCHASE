using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEAdjustOutEdit ��ժҪ˵����
	/// </summary>
	public class DAEAdjustOutEdit : DAEBase
	{
		/// <summary>
		/// 
		/// </summary>
		public DAEAdjustOutEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public void UpdateAdjustOutMaterial(DataTable dtAdjustOutMaterial,PriceType enWHPriceType)
		{
			foreach ( DataRow drAdjustOutMaterial in dtAdjustOutMaterial.Rows )
			{
				if (drAdjustOutMaterial.RowState != DataRowState.Deleted)
				{
					string sSql = @"SELECT WH_InStoreMaterialDetail.*,Material.MaterialName,MaterialUOM.MaterialUomID,QuantityReject From WH_InStoreMaterialDetail LEFT JOIN 
								Material on Material.ItemCode = WH_InStoreMaterialDetail.ItemCode LEFT JOIN  MaterialUOM 
								on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID  AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1
								LEFT JOIN WH_AdjustOutMaterial ON WH_AdjustOutMaterial.InStockMaterialID = WH_InStoreMaterialDetail.InStockMaterialID 
								WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+drAdjustOutMaterial["InStockMaterialID"].ToString()+"'";
					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{					
						//�������	
						drAdjustOutMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ; 
						drAdjustOutMaterial["WH_AdjustOutMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ; 
						//���
						drAdjustOutMaterial["QuantityInBin"] = dtTempInfo.Rows[0]["QuantityInBin"] ;
						//��λ	
						drAdjustOutMaterial["BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						drAdjustOutMaterial["WH_AdjustOutMaterial__BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						//������
						drAdjustOutMaterial["PartNO"] = dtTempInfo.Rows[0]["PartNO"] ;
						drAdjustOutMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;
						//�̿�����
						//					drAdjustOutMaterial["QuantityReject"] = dtTempInfo.Rows[0]["QuantityReject"] ;
						//��λ	
						drAdjustOutMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drAdjustOutMaterial["WH_AdjustOutMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UomID"] ;
						if(enWHPriceType == PriceType.TYPE_PO)
						{
							//������λ����(��)	
							drAdjustOutMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"] ;
							//������λ����(��)	
							drAdjustOutMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"] ;	
						}
						else if(enWHPriceType == PriceType.TYPE_Average)
						{
							//������λ����(��)
							drAdjustOutMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"] ;
							//������λ����(��)	
							drAdjustOutMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["AveragePriceStandard"] ;

						}
						//��������
						drAdjustOutMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ;
					}
				}
			}
		}
		
		#region ���ύ���ʱ����У��( ���������Ƿ���ڿ������ )

		/// <summary>
		/// ���ύ���ʱ����У��
		/// </summary>
		/// <param name="sReceiveID">ת�ϵ����</param>
		/// <returns></returns>
		public string CheckNum ( string sAdjustOutID )
		{
			string sErrorMsg = string.Empty;

			string sSelectRejectMaterial = @"select WH_InStoreMaterialDetail.ItemCode,WH_InStoreMaterialDetail.InStockMaterialID,QuantityReject,
														WH_InStoreMaterialDetail.QuantityInBin 
														from WH_AdjustOutMaterial 
														left join WH_InStoreMaterialDetail on WH_AdjustOutMaterial.InStockMaterialID 
														= WH_InStoreMaterialDetail.InStockMaterialID
														where 
														WH_InStoreMaterialDetail.QuantityInBin - WH_AdjustOutMaterial.QuantityReject < 0 AND AdjustOutID = '"+sAdjustOutID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectRejectMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ 
				sErrorMsg = dt.Rows[0]["ItemCode"].ToString();
			}

			return sErrorMsg;
		}

		#endregion

		#region ���±��ϵ���״̬

		public string UpdateAdjustOutState ( string sAdjustOutID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_AdjustOut SET Status = "+iState.ToString()+" WHERE AdjustOutID = '"+sAdjustOutID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			if(sErrorMsg.Length == 0)
			{			
				if(state == ApproveState.State_Approved)
				{
					string sSql = "SELECT a.* , b.WHID,b.AdjustOutNO,b.CreateBy FROM WH_AdjustOutMaterial a left join WH_AdjustOut b on a.AdjustOutID = b.AdjustOutID WHERE a.AdjustOutID = '"+sAdjustOutID+"'";
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
						pInterfaceOfFinance.BillNo = drRejectMaterial["AdjustOutNO"].ToString() ;
						//������
						pInterfaceOfFinance.Operater = drRejectMaterial["CreateBy"].ToString() ;
						//�ǳ��⻹�����
						pInterfaceOfFinance.OperationDirection =DIRECTIONTYPE.TYPE_OUT ;
						//��������
						pInterfaceOfFinance.OperationType =pInterfaceOfFinanceAccess.GetBillType(BILLTYPE.TYPE_AdjustOut) ;
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
