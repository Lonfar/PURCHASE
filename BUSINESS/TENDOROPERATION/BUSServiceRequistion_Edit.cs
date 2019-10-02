using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSServiceRequistion_Edit 的摘要说明。
	/// </summary>
	public class BUSServiceRequistion_Edit : BUSBase
	{
		public BUSServiceRequistion_Edit() 
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
				if ( IDKey == dr["ServiceRequestViewer.ViewerID"].ToString()) return true;
			}
			return false;
		}
	}
}
