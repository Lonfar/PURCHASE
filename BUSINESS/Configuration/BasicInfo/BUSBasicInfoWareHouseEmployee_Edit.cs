using System;
using Common;
using Cnwit.Utility;
using System.Data;
namespace Business
{
	/// <summary>
	/// BUSBITables
	/// Modified by QSQ 10.30
	/// </summary>
	public class BUSBasicInfoWareHouseEmployee_Edit:BUSBase
	{
		DataAcess da = Common.GetProjectDataAcess.GetDataAcess();
		public BUSBasicInfoWareHouseEmployee_Edit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �õ���ѯ��¼��Ŀ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public int getNum(string sql)
		{
			DataTable dte = da.GetDataTable(sql);
			return dte.Rows.Count;
		}
	}
}
