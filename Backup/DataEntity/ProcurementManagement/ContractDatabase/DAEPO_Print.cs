using System;
using System.Data;

using Cnwit.Utility;
using Common;

namespace DataEntity.ProcurementManagement.ContractDatabase
{
	/// <summary>
	/// PO打印界面数据实体类 Liujun add at 2007-6-21
	/// </summary>
	public class DAEPO_Print : DAEBase
	{
		public DAEPO_Print()
		{
			
		}

		#region 根据POID获得对应的PO物资信息

		/// <summary>
		/// 根据POID获得对应的PO物资信息
		/// </summary>
		/// <param name="sPOID">POID</param>
		/// <returns>PO物资子表</returns>
		public DataTable GetPOMaterial ( string sPOID )
		{
			DataTable dtPOMaterial = this.BaseDataAccess.GetDataTable ( "SELECT * FROM V_POMaterial WHERE POID = '"+sPOID+"'" );

			return dtPOMaterial;
		}

		#endregion

		#region 根据PO来获得PO详细信息

		/// <summary>
		/// 根据PO来获得PO详细信息
		/// </summary>
		/// <param name="sPOID">POID</param>
		/// <returns>PO详细信息</returns>
		public DataTable GetPODetails ( string sPOID )
		{
//            string sSelectSql = @"SELECT POID
//										,SignDate
//										,Vendor.VendorName
//										,Vendor.LoginAddress
//										,Vendor.Contact
//										,Vendor.Telphone
//										,Vendor.Fax 
//										,Vendor.Email
//										,BT_Payments.TypeDescription AS Payments
//										,WH_BI_WareHouse.WHName
//										,WH_BI_WareHouse.WHContactBy
//										,EstiamteArrivalDate
//										,WH_BT_Incoterms.TypeDescription AS INCO
//										FROM PurchaseOrder 
//										LEFT JOIN Vendor ON PurchaseOrder.VendorID = Vendor.IDKey
//										LEFT JOIN WH_BI_WareHouse ON WH_BI_WareHouse.WHID = PurchaseOrder.WHID
//										LEFT JOIN BT_Payments ON BT_Payments.IDKey = PurchaseOrder.PayMoneyID
//										LEFT JOIN WH_BT_Incoterms ON WH_BT_Incoterms.IDKey = PurchaseOrder.INCOTERMSID WHERE POID = '"+sPOID+@"'";

            string sSQL = @"SELECT distinct PurchaseOrder.POID,convert(char(10), PurchaseOrder.SignDate,120) as SignDate,PurchaseOrder.AFEID,
                            BT_Payments.TypeDescription AS Payments,WH_BT_Incoterms.TypeDescription AS INCO ,V_MergeMRNO.MRNO
                            ,PurchaseOrder.ShipTermID,WH_BT_TransportMethod.TypeDescription as ShippingMethod
                            ,PurchaseOrder.ContractTotalCostCUR as Currency,BI_Department.DepartmentName,V_GetDepartmentManager.EmployeeID as UserManager
                            ,PurchaseOrder.EmployeeID as Buyer,BI_Employee.Fax as POFax,BI_Employee.Tel as POTel,BI_Employee.Email as POEmail
                            ,Vendor.VendorName,Vendor.LoginAddress,Vendor.Contact
                            ,Vendor.Telphone as VendorTel,Vendor.Fax as VendorFax,Vendor.Email as VendorEmail
                            FROM PurchaseOrder
                            LEFT JOIN V_MergeMRNO ON V_MergeMRNO.POID = PurchaseOrder.POID
                            LEFT JOIN BT_Payments ON BT_Payments.IDKey = PurchaseOrder.PayMoneyID
                            LEFT JOIN WH_BT_Incoterms ON WH_BT_Incoterms.IDKey = PurchaseOrder.INCOTERMSID
                            LEFT JOIN WH_BT_TransportMethod ON WH_BT_TransportMethod.IDKey = PurchaseOrder.ShipMethodID
                            LEFT JOIN BI_Department ON BI_Department.IDKey = PurchaseOrder.DepID
                            LEFT JOIN BI_DepartmentEmployee ON BI_DepartmentEmployee.DepartmentID = BI_Department.IDKey
                            LEFT JOIN BI_Employee ON BI_Employee.IDKey = PurchaseOrder.EmployeeID
                            LEFT JOIN V_GetDepartmentManager ON V_GetDepartmentManager.DepartmentID = BI_Department.IDKey 
                            LEFT JOIN Vendor ON PurchaseOrder.VendorID = Vendor.IDKey WHERE PurchaseOrder.POID = '" + sPOID + "' ";

            DataTable dtPODetails = this.BaseDataAccess.GetDataTable(sSQL);

			return dtPODetails;
		}

		#endregion
	}
}
