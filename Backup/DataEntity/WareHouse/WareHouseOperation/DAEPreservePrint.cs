using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEPreservePrint 的摘要说明。
	/// </summary>
	public class DAEPreservePrint: DAEBase
	{
		CEntityUitlity  cen = new CEntityUitlity();

		public DAEPreservePrint()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
  
		public DataTable GetPrintData(string sWhere)
		{
			string sSql = string.Empty;			
			sSql = @"SELECT dbo.WH_Preserve.PreserveID, dbo.BI_Department.DepartmentName, 
      dbo.WH_Preserve.PreserveNO, dbo.WH_Preserve.EmployeeID, 
      dbo.WH_Preserve.AFEID, dbo.WH_Preserve.PreserveDate, 
      dbo.WH_Preserve.EfectiveDate, dbo.WH_PreserveMaterial.POID, 
      dbo.WH_Preserve.WHID, dbo.WH_Preserve.SubProjectID, 
      dbo.WH_Preserve.CreateBy, dbo.WH_Preserve.PreserveNO AS Expr1, 
      dbo.WH_Preserve.CreateDate, dbo.WH_Preserve.Comment, 
      dbo.WH_PreserveMaterial.ItemCode, dbo.WH_PreserveMaterial.MaterialName, 
      dbo.WH_PreserveMaterial.QuantityInBin, 
      dbo.WH_PreserveMaterial.QuantityPreserve, 
      dbo.WH_PreserveMaterial.QuantityIssuedFact, dbo.MaterialUOM.UOMID, 
      PreserveEmployee.FullName AS PreserveEmployeeName, 
      CreateEmployee.FullName AS CreateEmployeeName, 
      dbo.WH_BI_WareHouse.WHDescription, dbo.WH_BI_AFE.AFEDescription
FROM dbo.WH_PreserveMaterial LEFT OUTER JOIN
      dbo.WH_Preserve ON 
      dbo.WH_PreserveMaterial.PreserveID = dbo.WH_Preserve.PreserveID LEFT OUTER JOIN
      dbo.Material ON 
      dbo.WH_PreserveMaterial.ItemCode = dbo.Material.ItemCode INNER JOIN
      dbo.MaterialUOM ON 
      dbo.MaterialUOM.MaterialUomID = dbo.WH_PreserveMaterial.MaterialUomID INNER JOIN
      dbo.BI_Department ON 
      dbo.BI_Department.IDKey = dbo.WH_Preserve.DepID INNER JOIN
      dbo.BI_Employee PreserveEmployee ON 
      PreserveEmployee.IDKey = dbo.WH_Preserve.EmployeeID INNER JOIN
      dbo.BI_Employee CreateEmployee ON 
      CreateEmployee.IDKey = dbo.WH_Preserve.CreateBy INNER JOIN
      dbo.WH_BI_WareHouse ON 
      dbo.WH_BI_WareHouse.WHID = dbo.WH_Preserve.WHID LEFT OUTER JOIN
      dbo.WH_BI_AFE ON dbo.WH_Preserve.AFEID = dbo.WH_BI_AFE.AFEID"; 

			if(sWhere != null && sWhere.Length > 0)
			{
				sSql +=" WHERE WH_PreserveMaterial.PreserveID = '"+sWhere+"'";
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSql.ToString());
			return dt;

		}
		


	}
}
