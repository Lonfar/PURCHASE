using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEPOInWareHouse 的摘要说明。
	/// </summary>
	public class DAEPOInWareHouse  : DAEBase
	{
		public DAEPOInWareHouse()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  DataTable GetRptData(string  sWhere)
		{
			string sSql = string.Empty;			
			sSql = @"SELECT WH_InStoreMaterialDetail.WHID, "+
							"PurchaseOrder.POID , "+
							"COUNT(ItemCode) AS Num , "+ 
							"SUM( (QuantityInBin + PreserveQuantity) * UnitPricePONatural ) AS NaturalPrice, "+
							"SUM( (QuantityInBin + PreserveQuantity) * UnitPricePOStandard ) AS StandardPrice "+
							"FROM WH_InStoreMaterialDetail  "+
							"LEFT JOIN PurchaseOrder ON PurchaseOrder.POID = WH_InStoreMaterialDetail.POID "+
				            "LEFT JOIN WH_BI_WareHouse ON WH_InStoreMaterialDetail.WHID = WH_BI_WareHouse.WHID "; 
			if(sWhere!=null&&sWhere.Length>0)
			{
				sSql += " WHERE "+sWhere+" ";
			}
           
			sSql +=" GROUP BY WH_InStoreMaterialDetail.WHID,PurchaseOrder.POID ";
		
			DataTable dt = this.BaseDataAccess.GetDataTable(sSql);
			return dt;
		}



	}
}
