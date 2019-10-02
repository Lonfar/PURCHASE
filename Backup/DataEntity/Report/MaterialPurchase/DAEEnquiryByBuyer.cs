using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEEnquiryByBuyer 的摘要说明。
	/// </summary>
	public class DAEEnquiryByBuyer : DAEBase
	{
		public DAEEnquiryByBuyer()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			sSqlRep.Append(@"SELECT 
							MR_EnquiryPrice.EnquiryPriceNo
							,MR_EnquiryPrice.EnquiryPriceDate
							,MR_EnquiryMaterial.ItemCode
							,MR_EnquiryMaterial.MaterialName
							,MR_EnquiryMaterial.MFG
							,MR_EnquiryMaterial.PartNo
							,MR_EnquiryMaterial.MRNO
							,MR_EnquiryMaterial.UOMID
							,MR_EnquiryMaterial.MRQuantity
							,BI_Employee.FullName
							FROM MR_EnquiryPrice
							INNER JOIN MR_EnquiryMaterial ON MR_EnquiryMaterial.EnquiryPriceID = MR_EnquiryPrice.EnquiryPriceID
							INNER JOIN BI_Employee ON BI_Employee.IDKey = MR_EnquiryPrice.EmployeeID
							");		
			if ( sWhere.Length > 0 )
			{
				sSqlRep.Append(" WHERE " + sWhere);
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
