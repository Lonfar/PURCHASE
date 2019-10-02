using System;
using System.Data;
using System.Data.SqlClient;


namespace DataEntity
{
	/// <summary>
	/// �������ݿ��ȡ���ݡ�
	/// </summary>
	public class DAEBorrowEdit: DAEBase
	{
		public DAEBorrowEdit()
		{

		}
		#region �����ӱ�
		public void UpdateBorrowMaterial(DataTable dtIssueMaterial,PriceType enWHPriceType)
		{
			foreach ( DataRow drIssueMaterial in dtIssueMaterial.Rows )
			{
				if(drIssueMaterial.RowState != DataRowState.Deleted)
				{
					string sSql = @"SELECT WH_InStoreMaterialDetail.*,MaterialUOM.MaterialUomID,Material.MaterialName
								From WH_InStoreMaterialDetail
							 LEFT JOIN  MaterialUOM 
								on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID  AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode AND MaterialUOM.IsBaseUOM = 1
								inner JOIN Material on Material.ItemCode = WH_InStoreMaterialDetail.ItemCode
							WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+drIssueMaterial["InStockMaterialID"].ToString()+"'";
					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{
					
						//�������	
						drIssueMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ; 
						drIssueMaterial["WH_BorrowMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ; 
					   
						//��λ	
						drIssueMaterial["BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						drIssueMaterial["WH_BorrowMaterial__BINID"] = dtTempInfo.Rows[0]["BINID"] ;

						drIssueMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;
						//��λ	
						drIssueMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;

						drIssueMaterial["WH_BorrowMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UOMID"] ;
						
						//�������	
						drIssueMaterial["QuantityInBin"] = dtTempInfo.Rows[0]["QuantityInBin"] ;
						//Ԥ������	
						//					drIssueMaterial["PreserveQuantity"] = dtTempInfo.Rows[0]["PreserveQuantity"] ;
						//�ɷ�����	
						//					drIssueMaterial["CanIssuedQuantity"] = DetIssueMaterial(dtTempInfo.Rows[0]["QuantityInBin"].ToString(),dtTempInfo.Rows[0]["PreserveQuantity"].ToString());		
						if(enWHPriceType == PriceType.TYPE_PO)
						{
							//������λ����(��)	
							drIssueMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"] ;
							//ʵ������						
							drIssueMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"] ;
						}
						else if(enWHPriceType == PriceType.TYPE_Average)
						{
							//������λ����(��)
							drIssueMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"] ;
							//������λ����(��)	
							drIssueMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["AveragePriceStandard"] ;

						}
						drIssueMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ; 
						try
						{
							drIssueMaterial["SumPrice"] = Convert.ToDecimal(drIssueMaterial["FactIssuedQuantity"]) * Convert.ToDecimal(drIssueMaterial["UnitPriceStandard"]);
						}catch
						{}
					}
				}
			}
		}

		#endregion
	}
}
