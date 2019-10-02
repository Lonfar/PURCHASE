using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSContractSign 的摘要说明。
	/// </summary>
	public class BUSContractSign:BUSBase
	{
		public BUSContractSign()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		/// <summary>
		/// 是否在SeePerson表中存在
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtSeePerson )
		{
			foreach(DataRow dr in dtSeePerson.Rows)
			{
				if ( IDKey == dr["ContractView.ViewerID"].ToString()) return true;
			}
			return false;
		}
	}
}
