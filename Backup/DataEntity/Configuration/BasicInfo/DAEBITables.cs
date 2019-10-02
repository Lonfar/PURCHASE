using System;
using System.Data.SqlClient;//新加
namespace DataEntity
{
	/// <summary>
	/// DAESex 的摘要说明。
	/// </summary>
	public class DAEBITables:DAEBase
	{
		public DAEBITables()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public SqlDataReader GetData ()
		{
		string sSql = "select idkey from projects where oneself = '2'" ;
		System.Data.SqlClient.SqlDataReader drInfo  =	 Common.GetProjectDataAcess.GetDataAcess().GetDataReader(sSql) ; 
				
return drInfo;
		}

		// ============== 项目信息 =============== //

		/// <summary>
		/// 项目信息中只能有一个为"本项目(2)",一个为"总部(3)"当有记录更改为这两项时将原来得记录更新为"其他(1)",
		/// </summary>
		/// <param name="iState"></param>
		public void UpdateProjectInfo ( int iState )
		{
			if ( iState == 2 || iState == 3 )
			{
				string strUpdateSql = "UPDATE Projects SET OneSelf = 1 WHERE OneSelf = "+iState;

				Common.GetProjectDataAcess.GetDataAcess().ExecuteDMLSQL( strUpdateSql );
			}
		}

		// ======================================== //

	}
}
