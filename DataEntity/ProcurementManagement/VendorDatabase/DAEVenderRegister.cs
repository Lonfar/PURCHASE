using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEVenderRegister ��ժҪ˵����
	/// </summary>
	public class DAEVenderRegister:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();

		public DAEVenderRegister()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �жϵ�¼���Ƿ��ظ�
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
