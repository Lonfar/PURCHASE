using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEPOWarning ��ժҪ˵����
	/// </summary>
	public class DAEPOWarning : DAEBase
	{
		public DAEPOWarning()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
