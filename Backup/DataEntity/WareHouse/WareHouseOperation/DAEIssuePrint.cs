using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// IssuePrint 的摘要说明。
	/// </summary>
	public class DAEIssuePrint : DAEBase
	{
	  CEntityUitlity  cen = new CEntityUitlity();

		public DAEIssuePrint()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetPrintData(string sWhere)
		{
			string sSql = string.Empty;			
			sSql = @"SELECT dbo.WH_Issue.*, dbo.Material.MaterialName AS MaterialName, 
      dbo.WH_IssueMaterial.POID AS POID, dbo.WH_IssueMaterial.BINID AS BINID, 
      dbo.WH_IssueMaterial.SumPrice AS SumPrice, 
      dbo.WH_IssueMaterial.FactIssuedQuantity AS FactIssuedQuantity, 
      dbo.WH_IssueMaterial.PreserveQuantity AS PreserveQuantity, 
      dbo.WH_IssueMaterial.CanIssuedQuantity AS CanIssuedQuantity, 
      dbo.WH_IssueMaterial.QuantityInBin AS QuantityInBin, 
      dbo.BI_Department.DepartmentName AS DepartmentName, 
      dbo.Material.UOMID AS UOMID, dbo.WH_IssueMaterial.ItemCode AS ItemCode, 
      dbo.WH_IssueMaterial.UnitPriceStandard AS UnitPriceNatural, 
      dbo.WH_IssueMaterial.FactIssuedQuantity * dbo.WH_IssueMaterial.UnitPriceStandard AS TotalPrice2, 
      dbo.WH_IssueMaterial.FactIssuedQuantity * dbo.WH_IssueMaterial.UnitPriceStandard
       AS TotalStandard, dbo.WH_BI_WareHouse.WHID AS HouseWHID, 
      dbo.WH_Issue.IssueNo AS Expr1, 
      IssueEmployee.FullName AS IssueEmployeeName, 
      ReceiveEmployee.FullName AS ReceiveEmployeeName, 
      dbo.WH_BI_WareHouse.WHDescription AS WareHouseDescription, 
      CreateEmployee.FullName AS CreateEmployeeName
FROM dbo.WH_IssueMaterial INNER JOIN
      dbo.Material ON 
      dbo.WH_IssueMaterial.ItemCode = dbo.Material.ItemCode INNER JOIN
      dbo.WH_Issue ON 
      dbo.WH_Issue.IssueID = dbo.WH_IssueMaterial.IssueID INNER JOIN
      dbo.BI_Department ON 
      dbo.BI_Department.IDKey = dbo.WH_Issue.DepID INNER JOIN
      dbo.MaterialUOM ON 
      dbo.MaterialUOM.MaterialUomID = dbo.WH_IssueMaterial.MaterialUomID 
           INNER JOIN
      dbo.WH_BI_WareHouse ON 
      dbo.WH_BI_WareHouse.WHID = dbo.WH_Issue.WHID INNER JOIN
      dbo.BI_Employee IssueEmployee ON 
      IssueEmployee.IDKey = dbo.WH_Issue.EmployeeIDIssue INNER JOIN
      dbo.BI_Employee ReceiveEmployee ON 
      ReceiveEmployee.IDKey = dbo.WH_Issue.EmployeeIDReceive INNER JOIN
      dbo.BI_Employee CreateEmployee ON 
      CreateEmployee.IDKey = dbo.WH_Issue.CreateBy LEFT OUTER JOIN
      dbo.POMaterial ON dbo.WH_IssueMaterial.POID = dbo.POMaterial.POID AND 
      dbo.WH_IssueMaterial.ItemCode = dbo.POMaterial.ItemCode"; 

			if(sWhere != null && sWhere.Length > 0)
			{
				sSql +=" WHERE WH_Issue.IssueID = '"+sWhere+"'";
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSql.ToString());
		return dt;

		}
		

	}
}
