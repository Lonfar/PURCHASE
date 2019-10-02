using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAERejectPrint 的摘要说明。
	/// </summary>
	public class DAERejectPrint: DAEBase
	{

		CEntityUitlity  cen = new CEntityUitlity();

		public DAERejectPrint()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetPrintData(string sWhere)
		{
			string sSql = string.Empty;			
			sSql = @"SELECT dbo.WH_Reject.RejectID, dbo.WH_Reject.WHID, dbo.WH_Reject.RejectDate, 
      dbo.WH_Reject.CreateBy, dbo.WH_Reject.CreateDate, dbo.WH_Reject.RejectNO, 
      dbo.BI_Employee.FullName, dbo.WH_Reject.RejectReason, dbo.WH_Reject.TotalPriceNatural, dbo.WH_Reject.TotalPriceStandard, 
      dbo.WH_RejectMaterial.ItemCode, dbo.Material.MaterialName, 
      dbo.WH_Reject.RejectNO AS Expr1, dbo.WH_RejectMaterial.QuantityReject, 
      dbo.MaterialUOM.UOMID, dbo.WH_RejectMaterial.POID, 
      dbo.WH_RejectMaterial.UnitPriceNatural, dbo.WH_RejectMaterial.UnitPriceStandard, 
      dbo.WH_RejectMaterial.BINID, dbo.WH_RejectMaterial.Comment, 
      CreateEmployee.FullName AS CreateEmployeeName, 
      dbo.WH_BI_WareHouse.WHDescription
FROM dbo.WH_RejectMaterial INNER JOIN
      dbo.Material ON 
      dbo.WH_RejectMaterial.ItemCode = dbo.Material.ItemCode INNER JOIN
      dbo.WH_Reject ON 
      dbo.WH_Reject.RejectID = dbo.WH_RejectMaterial.RejectID INNER JOIN
      dbo.MaterialUOM ON 
      dbo.MaterialUOM.MaterialUomID = dbo.WH_RejectMaterial.MaterialUomID INNER JOIN
      dbo.WH_BI_WareHouse ON 
      dbo.WH_BI_WareHouse.WHID = dbo.WH_Reject.WHID INNER JOIN
      dbo.BI_Employee ON 
      dbo.BI_Employee.IDKey = dbo.WH_Reject.EmployeeID INNER JOIN
      dbo.BI_Employee CreateEmployee ON 
      CreateEmployee.IDKey = dbo.WH_Reject.CreateBy"; 

			if(sWhere != null && sWhere.Length > 0)
			{
				sSql +=" WHERE WH_Reject.RejectID = '"+sWhere+"'";
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSql.ToString());
			return dt;

		}
		



	}
}
