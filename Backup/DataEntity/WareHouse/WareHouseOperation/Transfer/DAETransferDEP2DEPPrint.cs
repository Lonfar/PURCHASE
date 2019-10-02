using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAETransferDEP2DEPPrint 的摘要说明。
	/// </summary>
	public class DAETransferDEP2DEPPrint : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT 
					WH_TransferDEP2DEP.TransferDEP2DEPNO, 
					WH_TransferDEP2DEP.TransferDEP2DEPDate,
					(SELECT DepartmentName FROM BI_Department
					WHERE BI_Department.IDKey = WH_TransferDEP2DEP.DepIDOld) AS OldDepName,
					(SELECT DepartmentName FROM BI_Department
					WHERE BI_Department.IDKey = WH_TransferDEP2DEP.DepIDNew) AS NewDepName,
					(SELECT FullName FROM BI_Employee
					WHERE BI_Employee.IDKey = WH_TransferDEP2DEP.EmployeeIDTrans) AS TransFullName,
					(SELECT FullName FROM BI_Employee
					WHERE BI_Employee.IDKey = WH_TransferDEP2DEP.EmployeeIDReceive) AS ReceiveFullName,
					(SELECT FullName FROM BI_Employee
					WHERE BI_Employee.IDKey = WH_TransferDEP2DEP.TransferPerson) AS TransferPersonName,
					(SELECT FullName FROM BI_Employee
					WHERE BI_Employee.IDKey = WH_TransferDEP2DEP.CreateBy) AS CreateName,
					(SELECT AFEDescription FROM WH_BI_AFE
					WHERE WH_BI_AFE.AFEID = WH_TransferDEP2DEP.AFEIDOld) AS OldAFEDescription,
					(SELECT AFEDescription FROM WH_BI_AFE
					WHERE WH_BI_AFE.AFEID = WH_TransferDEP2DEP.AFEIDNew) AS NewAFEDescription,
						WH_TransferDEP2DEP.TotalPriceStandard, 
					WH_TransferDEP2DEP.TotalPriceNatural, WH_TransferDEP2DEP.CreateBy, 
					WH_TransferDEP2DEP.CreateDate, 
					WH_TransferDEP2DEPMaterial.ItemCode, Material.MaterialName, 
					WH_TransferDEP2DEPMaterial.POID, 
					WH_TransferDEP2DEPMaterial.IssueQuantity, 
					WH_TransferDEP2DEPMaterial.UnitPriceStandard, 
					WH_TransferDEP2DEPMaterial.UnitPriceNatural, MaterialUOM.UOMID, 
					WH_TransferDEP2DEPMaterial.depreciationRate, 
					WH_TransferDEP2DEPMaterial.FactIssuedQuantity, 
					WH_Issue.IssueNo 
				FROM WH_TransferDEP2DEP  
				LEFT OUTER JOIN WH_Issue ON WH_TransferDEP2DEP.IssueID = WH_Issue.IssueID 
				LEFT OUTER JOIN WH_TransferDEP2DEPMaterial ON WH_TransferDEP2DEP.TransferDEP2DEPID = WH_TransferDEP2DEPMaterial.TransferDEP2DEPID 
				LEFT OUTER JOIN Material ON Material.ItemCode = WH_TransferDEP2DEPMaterial.ItemCode  
				LEFT OUTER JOIN MaterialUOM ON MaterialUOM.MaterialUomID = WH_TransferDEP2DEPMaterial.MaterialUomID  "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_TransferDEP2DEP.TransferDEP2DEPID='"+sWhere+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
