using System;
using System.Data;
using System.Data.SqlClient;


namespace DataEntity
{
	/// <summary>
	/// 访问数据库获取数据。
	/// </summary>
	public class DAEBorrowEdit: DAEBase
	{
		public DAEBorrowEdit()
		{

		}
		#region 更新子表
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
					
						//订单编号	
						drIssueMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ; 
						drIssueMaterial["WH_BorrowMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ; 
					   
						//库位	
						drIssueMaterial["BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						drIssueMaterial["WH_BorrowMaterial__BINID"] = dtTempInfo.Rows[0]["BINID"] ;

						drIssueMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;
						//单位	
						drIssueMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;

						drIssueMaterial["WH_BorrowMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UOMID"] ;
						
						//库存数量	
						drIssueMaterial["QuantityInBin"] = dtTempInfo.Rows[0]["QuantityInBin"] ;
						//预留数量	
						//					drIssueMaterial["PreserveQuantity"] = dtTempInfo.Rows[0]["PreserveQuantity"] ;
						//可发数量	
						//					drIssueMaterial["CanIssuedQuantity"] = DetIssueMaterial(dtTempInfo.Rows[0]["QuantityInBin"].ToString(),dtTempInfo.Rows[0]["PreserveQuantity"].ToString());		
						if(enWHPriceType == PriceType.TYPE_PO)
						{
							//基本单位单价(核)	
							drIssueMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPricePOStandard"] ;
							//实发数量						
							drIssueMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPricePONatural"] ;
						}
						else if(enWHPriceType == PriceType.TYPE_Average)
						{
							//基本单位单价(本)
							drIssueMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["AveragePriceNatural"] ;
							//基本单位单价(核)	
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
