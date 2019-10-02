using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEAdjustOutPrint 的摘要说明。
	/// </summary>
	public class DAEAdjustOutPrint : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT 
					WH_AdjustOut.AdjustOutID, WH_AdjustOut.AdjustOutNO, WH_AdjustOut.AdjustDate,
					(SELECT FullName FROM BI_Employee WHERE BI_Employee.IDKey = WH_AdjustOut.CreateBy) AS CreateName,
					(SELECT FullName FROM BI_Employee WHERE BI_Employee.IDKey = WH_AdjustOut.BI_Emp_IDKey) AS TransName, 
					WH_BI_WareHouse.WHName, BI_Department.DepartmentName, 
					WH_BI_AFE.AFEDescription, WH_BI_SubProject.SubDescription, WH_AdjustOut.TotalPriceNatural, 
					WH_AdjustOut.TotalPriceStandard, WH_AdjustOut.CreateDate, 
					WH_AdjustOutMaterial.ItemCode, Material.MaterialName, 
					WH_AdjustOutMaterial.BINID, WH_AdjustOutMaterial.PartNO, 
					WH_AdjustOutMaterial.QuantityReject, WH_AdjustOutMaterial.QuantityInBin, 
					WH_AdjustOutMaterial.UnitPriceStandard, 
					WH_AdjustOutMaterial.UnitPriceNatural, MaterialUOM.UOMID, 
					WH_AdjustOutMaterial.POID 
				FROM WH_AdjustOut  
				LEFT OUTER JOIN WH_AdjustOutMaterial ON WH_AdjustOut.AdjustOutID = WH_AdjustOutMaterial.AdjustOutID  
				LEFT OUTER JOIN WH_BI_WareHouse ON WH_BI_WareHouse.WHID = WH_AdjustOut.WHID  
				LEFT OUTER JOIN BI_Department ON BI_Department.IDKey = WH_AdjustOut.IDKey  
				LEFT OUTER JOIN WH_BI_AFE ON WH_BI_AFE.AFEID = WH_AdjustOut.AFEID  
				LEFT OUTER JOIN Material ON Material.ItemCode = WH_AdjustOutMaterial.ItemCode  
				LEFT OUTER JOIN MaterialUOM ON MaterialUOM.MaterialUomID = WH_AdjustOutMaterial.MaterialUomID  
				left outer join WH_BI_SubProject on WH_BI_SubProject.SubProjectID =  WH_AdjustOut.SubProjectID    "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_AdjustOut.AdjustOutID='"+sWhere+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
