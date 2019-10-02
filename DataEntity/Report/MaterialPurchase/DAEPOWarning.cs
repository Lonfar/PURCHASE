using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEPOWarning 的摘要说明。
	/// </summary>
	public class DAEPOWarning : DAEBase
	{
		public DAEPOWarning()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			sSqlRep.Append("SELECT * FROM v_Report_POWarning ");	
			if ( sWhere.Length > 0 )
			{
				sSqlRep.Append(" WHERE " + sWhere);
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
