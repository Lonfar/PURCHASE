using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// �ɰ���Ա༭ҳ�������ʵ����
	/// </summary>
	public class DAETCStrategy_Edit:DAEBase
	{

		Cnwit.Utility.DataAcess _da ;
		string strSql ;

		public DAETCStrategy_Edit()
		{
			_da = Common.GetProjectDataAcess.GetDataAcess();
			strSql=string.Empty;
		}


		#region ɾ���鿴��
		public void DelViewer(string strIDKey)
		{
			string strSql = "delete TCStrategyViewer where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region  ����BI_DepartmentEmployee.IDKey �õ�BI_Employee.IDKey
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
			}
			else return "";
		}
		#endregion

		#region �õ���ͬС�����Ա��¼
		/// <summary>
		/// �õ���ͬС�����Ա��¼
		/// </summary>
		/// <param name="pkValue">�ɰ������������ֵ</param>
		/// <param name="IsMember">1--��������С�飬2---��������С�飬3---�鿴��</param>
		/// <returns></returns>
		public DataTable GetTable( string pkValue ,int IsMember)
		{
			string strSql = "";
			strSql = @"select distinct 
							TCStrategyViewer.GroupPeopleID as [TCStrategyViewer.GroupPeopleID],
							BI_Employee.FullName as [BI_Employee.FullName],
							TCStrategyViewer.OrderID as [TCStrategyViewer.OrderID],
							'Edit' RowStatus,
							TCStrategyViewer.IDKey as [TCStrategyViewer.IDKey],
							TCStrategyViewer.TenderID as [TCStrategyViewer.TenderID],
							TCStrategyViewer.IsMember as [TCStrategyViewer.IsMember],
							TCStrategyViewer.GroupPosition as [TCStrategyViewer.GroupPosition]
							  
						from TCStrategyViewer,
							BI_Employee,
							BI_DepartmentEmployee

						where 
							TCStrategyViewer.GroupPeopleID = BI_Employee.IDKey
						and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TCStrategyViewer.Ismember = '"+IsMember+"'";
			strSql+="	and TCStrategyViewer.TenderID ='"+pkValue+"'";
			if( IsMember != 3)
			{
				strSql+=" Order by TCStrategyViewer.OrderID";
			}

			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}

		// �ڼ��������ʱ���ò��Զ�Ӧ�ļ�������С��
		public DataTable GetTable_Tech( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct 
							TCStrategyViewer.GroupPeopleID as [TCStrategyViewer.GroupPeopleID],
							BI_Employee.FullName as [BI_Employee.FullName],
							BI_Employee.IDKey as [TechEvaluationGroup.personID],
							'New' RowStatus,
							TCStrategyViewer.GroupPosition as [TechEvaluationGroup.GroupPosition],
							BI_GroupPostion.PositionName  as [BI_GroupPostion.PositionName],
							'' [TechEvaluationGroup.IDKey],
							'' [TechEvaluationGroup.TechEvaluationID]						

						from TCStrategyViewer,
							BI_Employee,
							BI_DepartmentEmployee,
							BI_GroupPostion

						where 
							BI_GroupPostion.IDKey = TCStrategyViewer.GroupPosition
						and	TCStrategyViewer.GroupPeopleID = BI_Employee.IDKey
						and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TCStrategyViewer.Ismember = '1'";
			strSql+="	and TCStrategyViewer.TenderID ='"+pkValue+"'";
			
			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}

		// �����������ʱ���ò��Զ�Ӧ����������С��
		public DataTable GetTable_Comm( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct 
							TCStrategyViewer.GroupPeopleID as [TCStrategyViewer.GroupPeopleID],
							BI_Employee.FullName as [BI_Employee.FullName],
							BI_Employee.IDKey as [CommEvaluationGroup.personID],
							'New' RowStatus,
							TCStrategyViewer.GroupPosition as [CommEvaluationGroup.GroupPosition],
							BI_GroupPostion.PositionName  as [BI_GroupPostion.PositionName],
							'' [CommEvaluationGroup.IDKey],
							'' [CommEvaluationGroup.CommEvaluationID]						

						from TCStrategyViewer,
							BI_Employee,
							BI_DepartmentEmployee,
							BI_GroupPostion

						where 
							BI_GroupPostion.IDKey = TCStrategyViewer.GroupPosition
						and	TCStrategyViewer.GroupPeopleID = BI_Employee.IDKey
						and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TCStrategyViewer.Ismember = '2'";
			strSql+="	and TCStrategyViewer.TenderID ='"+pkValue+"'";
			
			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}

		#endregion

		#region ͨ���û�ID���õ���Ա��Ϣ
		/// <summary>
		/// �õ������û���Ϣ
		/// </summary>
		/// <param name="UserID">�û�ID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_User( string IDKey )
		{
			string strSql = "";
			strSql = @" SELECT BI_Employee.FullName,                 	  
							   BI_Employee.IDKey	

						FROM  BI_Employee , BI_DepartmentEmployee

						where BI_Employee.IDKey='"+IDKey+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID";
						

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		#endregion

		#region  ͨ������id���õ���Ա��Ϣ
		/// <summary>
		/// �õ�ָ�����ŵ���Ա��Ϣ
		/// </summary>
		/// <param name="UserID">DepartmentID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Dep( string DepartmentID )
		{
			string strSql = "";
			strSql = @"select  
							BI_Employee.FullName,
							BI_Employee.IDKey

						from 	 
							BI_Employee,
							BI_DepartmentEmployee

						where BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID

						and BI_DepartmentEmployee.DepartmentID ='"+DepartmentID+"'";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		#endregion
		
		#region ͨ����ID �õ���Ա��Ϣ
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
							--BI_PositionType.PositionName,
  							BI_Employee.IDKey
							--BI_DepartmentEmployee.DepartmentID

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
		#endregion

		#region �õ������б�
		public DataTable GetGroupPosition()
		{
			string sql = "select IDKey,PositionName from BI_GroupPostion";
			DataTable dt = _da.GetDataTable ( sql );
			
			return dt;
		}
		#endregion

		
		public int GetTCState(string strPkValue,string UserID,string popedomNew)
		{

			strSql="SELECT * FROM TCStrategy WHERE charindex(','+convert(varchar,TCStrategy.status)+',',',"+popedomNew+",')>0 AND  TCStrategy.TenderID='"+strPkValue+"' AND TCStrategy.CreateBy= '"+ UserID +"'";
			DataTable dtSRState=_da.GetDataTable(strSql);
			return dtSRState.Rows.Count;

		}

		public string GetTypeID(string sTenderID)
		{
			string sSql = " SELECT MRTypeID FROM TCStrategy WHERE TenderID = '"+sTenderID+"'";
			DataTable dt =  _da.GetDataTable(sSql);
			if ( dt.Rows.Count > 0 )
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}

		public DataTable GetSRinfo(string srid)
		{
			strSql="SELECT ServiceRequistion.* ,BI_Department.IDKey FROM ServiceRequistion ,BI_Department  WHERE ServiceRequistion.IDKey='"+ srid +"' and ServiceRequistion.DepartmentID = BI_Department.IDKey";
			DataTable dt= _da.GetDataTable(strSql); 
			if(dt.Rows.Count>0)
				return dt;
			else
				return null;
		}

		public void UpdateTable(DataTable dt)
		{
			int nCount = dt.Rows.Count;
			if(nCount>0)
			{
				for(int i = 0;i<nCount;i++)
				{
					if(dt.Rows[i].RowState != DataRowState.Deleted)
					{
						strSql = "SELECT * FROM TI_PlanActivity,TI_TenderCourse WHERE TI_PlanActivity.Idkey = '"+ dt.Rows[i]["IDKey"].ToString() +"'  "+
							" and TI_TenderCourse.Idkey =* TI_PlanActivity.TenderCourseID ";
					
						DataTable dtTemp = _da.GetDataTable(strSql);

						if (dtTemp.Rows.Count > 0 )
						{
							if(dt.Rows[i]["DateDemand"] == System.DBNull.Value)
							{
								dt.Rows[i]["DateDemand"] = dtTemp.Rows[0]["NumberDays"];
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// ���ݱ������еĹ�Ӧ�̱�ŵõ���Ӧ��Ӧ�̵�������Ϣ
		/// </summary>
		/// <param name="dataTable"></param>
		public void UpdateDataTable_VendorList ( DataTable dataTable )
		{
			string strVendorCode = string.Empty;

			string SelectSql = string.Empty;

			DataTable dt_Temp;

			foreach ( DataRow dr in dataTable.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					strVendorCode = Convert.ToString( dr["VendorCode"]);
		
					SelectSql = @"SELECT Vendor.VendorNo,
									Vendor.VendorName,
									Vendor.Address,
									Vendor.Telphone,
									Vendor.Fax,
									Vendor.Email,
									Vendor.Contact FROM Vendor LEFT JOIN VendorCountry ON Vendor.IDKey = VendorCountry.VendorID WHERE Vendor.IDkey = '"+strVendorCode+"' AND VendorCountry.State = 1";

					dt_Temp = _da.GetDataTable(SelectSql );

					if ( dt_Temp.Rows.Count > 0 )
					{
						//dr["VendorCode"] = dt_Temp.Rows[0]["VendorNo"];
						dr["VendorName"] =  dt_Temp.Rows[0]["VendorName"] ;
						dr["VendorAddr"] = dt_Temp.Rows[0]["Address"] ;
						dr["PhoneNumber"] = dt_Temp.Rows[0]["Telphone"] ;
						dr["FaxNumber"] = dt_Temp.Rows[0]["Fax"] ;
						dr["EMAIL"] = dt_Temp.Rows[0]["Email"] ;
						dr["LinkMan"] = dt_Temp.Rows[0]["Contact"];
					}
				}
			}
		}

		/// <summary>
		/// ���ݷ��������Ż�÷�������������Ϣ
		/// </summary>
		/// <param name="dataTable"></param>
		public void UpdataDataTable_RelatSR ( DataTable dataTable )
		{
			string SelectSql = string.Empty;
			DataTable dt_Temp ;

			foreach ( DataRow dr in dataTable.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					SelectSql = @"SELECT AppDate ,
									 PlanAmount , 
									 Currency ,
									 DepartmentID,DepartmentName
									 FROM ServiceRequistion Left Join BI_Department on ServiceRequistion.DepartmentID = BI_Department.IDKey
										 WHERE ServiceRequistion.IDKey = '"+dr["SRID"].ToString()+"'";

					dt_Temp = _da.GetDataTable ( SelectSql );

					if ( dt_Temp.Rows.Count > 0 )
					{
						if ( dr["ApplyDate"] != null)
						{ 
							dr["ApplyDate"] = dt_Temp.Rows[0]["AppDate"] ; 
						}
						if ( dr["ApplyDepartment"] != null )
						{ 
							dr["ApplyDepartment"] = dt_Temp.Rows[0]["DepartmentID"] ; 
						}
						if( dr["TCStrategySR__ApplyDepartment"] != null)
						{
							dr["TCStrategySR__ApplyDepartment"] = dt_Temp.Rows[0]["DepartmentName"] ;
						}
						if ( dr["PlanCurrency"] != null)
						{ 
							dr["PlanCurrency"] = dt_Temp.Rows[0]["Currency"] ; 
						}
						if ( dr["TCStrategySR__PlanCurrency"] != null)
						{ 
							dr["TCStrategySR__PlanCurrency"] = dt_Temp.Rows[0]["Currency"] ; 
						}
						if ( dr["PlanAmount"] != null )
						{ 
							dr["PlanAmount"] = Double.Parse(dt_Temp.Rows[0]["PlanAmount"].ToString()).ToString("f2");
						}
						if (dr["IsFinished"] != null )  
						{ 
							dr["IsFinished"] = 1; 
						}
					}
				}
			}
		}

		/// <summary>
		/// ������ݱ�����ѡ��Ӧ�̱�ŵ��б�
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string GetSelectedVendorNoList ( DataTable dt )
		{
			System.Text.StringBuilder strBuilder =  new System.Text.StringBuilder();

			string strVendorNoList = string.Empty;

			foreach ( DataRow dataRow in dt.Rows )
			{
				if( dataRow.RowState != DataRowState.Deleted )  // add by wanglijie on 2008-02-02 
				{
					strBuilder.Append ( "'" );
					strBuilder.Append ( Convert.ToString( dataRow["TCStrategyVendor.VendorCode"] ) );
					strBuilder.Append ( "'," );
				}
			}

			if ( strBuilder.Length > 0 ) 
			{
				if ( strBuilder[strBuilder.Length-1] == ',' )
				{
					strVendorNoList = strBuilder.ToString().TrimEnd( ',' );
				}
			}

			return strVendorNoList ;
		}

		/// <summary>
		/// �����������
		/// </summary>
		/// <returns></returns>
		public DataTable GetApproveProcess ( string strTenderID )
		{
			string SelectSql = @"SELECT ( CurrApproveLevel - 1 ) As Num , FullName , Contents,ApprovedDate,State From Approved , BI_Employee
										WHERE Approved.ApprovedBy = BI_Employee.IDKey AND ObjectiveType = 3 AND ObjectiveID = '"+strTenderID+"' ORDER BY CurrApproveLevel" ;

			DataTable dt_Temp = _da.GetDataTable ( SelectSql ) ;

			foreach ( DataRow dr in dt_Temp.Rows )
			{
				dr["ApprovedDate"] = Convert.ToDateTime( dr["ApprovedDate"]).ToShortDateString();
			}

			dt_Temp.AcceptChanges();

			return dt_Temp;
		}

	}
}
