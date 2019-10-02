using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEVenderRegister 的摘要说明。
	/// </summary>
	public class DAEVenderRegister:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();

		public DAEVenderRegister()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 判断登录名是否重复
		/// </summary>
		/// <param name="loginName"></param>
		/// <returns></returns>
		public bool IsExistLoginName(string loginName)
		{
			string sql = @"select * from vendor where LoginName = '"+loginName+"'";
			DataTable dt = _da.GetDataTable(sql);
			if (dt.Rows.Count > 0)
			{
				return true; 
			}else return false;
		}
	}
}
