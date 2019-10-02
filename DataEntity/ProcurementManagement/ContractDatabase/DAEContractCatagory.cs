using System;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// ��ͬ��������ʵ����
	/// </summary>
	public class DAEContractCatagory : DAEBase
	{
		DataAcess _da ;
		
		public DAEContractCatagory()
		{
			_da = GetProjectDataAcess.GetDataAcess();
		}
	
		public string GetConID ( string strParentID )
		{
			// ============== Modified by Liujun at 12.18 ============= //
			// ���±������ 
			// 1.���ȿ���û���ӽڵ�,���û��ֱ�Ӽ�1
			// 2.������ӽڵ�Ļ�,�ҳ�����ӽڵ�,Ȼ�󽫸��ڵ��ȥ,���������ӽڵ��˳��


			string SelectSql = "SELECT MAX(ConID) As ConID From ContractCategory WHERE ParentID = '"+strParentID+"'";

			string GetConID = string.Empty;

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader(SelectSql) )
			{
				while ( dr.Read () )
				{
					if ( dr.IsDBNull(0) )
					{
						GetConID = strParentID+"_1";
					}
					else
					{
						string strTemp = Convert.ToString( dr["ConID"] ).Remove( 0 , strParentID.Length+1 );

						GetConID = strParentID +"_"+Convert.ToString ( Convert.ToInt32( strTemp ) + 1 );
					}
				}
			}
			return GetConID;
		}

		/// <summary>
		/// ���ͬһ����������������Ŀ����������ͬ����������
		/// </summary>
		/// <param name="strContractDescription">��ͬ��������</param>
		/// <param name="strConID">����</param>
		/// <param name="strParentID">��ID</param>
		/// <returns>��¼����</returns>
		public int ContractNum ( string strContractDescription , string strConID , string strParentID )
		{
			string SelectSql = "SELECT COUNT(*) AS Num FROM ContractCategory WHERE ParentID = '"+strParentID+"' AND Description = '"+strContractDescription+"' AND ConID <> '"+strConID+"'";

			int iNum = 0;

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader(SelectSql) )
			{
				while ( dr.Read() )
				{
					iNum = Convert.ToInt32( dr["Num"] );
				}
			}

			return iNum;
		}

		/// <summary>
		///  wether one node has children
		/// </summary>
		/// <param name="idKey">Bi_department's pk</param>
		/// <returns></returns>
		public bool HashChildren(string strConID)
		{
			string sql="";
			sql="select count(*) from ContractCategory where ParentID  = '"+strConID+"'";
			System.Data.DataTable dt=_da.GetDataTable(sql);
			if ((int)dt.Rows[0][0]>0)
				return true;
			else
				return false;
		}
	}
}
