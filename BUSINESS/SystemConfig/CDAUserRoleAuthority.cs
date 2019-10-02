/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Module Name: BusyAccess.SystemConfig.CDAUserRoleAuthority
-- Date Generated: 2005.6.6
-- Version List
--  Version 1.0 2005.7.1
Version 1.1 2005-7-12 add GetUserName method
Version 1.1 2005.7.15 Dou Zhi-cheng add Select...Paged functions for user and role
Version 1.2 2005.7.28 Dou Zhi-cheng add function SelectUserAuthoritiesAndModulesAll, SelectAuthoritiesAllByUserID
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data;
using System.Data.SqlClient;
using Cnwit.Utility ;

namespace Business.SystemConfig
{
	/// <summary>
	/// Summary description for CDAUserRoleAuthority.
	/// </summary>
	public class CDAUserRoleAuthority 
	{
		/// <summary>
		/// constructor
		/// </summary>
		public CDAUserRoleAuthority()
		{
		}
		#region User
		#region SelectUserInfosAllIDAndName Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectUserInfosAllIDAndName 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectUserInfosAllIDAndName  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectUserInfosAllIDAndName]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectUserInfosAllIDAndName]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectUserInfosAllIDAndName]	
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT
		///	[UserID],
		///	[UserName]
		///FROM
		///	[dbo].[UserInfo]
		///order by 
		///	UserID
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString UserID, SqlString UserName,
		/// </remarks>
		public SqlDataReader SelectUserInfosAllIDAndName()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectUserInfosAllIDAndName",sParams,objParamValues,paramTypes) ; 
	
		}


		#endregion

		public SqlDataReader SelectUserInfosAllIDAndNameByDepartmentID(string depID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"DepartmentID"} ;
			object[] objParamValues = {depID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			if(depID==null)
			{objParamValues[0]=DBNull.Value;}
						
			return pDataAccess.ExecuteSPQueryReader("spSelectUserInfosAllIDAndNameByDepartmentID",sParams,objParamValues,paramTypes) ; 
	
		}



		public SqlDataReader SelectUserInfosAllIDAndNameByDepartmentIDNoExistsRole(string depID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 
			string strSql=string.Empty;
			if(depID==null||depID.Trim().Length==0)
			{
				//全部用户
				strSql="SELECT	DISTINCT BI_Employee.[IDKey],BI_Employee.[FullName]+'('+ BI_Department.DepartmentName+')' AS FullName FROM [dbo].[BI_Employee],BI_DepartmentEmployee,BI_Department "+
						" where  BI_Employee.CanLogin=1 AND BI_DepartmentEmployee.DepartmentID =BI_Department.IDKey AND "+
						" BI_Employee.IDKey=BI_DepartmentEmployee.EmployeeID AND  NOT EXISTS(SELECT DISTINCT UserRole.UserID FROM UserRole WHERE UserID =BI_Employee.IDKey ) "+
						" order by BI_Employee.[IDKey] ";
			}
			else
			{

					//某个部门下的用户		
				strSql="SELECT	DISTINCT BI_Employee.[IDKey],BI_Employee.[FullName]+'('+ BI_Department.DepartmentName+')' AS FullName FROM [dbo].[BI_Employee],BI_DepartmentEmployee,BI_Department "+
					" where  BI_Employee.CanLogin=1 AND BI_DepartmentEmployee.DepartmentID =BI_Department.IDKey AND "+
					" BI_Employee.IDKey=BI_DepartmentEmployee.EmployeeID AND  NOT EXISTS(SELECT DISTINCT UserRole.UserID FROM UserRole WHERE UserID =BI_Employee.IDKey ) AND  BI_DepartmentEmployee.DepartmentID = '"+depID+"'"+
					" order by BI_Employee.[IDKey] ";
					
			}	
						
			return pDataAccess.GetDataReader(strSql);
	
		}
		#region SelectUserInfosDynamic Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectUserInfosDynamic 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="keyword">变量 keyword: 用于设置参数 '@Keyword' 给存储过程spSelectUserInfosDynamic </param>	
		/// <returns>包含记录集的数据表</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectUserInfosDynamic  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectUserInfosDynamic]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectUserInfosDynamic]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectUserInfosDynamic]
		///	@Keyword nvarchar(50)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT
		///	[UserID],
		///	[UserName],
		///	[UserDescription],
		///	[Passwd],
		///	[CanLogin],
		///	[LastLoginTime],
		///	[LastLoginIP],
		///	[RelativeEmployeeID],
		///	[Status]
		///FROM
		///	[dbo].[UserInfo]
		///WHERE
		///	UserID like '%'+@Keyword+'%'
		///	or UserName like '%'+@Keyword+'%'
		///	or UserDescription like '%'+@Keyword+'%'
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString UserID, SqlString UserName, SqlString UserDescription, SqlString Passwd, SqlBoolean CanLogin, SqlDateTime LastLoginTime, SqlString LastLoginIP, SqlString RelativeEmployeeID, SqlByte Status,
		/// </remarks>
		public DataTable SelectUserInfosDynamic( string keyword)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"Keyword"} ;
			object[] objParamValues = {keyword} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectUserInfosDynamic",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectUserInfosDynamic";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@Keyword", SqlDbType.NVarChar,50).Value=keyword;
//			//return the result reader
//			return base.ResultDataTable ;
//	
		}
		#endregion
		//add by hbing 2006-03
		public SqlDataReader SelectUserInfoByRole(string sRoleID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"sRoleID"} ;
			object[] objParamValues = {sRoleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectUserByRoleID",sParams,objParamValues,paramTypes) ; 
//			mCommandText = "spSelectUserByRoleID" ;
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@sRoleID", SqlDbType.NVarChar,200).Value=sRoleID;
//			//return the result reader
//			return base.ResultReader ;
		}
		//add by hbing 2006-03

		//add by hbing 2006-03-13 
		//function : get the connnect info of the user
		public SqlDataReader GetConnnectInfoByUserID(string sUserID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {sUserID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spGetConnectInfoOfUser",sParams,objParamValues,paramTypes) ; 
//			mCommandText = "spGetConnectInfoOfUser" ;
//			mCommandType = CommandType.StoredProcedure ;
//			base.ClearParameters() ;
//			base.AddParameter("@UserID",SqlDbType.NVarChar,200).Value = sUserID ;
//			return base.ResultReader ;
		}
		/// <summary>
		/// function to select one page's User list.
		/// </summary>
		/// <param name="where">query condition</param>
		/// <param name="orderBy" ></param>
		/// <param name="ascOrDesc" ></param>
		/// <param name="recordCount">total Department count to fill query condition</param>
		/// <param name="pageIndex">required page</param>
		/// <param name="pageSize">record number per page</param>
		/// <returns>SqlDataReader with the result</returns>
		public SqlDataReader SelectUserInfoListPaged(string where,string orderBy,string ascOrDesc,int recordCount,int pageIndex,int pageSize)
		{
			string what=@" 
			BI_Employee.[IDKey],
			BI_Employee.[FullName],
			BI_Employee.[LoginName],
			BI_Employee.[CanLogin],
			BI_Employee.[LastLoginIP],
			BI_Employee.[LastLoginTime],
			BI_Employee.[Status]
			";
			string from=" BI_Employee  join (select distinct Userid from userrole)a on a.UserID=BI_Employee.IDKey";
			
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPaged(what,from,where,orderBy,ascOrDesc,recordCount,pageIndex,pageSize);
		}

		/// <summary>
		/// get record count
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		public int SelectUserInfoListPagedTotalCount(string where)
		{
			
			string from=" BI_Employee  join (select distinct Userid from userrole)a on a.UserID=BI_Employee.IDKey";
		
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPagedTotalCount(from,where);
			
		}


		#region SelectUserInfo Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectUserInfo 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectUserInfo </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectUserInfo  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectUserInfo]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectUserInfo]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectUserInfo]
		///	@UserID nvarchar(32)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[UserID],
		///	[UserName],
		///	[UserDescription],
		///	[Passwd],
		///	[CanLogin],
		///	[LastLoginTime],
		///	[LastLoginIP],
		///	[RelativeEmployeeID],
		///	[Status]
		///FROM
		///	[dbo].[UserInfo]
		///WHERE
		///	[UserID] = @UserID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString UserID, SqlString UserName, SqlString UserDescription, SqlString Passwd, SqlBoolean CanLogin, SqlDateTime LastLoginTime, SqlString LastLoginIP, SqlString RelativeEmployeeID, SqlByte Status,
		/// </remarks>
		public SqlDataReader SelectUserInfo(string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectUserInfo",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectUserInfo";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		/// <summary>
		/// get the userName
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public string GetUserName(string userID)
		{
			string userName="";
			SqlDataReader r=this.SelectUserInfo(userID) ;
			if(r.Read() )
			{
				userName=(string)r["UserName"];
			}
			r.Close() ;
			return userName;
		}
		/// <summary>
		/// get the userName
		/// </summary>

		public bool CheckUserValid(string UserID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {UserID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			DataTable tbl = pDataAccess.ExecuteSPQueryDataTable("spCheckUserValid",sParams,objParamValues,paramTypes) ; 
			if(tbl.Rows.Count >0)
			{
				return true;
			}
			else
			{
				return false;
			}
//			//set the commandText
//			mCommandText = "spCheckUserValid";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=UserID;
			//return the result reader
//			DataRow dr=base.ResultDataRow;
//			if(dr==null)
//			{
//				return false;
//			}
//			return true;
		}
		#region CheckUserExists Methods


		/// <summary>
		/// check wether the userID is a valid user id
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public bool CheckUserExists(string userID)
		{
			SqlDataReader reader= SelectUserInfo(userID);
			if(reader.Read () && (bool)reader["CanLogin"])
			{
				return true;
			}
			else
			{
				return false;
			}				
		}


		#endregion
		
		#region  CheckUserPassword Methods
		/// <summary>
		/// 此函数调用存储过程 spCheckUserPassword stored procedure .
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spCheckUserPassword </param>	
		///<param name="passwd">变量 passwd: 用于设置参数 '@Passwd' 给存储过程spCheckUserPassword </param>	
		/// <returns>bool</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spCheckUserPassword  ] 如下：
		/// <code>
		/// 
		///
		///
		///--region [dbo].[spCheckUserPassword]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spCheckUserPassword]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spCheckUserPassword]
		///	@UserID nvarchar(32),
		///	@Passwd nvarchar(50)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///	if exists (
		///		select * from UserInfo where UserID=@UserID and Passwd=@Passwd and CanLogin=1
		///	)
		///	return 1
		///	else return 0
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public bool CheckUserPassword(string userID, string passwd)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;
			string strSql="select * from BI_Employee where IDKey='"+userID+"' and Passwd=N'"+passwd+"' and CanLogin=1";
			DataTable dt=pDataAccess.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			{
				return true;
			}
			else
			{
				return false;
			}			
//			//set the commandText
//			mCommandText = "spCheckUserPassword";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@Passwd", SqlDbType.NVarChar,50).Value=passwd;
//			base.AddParameter ("@ReturnValue",SqlDbType.Int).Direction=ParameterDirection.ReturnValue  ;
			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				base.ExecuteNonQuery();
//				int ReturnValue=(int)base.GetParameterValue("@ReturnValue");
//				if(ReturnValue==1)
//				{
//					return true;
//				}
//				else
//				{
//					return false;
//				}
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region  InsertUserInfo Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertUserInfo 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spInsertUserInfo </param>	
		///<param name="userName">变量 userName: 用于设置参数 '@UserName' 给存储过程spInsertUserInfo </param>	
		///<param name="userDescription">变量 userDescription: 用于设置参数 '@UserDescription' 给存储过程spInsertUserInfo </param>	
		///<param name="passwd">变量 passwd: 用于设置参数 '@Passwd' 给存储过程spInsertUserInfo </param>	
		///<param name="canLogin">变量 canLogin: 用于设置参数 '@CanLogin' 给存储过程spInsertUserInfo </param>	
		///<param name="relativeEmployeeID">变量 relativeEmployeeID: 用于设置参数 '@RelativeEmployeeID' 给存储过程spInsertUserInfo </param>	
		///<param name="status">变量 status: 用于设置参数 '@Status' 给存储过程spInsertUserInfo </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertUserInfo  ] 如下：
		/// <code>
		/// 
		///
		///
		///--region [dbo].[spInsertUserInfo]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUserInfo]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spInsertUserInfo]
		///	@UserID nvarchar(32),
		///	@UserName nvarchar(64),
		///	@UserDescription nvarchar(1024),
		///	@Passwd nvarchar(50),
		///	@CanLogin bit,
		///	@RelativeEmployeeID nvarchar(32),
		///	@Status tinyint
		///AS
		///
		///--SET NOCOUNT ON
		///
		///INSERT INTO [dbo].[UserInfo] (
		///	[UserID],
		///	[UserName],
		///	[UserDescription],
		///	[Passwd],
		///	[CanLogin],
		///	[LastLoginTime],
		///	[LastLoginIP],
		///	[RelativeEmployeeID],
		///	[Status]
		///) VALUES (
		///	@UserID,
		///	@UserName,
		///	@UserDescription,
		///	@Passwd,
		///	@CanLogin,
		///	null,
		///	null,
		///	@RelativeEmployeeID,
		///	@Status
		///)
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int InsertUserInfo( string userID, string userName, string userDescription, string passwd, bool canLogin, string relativeEmployeeID, byte status,string EmployeeNo)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID","UserName","UserDescription","EmployeeNo","Passwd","CanLogin","RelativeEmployeeID","Status"} ;
			object[] objParamValues = {userID,userName,userDescription,EmployeeNo,passwd,canLogin,relativeEmployeeID,status} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Bit,SqlDbType.NVarChar,SqlDbType.TinyInt} ;
			
			if(relativeEmployeeID=="" || relativeEmployeeID==null)
			{
				objParamValues[6] = DBNull.Value ;
			}
			pDataAccess.ExecuteSPQueryReader("spInsertUserInfo",sParams,objParamValues,paramTypes) ;

			return 1;
//			//set the commandText
//			mCommandText = "spInsertUserInfo";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@UserName", SqlDbType.NVarChar,64).Value=userName;
//			base.AddParameter("@UserDescription", SqlDbType.NVarChar,1024).Value=userDescription;
//			base.AddParameter("@EmployeeNo", SqlDbType.NVarChar,1024).Value=EmployeeNo;
//			base.AddParameter("@Passwd", SqlDbType.NVarChar,50).Value=passwd;
//			base.AddParameter("@CanLogin", SqlDbType.Bit,0).Value=canLogin;
//			if(relativeEmployeeID=="" || relativeEmployeeID==null)
//			{
//				base.AddParameter("@RelativeEmployeeID", SqlDbType.NVarChar,32).Value=DBNull.Value ;
//			}
//			else
//			{
//				base.AddParameter("@RelativeEmployeeID", SqlDbType.NVarChar,32).Value=relativeEmployeeID;
//			}
//			base.AddParameter("@Status", SqlDbType.TinyInt,0).Value=status;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region  UpdateUserInfo Methods
		/// <summary>
		/// 此函数调用存储过程 spUpdateUserInfo 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spUpdateUserInfo </param>	
		///<param name="userName">变量 userName: 用于设置参数 '@UserName' 给存储过程spUpdateUserInfo </param>	
		///<param name="userDescription">变量 userDescription: 用于设置参数 '@UserDescription' 给存储过程spUpdateUserInfo </param>	
		///<param name="canLogin">变量 canLogin: 用于设置参数 '@CanLogin' 给存储过程spUpdateUserInfo </param>	
		///<param name="relativeEmployeeID">变量 relativeEmployeeID: 用于设置参数 '@RelativeEmployeeID' 给存储过程spUpdateUserInfo </param>	
		///<param name="status">变量 status: 用于设置参数 '@Status' 给存储过程spUpdateUserInfo </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spUpdateUserInfo  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spUpdateUserInfo]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spUpdateUserInfo]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spUpdateUserInfo]
		///	@UserID nvarchar(32),
		///	@UserName nvarchar(64),
		///	@UserDescription nvarchar(1024),
		///	@CanLogin bit,
		///	@RelativeEmployeeID nvarchar(32),
		///	@Status tinyint
		///AS
		///
		///--SET NOCOUNT ON
		///
		///UPDATE [dbo].[UserInfo] SET
		///	[UserName] = @UserName,
		///	[UserDescription] = @UserDescription,
		///	[CanLogin] = @CanLogin,	
		///	[RelativeEmployeeID] = @RelativeEmployeeID,
		///	[Status] = @Status
		///WHERE
		///	[UserID] = @UserID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int UpdateUserInfo( string userID, string userName, string userDescription, bool canLogin, string relativeEmployeeID, byte status,string EmployeeNo)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID","UserName","UserDescription","EmployeeNo","CanLogin","RelativeEmployeeID","Status"} ;
			object[] objParamValues = {userID,userName,userDescription,EmployeeNo,canLogin,relativeEmployeeID,status} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Bit,SqlDbType.NVarChar,SqlDbType.TinyInt} ;
			
			if(relativeEmployeeID=="" || relativeEmployeeID==null)
			{
				objParamValues[6] = DBNull.Value ;
			}
			pDataAccess.ExecuteSPQueryReader("spUpdateUserInfo",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spUpdateUserInfo";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@UserName", SqlDbType.NVarChar,64).Value=userName;
//			base.AddParameter("@UserDescription", SqlDbType.NVarChar,1024).Value=userDescription;
//			base.AddParameter("@CanLogin", SqlDbType.Bit,0).Value=canLogin;
//			base.AddParameter("@EmployeeNo", SqlDbType.NVarChar,1024).Value=EmployeeNo;
//			if(relativeEmployeeID=="" || relativeEmployeeID==null)
//			{
//				base.AddParameter("@RelativeEmployeeID", SqlDbType.NVarChar,32).Value=DBNull.Value ;
//			}
//			else
//			{
//				base.AddParameter("@RelativeEmployeeID", SqlDbType.NVarChar,32).Value=relativeEmployeeID;
//			}
//			base.AddParameter("@Status", SqlDbType.TinyInt,0).Value=status;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region  UpdateUserPasswd Methods
		/// <summary>
		/// 此函数调用存储过程 spUpdateUserPasswd 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spUpdateUserPasswd </param>	
		///<param name="passwd">变量 passwd: 用于设置参数 '@Passwd' 给存储过程spUpdateUserPasswd </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spUpdateUserPasswd  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spUpdateUserPasswd]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spUpdateUserPasswd]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spUpdateUserPasswd]
		///	@UserID nvarchar(32),
		///	@Passwd nvarchar(50)
		///	
		///AS
		///
		///--SET NOCOUNT ON
		///
		///UPDATE [dbo].[UserInfo] SET
		///	Passwd = @Passwd
		///WHERE
		///	[UserID] = @UserID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int UpdateUserPasswd( string userID, string passwd)
		{

			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 					
			string strSql="UPDATE [dbo].[BI_Employee] SET Passwd = N'"+passwd+"' WHERE [IDKey] = '"+userID+"'";
			if(pDataAccess.ExecuteDMLSQL(strSql)=="")
				return 1 ;
			else
				return 0;
//			//set the commandText
//			mCommandText = "spUpdateUserPasswd";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@Passwd", SqlDbType.NVarChar,50).Value=passwd;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion


		#region  UpdateUserLoginInfo Methods
		/// <summary>
		/// 此函数调用存储过程 spUpdateUserLoginInfo 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spUpdateUserLoginInfo </param>	
		///<param name="lastLoginTime">变量 lastLoginTime: 用于设置参数 '@LastLoginTime' 给存储过程spUpdateUserLoginInfo </param>	
		///<param name="lastLoginIP">变量 lastLoginIP: 用于设置参数 '@LastLoginIP' 给存储过程spUpdateUserLoginInfo </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spUpdateUserLoginInfo  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spUpdateUserLoginInfo]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spUpdateUserLoginInfo]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spUpdateUserLoginInfo]
		///	@UserID nvarchar(32),
		///	@LastLoginTime datetime,
		///	@LastLoginIP nvarchar(50)
		///AS
		///
		///--SET NOCOUNT ON
		///
		///UPDATE [dbo].[UserInfo] SET
		///	LastLoginTime=@LastLoginTime,
		///	LastLoginIP=@LastLoginIP
		///WHERE
		///	[UserID] = @UserID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int UpdateUserLoginInfo( string userID, DateTime lastLoginTime, string lastLoginIP)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID","LastLoginTime","LastLoginIP"} ;
			object[] objParamValues = {userID,lastLoginTime,lastLoginIP} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.DateTime,SqlDbType.NVarChar} ;
			
			pDataAccess.ExecuteSP("spUpdateUserLoginInfo",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spUpdateUserLoginInfo";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@LastLoginTime", SqlDbType.DateTime,0).Value=lastLoginTime;
//			base.AddParameter("@LastLoginIP", SqlDbType.NVarChar,50).Value=lastLoginIP;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion


		#region  DeleteUserInfo Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteUserInfo 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spDeleteUserInfo </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteUserInfo  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spDeleteUserInfo]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spDeleteUserInfo]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spDeleteUserInfo]
		///	@UserID nvarchar(32)
		///AS
		///
		///--SET NOCOUNT ON
		///
		///DELETE FROM [dbo].[UserInfo]
		///WHERE
		///	[UserID] = @UserID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int DeleteUserAllRole(string userID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string strSql="DELETE FROM UserRole WHERE UserID='"+userID+"'";
			if(pDataAccess.ExecuteDMLSQL(strSql)=="")
			{			
				return 1 ;
			}
			else
			{
				return 0;
			}
			

//			//set the commandText
//			mCommandText = "spDeleteUserInfo";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion
		#endregion

		#region Role
		#region SelectRolesIDAndName Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectRolesIDAndName 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectRolesIDAndName  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectRolesIDAndName]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectRolesIDAndName]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectRolesIDAndName]
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[RoleID],
		///	[RoleName]
		///FROM
		///	[dbo].[Role]
		///ORDER BY
		///	RoleID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt32 RoleID, SqlString RoleName,
		/// </remarks>
		public SqlDataReader SelectRolesIDAndName()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectRolesIDAndName",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectRolesIDAndName";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectRolesDynamic Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectRolesDynamic 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="keyword">变量 keyword: 用于设置参数 '@Keyword' 给存储过程spSelectRolesDynamic </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectRolesDynamic  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectRolesDynamic]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectRolesDynamic]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectRolesDynamic]
		///	@Keyword nvarchar(50)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[RoleID],
		///	[RoleName],
		///	[RoleDesc]
		///FROM
		///	[dbo].[Role]
		///WHERE
		///	RoleName like '%'+@Keyword+'%'
		///	or RoleDesc like '%'+@Keyword+'%'
		///ORDER BY
		///	RoleID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt32 RoleID, SqlString RoleName, SqlString RoleDesc,
		/// </remarks>
		public SqlDataReader SelectRolesDynamic( string keyword)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"Keyword"} ;
			object[] objParamValues = {keyword} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectRolesDynamic",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectRolesDynamic";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@Keyword", SqlDbType.NVarChar,50).Value=keyword;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		/// <summary>
		/// function to select one page's Role list.
		/// </summary>
		/// <param name="where">query condition</param>
		/// <param name="orderBy" ></param>
		/// <param name="ascOrDesc" ></param>
		/// <param name="recordCount">total Department count to fill query condition</param>
		/// <param name="pageIndex">required page</param>
		/// <param name="pageSize">record number per page</param>
		/// <returns>SqlDataReader with the result</returns>
		public SqlDataReader SelectRoleListPaged(string where,string orderBy,string ascOrDesc,int recordCount,int pageIndex,int pageSize)
		{
			string what=@" 
			[RoleID],
			[RoleName],
			[RoleDesc]
			";
			string from=" Role ";
			
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPaged(what,from,where,orderBy,ascOrDesc,recordCount,pageIndex,pageSize);
		}

		/// <summary>
		/// get record count
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		public int SelectRoleListPagedTotalCount(string where)
		{
			
			string from=" Role ";
		
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPagedTotalCount(from,where);
			
		}
		#region SelectRole Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectRole 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spSelectRole </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectRole  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectRole]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectRole]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectRole]
		///	@RoleID int
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[RoleID],
		///	[RoleName],
		///	[RoleDesc]
		///FROM
		///	[dbo].[Role]
		///WHERE
		///	[RoleID] = @RoleID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt32 RoleID, SqlString RoleName, SqlString RoleDesc,
		/// </remarks>
		public SqlDataReader SelectRole( int roleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID"} ;
			object[] objParamValues = {roleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectRole",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectRole";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion


		#region  InsertRole Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertRole 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="roleName">变量 roleName: 用于设置参数 '@RoleName' 给存储过程spInsertRole </param>	
		///<param name="roleDesc">变量 roleDesc: 用于设置参数 '@RoleDesc' 给存储过程spInsertRole </param>	
		///<param name="roleID">变量 roleID: get Parameter '@RoleID' 给存储过程spInsertRole </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertRole  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spInsertRole]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertRole]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spInsertRole]
		///	@RoleName nvarchar(100),
		///	@RoleDesc nvarchar(500),
		///	@RoleID int OUTPUT
		///AS
		///
		///--SET NOCOUNT ON
		///
		///INSERT INTO [dbo].[Role] (
		///	[RoleName],
		///	[RoleDesc]
		///) VALUES (
		///	@RoleName,
		///	@RoleDesc
		///)
		///
		///SET @RoleID = SCOPE_IDENTITY()
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int InsertRole( string roleName, string roleDesc, out int roleID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleName","RoleDesc"} ;
			object[] objParamValues = {roleName,roleDesc} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			object objRoleID = new object() ;
			pDataAccess.ExecuteSPReturnParam("spInsertRole",sParams,objParamValues,paramTypes,SqlDbType.Int,ref objRoleID) ;
			roleID = System.Convert.ToInt32(objRoleID) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertRole";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleName", SqlDbType.NVarChar,100).Value=roleName;
//			base.AddParameter("@RoleDesc", SqlDbType.NVarChar,500).Value=roleDesc;
//
//			base.AddParameter("@RoleID", SqlDbType.Int).Direction=ParameterDirection.Output ;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				int retValue=base.ExecuteNonQuery();
//				roleID=(int)base.GetParameterValue("@RoleID");
//				return retValue;
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion


		#region  UpdateRole Methods
		/// <summary>
		/// 此函数调用存储过程 spUpdateRole 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spUpdateRole </param>	
		///<param name="roleName">变量 roleName: 用于设置参数 '@RoleName' 给存储过程spUpdateRole </param>	
		///<param name="roleDesc">变量 roleDesc: 用于设置参数 '@RoleDesc' 给存储过程spUpdateRole </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spUpdateRole  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spUpdateRole]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spUpdateRole]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spUpdateRole]
		///	@RoleID int,
		///	@RoleName nvarchar(100),
		///	@RoleDesc nvarchar(500)
		///AS
		///
		///--SET NOCOUNT ON
		///
		///UPDATE [dbo].[Role] SET
		///	[RoleName] = @RoleName,
		///	[RoleDesc] = @RoleDesc
		///WHERE
		///	[RoleID] = @RoleID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int UpdateRole( int roleID, string roleName, string roleDesc)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID","RoleName","RoleDesc"} ;
			object[] objParamValues = {roleID,roleName,roleDesc} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int,SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			
			pDataAccess.ExecuteSP("spUpdateRole",sParams,objParamValues,paramTypes) ; 
			return 1 ;
//			//set the commandText
//			mCommandText = "spUpdateRole";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			base.AddParameter("@RoleName", SqlDbType.NVarChar,100).Value=roleName;
//			base.AddParameter("@RoleDesc", SqlDbType.NVarChar,500).Value=roleDesc;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region  DeleteRole Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteRole 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spDeleteRole </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteRole  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spDeleteRole]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spDeleteRole]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spDeleteRole]
		///	@RoleID int
		///AS
		///
		///--SET NOCOUNT ON
		///
		///DELETE FROM [dbo].[Role]
		///WHERE
		///	[RoleID] = @RoleID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int DeleteRole( int roleID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID"} ;
			object[] objParamValues = {roleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int} ;
			
			pDataAccess.ExecuteSP("spDeleteRole",sParams,objParamValues,paramTypes) ; 
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeleteRole";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleID", SqlDbType.Int).Value=roleID;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion
		#endregion

		#region UserRole

		#region SelectRolesByUserID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectRolesByUserID 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectRolesByUserID </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectRolesByUserID  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///--region [dbo].[spSelectRolesByUserID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectRolesByUserID]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectRolesByUserID]
		///	@UserID nvarchar(32)=null
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///if @UserID is not null
		///begin
		///	
		///	SELECT
		///		Role.[RoleID],
		///		Role.[RoleName],
		///		case when UserRole.UserID is null then cast(0 as bit)
		///		else cast(1 as bit) end as HasRole
		///	FROM
		///		[dbo].[Role]
		///	left outer join
		///		UserRole
		///	on
		///		Role.RoleID = UserRole.RoleID
		///		and UserRole.UserID=@UserID
		///end
		///else
		///begin
		///	select  
		///		Role.RoleID,
		///		Role.RoleName,
		///		cast(0 as bit) as HasRole
		///	from
		///		Role	
		///end
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt32 RoleID, SqlString RoleName, SqlBoolean HasRole,
		/// </remarks>
		public SqlDataReader SelectRolesByUserID( string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			if(userID ==null || userID=="")
			{objParamValues[0]=DBNull.Value;}			
			return pDataAccess.ExecuteSPQueryReader("spSelectRolesByUserID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectRolesByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			if(userID ==null || userID=="")
//			{
//				base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=DBNull.Value ;
//			}
//			else
//			{
//				base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			}
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion



		#region  InsertUserRole Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertUserRole 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spInsertUserRole </param>	
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spInsertUserRole </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertUserRole  ] 如下：
		/// <code>
		/// 
		///
		///-----------userrole
		///
		///
		///--region [dbo].[spInsertUserRole]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUserRole]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spInsertUserRole]
		///	@UserID nvarchar(32),
		///	@RoleID int
		///AS
		///
		///--SET NOCOUNT ON
		///
		///INSERT INTO [dbo].[UserRole] (
		///	[UserID],
		///	[RoleID]
		///) VALUES (
		///	@UserID,
		///	@RoleID
		///)
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int InsertUserRole( string userID, int roleID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID","RoleID"} ;
			object[] objParamValues = {userID,roleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.Int} ;
			
			pDataAccess.ExecuteSP("spInsertUserRole",sParams,objParamValues,paramTypes) ; 
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertUserRole";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region  DeleteUserRole Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteUserRole 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spDeleteUserRole </param>	
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spDeleteUserRole </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteUserRole  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spDeleteUserRole]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spDeleteUserRole]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spDeleteUserRole]
		///	@UserID nvarchar(32),
		///	@RoleID int
		///AS
		///
		///--SET NOCOUNT ON
		///
		///DELETE FROM [dbo].[UserRole]
		///WHERE
		///	[UserID] = @UserID
		///	AND [RoleID] = @RoleID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int DeleteUserRole( string userID, int roleID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID","RoleID"} ;
			object[] objParamValues = {userID,roleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.Int} ;
			
			pDataAccess.ExecuteSP("spDeleteUserRole",sParams,objParamValues,paramTypes) ; 
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeleteUserRole";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#endregion

		#region Authority
		#region SelectAuthoritiesAll Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectAuthoritiesAll 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectAuthoritiesAll  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectAuthoritiesAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectAuthoritiesAll]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectAuthoritiesAll]
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[AuthorityID],
		///	[ModuleID],
		///	[AuthorityType],
		///	[AuthorityDescription]
		///FROM
		///	[dbo].[Authority]
		///ORDER BY
		///	AuthorityID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString AuthorityID, SqlString ModuleID, SqlString AuthorityType, SqlString AuthorityDescription,
		/// </remarks>
		public SqlDataReader SelectAuthoritiesAll()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectAuthoritiesAll",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectAuthoritiesAll";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		
		#region SelectAuthoritiesByModuleID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectAuthoritiesByModuleID 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="moduleID">变量 moduleID: 用于设置参数 '@ModuleID' 给存储过程spSelectAuthoritiesByModuleID </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectAuthoritiesByModuleID  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectAuthoritiesByModuleID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectAuthoritiesByModuleID]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectAuthoritiesByModuleID]
		///	@ModuleID nvarchar(100)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[AuthorityID],
		///	[ModuleID],
		///	[AuthorityType],
		///	[AuthorityDescription]
		///FROM
		///	[dbo].[Authority]
		///WHERE
		///	[ModuleID] = @ModuleID
		///ORDER BY
		///	AuthorityID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString AuthorityID, SqlString ModuleID, SqlString AuthorityType, SqlString AuthorityDescription,
		/// </remarks>
		public SqlDataReader SelectAuthoritiesByModuleID( string moduleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID"} ;
			object[] objParamValues = {moduleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectAuthoritiesByModuleID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectAuthoritiesByModuleID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@ModuleID", SqlDbType.NVarChar,100).Value=moduleID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		
		#region SelectAuthoritiesAndModulesAll Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectAuthoritiesAndModulesAll 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectAuthoritiesAndModulesAll  ] 如下：
		/// <code>
		/// 
		///
		///
		///--region [dbo].[spSelectAuthoritiesAndModulesAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectAuthoritiesAndModulesAll]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectAuthoritiesAndModulesAll]	
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///--select all the parentModuleID
		///select distinct 
		///	ParentModuleID
		///from
		///	Module
		///where
		///	ModuleID in 
		///	(
		///		select ModuleID from Authority
		///	)	
		///
		///--get all the modules
		///SELECT 
		///	ParentModuleID,ModuleID,SubID
		///FROM
		///	Module
		///where
		///	ModuleID in 
		///	(
		///		select ModuleID from Authority
		///	)	
		///ORDER BY
		///	ParentModuleID,IndexNo
		///
		///
		///--get all the authorities
		///SELECT
		///	[AuthorityID],
		///	[ModuleID],
		///	[AuthorityType],
		///	[AuthorityDescription]
		///FROM
		///	[dbo].[Authority]
		///ORDER BY
		///	AuthorityID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public DataSet SelectAuthoritiesAndModulesAll( )		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;		
			DataSet ds = pDataAccess.ExecuteSPQuery("spSelectAuthoritiesAndModulesAll",sParams,objParamValues,paramTypes) ; 
            ds.Tables[0].TableName="RootNode";		
			return ds;

	
		}
		#endregion
		#endregion

		#region RoleAuthority
		#region SelectRoleAuthoritiesAndModulesAll Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectRoleAuthoritiesAndModulesAll 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spSelectRoleAuthoritiesAndModulesAll </param>	
		/// <returns>DataSet</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectRoleAuthoritiesAndModulesAll  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///--region [dbo].[spSelectRoleAuthoritiesAndModulesAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectRoleAuthoritiesAndModulesAll]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectRoleAuthoritiesAndModulesAll]	
		///	@RoleID int = null
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///--select all the parentModuleID
		///select distinct 
		///	ParentModuleID
		///from
		///	Module
		///where
		///	ModuleID in 
		///	(
		///		select ModuleID from Authority
		///	)	
		///
		///
		///--get all the modules
		///SELECT 
		///	ParentModuleID,ModuleID,SubID
		///FROM
		///	Module
		///where
		///	ModuleID in 
		///	(
		///		select ModuleID from Authority
		///	)	
		///ORDER BY
		///	ParentModuleID,IndexNo
		///
		///
		///if @RoleID is null
		///begin
		///
		///	--get all the authorities
		///	SELECT
		///		[AuthorityID],
		///		[ModuleID],
		///		[AuthorityType],
		///		cast(0 as bit) as IsInRole
		///	FROM
		///		[dbo].[Authority]
		///	ORDER BY
		///		AuthorityID
		///end 
		///else
		///begin
		///	--get all the authorities
		///	SELECT
		///		Authority.[AuthorityID],
		///		Authority.[ModuleID],
		///		Authority.[AuthorityType],
		///		case 
		///			when RoleID is null then cast(0 as bit)
		///			else cast(1 as bit)
		///		end as IsInRole
		///	FROM
		///		[dbo].[Authority]
		///	LEFT OUTER JOIN
		///		RoleAuthority
		///	on
		///		Authority.AuthorityID = RoleAuthority.AuthorityID
		///		and RoleAuthority.RoleID = @RoleID
		///end
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public DataSet SelectRoleAuthoritiesAndModulesAll( int roleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID"} ;
			object[] objParamValues = {roleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int} ;
			if(roleID==0)
			{objParamValues[0]=DBNull.Value;}
			
			DataSet ds = pDataAccess.ExecuteSPQuery("spSelectRoleAuthoritiesAndModulesAll",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectRoleAuthoritiesAndModulesAll";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			if(roleID==0)
//			{
//				base.AddParameter("@RoleID", SqlDbType.Int,0).Value=DBNull.Value ;
//			}
//			else
//			{
//				base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			}
//			DataSet ds=base.ResultDataSet;
			ds.Tables[0].TableName="RootNode";			
			return ds;	
		}
		#endregion

		#region SelectAuthoritiesByRoleID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectAuthoritiesByRoleID 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spSelectAuthoritiesByRoleID </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectAuthoritiesByRoleID  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///--region [dbo].[spSelectAuthoritiesByRoleID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectAuthoritiesByRoleID]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectAuthoritiesByRoleID]	
		///	@RoleID int = null
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///if @RoleID is null
		///begin
		///
		///	--get all the authorities
		///	SELECT
		///		[AuthorityID],
		///		[ModuleID],
		///		[AuthorityType],
		///		cast(0 as bit) as IsInRole
		///	FROM
		///		[dbo].[Authority]
		///	ORDER BY
		///		AuthorityID
		///end 
		///else
		///begin
		///	--get all the authorities
		///	SELECT
		///		Authority.[AuthorityID],
		///		Authority.[ModuleID],
		///		Authority.[AuthorityType],
		///		case 
		///			when RoleID is null then cast(0 as bit)
		///			else cast(1 as bit)
		///		end as IsInRole
		///	FROM
		///		[dbo].[Authority]
		///	LEFT OUTER JOIN
		///		RoleAuthority
		///	on
		///		Authority.AuthorityID = RoleAuthority.AuthorityID
		///		and RoleAuthority.RoleID = @RoleID
		///end
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString AuthorityID, SqlString ModuleID, SqlString AuthorityType, SqlBoolean IsInRole,
		/// </remarks>
		public SqlDataReader SelectAuthoritiesByRoleID( int roleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID"} ;
			object[] objParamValues = {roleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectAuthoritiesByRoleID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectAuthoritiesByRoleID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region  InsertRoleAuthority Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertRoleAuthority 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spInsertRoleAuthority </param>	
		///<param name="authorityID">变量 authorityID: 用于设置参数 '@AuthorityID' 给存储过程spInsertRoleAuthority </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertRoleAuthority  ] 如下：
		/// <code>
		/// 
		///
		///
		///--region [dbo].[spInsertRoleAuthority]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertRoleAuthority]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spInsertRoleAuthority]
		///	@RoleID int,
		///	@AuthorityID nvarchar(150)
		///AS
		///
		///--SET NOCOUNT ON
		///
		///INSERT INTO [dbo].[RoleAuthority] (
		///	[RoleID],
		///	[AuthorityID]
		///) VALUES (
		///	@RoleID,
		///	@AuthorityID
		///)
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int InsertRoleAuthority( int roleID, string authorityID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID","AuthorityID"} ;
			object[] objParamValues = {roleID,authorityID} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int,SqlDbType.NVarChar} ;
			
			pDataAccess.ExecuteSP("spInsertRoleAuthority",sParams,objParamValues,paramTypes) ; 
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertRoleAuthority";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			base.AddParameter("@AuthorityID", SqlDbType.NVarChar,150).Value=authorityID;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region  DeleteRoleAuthority Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteRoleAuthority 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spDeleteRoleAuthority </param>	
		///<param name="authorityID">变量 authorityID: 用于设置参数 '@AuthorityID' 给存储过程spDeleteRoleAuthority </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteRoleAuthority  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spDeleteRoleAuthority]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spDeleteRoleAuthority]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spDeleteRoleAuthority]
		///	@RoleID int,
		///	@AuthorityID nvarchar(150)
		///AS
		///
		///--SET NOCOUNT ON
		///
		///DELETE FROM [dbo].[RoleAuthority]
		///WHERE
		///	[RoleID] = @RoleID
		///	AND [AuthorityID] = @AuthorityID
		///
		///--endregion
		///
		/// </code>
		/// </remarks>
		public int DeleteRoleAuthority( int roleID, string authorityID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID","AuthorityID"} ;
			object[] objParamValues = {roleID,authorityID} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int,SqlDbType.NVarChar} ;
			
			pDataAccess.ExecuteSP("spDeleteRoleAuthority",sParams,objParamValues,paramTypes) ; 
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeleteRoleAuthority";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			base.AddParameter("@AuthorityID", SqlDbType.NVarChar,150).Value=authorityID;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region  DeleteRoleAuthoritiesByRoleID Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteRoleAuthoritiesByRoleID 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@RoleID' 给存储过程spDeleteRoleAuthoritiesByRoleID </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteRoleAuthoritiesByRoleID  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spDeleteRoleAuthoritiesByRoleID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spDeleteRoleAuthoritiesByRoleID]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spDeleteRoleAuthoritiesByRoleID]
		///	@RoleID int
		///AS
		///
		///--SET NOCOUNT ON
		///
		///DELETE FROM [dbo].[RoleAuthority]
		///WHERE
		///	[RoleID] = @RoleID
		///
		///
		/// </code>
		/// </remarks>
		public int DeleteRoleAuthoritiesByRoleID( int roleID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"RoleID"} ;
			object[] objParamValues = {roleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.Int,SqlDbType.NVarChar} ;
			
			pDataAccess.ExecuteSP("spDeleteRoleAuthoritiesByRoleID",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeleteRoleAuthoritiesByRoleID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@RoleID", SqlDbType.Int,0).Value=roleID;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion


		#endregion

		#region ContractUserAuthority
		/// <summary>
		/// 此函数调用存储过程 SelectContractAuthoritiesTableByUserID 并返回包含查询结果的数据集
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程SelectContractAuthoritiesTableByUserID </param>	
		/// <returns>包含记录集的数据表</returns>
		public DataTable SelectContractAuthoritiesTableByUserID( string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectContractAuthorityByUserID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectContractAuthorityByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			//return the result reader
//			return base.ResultDataTable;
	
		}

		/// <summary>
		/// 此函数调用存储过程 spSelectContractAuthorityByUserID 并返回包含查询结果的数据集
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectContractAuthorityByUserID </param>	
		/// <returns>包含记录集的数据表</returns>
		public SqlDataReader SelectContractAuthoritiesReaderByUserID( string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectContractAuthorityByUserID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectContractAuthorityByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			//return the result reader
//			return base.ResultReader;
	
		}

		#endregion ContractUserAuthority


		#region UserAuthority

		#region  CheckUserAuthority Methods
		/// <summary>
		/// 此函数调用存储过程 spCheckUserAuthority stored procedure .
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spCheckUserAuthority </param>	
		///<param name="authorityID">变量 authorityID: 用于设置参数 '@AuthorityID' 给存储过程spCheckUserAuthority </param>	
		/// <returns>bool</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spCheckUserAuthority  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spCheckUserAuthority]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spCheckUserAuthority]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spCheckUserAuthority]
		///	@UserID nvarchar(32),
		///	@AuthorityID nvarchar(150)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///	if exists (
		///		select * from RoleAuthority
		///		where AuthorityID=@AuthorityID
		///		and RoleID in
		///		(--all the role of user
		///			select RoleID from UserRole
		///			where UserID=@UserID
		///		)
		///	)
		///	return 1
		///	else return 0
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public bool CheckUserAuthority( string userID, string authorityID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID","AuthorityID"} ;
			object[] objParamValues = {userID,authorityID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			object objReturnValue = new object();
			
			pDataAccess.ExecuteSPReturnParam("spCheckUserAuthority",sParams,objParamValues,paramTypes,SqlDbType.Int,ref objReturnValue) ;
			 
			if(System.Convert.ToInt32(objReturnValue) ==1)
			{
				return true ;
			}
			else
			{
				return false ;
			}
//			//set the commandText
//			mCommandText = "spCheckUserAuthority";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@AuthorityID", SqlDbType.NVarChar,150).Value=authorityID;
//			base.AddParameter ("@ReturnValue",SqlDbType.Int).Direction=ParameterDirection.ReturnValue  ;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				base.ExecuteNonQuery();
//				int ReturnValue=(int)base.GetParameterValue("@ReturnValue");
//				if(ReturnValue==1)
//				{
//					return true;
//				}
//				else
//				{
//					return false;
//				}
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}	
		}
		#endregion

		#region SelectUsersByAuthorityID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectUsersByAuthorityID 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="authorityID">变量 authorityID: 用于设置参数 '@AuthorityID' 给存储过程spSelectUsersByAuthorityID </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectUsersByAuthorityID  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectUsersByAuthorityID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectUsersByAuthorityID]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectUsersByAuthorityID]
		///	@AuthorityID nvarchar(150)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT
		///	[UserID],
		///	[UserName],
		///	[CanLogin],
		///	[Status]
		///FROM
		///	[dbo].[UserInfo]
		///WHERE
		///	UserID in
		///	(
		///		select UserID from UserRole
		///		where RoleID in
		///		(
		///			select RoleID from RoleAuthority where AuthorityID=@AuthorityID
		///		)
		///	)
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString UserID, SqlString UserName, SqlBoolean CanLogin, SqlByte Status,
		/// </remarks>
		public SqlDataReader SelectUsersByAuthorityID( string authorityID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"AuthorityID"} ;
			object[] objParamValues = {authorityID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectUsersByAuthorityID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectUsersByAuthorityID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@AuthorityID", SqlDbType.NVarChar,150).Value=authorityID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectAuthoritiesByUserID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectAuthoritiesByUserID 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectAuthoritiesByUserID </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectAuthoritiesByUserID  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///--region [dbo].[spSelectAuthoritiesByUserID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectAuthoritiesByUserID]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectAuthoritiesByUserID]
		///	@UserID nvarchar(32)=null
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT
		///	[AuthorityID],
		///	[ModuleID],
		///	[AuthorityType],
		///	[AuthorityDescription]
		///FROM
		///	[dbo].[Authority]
		///	
		///WHERE
		///	AuthorityID in 
		///	(
		///	select 
		///		AuthorityID 
		///	from 
		///		RoleAuthority 
		///	where 
		///		RoleID in
		///		(
		///		select RoleID from UserRole where UserID=@UserID
		///		)
		///	)
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString AuthorityID, SqlString ModuleID, SqlString AuthorityType, SqlString AuthorityDescription,
		/// </remarks>
		public SqlDataReader SelectAuthoritiesByUserID( string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectAuthoritiesByUserID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectAuthoritiesByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion



		#region SelectAuthoritiesAllByUserID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectAuthoritiesAllByUserID 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectAuthoritiesAllByUserID </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectAuthoritiesAllByUserID  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///--region [dbo].[spSelectAuthoritiesAllByUserID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectAuthoritiesAllByUserID]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectAuthoritiesAllByUserID]
		///	@UserID nvarchar(32)=null
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT
		///	Authority.[AuthorityID],
		///	Authority.[ModuleID],
		///Authority.[AuthorityType],
		///case when UserAuthority.AuthorityID is null then cast(0 as bit)
		///else cast(1 as bit)
		///end as HasAuth
		///		   FROM
		///		   [dbo].[Authority]
		///LEFT OUTER JOIN
		///			   (
		///			   select 
		///			   AuthorityID 
		///			   from 
		///				   RoleAuthority 
		///				   where 
		///					   RoleID in
		///(
		///select RoleID from UserRole where UserID=@UserID
		///								)
		///)UserAuthority
		///	 ON Authority.AuthorityID = UserAuthority.AuthorityID
		///									--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString AuthorityID, SqlString ModuleID, SqlString AuthorityType, SqlString AuthorityDescription,
		/// </remarks>
		public SqlDataReader SelectAuthoritiesAllByUserID( string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectAuthoritiesAllByUserID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectAuthoritiesAllByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectAuthoritiesTableByUserID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectAuthoritiesByUserID 并返回包含查询结果的数据集
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectAuthoritiesByUserID </param>	
		/// <returns>包含记录集的数据表</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectAuthoritiesByUserID  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///--region [dbo].[spSelectAuthoritiesByUserID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectAuthoritiesByUserID]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectAuthoritiesByUserID]
		///	@UserID nvarchar(32)=null
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT
		///	[AuthorityID],
		///	[ModuleID],
		///	[AuthorityType],
		///	[AuthorityDescription]
		///FROM
		///	[dbo].[Authority]
		///	
		///WHERE
		///	AuthorityID in 
		///	(
		///	select 
		///		AuthorityID 
		///	from 
		///		RoleAuthority 
		///	where 
		///		RoleID in
		///		(
		///		select RoleID from UserRole where UserID=@UserID
		///		)
		///	)
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString AuthorityID, SqlString ModuleID, SqlString AuthorityType, SqlString AuthorityDescription,
		/// </remarks>
		public DataTable SelectAuthoritiesTableByUserID(string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectAuthoritiesByUserID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectAuthoritiesByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			//return the result reader
//			return base.ResultDataTable;
	
		}
		#endregion


		#region SelectUserAuthoritiesAndModulesAll Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectUserAuthoritiesAndModulesAll 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="roleID">变量 roleID: 用于设置参数 '@UserID' 给存储过程spSelectUserAuthoritiesAndModulesAll </param>	
		/// <returns>DataSet</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectUserAuthoritiesAndModulesAll  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///--region [dbo].[spSelectUserAuthoritiesAndModulesAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectRoleAuthoritiesAndModulesAll]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectRoleAuthoritiesAndModulesAll]	
		///	@UserID int = null
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///--select all the parentModuleID
		///select distinct 
		///	ParentModuleID
		///from
		///	Module
		///where
		///	ModuleID in 
		///	(
		///		select ModuleID from Authority
		///	)	
		///
		///
		///--get all the modules
		///SELECT 
		///	ParentModuleID,ModuleID,SubID
		///FROM
		///	Module
		///where
		///	ModuleID in 
		///	(
		///		select ModuleID from Authority
		///	)	
		///ORDER BY
		///	ParentModuleID,IndexNo
		///
		///
		///if @UserID is null
		///begin
		///
		///	--get all the authorities
		///	SELECT
		///		[AuthorityID],
		///		[ModuleID],
		///		[AuthorityType],
		///		cast(0 as bit) as IsInRole
		///	FROM
		///		[dbo].[Authority]
		///	ORDER BY
		///		AuthorityID
		///end 
		///else
		///begin
		///	--get all the authorities
		///SELECT
		///	Authority.[AuthorityID],
		///	Authority.[ModuleID],
		///Authority.[AuthorityType],
		///case when UserAuthority.AuthorityID is null then cast(0 as bit)
		///else cast(1 as bit)
		///end as HasAuth
		///		   FROM
		///		   [dbo].[Authority]
		///LEFT OUTER JOIN
		///			   (
		///			   select 
		///			   AuthorityID 
		///			   from 
		///				   RoleAuthority 
		///				   where 
		///					   RoleID in
		///(
		///select RoleID from UserRole where UserID=@UserID
		///								)
		///)UserAuthority
		///	 ON Authority.AuthorityID = UserAuthority.AuthorityID
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public DataSet SelectUserAuthoritiesAndModulesAll( string userID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {userID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			if(userID==null)
			{objParamValues[0]=DBNull.Value;}
			
			DataSet ds = pDataAccess.ExecuteSPQuery("spSelectUserAuthoritiesAndModulesAll",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectUserAuthoritiesAndModulesAll";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			if(userID==null)
//			{
//				base.AddParameter("@UserID", SqlDbType.NVarChar, 32).Value=DBNull.Value ;
//			}
//			else
//			{
//				base.AddParameter("@UserID", SqlDbType.NVarChar, 32).Value=userID;
//			}
//			DataSet ds=base.ResultDataSet;
//			ds.Tables[0].TableName="ParentModule";
//			ds.Tables[1].TableName="Module";
//			ds.Tables[2].TableName="Authority";
//
//			ds.Relations.Add( "ParentModuleModule",
//				ds.Tables["ParentModule"].Columns["ParentModuleID"],
//				ds.Tables["Module"].Columns["ParentModuleID"]);
//
//			ds.Relations.Add( "ModuleAuthority",
//				ds.Tables["Module"].Columns["ModuleID"],
//				ds.Tables["Authority"].Columns["ModuleID"]);


			ds.Tables[0].TableName="RootNode";

			//return the result DataSet			
			return ds;	
		}
		#endregion
		#endregion

		#region UserDepartment
	
		/// <summary>
		/// 此函数调用存储过程 spSelectUserDepartmentByUserID 并返回包含查询结果的数据集
		/// </summary>
		///<param name="UserID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectUserDepartmentByUserID </param>	
		/// <returns>包含记录集的数据行</returns>
		public string SelectUserDepartmentIDByUserID(string UserID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID"} ;
			object[] objParamValues = {UserID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			System.Data.DataTable dtInfo = pDataAccess.ExecuteSPQueryDataTable("spSelectUserDepartmentByUserID",sParams,objParamValues,paramTypes) ; 
			if(dtInfo.Rows[0] == null)
				return "" ;
			return dtInfo.Rows[0]["DepartmentID"].ToString() ; 
//			mCommandText = "spSelectUserDepartmentByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=UserID;
//			//return the result reader
//			DataRow dr=base.ResultDataRow;
//			string DepartmentID="";
//			if(dr!=null)
//			{
//				DepartmentID=dr["DepartmentID"].ToString();
//			}
//			return DepartmentID;
				
			
		}
		
		#endregion UserDepartment
	}
}
