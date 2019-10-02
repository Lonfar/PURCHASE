using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// daePurchaseOrderByVendor 的摘要说明。
	/// </summary>
	public class DAEPurchaseOrderByVendor : DAEBase
	{
		public DAEPurchaseOrderByVendor()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT PurchaseOrder.POID, PurchaseOrder.EmployeeID, 
					PurchaseOrder.DepID, BI_Employee.FullName, PurchaseOrder.SignDate, 
					PurchaseOrder.ContractTotalCostCUR, 
					PurchaseOrder.ContractTotalCostNatural, 
					PurchaseOrder.ContractTotalCostStandard, PurchaseOrder.POPurpose, 
					PurchaseOrder.EstiamteArrivalDate, PurchaseOrder.EffectiveDate, 
					PurchaseOrder.ExecuteDate, POMaterial.MRNO, POMaterial.ItemCode, 
					POMaterial.MaterialDescription, POMaterial.PartNo, 
					MaterialUOM.UOMID, POMaterial.CanPOQuantity, 
					POMaterial.POQuantity, POMaterial.UnitPrice, POMaterial.TotalCost, 
					POMaterial.MRMaterialID, Vendor.IDKey, 
					Vendor.VendorNo, Vendor.VendorName, 
					BI_Department.DepartmentName,BT_MRStatus.TypeDescription
					FROM PurchaseOrder 
					LEFT OUTER JOIN POMaterial ON PurchaseOrder.POID = POMaterial.POID 
					LEFT OUTER JOIN Vendor ON Vendor.IDKey = PurchaseOrder.VendorID 
					LEFT OUTER JOIN BI_Employee ON PurchaseOrder.EmployeeID = BI_Employee.IDKey 
					LEFT OUTER JOIN MaterialUOM ON MaterialUOM.MaterialUomID = POMaterial.MaterialUomID 
					LEFT OUTER JOIN BI_Department ON BI_Department.IDKey = PurchaseOrder.DepID
					LEFT OUTER JOIN BT_MRStatus ON BT_MRStatus.IDKey = PurchaseOrder.ApproveStatus WHERE "+sWhere;
			sSqlRep.Append(sSql);			
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
