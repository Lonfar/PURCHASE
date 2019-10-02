using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEPurchaseOrder 的摘要说明。
	/// </summary>
	public class DAEReportPurchaseOrder : DAEBase
	{
		public DAEReportPurchaseOrder()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region GetRptData_PurchaseOrder

		public DataTable GetRptData_PurchaseOrder ( string sWhere )
		{
			//一行
			string sSql = string.Empty;

			// Modified by Liujun at 2007-8-30 取消与部门的关联
			
			#region 原代码
//			sSql = @"SELECT POID, SignDate, ContractTotalCostCUR, ContractTotalCost, 
//						ContractTotalCostStandard, dbo.f_GetReceiveSum(POID) AS ReceiveSum, 
//						dbo.f_GetIssueSum(POID) AS IssueSum, dbo.f_GetReturnSum(POID) AS ReturnSum, 
//						dbo.f_GetInStockSum(POID) AS InStockSum
//					FROM dbo.PurchaseOrder inner join BI_Employee
//						on PurchaseOrder.EmployeeID = BI_Employee.IDKey
//						inner join BI_Department
//						on PurchaseOrder.DepID = BI_Department.IDKey";
			#endregion

			sSql = @"SELECT POID, SignDate, ContractTotalCostCUR, ContractTotalCost, 
						ContractTotalCostStandard, dbo.f_GetReceiveSum(POID) AS ReceiveSum, 
						dbo.f_GetIssueSum(POID) AS IssueSum, dbo.f_GetReturnSum(POID) AS ReturnSum, 
						dbo.f_GetInStockSum(POID) AS InStockSum
					FROM dbo.PurchaseOrder inner join BI_Employee
						on PurchaseOrder.EmployeeID = BI_Employee.IDKey 
                        						inner join BI_Department
						on PurchaseOrder.DepID = BI_Department.IDKey ";
			

			if(sWhere!=null&&sWhere.Length>0)
			{
				sSql += " WHERE "+sWhere+"";
			}
			return BaseDataAccess.GetDataTable(sSql);
		}

		#endregion
	}
}
