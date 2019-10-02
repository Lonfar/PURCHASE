using System;

namespace DataEntity
{
	/// <summary>
	/// 招标公告(浏览页面)的数据实体类
	/// </summary>
	public class DAETenderBulletin : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETenderBulletin()
		{
			//
			// TODO: 在此处添加构造函数逻辑
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
