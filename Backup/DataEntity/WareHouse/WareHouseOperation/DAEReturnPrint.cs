using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEReturnPrint 的摘要说明。
	/// </summary>
	public class DAEReturnPrint: DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT dbo.WH_Return.*,
          (SELECT WHName
         FROM WH_BI_WareHouse
         WHERE WH_BI_WareHouse.WHID = WH_Return.WHIDReceive) 
      AS WHReceiveName,
          (SELECT WHName
         FROM WH_BI_WareHouse
         WHERE WH_BI_WareHouse.WHID = WH_Return.WHIDIssue) AS WHIssueName,
          (SELECT FullName
         FROM BI_Employee
         WHERE BI_Employee.IDKey = WH_Return.EmployeeReceive) AS ReceivedName,
          (SELECT FullName
         FROM BI_Employee
         WHERE BI_Employee.IDKey = WH_Return.EmployeeReturn) AS ReturnName,
          (SELECT FullName
         FROM BI_Employee
         WHERE BI_Employee.IDKey = WH_Return.CreateBy) AS CreateName,
          (SELECT DepartmentName
         FROM BI_Department
         WHERE BI_Department.IDKey = WH_Return.DepID) AS DepartmentName, 
      dbo.WH_ReturnMaterial.BINID AS BINID, 
      dbo.WH_ReturnMaterial.ItemCode AS ItemCode, 
      dbo.WH_ReturnMaterial.IssueQuantity AS IssueQuantity, 
      dbo.WH_ReturnMaterial.CanReturnQuantity AS CanReturnQuantity, 
      dbo.WH_ReturnMaterial.FactReturnQuantity AS FactReturnQuantity, 
      dbo.WH_ReturnMaterial.UnitPriceNatural AS UnitPriceNatural, 
      dbo.WH_ReturnMaterial.UnitPriceStandard AS UnitPriceStandard, 
      dbo.WH_ReturnMaterial.SumPrice AS SumPrice, 
      dbo.WH_ReturnMaterial.depreciationRate AS depreciationRate, 
      dbo.WH_ReturnMaterial.POID AS POID, dbo.MaterialUOM.UOMID AS UOMID, 
      dbo.WH_Issue.IssueNo AS IssueNo, dbo.Material.MaterialName AS MaterialName, 
      dbo.WH_BI_AFE.AFEDescription AS AFEDNo
FROM dbo.WH_Return LEFT OUTER JOIN
      dbo.WH_ReturnMaterial ON 
      dbo.WH_Return.ReturnID = dbo.WH_ReturnMaterial.ReturnID LEFT OUTER JOIN
      dbo.MaterialUOM ON 
      dbo.MaterialUOM.MaterialUomID = dbo.WH_ReturnMaterial.MaterialUomID LEFT OUTER
       JOIN
      dbo.Material ON 
      dbo.Material.ItemCode = dbo.WH_ReturnMaterial.ItemCode LEFT OUTER JOIN
      dbo.BI_Department ON 
      dbo.BI_Department.IDKey = dbo.WH_Return.DepID LEFT OUTER JOIN
      dbo.WH_Issue ON 
      dbo.WH_Issue.IssueID = dbo.WH_Return.IssueID LEFT OUTER JOIN
      dbo.WH_BI_AFE ON dbo.WH_Return.AFEID = dbo.WH_BI_AFE.AFEID"; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_Return.ReturnID='"+sWhere+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
