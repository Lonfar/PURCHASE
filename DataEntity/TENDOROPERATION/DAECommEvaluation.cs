using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAECommEvaluation 的摘要说明。
	/// </summary>
	public class DAECommEvaluation : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;

		public DAECommEvaluation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
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
			//商务评标有其特殊性，这里判断其对应的合同里是否有记录，有记录则不允许删除
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

		/// <summary>
		/// 删除查看人
		/// </summary>
		/// <param name="strIDKey"></param>
		public void DelViewer(string strIDKey)
		{
			string strSql = "delete CommEvaluationView where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region 评标小组相关方法
		/// <summary>
		/// 初始化评标小组列表，得到相应小组类型下的全部人员信息
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
		/// 判断是否已存在评标小组成员
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
		/// 通过商务评标表主键得到评标小组成员
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
		/// 得到指定组的人员信息
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
		/// 删除商务评标小组成员
		/// </summary>
		/// <param name="strIDKey"></param>
		public void DelCommEvaluationGroupViewer(string strIDKey)
		{
			string strSql = "delete CommEvaluationGroup where IDKey ='"+strIDKey+"'";
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

		#region 按照对应的SRID或者SRIDKey来更新的技术评标的状态

		/// <summary>
		/// 按照对应的SRID或者SRIDKey来更新的技术评标的状态
		/// </summary>
		/// <param name="SRIDOrSRIDKey">SRID或者SRIDKey</param>
		/// <param name="srState">SR状态</param>
		/// <param name="iState">0为使用SRID进行更新,1为使用SRIDKey进行更新,2为使用本身的IDKey进行更新</param>
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

		#region 获得对应策略中已经开商务标的标书的供应商


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

		#region 通过CommEvaluation 的IDkey 获得TenderID
		/// <summary>
		/// 通过TechEvaluation 的IDkey 获得TenderID
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

		#region 查看某个供应商在指定承办人的流程中是否存在或者存在并易签订合同

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
