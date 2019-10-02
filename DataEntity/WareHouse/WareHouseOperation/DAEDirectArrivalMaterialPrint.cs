using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEDirectArrivalMaterialPrint 的摘要说明。
	/// </summary>
	public class DAEDirectArrivalMaterialPrint : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT 
      dbo.WH_Receive.POID, dbo.WH_Receive.ReceiveNO, 
      dbo.WH_Receive.ReceiveDate, dbo.WH_Receive.TotalPrice, 
      dbo.WH_Receive.IsDirect, dbo.WH_Receive.TotalPriceNaturalER, 
      dbo.WH_Receive.TotalPriceNaturalCUR, dbo.WH_Receive.TotalPriceStandardlER, 
      dbo.WH_Receive.TotalPriceStandarCUR, 
      dbo.WH_Receive.CreateBy AS ReceiveCreateBy, 
      dbo.WH_Receive.CreateDate AS ReceiveCreateDate, 
      dbo.BI_Currency.IDKey AS CurrencyDescription, 
		dbo.Vendor.VendorName,
          (SELECT WHName
         FROM WH_BI_WareHouse
         WHERE WH_BI_WareHouse.WHID = WH_Receive.WHID) AS WHReceiveName,
          (SELECT FullName
         FROM BI_Employee
         WHERE BI_Employee.IDKey = WH_Receive.EmployeeID) AS ReceivedName,
          (SELECT FullName
         FROM BI_Employee
         WHERE BI_Employee.IDKey = WH_Receive.CreateBy) AS CreateName,
          (SELECT FullName
         FROM BI_Employee
         WHERE BI_Employee.IDKey = WH_Issue.EmployeeIDIssue) AS IssueFullName,
          (SELECT WHName
         FROM WH_BI_WareHouse
         WHERE WH_BI_WareHouse.WHID = WH_Issue.WHID) AS WHIDIssueame,
          (SELECT DepartmentName
         FROM BI_Department
         WHERE BI_Department.IDKey = WH_Issue.DepID) AS DepartmentName,
          (SELECT AFEDescription
         FROM WH_BI_AFE
         WHERE WH_BI_AFE.AFEID = WH_Issue.AFEID) AS AFEDescription, 
      dbo.WH_Issue.TotalPriceNatural AS IssueTotalPriceNatural, 
      dbo.WH_Issue.TotalPriceStandard AS IssueTotalPriceStandard, 
      dbo.WH_Issue.CreateBy AS IssueCreateBy, 
      dbo.WH_Issue.CreateDate AS IssueCreateDate, dbo.WH_Issue.IssueNo, 
      dbo.WH_Issue.SubProjectID, dbo.WH_Issue.IssueDate, 
      dbo.WH_ReceiveMaterial.ItemCode, dbo.WH_ReceiveMaterial.POQuantity, 
      dbo.WH_ReceiveMaterial.ReceivedQuantity, 
      dbo.WH_ReceiveMaterial.CanReceivedQuantity, 
      dbo.WH_ReceiveMaterial.FactReceivedQuantity, dbo.WH_ReceiveMaterial.UnitPrice, 
      dbo.WH_ReceiveMaterial.SumPrice, dbo.WH_ReceiveMaterial.PartNO, 
      dbo.MaterialUOM.UOMID, dbo.Material.MaterialName, 
      dbo.WH_BI_SubProject.SubDescription
FROM dbo.WH_Receive INNER JOIN
      dbo.WH_Issue ON 
      dbo.WH_Issue.IssueID = dbo.WH_Receive.ReceiveID LEFT OUTER JOIN
      dbo.WH_ReceiveMaterial ON 
      dbo.WH_Receive.ReceiveID = dbo.WH_ReceiveMaterial.ReceiveID LEFT OUTER JOIN
      dbo.MaterialUOM ON 
      dbo.MaterialUOM.MaterialUomID = dbo.WH_ReceiveMaterial.MaterialUomID LEFT OUTER
       JOIN
      dbo.Material ON 
      dbo.Material.ItemCode = dbo.WH_ReceiveMaterial.ItemCode LEFT OUTER JOIN
      dbo.Vendor ON dbo.Vendor.IDKey = dbo.WH_Receive.VendorID LEFT OUTER JOIN
      dbo.BI_Currency ON 
      dbo.BI_Currency.IDKey = dbo.WH_Receive.CurrencyID LEFT OUTER JOIN
      dbo.WH_BI_SubProject ON 
      dbo.WH_BI_SubProject.SubProjectID = dbo.WH_Issue.SubProjectID "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_Receive.IsDirect = '1' and  WH_Receive.ReceiveID='"+sWhere+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
