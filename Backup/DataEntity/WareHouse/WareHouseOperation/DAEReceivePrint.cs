using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEReceivePrint 的摘要说明。
	/// </summary>
	public class DAEReceivePrint : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT 
					WH_Receive.*, 
					(SELECT WHName
							FROM WH_BI_WareHouse
							WHERE WH_BI_WareHouse.WHID = WH_Receive.WHID) AS WHName,
					(SELECT FullName
							FROM BI_Employee
							WHERE BI_Employee.IDKey = WH_Receive.EmployeeID) AS ReceivedName,
					(SELECT VendorNo
							FROM Vendor
							WHERE Vendor.IDKey = WH_Receive.VendorID) AS VendorName,
					(SELECT FullName
							FROM BI_Employee
							WHERE BI_Employee.IDKey = WH_Receive.CreateBy) AS CreateName,
					WH_ReceiveMaterial.BINID AS BINID, 
					WH_ReceiveMaterial.ItemCode AS ItemCode, 
					WH_ReceiveMaterial.POQuantity AS POQuantity, 
					WH_ReceiveMaterial.ReceivedQuantity AS ReceivedQuantity, 
					WH_ReceiveMaterial.CanReceivedQuantity AS CanReceivedQuantity, 
					WH_ReceiveMaterial.FactReceivedQuantity AS FactReceivedQuantity, 
					WH_ReceiveMaterial.UnitPrice AS UnitPrice, 
					WH_ReceiveMaterial.SumPrice AS SumPrice, 
					WH_ReceiveMaterial.PartNO AS PartNO, MaterialUOM.UOMID AS UOMID, 
					Material.MaterialName AS MaterialName, 
					Vendor.VendorNo AS VendorNo 
					FROM WH_Receive left JOIN 
					WH_ReceiveMaterial ON 
					WH_Receive.ReceiveID = WH_ReceiveMaterial.ReceiveID left JOIN 
					MaterialUOM ON  
					MaterialUOM.MaterialUomID = WH_ReceiveMaterial.MaterialUomID left JOIN 
					Material ON  
					Material.ItemCode = WH_ReceiveMaterial.ItemCode left JOIN 
					Vendor ON Vendor.IDKey = WH_Receive.VendorID "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_Receive.ReceiveID='"+sWhere+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
