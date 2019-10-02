using System;
using  Cnwit.Utility ;
using Common;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEContractSign 的摘要说明。
	/// </summary>
	public class DAEContractSign:DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		CEntityUitlity pEntityUitlity = new CEntityUitlity();
		string strSql=string.Empty;
		public DAEContractSign()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetSRTCInfo(string sTenderID,string sCurrentUser , bool bolValue)
		{
			if(bolValue)
			{

				strSql = "SELECT TCStrategy.ProTypeName,TCStrategy.ProTypeName,TCStrategy.PlanAmount,TCStrategy.Currency,TCStrategy.TenderID,TCStrategy.ProjectName "+
					" FROM  TCStrategy "+
					" WHERE TCStrategy.TenderID = '" + sTenderID + "'";

				DataTable dt = _da.GetDataTable(strSql);
				if(dt.Rows.Count>0)
				{
					return dt;
				}
				
					
				
			}
			return null;

		}

		public DataTable GetSRTCInfo(string sVendorID,string sCurrentUser)
		{
			strSql = "SELECT Vendor.VendorNo,Vendor.VendorName,Vendor.Address,Vendor.Telphone,Vendor.Fax,Vendor.Email FROM  Vendor "+
					 "WHERE Vendor.IDKey = '"+sVendorID+"'";

//			strSql = "SELECT TCStrategy.ProTypeName,TCStrategy.ProTypeName,TCStrategy.PlanAmount,TCStrategy.Currency,TCStrategy.TenderID,TCStrategy.ProjectName, "+
//				"Vendor.VendorNo,Vendor.VendorName,Vendor.Address,Vendor.Telphone,Vendor.Fax,Vendor.Email "+
//				"FROM  CommResult,CommEvaluation,TCStrategy,Vendor "+
//				"WHERE  TCStrategy.TenderID  =CommEvaluation.TenderID "+
//				"AND CommEvaluation.IDKey=CommResult.Com_ID "+
//				"AND Vendor.IDKey = CommResult.VendorID "+
//				"AND Vendor.IDKey = '"+sVendorID+"' AND "+
//				"TCStrategy.status = "+(int)TenderState.State_CommApproved+" AND CommResult.Passed = 1 AND  TCStrategy.CreateBy = '"+sCurrentUser+"'";

			DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			{
				return dt;
			}
			else
			{
				return null;
			}

		}
		public string GetSRType(string sTenderID)
		{

			strSql = "SELECT ServiceRequistion.PlanType FROM TCStrategySR,ServiceRequistion WHERE  TCStrategySR.SRID = ServiceRequistion.IDkey AND TCStrategySR.TenderID = '"+sTenderID+"'";

			DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}

		}

		public string UpdateFlowContractState(string sIDKey,string sTenderID, TenderState enState)
		{
			int nState = (int)enState ;
			string sError = string.Empty ;
			string UpdateSql  = "Update Contract set State = "+nState+" where IDKey = '"+sIDKey+"'";
			sError += _da.ExecuteDMLSQL(UpdateSql);
			sError += pEntityUitlity.UpdateITBDocumentState(sTenderID,enState);
			return sError ;

		}

		public int GetFlowContractState(string sIDKey,string nState)
		{
			string sql  = "SELECT 1 FROM Contract WHERE charindex(','+convert(varchar,Contract.State)+',',',"+nState+",')>0 AND IDKey ='"+sIDKey+"'";
			return _da.GetDataTable(sql).Rows.Count;
		}

		public DataTable GetVendorInfo(string sCid)
		{
			strSql="SELECT Vendor.* , VendorCountry.* FROM Vendor,Contract,VendorCountry WHERE Contract.VendorID = Vendor.IDKey and Contract.IDkey='"+sCid+"' AND Vendor.IDKey *= VendorCountry.VendorID AND VendorCountry.State = 1";
			DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			{
				return dt;
			}
			else
			{
				return null;
			}
		}



		public void UpdateContractState(string sIDKey , TenderState state)
		{
			int contractState = (int)state ; 
			strSql="UPDATE Contract SET State = " + contractState + " WHERE Contract.IDkey='"+sIDKey+"'";
			string sErrorMsg = _da.ExecuteDMLSQL(strSql);
			
		}


		/// <summary>
		/// 审批进程
		/// </summary>
		/// <param name="strContractID"></param>
		/// <returns></returns>
		public DataTable GetApprovedProcess ( string strContractID )
		{
			string SelectSql = @"SELECT DepartmentName , PositionName , FullName ,State, Contents,ApprovedDate From Approved 
										INNER JOIN BI_Employee ON Approved.ApprovedBy = BI_Employee.IDKey
										INNER JOIN BI_DepartmentEmployee ON BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID 
										INNER JOIN BI_Department ON BI_DepartmentEmployee.DepartmentID = BI_Department.IDKey
										INNER JOIN BI_PositionType ON BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
										WHERE  ObjectiveType = 7 AND ObjectiveID = '"+strContractID+"' AND State = 1 ORDER BY CurrApproveLevel";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );

			return dt_Temp ; 
		}

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
							ContractView.ViewerID as [ContractView.ViewerID],
							'Edit' RowStatus,
							ContractView.IDKey as [ContractView.IDKey],
							ContractView.ContractID as [ContractView.ContractID]
							  
						from (((ContractView left outer join BI_Employee on ContractView.ViewerID = BI_Employee.IDKey)
						left join  BI_DepartmentEmployee on BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID)
						left join  BI_PositionType on  BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey)
						where ContractView.ContractID ='"+pkValue+"'";

			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}


		public DataTable GetSRInfo(string pkValue )
		{
			string strSql = "";
			strSql = @"select IDKey,ContractID,SRID,ApplyDate,ApplyDepartment,PlanAmount,
						PlanCurrency,PlanAmountPercent,ContractAmountPercent
						from  ContractRequistion
						where ContractRequistion.ContractID ='"+pkValue+"'";

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


		public DataTable GetSR(string strSRID)
		{
			string strSql = "";
			strSql = @"select  
							SRID,AppDate,DepartmentID,PlanAmount,Currency
						from ServiceRequistion
						where ServiceRequistion.SRID = '"+strSRID+"'";
			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;

		}
		/// <summary>
		/// 删除查看人
		/// </summary>
		/// <param name="strIDKey"></param>
		public void DelViewer(string strIDKey)
		{
			string strSql = "delete ContractView where IDKey ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}
		#endregion

		#region 通过Contract 的IDkey 获得SRID <NoFlow>

		public string GetSRIDNoFlow (string contractIDKey)
		{
			System.Text.StringBuilder strBuilder =  new System.Text.StringBuilder();
			strSql = @"
				SELECT ServiceRequistion.IDKey 
				FROM 
					ContractRequistion inner join ServiceRequistion
					on ContractRequistion.SRID = ServiceRequistion.SRID
				WHERE ContractRequistion.ContractID ='"+contractIDKey+"' ";
			System.Data.DataTable dt = _da.GetDataTable(strSql);

			string strSrIDkey = string.Empty;

			for (int i=0;i< dt.Rows.Count;i++ )
			{
				strBuilder.Append ( "'" );
				strBuilder.Append ( Convert.ToString(dt.Rows[i][0]) );
				strBuilder.Append ( "'," );
			}
			if ( strBuilder.Length > 0 ) 
			{
				if ( strBuilder[strBuilder.Length-1] == ',' )
				{
					strSrIDkey = strBuilder.ToString().TrimEnd( ',' );
				}
			}

			return strSrIDkey ;
		}

		#endregion

		#region 通过Contract 的ContractID 获得TenderNumber

		public string GetTenderID (string sIDkey)
		{
			string SelectSql = " SELECT TenderNumber from Contract where IDKey = '"+sIDkey+"'";
			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			if (dt_Temp.Rows.Count >0)
			{
				return dt_Temp.Rows[0][0].ToString();
			}
			else
				return "";
		}
		#endregion

		public bool  JudgeIsLastVendor(string sCurrentUser)
		{
			string sSql = "SELECT CommResult.VendorID FROM CommResult,CommEvaluation,TCStrategy "+
				" WHERE  TCStrategy.TenderID  =CommEvaluation.TenderID AND CommEvaluation.IDKey=CommResult.Com_ID  "+
				" AND NOT EXISTS(SELECT 1 FROM Contract WHERE Contract.VendorID =CommResult.VendorID  AND Contract.TenderNumber = TCStrategy.TenderID) "+
				" AND TCStrategy.CreateBy = '"+sCurrentUser+"'  AND TCStrategy.status = "+(int)TenderState.State_ContractApproved+" AND CommResult.Passed = 1 ";
			int nCount = _da.GetDataTable(sSql).Rows.Count;
			if(nCount >0)
			{
				return false;
			}
			else
			{
				return true;
			}

		}


		public  void GetSRList(DataTable dtTemp,string sTenderID,string yesRes,string noRes)
		{
			string strSQL = "SELECT ServiceRequistion.SRID,ServiceRequistion.IDKey as SRIDKey, "+
				" TCStrategySR.ApplyDate,TCStrategySR.ApplyDepartment, BI_Department.DepartmentName,"+
				" TCStrategySR.PlanAmount,TCStrategySR.PlanCurrency,TCStrategySR.PlanAmountPercent, "+
				" TCStrategySR.ContractAmountPercent,TCStrategySR.IsFinished "+
				" from  ServiceRequistion,TCStrategySR,BI_Department "+
				 " where  TCStrategySR.ApplyDepartment = BI_Department.IDKey "+
				" AND ServiceRequistion.IDKey = TCStrategySR.SRID AND TenderID = '"+sTenderID+"'";
			DataTable dt = _da.GetDataTable(strSQL);
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
				DataRow dr = dtTemp.NewRow();
				dr["IDKey"] = System.Guid.NewGuid().ToString();
				dr["SRIDKey"] = dt.Rows[i]["SRIDKey"].ToString();
				dr["SRID"] = dt.Rows[i][0].ToString();
				dr["ApplyDate"] = dt.Rows[i]["ApplyDate"].ToString();
				dr["ApplyDepartment"] = dt.Rows[i]["ApplyDepartment"].ToString();
				dr["ContractRequistion__ApplyDepartment"] = dt.Rows[i]["DepartmentName"].ToString();

				dr["PlanAmount"] = dt.Rows[i]["PlanAmount"].ToString();
				dr["PlanCurrency"] = dt.Rows[i]["PlanCurrency"].ToString();
				dr["ContractRequistion__PlanCurrency"] = dt.Rows[i]["PlanCurrency"].ToString();
				if(dt.Rows[i]["PlanAmountPercent"] != System.DBNull.Value)
				{
					dr["PlanAmountPercent"] = float.Parse(dt.Rows[i]["PlanAmountPercent"].ToString());
				}
				if(dt.Rows[i]["ContractAmountPercent"] != System.DBNull.Value)
				{
					dr["ContractAmountPercent"] = float.Parse(dt.Rows[i]["ContractAmountPercent"].ToString());
				}
				dr["IsFinished"] = dt.Rows[i]["IsFinished"];

				if(Convert.ToString(dt.Rows[i]["IsFinished"]).ToUpper() == "TRUE" || Convert.ToInt32(dt.Rows[i]["IsFinished"]).ToString().ToUpper() == "1")
				{
					dr["ContractRequistion__IsFinished"] = yesRes ;
				}
				else
				{
					dr["ContractRequistion__IsFinished"] = noRes ;
				}
				dr["RowStatus"] = "New";
				dtTemp.Rows.Add(dr);
			}

		}


	
		/// <summary>
		/// 直接签定合同中的更新ＳＲ子表
		/// </summary>
		/// <param name="dataTable"></param>
		public void UpdateDataTable_SRList ( DataTable dataTable )
		{
			string strSRIDkey = string.Empty;

			DataTable dt_Temp;

			foreach ( DataRow dr in dataTable.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					strSRIDkey = Convert.ToString(dr["SRIDKey"]);
		
					string strSQL = "SELECT ServiceRequistion.*, BI_Department.DepartmentName"+
						" from  ServiceRequistion,BI_Department "+
						" where  ServiceRequistion.DepartmentID = BI_Department.IDKey "+
						" AND  ServiceRequistion.IDkey = '"+strSRIDkey+"'" ;

					dt_Temp = _da.GetDataTable( strSQL );

					if ( dt_Temp.Rows.Count > 0 )
					{
						dr["ApplyDate"] = dt_Temp.Rows[0]["AppDate"];
						dr["ApplyDepartment"] =  dt_Temp.Rows[0]["DepartmentID"] ;
						dr["ContractRequistion__ApplyDepartment"] = dt_Temp.Rows[0]["DepartmentName"].ToString();
						dr["PlanAmount"] = dt_Temp.Rows[0]["PlanAmount"] ;
						dr["PlanCurrency"] = dt_Temp.Rows[0]["Currency"] ;
						dr["ContractRequistion__PlanCurrency"] = dt_Temp.Rows[0]["Currency"].ToString();
						dr["SRID"] = dt_Temp.Rows[0]["SRID"];
					}
				}
			}
		}
	}
}
