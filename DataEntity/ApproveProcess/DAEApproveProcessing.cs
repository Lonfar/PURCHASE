using System;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// 审批处理(浏览页面)的数据实体类
	/// </summary>
	public class DAEApproveProcessing : DAEBase
	{

		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;
		public DAEApproveProcessing()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}



		public string  GetObjectTypeID(string SIDkey)
		{
			strSql = "SELECT ObjectiveType FROM PutIn WHERE IDKey='"+SIDkey+"'";
			return _da.GetDataTable(strSql).Rows[0]["ObjectiveType"].ToString();

		}

		#region 根据审核人来获得对应的审核记录的集合

		/// <summary>
		/// 按条件选取审核人表中审核人对应的审核级别列表
		/// </summary>
		/// <param name="personID"></param>
		/// <returns>审核级别列表</returns>
		public IList GetApproveLevel( string personID )
		{
			IList ApproveInfoList = new ArrayList();

			string SelectSql = @"SELECT ApproveMember.ApproveLevel , 
								ApproveMember.PutInID 
								FROM ApproveMember 
								INNER JOIN BI_DepartmentEmployee ON ApproveMember.ApproveID = BI_DepartmentEmployee.IDKey  
								INNER JOIN PutIn ON PutIn.IDKey = ApproveMember.PutInID
								WHERE BI_DepartmentEmployee.EmployeeID = '"+personID+"' AND PutIn.State <> 1";
		
			using ( SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					ApproveInfo approveInfo = new ApproveInfo();

					approveInfo.ApproveLevel = Convert.ToInt32 ( dr["ApproveLevel"] );
					approveInfo.PutInID = Convert.ToString ( dr["PutInID"] );
					approveInfo.PersonID = personID ;

					ApproveInfoList.Add ( approveInfo );
				}
			}

			return ApproveInfoList;
		}

		#endregion

		#region 通过登陆人员获得需要此人审批的IDKey

		/// <summary>
		/// 通过登陆人员获得需要此人审批的IDKey
		/// </summary>
		/// <param name="personID"></param>
		/// <param name="ApproveLevel"></param>
		/// <returns></returns>
		public string GetPutInIDKey ( string personID )
		{
			// 获得审批级别列表
			IList ApproveInfoList = GetApproveLevel( personID );

			if ( ApproveInfoList.Count == 0 )
			{
				return string.Empty;
			}
			else
			{
				StringBuilder strBuilder = new StringBuilder();
			
				foreach ( ApproveInfo approveInfo in ApproveInfoList )
				{		
					string SelectSql = string.Empty;
					// 判断此人的审核级别
					if ( approveInfo.ApproveLevel == 1 )
					{
						// 到PutIn表中根据ObjectiveID,ObjectiveType来查找State为-1(待审)的记录
						SelectSql = @"SELECT DISTINCT ApproveMember.PutInID FROM ApproveMember 
									INNER JOIN PutIn ON ApproveMember.PutInID = PutIn.IDKey 
									INNER JOIN BI_DepartmentEmployee ON ApproveMember.ApproveID = BI_DepartmentEmployee.IDKey  
									WHERE PutIn.State = -1 AND BI_DepartmentEmployee.EmployeeID = '"+personID+"'AND ApproveMember.PutInID = '"+approveInfo.PutInID+"'";
						
					}
					else
					{
						// 到Approve中查找当前状态State = 1的记录的IDKey
						SelectSql = @"SELECT ApproveMember.PutInID , MAX(Approved.CurrApproveLevel) AS Level 
									FROM ApproveMember 
									INNER JOIN PutIn ON ApproveMember.PutInID = PutIn.IDKey 
									INNER JOIN Approved On Approved.PutInID = PutIn.IDKey 
									INNER JOIN BI_DepartmentEmployee ON ApproveMember.ApproveID = BI_DepartmentEmployee.IDKey  
									WHERE Approved.State = 1 AND PutIn.State = 0 AND BI_DepartmentEmployee.EmployeeID = '"+personID+"'GROUP BY ApproveMember.PutInID HAVING ApproveMember.PutInID = '"+approveInfo.PutInID+"' ";
					}

					using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
					{
						while ( dr.Read() )
						{
							if ( approveInfo.ApproveLevel == 1 )
							{
								strBuilder.Append ( "'"+Convert.ToString( dr["PutInID"] ) + "'," );
							}
							else
							{
								// 如果级别为当前的审核级别
								if ( approveInfo.ApproveLevel == Convert.ToInt32( dr["Level"] ) && approveInfo.PutInID == Convert.ToString( dr["PutInID"] ) )
								{
									strBuilder.Append ( "'"+Convert.ToString( dr["PutInID"] ) + "'," );
								}
							}
						}
					}
				}

				string strIDKey = string.Empty;

				if (strBuilder.Length > 0 )
				{
					if ( strBuilder[strBuilder.Length-1] == ',' )
					{
						// 将最后一个","去掉
						strIDKey = strBuilder.Remove( strBuilder.Length - 1 , 1 ).ToString();
					}
				}
				//				if ( strIDKey.EndsWith(",") )
				//				{
				//					strIDKey.Remove( strIDKey.Length - 1 , 1 );
				//				}

				return strIDKey;
			}
		}

		#endregion
	}
}
