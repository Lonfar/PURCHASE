using System;
using Common;
using Cnwit.Utility;

namespace DataEntity
{
	/// <summary>
	/// DAEVendorBlackList ��ժҪ˵����
	/// </summary>
	public class DAEVendorBlackList: DAEBase
	{
		DataAcess _da = GetProjectDataAcess.GetDataAcess();

		public DAEVendorBlackList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��ӵ����������û�����������Ϊ��
		/// </summary>
		/// <param name="strVendorIDKey"></param>
		public void AddToBlackList ( string strVendorIDKey )
		{
			string SelectSql = "UPDATE Vendor SET LoginName = '' , Passwd = '' WHERE IDKey = '"+strVendorIDKey+"'";

			_da.ExecuteDMLSQL( SelectSql );
		}
	}
}
