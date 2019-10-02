using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEAdjustInPrint 的摘要说明。
	/// </summary>
	public class DAEAdjustInPrint: DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT WH_AdjustIN.AdjustInNO, WH_AdjustIN.AdjustDate,
					(SELECT WHName
					FROM WH_BI_WareHouse
					WHERE WH_BI_WareHouse.WHID = WH_AdjustIn.WHID) AS WHID,
					(SELECT FullName
					FROM BI_Employee
					WHERE BI_Employee.IDKey = WH_AdjustIn.EmployeeID) AS TransFullName, 
					WH_AdjustIN.TotalPriceStandard, WH_AdjustIN.TotalPriceNatural, 
					WH_AdjustIN.CreateBy, WH_AdjustIN.CreateDate, 
					WH_AdjustInMaterial.ItemCode, Material.MaterialName, 
					WH_AdjustInMaterial.PartNO, WH_AdjustInMaterial.AdjustInQuantity, 
					WH_AdjustInMaterial.UnitPriceStandard, 
					WH_AdjustInMaterial.UnitPriceNatural, MaterialUOM.UOMID, 
					WH_AdjustInMaterial.POID,
					(SELECT BINID
					FROM WH_BI_BIN
					WHERE WH_BI_BIN.BINID = WH_AdjustInMaterial.BINID) AS BINID
					FROM WH_AdjustIN LEFT OUTER JOIN
					WH_AdjustInMaterial ON 
					WH_AdjustIN.AdjustInID = WH_AdjustInMaterial.AdjustInID LEFT OUTER JOIN
					Material ON 
					Material.ItemCode = WH_AdjustInMaterial.ItemCode LEFT OUTER JOIN
					MaterialUOM ON 
					MaterialUOM.MaterialUomID = WH_AdjustInMaterial.MaterialUomID "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_AdjustIN.AdjustINID='"+sWhere+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
