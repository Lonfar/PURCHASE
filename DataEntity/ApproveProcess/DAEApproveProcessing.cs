using System;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// ��������(���ҳ��)������ʵ����
	/// </summary>
	public class DAEApproveProcessing : DAEBase
	{

		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;
		public DAEApproveProcessing()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}



		public string  GetObjectTypeID(string SIDkey)
		{
			strSql = "SELECT ObjectiveType FROM PutIn WHERE IDKey='"+SIDkey+"'";
			return _da.GetDataTable(strSql).Rows[0]["ObjectiveType"].ToString();

		}

		#region �������������ö�Ӧ����˼�¼�ļ���

		/// <summary>
		/// ������ѡȡ����˱�������˶�Ӧ����˼����б�
		/// </summary>
		/// <param name="personID"></param>
		/// <returns>��˼����б�</returns>
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

		#region ͨ����½��Ա�����Ҫ����������IDKey

		/// <summary>
		/// ͨ����½��Ա�����Ҫ����������IDKey
		/// </summary>
		/// <param name="personID"></param>
		/// <param name="ApproveLevel"></param>
		/// <returns></returns>
		public string GetPutInIDKey ( string personID )
		{
			// ������������б�
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
					// �жϴ��˵���˼���
					if ( approveInfo.ApproveLevel == 1 )
					{
						// ��PutIn���и���ObjectiveID,ObjectiveType������StateΪ-1(����)�ļ�¼
						SelectSql = @"SELECT DISTINCT ApproveMember.PutInID FROM ApproveMember 
									INNER JOIN PutIn ON ApproveMember.PutInID = PutIn.IDKey 
									INNER JOIN BI_DepartmentEmployee ON ApproveMember.ApproveID = BI_DepartmentEmployee.IDKey  
									WHERE PutIn.State = -1 AND BI_DepartmentEmployee.EmployeeID = '"+personID+"'AND ApproveMember.PutInID = '"+approveInfo.PutInID+"'";
						
					}
					else
					{
						// ��Approve�в��ҵ�ǰ״̬State = 1�ļ�¼��IDKey
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
								// �������Ϊ��ǰ����˼���
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
						// �����һ��","ȥ��
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
