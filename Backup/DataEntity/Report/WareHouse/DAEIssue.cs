using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEIssue 的摘要说明。
	/// </summary>
	public class DAEIssuef : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT WH_IssueMaterial.BINID, WH_IssueMaterial.POID, 
					WH_IssueMaterial.FactIssuedQuantity, WH_Issue.EmployeeIDIssue, 
					WH_IssueMaterial.UnitPriceNatural, WH_IssueMaterial.UnitPriceStandard, 
					WH_IssueMaterial.ItemCode, Material.MaterialName, 
					WH_Issue.IssueNo, WH_Issue.IssueDate, 
					BI_Department.DepartmentName, Material.UOMID, 
					WH_IssueMaterial.FactIssuedQuantity * WH_IssueMaterial.UnitPriceNatural AS
					TotalNatural, 
					WH_IssueMaterial.FactIssuedQuantity * WH_IssueMaterial.UnitPriceStandard
					AS TotalStandard, WH_BI_WareHouse.WHID
					FROM WH_IssueMaterial INNER JOIN
					Material ON 
					WH_IssueMaterial.ItemCode = Material.ItemCode INNER JOIN
					WH_Issue ON 
					WH_Issue.IssueID = WH_IssueMaterial.IssueID INNER JOIN
					BI_Department ON 
					BI_Department.IDKey = WH_Issue.DepID INNER JOIN
					MaterialUOM ON 
					MaterialUOM.ItemCode = Material.ItemCode INNER JOIN
					WH_BI_WareHouse ON 
					WH_BI_WareHouse.WHID = WH_Issue.WHID INNER JOIN
					BI_Employee ON BI_Employee.IDKey = WH_Issue.EmployeeIDIssue"; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere+" AND WH_Issue.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			else
			{
				sSqlRep.Append(" WHERE WH_Issue.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
