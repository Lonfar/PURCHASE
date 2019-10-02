using System;
using Common;
using Cnwit.Utility;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSABCCalssScale 的摘要说明。
	/// </summary>
	public class BUSABCCalssScale : BUSBase
	{
		DataAcess da = Common.GetProjectDataAcess.GetDataAcess();

		public BUSABCCalssScale()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 得到查询记录数目
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
