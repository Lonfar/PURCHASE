using System;
using System.Data;
using Cnwit.Utility;
using Common;
namespace DataEntity
{
	/// <summary>
	/// DAEOperationhistory ��ժҪ˵����
	/// Added by QSQ 10.27
	/// </summary>
	public class DAEOperationhistory:DAEBase
	{
		DataAcess _da = GetProjectDataAcess.GetDataAcess();

		public DAEOperationhistory()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public string GetVendorIDKey(string IDKey)
		{
			DataTable  dt = _da.GetDataTable(" Select IDKey from Vendor where IDKey = '"+IDKey+"'");

			return dt.Rows[0][0].ToString();
		}
	}
}
