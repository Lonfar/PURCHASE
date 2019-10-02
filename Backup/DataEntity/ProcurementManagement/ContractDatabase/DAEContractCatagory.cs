using System;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// 合同类别的数据实体类
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
			// 更新编码规则 
			// 1.首先看有没有子节点,如果没有直接加1
			// 2.如果有子节点的话,找出最大子节点,然后将父节点除去,单独计算子节点的顺序


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
		/// 获得同一级别下主键不等于目标主键的相同描述的数量
		/// </summary>
		/// <param name="strContractDescription">合同分类描述</param>
		/// <param name="strConID">主键</param>
		/// <param name="strParentID">父ID</param>
		/// <returns>记录数量</returns>
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
