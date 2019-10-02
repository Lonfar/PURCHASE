using System;
using System.Data;
namespace DataEntity
{
	/// <summary>
	/// 审批处理(编辑页面)的数据实体类
	/// </summary>
	public class DAEApproveProcessing_Edit : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAEApproveProcessing_Edit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 更新SR的状态
		/// </summary>
		/// <param name="State">状态</param>
		/// <param name="IDKey">SR主键</param>
		public void UpdateTenderState ( string IDKey , string State )
		{
			_da.ExecuteDMLSQL ( "UPDATE ServiceRequistion SET SRState = '"+State+"'WHERE ServiceRequistion.IDKey = '"+IDKey+"'" );
		}

		/// <summary>
		/// 更改提交表的状态
		/// </summary>
		/// <param name="ObjectiveID">对象ID</param>
		/// <param name="ObjectiveType">对象类型</param>
		/// <param name="State">对象状态</param>
		public void UpdatePutInState ( string IDKey , int State )
		{
			_da.ExecuteDMLSQL ( "UPDATE PutIn SET State = "+State+" WHERE IDKey = '"+IDKey+"'" );
		}

		/// <summary>
		/// 通过PubishID得到 title、content、releaseDate信息
		/// </summary>
		public DataTable GetBidPlacardInfo (string publishID)
		{
			string strSql = @" select Title ,Contents ,PublishDate from BidPlacard where publishID = '"+publishID+"'";
			DataTable dt = _da.GetDataTable( strSql );
			return dt;
		}

		#region 查看指定用户是否为指定流程的最后审批人

		/// <summary>
		/// 查看指定用户是否为指定流程的最后审批人
		/// </summary>
		/// <param name="personID">审批人</param>
		/// <param name="PutID">审批流程</param>
		/// <returns></returns>
		public bool IsLastApprove ( string PutInID , int ApproveLevel_CurrentUser )
		{
			bool IsLast = false ;

			string SelectSql = @"SELECT MAX(ApproveLevel) AS Level
								FROM ApproveMember 
								WHERE PutInID = '"+PutInID+"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				if ( Convert.ToInt32 ( dataTable.Rows[0]["Level"] ) == ApproveLevel_CurrentUser )
				{
					IsLast = true;
				}
			}

			return IsLast;
		}

		#endregion

		
		#region 获得指定SR的当前审核级别
		/// <summary>
		/// 根据提交ID ，联合Approved和PutIn表选取Approved表中（状态为通过的）在审的SR对应的当前审核级别
		/// 查不到记录，返回1；
		/// </summary>
		/// <param name="objType"></param>
		/// <param name="objID"></param>
		/// <returns></returns>
		public int GetCurrentSRApproveLevel(string objType,string objID)
		{
			int iApproveLevel = 1;

			string SelectSql = @"SELECT MAX( Approved.CurrApproveLevel ) AS Level FROM Approved 
								INNER JOIN PutIn 
								ON Approved.PutInID = PutIn.IDKey
								WHERE PutIn.ObjectiveType = '"+objType+"' AND PutIn.ObjectiveID = '"+objID+"' AND PutIn.State = 0 ";
		
			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					if ( dr["Level"] != DBNull.Value )
					{
						iApproveLevel = Convert.ToInt32 ( dr["Level"] );
					}
				}
			}

			return iApproveLevel;
		}
		#endregion
	}
}
