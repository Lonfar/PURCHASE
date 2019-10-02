using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEInStoreMaterialDetailReport 的摘要说明。
	/// </summary>
	public class DAEInStoreMaterialDetailReport : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"select distinct WH_InStoreMaterialDetail.ItemCode,WH_InStoreMaterialDetail.POID,WH_InStoreMaterialDetail.WHID,
					WH_InStoreMaterialDetail.QuantityInBin,WH_InStoreMaterialDetail.BINID,WH_InStoreMaterialDetail.PartNo,
					WH_InStoreMaterialDetail.UnitPricePONatural,WH_InStoreMaterialDetail.UnitPricePOStandard,
					WH_InStoreMaterialDetail.UnitPricePONatural * WH_InStoreMaterialDetail.QuantityInBin as TotalPricePONatural ,
					WH_InStoreMaterialDetail.UnitPricePOStandard * WH_InStoreMaterialDetail.QuantityInBin as TotalPricePOStandard,
					Material.MaterialName, WH_BI_UOM.UOMName 
					from WH_InStoreMaterialDetail  
					left join Material on Material.ItemCode = WH_InStoreMaterialDetail.ItemCode  
					left join WH_BI_UOM on WH_BI_UOM.UOMID = WH_InStoreMaterialDetail.UOMID  
					left join WH_BI_WareHouse  on WH_BI_WareHouse.WHID=WH_InStoreMaterialDetail.WHID  "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere);
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
