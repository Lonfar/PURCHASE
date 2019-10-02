using System;
using System.Data;
namespace DataEntity
{
	/// <summary>
	/// SR������ʵ����
	/// </summary>
	public class DAEServiceRequistion_Edit:DAEBase
	{
		/// <summary>
		/// ���ݴ洢��
		/// </summary>
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;
		public DAEServiceRequistion_Edit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        public string GetMaxID()
        {
            strSql = "SELECT dbo.f_GetSRNO() ";
            return _da.GetDataTable(strSql).Rows[0][0].ToString();
        }
		/// <summary>
		/// ��֤�������Ƿ����
		/// wanglijie on 2008-02-03
		/// </summary>
		/// <param name="SRID"></param>
		/// <returns></returns>
		public string CheckSRNO(string SRID)
		{
			string sSql = "SELECT * FROM ServiceRequistion WHERE SRID = '"+SRID+"'";
			if(_da.GetDataTable(sSql).Rows.Count > 0)			
			{
				return "ExistSRID";
			}
			return "";
		}


        public string CheckTenderNO(string tenderID)
        {
            string sSql = "SELECT * FROM ServiceRequistion WHERE MisTenderID = '" + tenderID + "'";
            if (_da.GetDataTable(sSql).Rows.Count > 0)
            {
                return "ExistTenderID";
            }
            return "";
        }

		public DataTable SelectSRPlanType()		
		{ 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;		
			return _da.ExecuteSPQueryDataTable("spSelectBT_SRPlanType",sParams,objParamValues,paramTypes) ; 	
		}


		public string GetMaxID(string SRPlanType)
		{
			strSql = "SELECT SRIDCodeRule FROM BT_SRPlanType WHERE IDKey = '"+SRPlanType+"'";
			DataTable dt = _da.GetDataTable(strSql);

			strSql="SELECT dbo.f_NextBH('"+dt.Rows[0][0].ToString()+"') ";
			string ID= _da.GetDataTable(strSql).Rows[0][0].ToString();
			return ID;
		}


		public string  SelectDepEmloyeeID(string strEmloyeeID)		
		{ 			
			strSql = "SELECT BI_DepartmentEmployee.IDKey FROM BI_DepartmentEmployee WHERE BI_DepartmentEmployee.EmployeeID = '"+strEmloyeeID+"'";
			DataTable dt=_da.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			return dt.Rows[0][0].ToString();
			else
				return "";
		}



		public bool IsUndertaker(string sCurrentUserID,string  sPkValue)
		{
			strSql = "SELECT COUNT(1) FROM ServiceRequistion WHERE ProUndertaker ='"+sCurrentUserID+"' AND IDKey = '"+sPkValue+"'";
			if(int.Parse(_da.GetDataTable(strSql).Rows[0][0].ToString())>0)
				return true;
			else
				return false;

		}

		/// <summary>
		/// �õ���½�ߺ����ز���֮��Ĺ�ϵ
		/// </summary>
		/// <param name="sCurrentUserID">�û�ID</param>
		/// <param name="nType">�ж�����</param>
		/// <returns></returns>
		public bool IsMainDepLeader(string sCurrentUserID,int nType)
		{
			if(nType == 1)
			{
				//�����ز��ŵ��쵼
				strSql="SELECT COUNT(1) FROM BI_DepartmentEmployee a,BI_Department b WHERE a.DepartmentID = b.idkey AND  b.IsMisDepartment=1  AND  a.Principal =1 AND a.EmployeeID ='"+sCurrentUserID+"'";
			}
			else
			{
				//�����ز��ŵ���Ա
				strSql="SELECT COUNT(1) FROM BI_DepartmentEmployee a,BI_Department b WHERE a.DepartmentID = b.idkey AND  b.IsMisDepartment=1   AND a.EmployeeID ='"+sCurrentUserID+"'";

			}
			if(int.Parse(_da.GetDataTable(strSql).Rows[0][0].ToString())>0)
				return true;
			else
				return false;


		}

		public DataTable GetDepartmentIdandNamebyEmployeeID(string EmployeeID)
		{
            strSql="SELECT BI_Department.IDKey, BI_Department.DepartmentName FROM BI_DepartmentEmployee,BI_Department WHERE BI_DepartmentEmployee.DepartmentID=BI_Department.IDKey AND BI_DepartmentEmployee.IDKey='"+ EmployeeID +"'";
			DataTable dtDepartmentIdandName=_da.GetDataTable(strSql);
			return dtDepartmentIdandName;
		}

		public int GetTenderState(string strPkValue,string UserID,string popedomNew)
		{
			strSql="SELECT * FROM ServiceRequistion WHERE charindex(','+convert(varchar,ServiceRequistion.SRState)+',',',"+popedomNew+",')>0 AND  ServiceRequistion.IDKey='"+strPkValue+"' AND ServiceRequistion.CreateBy= '"+ UserID +"'";
			DataTable dtTenderState=_da.GetDataTable(strSql);
			return dtTenderState.Rows.Count;

		}
		public DataTable GetProInfo(string pkValue)
		{
			strSql="SELECT BI_Department.DepartmentName, "+
					" BI_PositionType.PositionName,BI_Employee.FullName, "+
					" Approved.Contents,Approved.ApprovedDate FROM Approved  join (select IDKey,ApproveFlowID from PutIn t where "+
					" (select count(1) from PutIn where ObjectiveID=t.ObjectiveID and ApprovedBy>t.ApprovedBy)<1)a on "+
					" Approved.PutInID =a.IDKey left join TI_ApproveFlowMember on a.ApproveFlowID = TI_ApproveFlowMember.IDKey "+
					" join BI_Department on  TI_ApproveFlowMember.ApproeDepartmentID = BI_Department.IDKey join BI_PositionType "+
					" on TI_ApproveFlowMember.PositionID = BI_PositionType.IDKey join BI_Employee   on Approved.ApprovedBy = BI_Employee.IDKey  and (Approved.CurrApproveLevel -1) = TI_ApproveFlowMember.ApproeLevel where Approved.ObjectiveID = '"+pkValue+"' order by CurrApproveLevel asc";
			DataTable dtProInfo=_da.GetDataTable(strSql);
			return dtProInfo;
			


		}

		public int GetMtTenderState(string strPkValue,string popedomNew)
		{
			strSql="SELECT * FROM ServiceRequistion WHERE ServiceRequistion.SRState >= "+int.Parse(popedomNew)+" AND  ServiceRequistion.IDKey='"+strPkValue+"' ";
			DataTable dtTenderState=_da.GetDataTable(strSql);
			return dtTenderState.Rows.Count;

		}	

		
		public int GetCurrentTenderState(string strPkValue,string popedomNew)
		{
			strSql="SELECT * FROM ServiceRequistion WHERE charindex(','+convert(varchar,ServiceRequistion.SRState)+',',',"+popedomNew+",')>0 AND  ServiceRequistion.IDKey='"+strPkValue+"' ";
			DataTable dtTenderState=_da.GetDataTable(strSql);
			return dtTenderState.Rows.Count;

		}
		public void DelViewer(string strIDKey)
		{
			string strSql = "delete serviceRequestViewer where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		/// <summary>
		/// �õ����в鿴����Ϣ
		/// </summary>
		/// <returns></returns>
		public DataTable GetSeePerson( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName],
							  BI_PositionType.PositionName as [BI_PositionType.PositionName],
							  ServiceRequestViewer.ViewerID as [ServiceRequestViewer.ViewerID],
							   'Edit' RowStatus,
							  ServiceRequestViewer.IDKey as [ServiceRequestViewer.IDKey],
							  ServiceRequestViewer.SRIDKey as [ServiceRequestViewer.SRIDKey],
							  BI_DepartmentEmployee.DepartmentID as [ServiceRequestViewer.DepartmentID]
							  
						from ServiceRequestViewer,
							BI_Employee,
							BI_DepartmentEmployee,
							BI_PositionType
						where 
							ServiceRequestViewer.ViewerID = BI_Employee.IDKey
						and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and ServiceRequestViewer.DepartmentID = BI_DepartmentEmployee.DepartmentID
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						and ServiceRequestViewer.SRIDKey = '"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}
		/// <summary>
		/// ����BI_DepartmentEmployee.IDKey �õ�BI_Employee.IDKey
		/// </summary>
		/// <returns></returns>
		public string GetBI_EmployeeID( string pkValue )
		{
			string strSql = "";
			strSql = @"select	BI_Employee.IDKey
							  
						from 
							BI_Employee,
							BI_DepartmentEmployee
							
						where 
							BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and BI_DepartmentEmployee.IDKey = '"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			if(dt.Rows.Count > 0)
			{
				return dt.Rows[0]["IDKey"].ToString();
			}else return "";
		}
		/// <summary>
		/// �õ�ָ�����ŵ���Ա��Ϣ
		/// </summary>
		/// <param name="DepartmentID">����ID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Department( string DepartmentID )
		{
			string strSql = "";
			strSql = @"select  BI_Employee.FullName,
							   BI_PositionType.PositionName,
							   BI_Employee.IDKey,
							   BI_DepartmentEmployee.DepartmentID

						from BI_Employee,
							 BI_Department,
							 BI_DepartmentEmployee,
							 BI_PositionType
						where BI_DepartmentEmployee.DepartmentID = '"+DepartmentID+"'";
			strSql += @"and BI_Department.IDKey = BI_DepartmentEmployee.DepartmentID
					   and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
					   and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						";

			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		/// <summary>
		/// �õ������û���Ϣ
		/// </summary>
		/// <param name="UserID">�û�ID(��ʱ����Ϊ����Ա����IDKey���Ժ���ܻ��б䶯)</param>
		/// <returns></returns>
		public DataTable GetSeePerson_User( string UserID )
		{
			string strSql = "";
			strSql = @"SELECT BI_Employee.FullName,                 
							  BI_PositionType.PositionName,
							  BI_Employee.IDKey,
							  BI_DepartmentEmployee.DepartmentID

						FROM  BI_Employee , 
							  BI_DepartmentEmployee,
							  BI_PositionType

						where BI_DepartmentEmployee.IDKey='"+UserID+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// �õ�ָ�������Ա��Ϣ
		/// </summary>
		/// <param name="UserID">GroupID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Group( string GroupID )
		{
			string strSql = "";
			strSql = @"select  
							BI_Employee.FullName,
							BI_PositionType.PositionName,
  							BI_Employee.IDKey,
							BI_DepartmentEmployee.DepartmentID

						from TI_DefaultGroupMember,
							 BI_Employee,
							 BI_PositionType,
							 BI_DepartmentEmployee

						where TI_DefaultGroupMember.IDKEY = '"+GroupID+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		/// <summary>
		/// ͨ�����(���������)������ӱ������*****�����VoucherEdit�ؼ�������Ϊ�ӿؼ���Bug** Added By Liujun at 9.25 16:25 ****
		/// </summary>
		/// <param name="ParentKey"></param>
		/// <returns></returns>
		public string GetIDKey ( string ParentKeyFieldName , string ParentValue , string ChildTableName )
		{
			string IDKey = string.Empty;

			string SelectSql = " SELECT IDKey FROM "+ChildTableName+" WHERE "+ParentKeyFieldName +" = '"+ ParentValue+"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				IDKey = Convert.ToString ( dataTable.Rows[0][0] );
			}

			return IDKey ;
		}

		/// <summary>
		/// ͨ��Ա����ID��������ڲ��ŵ�ID�ַ���,��','�ָ� ( Added By Liujun at 11.1 )
		/// </summary>
		/// <param name="EmployeeID">����Ա��ID</param>
		/// <returns>���ڲ���ID</returns>
		public string GetDepartmentIDByEmployeeID ( string EmployeeID )
		{
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

			string SelectSql = "SELECT BI_Department.IDKey FROM BI_DepartmentEmployee,BI_Department WHERE BI_DepartmentEmployee.DepartmentID=BI_Department.IDKey AND BI_DepartmentEmployee.IDKey='"+ EmployeeID +"'";

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					strBuilder.Append ( "'"+Convert.ToString( dr["IDKey"] ) + "'," );
				}
			}

			string strDepartmentIDKey = string.Empty;

			if (strBuilder.Length > 0 )
			{
				if ( strBuilder[strBuilder.Length-1] == ',' )
				{
					// �����һ��","ȥ��
					strDepartmentIDKey = strBuilder.Remove( strBuilder.Length - 1 , 1 ).ToString();
				}
			}

			return strDepartmentIDKey;
		}

		
		/// <summary>
		/// ͨ����������������ӿ��� ( Added By wxc at 12.1 )
		/// </summary>
		/// <param name="EmployeeID"></param>
		/// <returns></returns>
		public string GetDepsByDepID(string DepID)
		{
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

			string SelectSql ="SELECT a.IDKey FROM BI_Department a,f_CDepartment_IDKey('"+DepID+"') b WHERE a.IDKey=b.[ID]";

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					strBuilder.Append( "'"+Convert.ToString( dr["IDKey"] ) + "'," );
				}
			}

			string strDepartmentIDKey = string.Empty;

			if ( strBuilder.Length > 0 )
			{
				if ( strBuilder[strBuilder.Length-1] == ',' )
				{
					strDepartmentIDKey = strBuilder.Remove ( strBuilder.Length - 1 , 1 ).ToString();
				}
			}

			return strDepartmentIDKey ;





		}

		/// <summary>
		/// ͨ��Ա�����������Ӧ��ID ( Added By Liujun at 11.2 )
		/// </summary>
		/// <param name="EmployeeID"></param>
		/// <returns></returns>
		public string GetPositionIDByEmployeeID ( string EmployeeID )
		{
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

			string SelectSql = "SELECT BI_DepartmentEmployee.PositionID FROM BI_DepartmentEmployee,BI_PositionType WHERE  BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey AND BI_DepartmentEmployee.IDKey='"+ EmployeeID +"'";

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					strBuilder.Append ( "'"+Convert.ToString( dr["PositionID"] ) + "'," );
				}
			}

			string strPositionID = string.Empty;

			if ( strBuilder.Length > 0 )
			{
				if ( strBuilder[strBuilder.Length-1] == ',' )
				{
					strPositionID = strBuilder.Remove ( strBuilder.Length - 1 , 1 ).ToString();
				}
			}

			return strPositionID ;
		}



		/// <summary>
		/// ͨ��Ա�����������Ӧְλ��ID ( Added By wxc at 11.4)
		/// </summary>
		/// <param name="EmployeeID"></param>
		/// <returns></returns>
		public DataTable GetPositionByDepEmployeeID ( string EmployeeID )
		{
			DataTable dt = new DataTable(); 
			string sSql = "SELECT  e.[PositionID] as [PositionID],"+    
				" p.[PositionName] as [PositionType] FROM  BI_DepartmentEmployee AS e  left JOIN BI_PositionType p ON p.IDKey=e.PositionID  "+
				"  WHERE e.EmployeeID = '"+EmployeeID+"'";

			dt = _da.GetDataTable(sSql);

			return dt;

		
		}

		/// <summary>
		/// ͨ��Ա�����������Ӧ��ID ( Added By wxc at 12.4 )
		/// </summary>
		/// <param name="EmployeeID"></param>
		/// <returns></returns>
		public DataTable GetDepartmentByEmployeeID(string EmployeeID)
		{
			DataTable dt = new DataTable(); 
			string rDepartmentID = string.Empty;
			
			string sSql = "SELECT  BI_Department.DepartmentDepth , e.[DepartmentID] as [DepartmentID], BI_Department.DepartmentName as   [DepartmentName],e.[PositionID] as [PositionID],"+    
						" p.[PositionName] as [PositionType] FROM  BI_DepartmentEmployee AS e  left JOIN BI_PositionType p ON p.IDKey=e.PositionID  "+
						" left join BI_Department on e.DepartmentID =BI_Department.IDKey  WHERE e.EmployeeID = '"+EmployeeID+"'";

			dt = _da.GetDataTable(sSql);

			//�жϲ��ż����Ƿ�Ϊ  2
			if(dt.Rows.Count > 0)
			{
				if(dt.Rows[0]["DepartmentDepth"]!=System.DBNull.Value&&dt.Rows[0]["DepartmentDepth"].ToString().Length>0)
				{
					if(dt.Rows[0]["DepartmentDepth"].ToString().Trim() =="2")
					{
						rDepartmentID=dt.Rows[0]["DepartmentID"].ToString().Trim();

					}
					else
					{
						//��һ�µݹ����
						int nDepartmentDepth = int.Parse(dt.Rows[0]["DepartmentDepth"].ToString().Trim());
						string sDepartmentID = dt.Rows[0]["DepartmentID"].ToString().Trim();
						FindDepartment(nDepartmentDepth,sDepartmentID,ref rDepartmentID);



					}
				}

				
			}

			sSql = "SELECT   BI_Department.[IDKey] as [DepartmentID], BI_Department.DepartmentName as   [DepartmentName] "+
				" FROM BI_Department  WHERE BI_Department.IDKey = '"+rDepartmentID+"'";

			dt = _da.GetDataTable(sSql);

			return dt;

		}


		/// <summary>
		/// ( Added By wxc at 12.4 )
		/// </summary>
		/// <param name="EmployeeID"></param>
		/// <returns></returns>

		private void FindDepartment(int nDepartmentDepth,string sDepartmentID,ref string rDepID)
		{
			if(nDepartmentDepth.ToString().Trim() == "2")
			{

				rDepID = sDepartmentID;
			}
			else
			{
				DataTable dt_Temp=new DataTable();
				string sSql= "SELECT PDepartmentID from BI_Department WHERE IDKey = '"+sDepartmentID.Trim()+"'";
				dt_Temp = _da.GetDataTable(sSql);
				if(dt_Temp.Rows.Count>0)			
				{
					nDepartmentDepth  = nDepartmentDepth-1;
					FindDepartment(nDepartmentDepth,dt_Temp.Rows[0]["PDepartmentID"].ToString(),ref rDepID);
				}
			}

		}

		/// <summary>
		/// ���Ա����Ϣ
		/// </summary>
		/// <param name="strEmployeeIDKey">Ա��IDKey</param>
		/// <returns></returns>
		public DataTable GetEmployeeInfo ( string strEmployeeIDKey )
		{
			string strSelectSql = "SELECT * FROM BI_Employee WHERE IDKey = '"+strEmployeeIDKey+"'";

			DataTable dt = BaseDataAccess.GetDataTable ( strSelectSql );

			return dt;
		}

	}
}
