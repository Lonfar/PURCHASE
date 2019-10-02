using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAECommEvaluation ��ժҪ˵����
	/// </summary>
	public class DAECommEvaluation : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;

		public DAECommEvaluation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public DataTable CheckState(String strTenderID )
		{
			string[] sParams = {"tableName","tablePK","tablePKValue","correlationField","ifCorrelation","correlationTable","correlationTableField","correlationFieldTable"} ;
			object[] objParamValues = {"CommEvaluation","IDKey",strTenderID,"TenderID","0","","","" } ; 
			SqlDbType[] paramTypes = { SqlDbType.VarChar , SqlDbType.VarChar , SqlDbType.VarChar, SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar } ;
			DataTable dt = BaseDataAccess.ExecuteSPQueryDataTable("sp_ControlState",sParams,objParamValues,paramTypes );
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			//�����������������ԣ������ж����Ӧ�ĺ�ͬ���Ƿ��м�¼���м�¼������ɾ��
			String mrType = String.Empty ;
			String strSql = String.Empty ; 
			String sqlStr = " Select  TCStrategy.MRTypeID From TCStrategy Inner Join CommEvaluation On TCStrategy.TenderID = CommEvaluation.TenderID Where CommEvaluation.IDKey='" + strPKValue + "'"  ; 
			DataTable dtt = _da.GetDataTable(sqlStr);
			if(dtt != null && dtt.Rows.Count>0)
			{
				mrType = dtt.Rows[0]["MRTypeID"] == DBNull.Value ? "0" : dtt.Rows[0]["MRTypeID"].ToString();
			}
			switch (Convert.ToInt32(mrType))
			{	
				case  (int)StrategyType.MR :
					strSql = " Select Count(*) as State From PurchaseOrder Inner Join TCStrategy On PurchaseOrder.TenderID=TCStrategy.TenderID Inner Join CommEvaluation On CommEvaluation.TenderID = TCStrategy.TenderID Where CommEvaluation.IDKey='" + strPKValue + "'" ;
					break ;
				case (int)StrategyType.SR :
					strSql = " Select Count(*) as State From Contract Inner Join TCStrategy On Contract.TenderNumber=TCStrategy.TenderID Inner Join CommEvaluation On CommEvaluation.TenderID = TCStrategy.TenderID Where CommEvaluation.IDKey='" + strPKValue + "'" ;
					break;
			}
			DataTable dt = new DataTable();
			if(strSql.Length > 0)
			{
				dt = _da.GetDataTable(strSql) ; 
			}
			return dt ; 
		}

		//****************************************************


		#region �鿴����ط���
		/// <summary>
		/// �õ����в鿴����Ϣ
		/// </summary>
		/// <returns></returns>
		public DataTable GetSeePerson( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName],
							BI_PositionType.PositionName as [BI_PositionType.PositionName],
							CommEvaluationView.ViewerID as [CommEvaluationView.ViewerID],
							'Edit' RowStatus,
							CommEvaluationView.IDKey as [CommEvaluationView.IDKey],
							CommEvaluationView.CommEvaluationID as [CommEvaluationView.CommEvaluationID]
							  
						from (((CommEvaluationView left outer join BI_Employee on CommEvaluationView.ViewerID = BI_Employee.IDKey)
						left join  BI_DepartmentEmployee on BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID)
						left join  BI_PositionType on  BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey)
						where CommEvaluationView.CommEvaluationID ='"+pkValue+"'";

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
			}
			else return "";
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
		/// <param name="UserID">�û�ID(Ա�����е�IDKey,BI_Employee)</param>
		/// <returns></returns>
		public DataTable GetSeePerson_User( string UserID )
		{
			string strSql = "";
			strSql = @"select BI_Employee.FullName ,
							BI_Employee.IDKey,
							BI_PositionType.PositionName
						from (BI_Employee left outer join BI_DepartmentEmployee 
							on BI_DepartmentEmployee.EmployeeID = BI_Employee.IDKey)
							left outer join BI_PositionType
							on BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						where BI_Employee.IDKey='"+UserID+"'";

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
		/// ɾ���鿴��
		/// </summary>
		/// <param name="strIDKey"></param>
		public void DelViewer(string strIDKey)
		{
			string strSql = "delete CommEvaluationView where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region ����С����ط���
		/// <summary>
		/// ��ʼ������С���б��õ���ӦС�������µ�ȫ����Ա��Ϣ
		/// </summary>
		/// <returns></returns>
		public DataTable InitCommEvaluation(int groupType)
		{
			string strSql = "";
			strSql = @"select 	
							BI_Employee.FullName as [BI_Employee.FullName] ,
							BI_GroupPostion.PositionName as [BI_GroupPostion.PositionName],
							TI_DefaultGroupMember.GroupUserID as [CommEvaluationGroup.personID],
							TI_DefaultGroupMember.PositionID  as [CommEvaluationGroup.GroupPosition],
							'New' RowStatus,
							cast(null as varchar(64))  [CommEvaluationGroup.IDKey],
							'' [CommEvaluationGroup.CommEvaluationID]

						from 	
							TI_DefaultGroup,
							TI_DefaultGroupMember,
							BI_GroupPostion,
							BI_DepartmentEmployee,
							BI_Employee 

						where 
							TI_DefaultGroup.IDKey = TI_DefaultGroupMember.IDKey
						and TI_DefaultGroupMember.PositionID = BI_GroupPostion.IDKey
						and TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
						and BI_DepartmentEmployee.EmployeeID = BI_Employee.IDKey
						and TI_DefaultGroup.DefaultGroupTypeID = "+groupType;

			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// �ж��Ƿ��Ѵ�������С���Ա
		/// </summary>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		public bool IsExistCommEvaluationGroup(string pkValue)
		{
			string strSql = "";	
			strSql = @" select 1 from CommEvaluationGroup where CommEvaluationID ='"+pkValue+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			if (dt.Rows.Count >0) return true;
			else return false;
		}
		
		/// <summary>
		/// ͨ����������������õ�����С���Ա
		/// </summary>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		public DataTable GetCommEvaluationInfo(string pkValue)
		{
			string strSql = "";
			//			strSql = @"
			//						select 	BI_Employee.FullName as [BI_Employee.FullName],
			//							BI_GroupPostion.PositionName as [BI_GroupPostion.PositionName],
			//								TI_DefaultGroupMember.GroupUserID as [CommEvaluationGroup.personID],
			//								TI_DefaultGroupMember.PositionID  as [CommEvaluationGroup.GroupPosition],
			//								'Edit' RowStatus,
			//								CommEvaluationGroup.IDKey as [CommEvaluationGroup.IDKey],
			//								CommEvaluationGroup.CommEvaluationID as [CommEvaluationGroup.CommEvaluationID]
			//
			//						from	CommEvaluationGroup,
			//							TI_DefaultGroupMember,
			//							BI_GroupPostion,
			//							BI_DepartmentEmployee,
			//							BI_Employee 
			//
			//						where 	CommEvaluationGroup.PersonID = BI_DepartmentEmployee.IDKey
			//						and 	CommEvaluationGroup.GroupPosition = TI_DefaultGroupMember.PositionID
			//						and 	TI_DefaultGroupMember.PositionID = BI_GroupPostion.IDKey
			//						and 	TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
			//						and 	BI_DepartmentEmployee.EmployeeID = BI_Employee.IDKey
			//						and 	CommEvaluationGroup.CommEvaluationID = '"+pkValue+"'";

			strSql = @"
						select 	BI_Employee.FullName as [BI_Employee.FullName],
								BI_GroupPostion.PositionName as [BI_GroupPostion.PositionName],
								CommEvaluationGroup.personID as [CommEvaluationGroup.personID],
								CommEvaluationGroup.GroupPosition  as [CommEvaluationGroup.GroupPosition],
								'Edit' RowStatus,
								CommEvaluationGroup.IDKey as [CommEvaluationGroup.IDKey],
								CommEvaluationGroup.CommEvaluationID as [CommEvaluationGroup.CommEvaluationID]

							from	CommEvaluationGroup,
								BI_GroupPostion,
								BI_Employee 

							where 	CommEvaluationGroup.PersonID = BI_Employee.IDKey
							and     CommEvaluationGroup.GroupPosition = BI_GroupPostion.IDKey
							and 	CommEvaluationGroup.CommEvaluationID ='"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// �õ�ָ�������Ա��Ϣ
		/// </summary>
		/// <param name="UserID">GroupID</param>
		/// <returns></returns>
		public DataTable GetCommEvaluation_Group( string GroupID )
		{
			string strSql = "";
			strSql = @"	select
							BI_Employee.FullName,
							BI_GroupPostion.PositionName,
							BI_GroupPostion.IDKey as PositionID,
							BI_Employee.IDKey,
							BI_DepartmentEmployee.DepartmentID

						from TI_DefaultGroupMember,
							BI_Employee,
							BI_GroupPostion,
							BI_DepartmentEmployee

						where TI_DefaultGroupMember.IDKEY = '"+GroupID+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TI_DefaultGroupMember.PositionID = BI_GroupPostion.IDKey
						and TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
						";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		/// <summary>
		/// ɾ����������С���Ա
		/// </summary>
		/// <param name="strIDKey"></param>
		public void DelCommEvaluationGroupViewer(string strIDKey)
		{
			string strSql = "delete CommEvaluationGroup where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region ɸѡ�Ѿ����������Ĳ��Ա��

		/// <summary>
		/// ɸѡ�Ѿ����˼�����Ĳ��Ա��
		/// </summary>
		/// <returns></returns>
		public string GetTenderIDFilter ()
		{
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
			//			string SelectSql = @"SELECT TCStrategy.TenderID, ServiceRequistion.IDKey , ServiceRequistion.TenderState 
			//										FROM ServiceRequistion
			//										INNER JOIN TCStrategy ON ServiceRequistion.IDKey = TCStrategy.SRIDKey
			//										WHERE 	ServiceRequistion.TenderState = "+(int)TenderState.State_ITBCommOpen+" AND TCStrategy.TenderID NOT IN ( SELECT TenderID FROM CommEvaluation ) ";
			//			string strTenderIDs = string.Empty;
			//modify by wxc at 2007/3/14
			string SelectSql = @"SELECT TCStrategy.TenderID
													FROM TCStrategy 
													WHERE 	TCStrategy.status = "+(int)TenderState.State_ITBCommOpen+" AND TCStrategy.TenderID NOT IN ( SELECT TenderID FROM CommEvaluation ) ";
			string strTenderIDs = string.Empty;


			System.Data.DataTable dt_Temp = _da.GetDataTable ( SelectSql ) ;


			foreach ( DataRow dr in dt_Temp.Rows )
			{
				strBuilder.Append ( "'" );
				strBuilder.Append ( Convert.ToString ( dr["TenderID"] ) );
				strBuilder.Append ( "'," );
			}

			if (strBuilder.Length > 0 )
			{
				if ( strBuilder[strBuilder.Length-1] == ',' )
				{
					// �����һ��","ȥ��
					strTenderIDs = strBuilder.Remove( strBuilder.Length - 1 , 1 ).ToString();
				}
			}

			return strTenderIDs;
		}

		#endregion
		
		#region ͨ����������IDKey��������SR��״̬

		/// <summary>
		/// ͨ����������IDKey��������SR��״̬
		/// </summary>
		/// <param name="strTechEvaluateIDKey"></param>
		/// <returns>SR��״̬,�������-1,���ʾ���Ҵ���</returns>
		public int GetTenderStateByTechEvaluation ( string strTechEvaluateIDKey )
		{
			int iState = -1;

			string SelectSql = @"SELECT ServiceRequistion.TenderState FROM CommEvaluation , TCStrategy , ServiceRequistion
										WHERE TCStrategy.SRIDKey = ServiceRequistion.IDKey  AND TCStrategy.TenderID = CommEvaluation.TenderID 
										AND CommEvaluation.IDKey = '"+strTechEvaluateIDKey+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );

			if ( dt_Temp.Rows.Count > 0 )
			{
				iState = Convert.ToInt32 ( dt_Temp.Rows[0]["TenderState"] );
			}
			
			return iState ;
		}

		#endregion

		#region ���ն�Ӧ��SRID����SRIDKey�����µļ��������״̬

		/// <summary>
		/// ���ն�Ӧ��SRID����SRIDKey�����µļ��������״̬
		/// </summary>
		/// <param name="SRIDOrSRIDKey">SRID����SRIDKey</param>
		/// <param name="srState">SR״̬</param>
		/// <param name="iState">0Ϊʹ��SRID���и���,1Ϊʹ��SRIDKey���и���,2Ϊʹ�ñ����IDKey���и���</param>
		public void UpdateCommEvaluationState ( string SRIDOrSRIDKey , TenderState srState , int iState )
		{
			string UpdateSql = string.Empty ;
		
			switch ( iState )
			{
				case 0 :
				{
					UpdateSql = "UPDATE CommEvaluation SET CommEvaluation.State = "+(int)srState+@" WHERE CommEvaluation.IDKey in (SELECT CommEvaluation.IDKey FROM CommEvaluation , TCStrategy , ServiceRequistion
									WHERE TCStrategy.SRIDKey = ServiceRequistion.IDKey  AND TCStrategy.TenderID = CommEvaluation.TenderID 
									AND ServiceRequistion.SRID = '"+SRIDOrSRIDKey+"')";
					break;
				}
				case 1 :
				{
					UpdateSql = "UPDATE CommEvaluation SET CommEvaluation.State = "+(int)srState+@" WHERE CommEvaluation.IDKey in (SELECT CommEvaluation.IDKey FROM CommEvaluation , TCStrategy , ServiceRequistion
										WHERE TCStrategy.SRIDKey = ServiceRequistion.IDKey  AND TCStrategy.TenderID = CommEvaluation.TenderID 
										AND ServiceRequistion.IDKey = '"+SRIDOrSRIDKey+"')";
					break;
				}
				case 2 :
				{
					UpdateSql = @"UPDATE CommEvaluation SET CommEvaluation.State = "+(int)srState+" WHERE CommEvaluation.IDKey = '"+SRIDOrSRIDKey+"'";
					break;
				}
			}

			if ( UpdateSql.Length > 0 )
			{
				_da.ExecuteDMLSQL ( UpdateSql );
			}
		}

		#endregion

		#region ��ö�Ӧ�������Ѿ��������ı���Ĺ�Ӧ��


		public void GetVendor ( string strTenderID , DataTable dt  )
		{
			string SelectSql = @"SELECT BidOpenID , ITBOpenVendorList.VendorID , Vendor.VendorName FROM ITBOpenVendorList 
										INNER JOIN BidOpen ON BidOpen.IDKey = ITBOpenVendorList.BidOpenID
										INNER JOIN ITBDocument ON ITBDocument.ITBIDKey = BidOpen.ITBIDKey
										INNER JOIN TCStrategy ON TCStrategy.TenderID = ITBDocument.TenderID
										INNER JOIN Vendor ON ITBOpenVendorList.VendorID = Vendor.IDKey
										WHERE Accede = 1 AND BidOpen.OpenTypeID IN (2,3) AND TCStrategy.TenderID = '"+strTenderID+"'";
			
			DataTable dt_Temp = _da.GetDataTable( SelectSql );

			foreach ( DataRow dr in dt_Temp.Rows )
			{
				DataRow dataRow = dt.NewRow();
				dataRow["IDKey"] = System.Guid.NewGuid().ToString();
				dataRow["VendorID"] = dr["VendorID"] ;
				dataRow["CommResult__VendorID"] =  dr["VendorName"] ;
				dataRow["RowStatus"] = "New";
				dt.Rows.Add(dataRow);
			}

			dt.AcceptChanges();
		}

		#endregion

		#region ͨ��CommEvaluation ��IDkey ���TenderID
		/// <summary>
		/// ͨ��TechEvaluation ��IDkey ���TenderID
		/// </summary>
		/// <param name="ITBIDKey"></param>
		/// <returns></returns>
		public string GetTenderID (string sIDkey)
		{
			string SelectSql = " SELECT TenderID from CommEvaluation where IDKey = '"+sIDkey+"'";
			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			if (dt_Temp.Rows.Count >0)
			{
				return dt_Temp.Rows[0][0].ToString();
			}
			else
				return "";
		}
		#endregion

		#region �鿴ĳ����Ӧ����ָ���а��˵��������Ƿ���ڻ��ߴ��ڲ���ǩ����ͬ

		public string IsVendorFinishContract ( string sVendorID , string UserID )
		{
			string VendorName = string.Empty;
//			string SelectSql = @"SELECT Vendor.VendorName FROM CommResult 
//								INNER JOIN CommEvaluation ON CommResult.Com_ID = CommEvaluation.IDKey
//								INNER JOIN TCStrategy ON TCStrategy.TenderID = CommEvaluation.TenderID
//								INNER JOIN Vendor ON CommResult.VendorID = Vendor.IDKey
//								WHERE TCStrategy.Status <> "+(int)TenderState.State_ContractSinged+"  AND TCStrategy.CreateBy = '"+UserID+"' AND CommResult.VendorID = '"+VendorID+"'";

			string sSql = "SELECT Vendor.VendorName FROM CommResult,CommEvaluation,TCStrategy,Vendor "+
				" WHERE  TCStrategy.TenderID  =CommEvaluation.TenderID AND CommEvaluation.IDKey=CommResult.Com_ID  "+
				" AND NOT EXISTS(SELECT 1 FROM Contract WHERE Contract.VendorID =CommResult.VendorID  AND Contract.TenderNumber = TCStrategy.TenderID) "+
				" AND  Vendor.IDKey = CommResult.VendorID AND TCStrategy.CreateBy = '"+UserID+"'  AND TCStrategy.status = "+(int)TenderState.State_CommApproved+" AND CommResult.Passed = 1 AND CommResult.VendorID ='"+sVendorID+"' ";

			DataTable dt = _da.GetDataTable ( sSql );

			if ( dt.Rows.Count > 0 && Convert.ToString( dt.Rows[0][0] ).Length > 0 )
			{ VendorName = Convert.ToString( dt.Rows[0][0] ); }
			
			return VendorName;
		}

		#endregion
	}
}
