using System;

namespace DataEntity
{
	/// <summary>
	/// ��Ӧ����¼����ʵ���� ( Added By Liujun at 10.24)
	/// </summary>
	public class DAEVendorList : DAEBase
	{
		public DAEVendorList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����״̬(���ڼ���Ƿ����)
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
