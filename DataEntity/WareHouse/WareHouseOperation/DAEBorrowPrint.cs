using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEBorrowPrint 的摘要说明。
	/// </summary>
	public class DAEBorrowPrint : DAEBase
	{
		 CEntityUitlity  cen = new CEntityUitlity();

		public DAEBorrowPrint()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		public DataTable GetPrintData(string sWhere)
		{
			string sSql = string.Empty;			
			sSql = @"SELECT dbo.WH_Borrow.BorrowID, dbo.WH_Borrow.WHID, dbo.WH_Borrow.ShipWhere, 
      dbo.WH_Borrow.CreateBy, dbo.WH_Borrow.CreateDate, dbo.WH_Borrow.BorrowNO, 
      dbo.WH_Borrow.BorrowDate, dbo.WH_Borrow.EmployeeIDExecute, 
      dbo.WH_Borrow.ReturnDatePlan, dbo.WH_Borrow.BorrowReason, 
      dbo.WH_Borrow.TotalPriceNatural, dbo.WH_Borrow.TotalPriceStandard, 
      dbo.WH_BorrowMaterial.ItemCode, dbo.Material.MaterialName, 
      dbo.WH_BorrowMaterial.QuantityBorrow, dbo.MaterialUOM.UOMID, 
      dbo.WH_BorrowMaterial.POID, dbo.WH_BorrowMaterial.UnitPriceNatural, 
      dbo.WH_BorrowMaterial.UnitPriceStandard, dbo.WH_BorrowMaterial.BINID, 
      dbo.WH_BorrowMaterial.Comment, dbo.WH_BI_WareHouse.WHDescription, 
      CreateEmployee.FullName AS CreateEmployeeName
FROM dbo.WH_BorrowMaterial INNER JOIN
      dbo.Material ON 
      dbo.WH_BorrowMaterial.ItemCode = dbo.Material.ItemCode INNER JOIN
      dbo.WH_Borrow ON 
      dbo.WH_Borrow.BorrowID = dbo.WH_BorrowMaterial.BorrowID INNER JOIN
      dbo.MaterialUOM ON 
      dbo.MaterialUOM.MaterialUomID = dbo.WH_BorrowMaterial.MaterialUomID INNER JOIN
      dbo.WH_BI_WareHouse ON 
      dbo.WH_BI_WareHouse.WHID = dbo.WH_Borrow.WHID INNER JOIN
      dbo.BI_Employee ON 
      dbo.BI_Employee.IDKey = dbo.WH_Borrow.EmployeeIDExecute INNER JOIN
      dbo.BI_Employee CreateEmployee ON 
      CreateEmployee.IDKey = dbo.WH_Borrow.CreateBy"; 

			if(sWhere != null && sWhere.Length > 0)
			{
				sSql +=" WHERE WH_Borrow.BorrowID = '"+sWhere+"'";
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSql.ToString());
			return dt;

		}
		


	}
}
