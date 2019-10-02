using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAETenderEvaluation 的摘要说明。
	/// </summary>
	public class DAETenderEvaluation:DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;

		public DAETenderEvaluation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
		public DataTable CheckState(String strTenderID )
		{
			string[] sParams = {"tableName","tablePK","tablePKValue","correlationField","ifCorrelation","correlationTable","correlationTableField","correlationFieldTable"} ;
			object[] objParamValues = {"TechEvaluation","IDKey",strTenderID,"TenderID","0","","","" } ; 
			SqlDbType[] paramTypes = { SqlDbType.VarChar , SqlDbType.VarChar , SqlDbType.VarChar, SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar,SqlDbType.VarChar } ;
			DataTable dt = BaseDataAccess.ExecuteSPQueryDataTable("sp_ControlState",sParams,objParamValues,paramTypes );
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select TCStrategy.Status as State From TechEvaluation Inner Join TCStrategy On TechEvaluation.TenderID = TCStrategy.TenderID  Where TechEvaluation.IDKey='" + strPKValue + "'" ;
			DataTable dt = _da.GetDataTable(strSql) ; 
			return dt ; 
		}

		//****************************************************


		#region 查看人相关方法
		/// <summary>
		/// 得到所有查看人信息
		/// </summary>
		/// <returns></returns>
		public DataTable GetSeePerson( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName],
							BI_PositionType.PositionName as [BI_PositionType.PositionName],
							TechEvaluationView.ViewerID as [TechEvaluationView.ViewerID],
							'Edit' RowStatus,
							TechEvaluationView.IDKey as [TechEvaluationView.IDKey],
							TechEvaluationView.TechEvaluationID as [TechEvaluationView.TechEvaluationID]
							  
						from (((TechEvaluationView left outer join BI_Employee on TechEvaluationView.ViewerID = BI_Employee.IDKey)
						left join  BI_DepartmentEmployee on BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID)
						left join  BI_PositionType on  BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey)
						where TechEvaluationView.TechEvaluationID ='"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}

		/// <summary>
		/// 根据BI_DepartmentEmployee.IDKey 得到BI_Employee.IDKey
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
		/// 得到指定部门的人员信息
		/// </summary>
		/// <param name="DepartmentID">部门ID</param>
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
		/// 得到单个用户信息
		/// </summary>
		/// <param name="UserID">用户ID(员工表中的IDKey,BI_Employee)</param>
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
		/// 得到指定组的人员信息
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

		public void DelViewer(string strIDKey)
		{
			string strSql = "delete TechEvaluationView where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region 评标小组相关方法
		/// <summary>
		/// 初始化评标小组列表，得到相应小组类型下的全部人员信息
		/// 把 TechEvaluationGroup.IDKey 设置成null并且为 nvarchar（64）
		/// 暂时作废
		/// </summary>
		/// <returns></returns>
		public DataTable InitEvaluation(int groupType)
		{
			string strSql = "";
			strSql = @"select 	
							BI_Employee.FullName as [BI_Employee.FullName] ,
							BI_GroupPostion.PositionName as [BI_GroupPostion.PositionName],
							TI_DefaultGroupMember.GroupUserID as [TechEvaluationGroup.personID],
							TI_DefaultGroupMember.PositionID  as [TechEvaluationGroup.GroupPosition],
							'New' RowStatus,
							cast(null as varchar(64))  [TechEvaluationGroup.IDKey],
							'' [TechEvaluationGroup.TechEvaluationID]

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

		//暂时作废
		public bool IsExistEvaluationGroup(string pkValue)
		{
			string strSql = "";	
			strSql = @" select 1 from TechEvaluationGroup where TechEvaluationID ='"+pkValue+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			if (dt.Rows.Count >0) return true;
			else return false;
		}
		/// <summary>
		/// 通过技术评标表主键得到评标小组成员
		/// </summary>
		/// <param name="pkValue"></param>
		/// <returns></returns>
		public DataTable GetEvaluationInfo(string pkValue)
		{
			string strSql = "";
			//			strSql = @"
			//						select 	BI_Employee.FullName as [BI_Employee.FullName],
			//							BI_GroupPostion.PositionName as [BI_GroupPostion.PositionName],
			//								TI_DefaultGroupMember.GroupUserID as [TechEvaluationGroup.personID],
			//								TI_DefaultGroupMember.PositionID  as [TechEvaluationGroup.GroupPosition],
			//								'Edit' RowStatus,
			//								TechEvaluationGroup.IDKey as [TechEvaluationGroup.IDKey],
			//								TechEvaluationGroup.TechEvaluationID as [TechEvaluationGroup.TechEvaluationID]
			//
			//						from	TechEvaluationGroup,
			//							TI_DefaultGroupMember,
			//							BI_GroupPostion,
			//							BI_DepartmentEmployee,
			//							BI_Employee 
			//
			//						where 	TechEvaluationGroup.PersonID = BI_DepartmentEmployee.IDKey
			//						and 	TechEvaluationGroup.GroupPosition = TI_DefaultGroupMember.PositionID
			//						and 	TI_DefaultGroupMember.PositionID = BI_GroupPostion.IDKey
			//						and 	TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
			//						and 	BI_DepartmentEmployee.EmployeeID = BI_Employee.IDKey
			//						and 	TechEvaluationGroup.TechEvaluationID = '"+pkValue+"'";

			strSql = @"
						select 	BI_Employee.FullName as [BI_Employee.FullName],
								BI_GroupPostion.PositionName as [BI_GroupPostion.PositionName],
								TechEvaluationGroup.personID as [TechEvaluationGroup.personID],
								TechEvaluationGroup.GroupPosition  as [TechEvaluationGroup.GroupPosition],
								'Edit' RowStatus,
								TechEvaluationGroup.IDKey as [TechEvaluationGroup.IDKey],
								TechEvaluationGroup.TechEvaluationID as [TechEvaluationGroup.TechEvaluationID]

							from	TechEvaluationGroup,
								BI_GroupPostion,
								BI_Employee 

							where 	TechEvaluationGroup.PersonID = BI_Employee.IDKey
							and     TechEvaluationGroup.GroupPosition = BI_GroupPostion.IDKey
							and 	TechEvaluationGroup.TechEvaluationID ='"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}
		
		/// <summary>
		/// 得到指定组的人员信息
		/// </summary>
		/// <param name="UserID">GroupID</param>
		/// <returns></returns>
		public DataTable GetEvaluation_Group( string GroupID )
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
		public void DelEvaluationGroupViewer(string strIDKey)
		{
			string strSql = "delete TechEvaluationGroup where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region 筛选已经开了商务标的策略编号

		/// <summary>
		/// 筛选已经开了技术标的策略编号
		/// </summary>
		/// <returns></returns>
		public string GetTenderIDFilter ()
		{
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
			//			string SelectSql = @"SELECT TCStrategy.TenderID, ServiceRequistion.IDKey , ServiceRequistion.TenderState 
			//										FROM ServiceRequistion
			//										INNER JOIN TCStrategy ON ServiceRequistion.IDKey = TCStrategy.SRIDKey
			//										WHERE 	ServiceRequistion.TenderState = "+(int)TenderState.State_ITBTechOpen+" AND TCStrategy.TenderID NOT IN ( SELECT TenderID FROM TechEvaluation ) ";
			//			string strTenderIDs = string.Empty;

			string SelectSql = @"SELECT TCStrategy.TenderID  FROM TCStrategy 
										WHERE 	TCStrategy.status = " + (int)TenderState.State_ITBTechOpen + " AND TCStrategy.TenderID NOT IN ( SELECT TenderID FROM TechEvaluation )  AND TCStrategy.TenderID  IN ( SELECT ITBDocument.TenderID  FROM ITBDocument Inner Join  BidOpen On ITBDocument.ITBIDKey =BidOpen.ITBIDKey  Inner Join ITBOpenVendorList On BidOpen.IDKey = ITBOpenVendorList.BidOpenID Where ITBOpenVendorList.Accede=1) ";
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
					// 将最后一个","去掉
					strTenderIDs = strBuilder.Remove( strBuilder.Length - 1 , 1 ).ToString();
				}
			}

			return strTenderIDs;
		}


		#endregion
		
		#region 通过技术评标IDKey来获得相关SR的状态

		/// <summary>
		/// 通过技术评标IDKey来获得相关SR的状态
		/// </summary>
		/// <param name="strTechEvaluateIDKey"></param>
		/// <returns>SR的状态,如果返回-1,则表示查找错误</returns>
		public int GetTenderStateByTechEvaluation ( string strTechEvaluateIDKey )
		{
			int iState = -1;

			string SelectSql = @"SELECT ServiceRequistion.TenderState FROM TechEvaluation , TCStrategy , ServiceRequistion
										WHERE TCStrategy.SRIDKey = ServiceRequistion.IDKey  AND TCStrategy.TenderID = TechEvaluation.TenderID 
										AND TechEvaluation.IDKey = '"+strTechEvaluateIDKey+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );

			if ( dt_Temp.Rows.Count > 0 )
			{
				iState = Convert.ToInt32 ( dt_Temp.Rows[0]["TenderState"] );
			}
			
			return iState ;
		}

		#endregion

		#region 按照对应的SRID或者SRIDKey来更新的技术评标的状态

		/// <summary>
		/// 按照对应的SRID或者SRIDKey来更新的技术评标的状态
		/// </summary>
		/// <param name="SRIDOrSRIDKey">SRID或者SRIDKey</param>
		/// <param name="srState">SR状态</param>
		/// <param name="iState">0为使用SRID进行更新,1为使用SRIDKey进行更新,2为使用本身的IDKey进行更新</param>
		public void UpdateTechEvaluationState ( string SRIDOrSRIDKey , TenderState srState , int iState )
		{
			string UpdateSql = string.Empty ;
		
			switch ( iState )
			{
				case 0 :
				{
					UpdateSql = "UPDATE TechEvaluation SET TechEvaluation.State = "+(int)srState+@" WHERE TechEvaluation.IDKey in (SELECT TechEvaluation.IDKey FROM TechEvaluation , TCStrategy , ServiceRequistion
									WHERE TCStrategy.SRIDKey = ServiceRequistion.IDKey  AND TCStrategy.TenderID = TechEvaluation.TenderID 
									AND ServiceRequistion.SRID = '"+SRIDOrSRIDKey+"')";
					break;
				}
				case 1 :
				{
					UpdateSql = "UPDATE TechEvaluation SET TechEvaluation.State = "+(int)srState+@" WHERE TechEvaluation.IDKey in (SELECT TechEvaluation.IDKey FROM TechEvaluation , TCStrategy , ServiceRequistion
										WHERE TCStrategy.SRIDKey = ServiceRequistion.IDKey  AND TCStrategy.TenderID = TechEvaluation.TenderID 
										AND ServiceRequistion.IDKey = '"+SRIDOrSRIDKey+"')";
					break;
				}
				case 2 :
				{
					UpdateSql = @"UPDATE TechEvaluation SET TechEvaluation.State = "+(int)srState+" WHERE TechEvaluation.IDKey = '"+SRIDOrSRIDKey+"'";
					break;
				}
			}

			if ( UpdateSql.Length > 0 )
			{
				_da.ExecuteDMLSQL ( UpdateSql );
			}
		}

		#endregion

		#region 获得对应策略中已经开商务标的标书的供应商


		public void GetVendor ( string strTenderID , DataTable dt  )
		{
			string SelectSql = @"SELECT BidOpenID , ITBOpenVendorList.VendorID , Vendor.VendorName FROM ITBOpenVendorList 
										INNER JOIN BidOpen ON BidOpen.IDKey = ITBOpenVendorList.BidOpenID
										INNER JOIN ITBDocument ON ITBDocument.ITBIDKey = BidOpen.ITBIDKey
										INNER JOIN TCStrategy ON TCStrategy.TenderID = ITBDocument.TenderID
										INNER JOIN Vendor ON ITBOpenVendorList.VendorID = Vendor.IDKey
										WHERE Accede = 1 AND BidOpen.OpenTypeID = 1 AND TCStrategy.TenderID = '"+strTenderID+"'";
			
			DataTable dt_Temp = _da.GetDataTable( SelectSql );

			foreach ( DataRow dr in dt_Temp.Rows )
			{
				DataRow dataRow = dt.NewRow();
				dataRow["IDKey"] = System.Guid.NewGuid().ToString();
				dataRow["VendorID"] = dr["VendorID"] ;
				dataRow["TechResult__VendorID"] = dr["VendorName"] ;
				dataRow["RowStatus"] = "New";
				dt.Rows.Add(dataRow);
			}

			dt.AcceptChanges();
		}

		#endregion

		#region 通过TechEvaluation 的IDkey 获得TenderID
		/// <summary>
		/// 通过TechEvaluation 的IDkey 获得TenderID
		/// </summary>
		/// <param name="ITBIDKey"></param>
		/// <returns></returns>
		public string GetTenderID (string sIDkey)
		{
			string SelectSql = " SELECT TenderID from TechEvaluation where IDKey = '"+sIDkey+"'";
			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			if (dt_Temp.Rows.Count >0)
			{
				return dt_Temp.Rows[0][0].ToString();
			}
			else
				return "";
		}
		#endregion

        /// <summary>
        /// 得到指定组的人员信息
        /// </summary>
        /// <param name="UserID">GroupID</param>
        /// <returns></returns>
        public DataTable GetTechEvaluation_Group(string GroupID)
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

						where TI_DefaultGroupMember.IDKEY = '" + GroupID + "'";
            strSql += @"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TI_DefaultGroupMember.PositionID = BI_GroupPostion.IDKey
						and TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
						";

            System.Data.DataTable dt = _da.GetDataTable(strSql);
            return dt;
        }

        public DataTable GetTechEvaluationInfo(string pkValue)
        {
            string strSql = "";
            strSql = @" select 	BI_Employee.FullName as [BI_Employee.FullName],
                        BI_GroupPostion.PositionName as [BI_GroupPostion.PositionName],
                        TechEvaluationGroup.personID as [TechEvaluationGroup.personID],
                        TechEvaluationGroup.GroupPosition  as [TechEvaluationGroup.GroupPosition],
                        'Edit' RowStatus,
                        TechEvaluationGroup.IDKey as [TechEvaluationGroup.IDKey],
                        TechEvaluationGroup.TechEvaluationID as [TechEvaluationGroup.TechEvaluationID]
                        from	TechEvaluationGroup,
                        BI_GroupPostion,
                        BI_Employee 
                        where 	TechEvaluationGroup.PersonID = BI_Employee.IDKey
                        and     TechEvaluationGroup.GroupPosition = BI_GroupPostion.IDKey
						and 	TechEvaluationGroup.TechEvaluationID ='" + pkValue + "'";

            DataTable dt = _da.GetDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 删除技术评标小组成员
        /// </summary>
        /// <param name="strIDKey"></param>
        public void DelTechEvaluationGroupViewer(string strIDKey)
        {
            string strSql = "delete TechEvaluationGroup where IDKey ='" + strIDKey + "'";
            _da.GetDataTable(strSql);
        }
	}
}
