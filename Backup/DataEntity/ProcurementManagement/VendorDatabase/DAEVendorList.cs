using System;

namespace DataEntity
{
	/// <summary>
	/// 供应商名录数据实体类 ( Added By Liujun at 10.24)
	/// </summary>
	public class DAEVendorList : DAEBase
	{
		public DAEVendorList()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 更新状态(用于检测是否过期)
		/// </summary>
		/// <param name="status"></param>
		/// <param name="IDKey"></param>
		/// <returns></returns>
		public string UpdateVendorStatus ( int status , string IDKey )
		{
			string errorMessage = string.Empty;

			errorMessage = this.BaseDataAccess.ExecuteDMLSQL ( "UPDATE Vendor SET Status = "+status + " WHERE IDKey = '"+IDKey+"'");

			return errorMessage;
		}
	}
}
