using System;
using Common;
using Cnwit.Utility;

namespace DataEntity
{
	/// <summary>
	/// DAEVendorBlackList 的摘要说明。
	/// </summary>
	public class DAEVendorBlackList: DAEBase
	{
		DataAcess _da = GetProjectDataAcess.GetDataAcess();

		public DAEVendorBlackList()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 添加到黑名单后将用户名和密码置为空
		/// </summary>
		/// <param name="strVendorIDKey"></param>
		public void AddToBlackList ( string strVendorIDKey )
		{
			string SelectSql = "UPDATE Vendor SET LoginName = '' , Passwd = '' WHERE IDKey = '"+strVendorIDKey+"'";

			_da.ExecuteDMLSQL( SelectSql );
		}
	}
}
