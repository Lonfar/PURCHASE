using System;
using System.Data;
using System.Data.SqlClient;
using Cnwit.Utility;

namespace DataEntity
{
	/// <summary>
	/// DAEVendorSubmit ��ժҪ˵����
	/// </summary>
	public class DAEVendorSubmit:DAEBase
	{
		DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		public DAEVendorSubmit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public string GetVendorObjectiveTitle(string sIDkey)
		{
			string sql = "SELECT VendorName from Vendor where IDKey = '"+sIDkey+"'";
			DataTable dt = _da.GetDataTable(sql);
			if(dt.Rows.Count>0&&dt.Rows[0][0]!=System.DBNull.Value)
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}


		}

		public string CancelSubmit(string sIDkey)
		{
			string errMsg = string.Empty;
			string sql = "DELETE FROM PutIn WHERE ObjectiveID = '"+sIDkey+"'";
			errMsg = _da.ExecuteDMLSQL(sql);
			if(errMsg==null||errMsg.Length==0)
			{
				sql = "UPDATE Vendor SET Status = 1 WHERE IDkey='"+sIDkey+"'";
				errMsg = _da.ExecuteDMLSQL(sql);
				return errMsg;
			}
			else
			{
				return errMsg;
			}
		}

	}
}
