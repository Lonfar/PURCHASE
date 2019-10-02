using System;
using System.Data;
using Cnwit.Utility;
using Common;
namespace DataEntity
{
	/// <summary>
	/// DAEOperationhistory 的摘要说明。
	/// Added by QSQ 10.27
	/// </summary>
	public class DAEOperationhistory:DAEBase
	{
		DataAcess _da = GetProjectDataAcess.GetDataAcess();

		public DAEOperationhistory()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public string GetVendorIDKey(string IDKey)
		{
			DataTable  dt = _da.GetDataTable(" Select IDKey from Vendor where IDKey = '"+IDKey+"'");

			return dt.Rows[0][0].ToString();
		}
	}
}
