using System;
using Common;
using Cnwit.Utility;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSABCCalssScale ��ժҪ˵����
	/// </summary>
	public class BUSABCCalssScale : BUSBase
	{
		DataAcess da = Common.GetProjectDataAcess.GetDataAcess();

		public BUSABCCalssScale()
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
		public int GetNum()
		{
			string sSql = "SELECT * FROM WH_BI_ABCCalssScale ";
			DataTable dte = da.GetDataTable(sSql);
			return dte.Rows.Count;
		}
	}
}
