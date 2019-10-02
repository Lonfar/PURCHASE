using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEPurchaseOrder ��ժҪ˵����
	/// </summary>
	public class DAEReportPurchaseOrder : DAEBase
	{
		public DAEReportPurchaseOrder()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region GetRptData_PurchaseOrder

		public DataTable GetRptData_PurchaseOrder ( string sWhere )
		{
			//һ��
			string sSql = string.Empty;

			// Modified by Liujun at 2007-8-30 ȡ���벿�ŵĹ���
			
			#region ԭ����
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
