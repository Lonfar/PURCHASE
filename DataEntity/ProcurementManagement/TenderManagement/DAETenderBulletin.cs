using System;

namespace DataEntity
{
	/// <summary>
	/// �б깫��(���ҳ��)������ʵ����
	/// </summary>
	public class DAETenderBulletin : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETenderBulletin()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public string GetProjectName ()
		{
			string SelectSQL = "SELECT ProjectName FROM Projects WHERE OneSelf = 1 ";

			string strProjectName = string.Empty;

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSQL ))
			{
				while ( dr.Read() )
				{
					strProjectName = Convert.ToString ( dr["ProjectName"] );		
				}
			}

			return strProjectName;
		}
	}
}
