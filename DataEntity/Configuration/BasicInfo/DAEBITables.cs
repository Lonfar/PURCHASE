using System;
using System.Data.SqlClient;//�¼�
namespace DataEntity
{
	/// <summary>
	/// DAESex ��ժҪ˵����
	/// </summary>
	public class DAEBITables:DAEBase
	{
		public DAEBITables()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public SqlDataReader GetData ()
		{
		string sSql = "select idkey from projects where oneself = '2'" ;
		System.Data.SqlClient.SqlDataReader drInfo  =	 Common.GetProjectDataAcess.GetDataAcess().GetDataReader(sSql) ; 
				
return drInfo;
		}

		// ============== ��Ŀ��Ϣ =============== //

		/// <summary>
		/// ��Ŀ��Ϣ��ֻ����һ��Ϊ"����Ŀ(2)",һ��Ϊ"�ܲ�(3)"���м�¼����Ϊ������ʱ��ԭ���ü�¼����Ϊ"����(1)",
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
