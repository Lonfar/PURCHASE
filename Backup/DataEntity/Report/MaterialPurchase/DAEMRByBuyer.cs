using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEMRByBuyer 的摘要说明。
	/// </summary>
	public class DAEMRByBuyer : DAEBase
	{
		public DAEMRByBuyer()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();

			sSqlRep.Append (@"Select MR_MaterialRequisition.MRNO
							,BI_Employee.FullName AS Buyer
							,MR_Material.ItemCode
							,MR_Material.MaterialName
							,MaterialUOM.UOMID
							,MR_Material.MRQuantity
							,MR_MaterialRequisition.CurrencyID
							,MR_Material.EstUnitPrice
							,(MR_Material.MRQuantity * MR_Material.EstUnitPrice) AS EstTotalPrice
							,BI_Department.DepartmentName
							FROM 
							MR_MaterialRequisition
							INNER JOIN BI_Employee  ON BI_Employee.IDKey = MR_MaterialRequisition.ReceiveBy
							INNER JOIN BI_Department ON BI_Department.IDKey = MR_MaterialRequisition.DeptID
							LEFT JOIN WH_BI_AFE ON WH_BI_AFE.AFEID = MR_MaterialRequisition.AFEID
							INNER JOIN MR_Material ON MR_MaterialRequisition.MRID = MR_Material.MRID
							Left JOIN MaterialUOM ON MR_Material.MaterialUomID = MaterialUOM.MaterialUomID ");	

			if ( sWhere.Length > 0 )
			{
				sSqlRep.Append(" WHERE " + sWhere);
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
