/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Module Name: BusyAccess.SystemConfig.CDAUserRoleAuthority
-- Date Generated: 2005.6.10
-- Version List
--  Version 1.0 2005.7.1
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data;
using System.Data.SqlClient;
using Cnwit.Utility ;
namespace Business.SystemConfig
{
	/// <summary>
	/// Summary description for CDASystemLog.
	/// </summary>
	public class CDASystemLog
	{
		/// <summary>
		/// constructor
		/// </summary>
		public CDASystemLog()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region  InsertSystemLog Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertSystemLog stored procedure and return the log id
		/// </summary>
		///<param name="sessionID">变量 sessionID: 用于设置参数 '@SessionID' 给存储过程spInsertSystemLog </param>	
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spInsertSystemLog </param>	
		///<param name="logTime">变量 logTime: 用于设置参数 '@LogTime' 给存储过程spInsertSystemLog </param>	
		///<param name="moduleID">变量 moduleID: 用于设置参数 '@ModuleID' 给存储过程spInsertSystemLog </param>	
		///<param name="logType">变量 logType: 用于设置参数 '@LogType' 给存储过程spInsertSystemLog </param>	
		///<param name="logDescription">变量 logDescription: 用于设置参数 '@LogDescription' 给存储过程spInsertSystemLog </param>	
		///<param name="logIP">变量 logIP: 用于设置参数 '@LogIP' 给存储过程spInsertSystemLog </param>	
		///<param name="platform">变量 platform: 用于设置参数 '@Platform' 给存储过程spInsertSystemLog </param>	
		///<param name="browserVersion">变量 browserVersion: 用于设置参数 '@BrowserVersion' 给存储过程spInsertSystemLog </param>	
		///<param name="language">变量 language: 用于设置参数 '@Language' 给存储过程spInsertSystemLog </param>	
		/// <returns>Log ID</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertSystemLog  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spInsertSystemLog]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertSystemLog]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spInsertSystemLog]
		///	@SessionID nvarchar(100),
		///	@UserID nvarchar(32),
		///	@LogTime datetime,
		///	@ModuleID nvarchar(100),
		///	@LogType nvarchar(50),
		///	@LogDescription nvarchar(500),
		///	@LogIP nvarchar(50),
		///	@Platform nvarchar(50),
		///	@BrowserVersion nvarchar(50),
		///	@Language nvarchar(50),
		///	@ID bigint OUTPUT
		///AS
		///
		///--SET NOCOUNT ON
		///
		///INSERT INTO [dbo].[SystemLog] (
		///	[SessionID],
		///	[UserID],
		///	[LogTime],
		///	[ModuleID],
		///	[LogType],
		///	[LogDescription],
		///	[LogIP],
		///	[Platform],
		///	[BrowserVersion],
		///	[Language]
		///) VALUES (
		///	@SessionID,
		///	@UserID,
		///	@LogTime,
		///	@ModuleID,
		///	@LogType,
		///	@LogDescription,
		///	@LogIP,
		///	@Platform,
		///	@BrowserVersion,
		///	@Language
		///)
		///
		///SET @ID = SCOPE_IDENTITY()
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public long InsertSystemLog( string sessionID, string userID, DateTime logTime, string moduleID, string logType, string logDescription, string logIP, string platform, string browserVersion, string language)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"SessionID","UserID","LogTime","ModuleID","LogType","LogDescription","LogIP","Platform","BrowserVersion","Language","ID"} ;
			object[] objParamValues = {sessionID,userID,logTime,moduleID,logType,logDescription,logIP,platform,browserVersion,language,ParameterDirection.Output} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.DateTime,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.BigInt} ;
			
			pDataAccess.ExecuteSP("spInsertSystemLog",sParams,objParamValues,paramTypes) ; 
			return 1;
//			//set the commandText
//			mCommandText = "spInsertSystemLog";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@SessionID", SqlDbType.NVarChar,100).Value=sessionID;
//			base.AddParameter("@UserID", SqlDbType.NVarChar,32).Value=userID;
//			base.AddParameter("@LogTime", SqlDbType.DateTime,0).Value=logTime;
//			base.AddParameter("@ModuleID", SqlDbType.NVarChar,100).Value=moduleID;
//			base.AddParameter("@LogType", SqlDbType.NVarChar,50).Value=logType;
//			base.AddParameter("@LogDescription", SqlDbType.NVarChar,500).Value=logDescription;
//			base.AddParameter("@LogIP", SqlDbType.NVarChar,50).Value=logIP;
//			base.AddParameter("@Platform", SqlDbType.NVarChar,50).Value=platform;
//			base.AddParameter("@BrowserVersion", SqlDbType.NVarChar,50).Value=browserVersion;
//			base.AddParameter("@Language", SqlDbType.NVarChar,50).Value=language;
//			base.AddParameter("@ID", SqlDbType.BigInt).Direction=ParameterDirection.Output;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				 base.ExecuteNonQuery();
//				return (long)base.GetParameterValue("@ID");
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}
		#endregion

		#region SelectSystemLogsByUserID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSystemLogsByUserID 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="userID">变量 userID: 用于设置参数 '@UserID' 给存储过程spSelectSystemLogsByUserID </param>	
		///<param name="beginDate">变量 beginDate: 用于设置参数 '@BeginDate' 给存储过程spSelectSystemLogsByUserID </param>	
		///<param name="endDate">变量 endDate: 用于设置参数 '@EndDate' 给存储过程spSelectSystemLogsByUserID </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSystemLogsByUserID  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectSystemLogsByUserID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSystemLogsByUserID]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectSystemLogsByUserID]
		///	@UserID nvarchar(50),
		///	@BeginDate datetime,
		///	@EndDate datetime
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT	
		///	[ID],
		///	[SessionID],
		///	[UserID],
		///	[LogTime],
		///	[ModuleID],
		///	[LogType],
		///	[LogDescription],
		///	[LogIP],
		///	[Platform],
		///	[BrowserVersion],
		///	[Language]
		///FROM
		///	[dbo].[SystemLog]
		///WHERE
		///	UserID=@UserID
		///	and 
		///		(LogTime >=@BeginDate  and LogTime &lt;=@EndDate)
		///order by ID desc
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt64 ID, SqlString SessionID, SqlString UserID, SqlDateTime LogTime, SqlString ModuleID, SqlString LogType, SqlString LogDescription, SqlString LogIP, SqlString Platform, SqlString BrowserVersion, SqlString Language,
		/// </remarks>
		public SqlDataReader SelectSystemLogsByUserID( string userID, DateTime beginDate, DateTime endDate)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"UserID","BeginDate","EndDate"} ;
			object[] objParamValues = {userID,beginDate,endDate} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.DateTime,SqlDbType.DateTime} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSystemLogsByUserID",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogsByUserID";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@UserID", SqlDbType.NVarChar,50).Value=userID;
//			base.AddParameter("@BeginDate", SqlDbType.DateTime,0).Value=beginDate;
//			base.AddParameter("@EndDate", SqlDbType.DateTime,0).Value=endDate;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region  SelectSystemLogsDynamic Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSystemLogsDynamic stored procedure and return the datareader.
		/// </summary>
		///<param name="whereCondition">变量 whereCondition: 用于设置参数 '@WhereCondition' 给存储过程spSelectSystemLogsDynamic </param>	
		///<param name="orderByExpression">变量 orderByExpression: 用于设置参数 '@OrderByExpression' 给存储过程spSelectSystemLogsDynamic </param>	
		///<param name="recordCount"></param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSystemLogsDynamic  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectSystemLogsDynamic]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSystemLogsDynamic]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectSystemLogsDynamic]
		///	@WhereCondition nvarchar(500),
		///	@OrderByExpression nvarchar(250) = NULL
		///	@RecordCount int
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///DECLARE @SQL nvarchar(3250)
		///
		///SET @SQL = '
		///	SELECT 
		///	top ' + cast(@RecordCount as nvarchar(10)) + ' 
		///	[ID],
		///	[SessionID],
		///	[UserID],
		///	[LogTime],
		///	[ModuleID],
		///	[LogType],
		///	[LogDescription],
		///	[LogIP],
		///	[Platform],
		///	[BrowserVersion],
		///	[Language]
		///FROM
		///	[dbo].[SystemLog]
		///WHERE
		///	' + @WhereCondition
		///
		///IF @OrderByExpression IS NOT NULL AND LEN(@OrderByExpression) > 0
		///BEGIN
		///	SET @SQL = @SQL + '
		///ORDER BY
		///	' + @OrderByExpression
		///END
		///
		///EXEC sp_executesql @SQL
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt64 ID, SqlString SessionID, SqlString UserID, SqlDateTime LogTime, SqlString ModuleID, SqlString LogType, SqlString LogDescription, SqlString LogIP, SqlString Platform, SqlString BrowserVersion, SqlString Language,
		/// </remarks>
		public SqlDataReader SelectSystemLogsDynamic( string whereCondition, string orderByExpression,int recordCount)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"WhereCondition","OrderByExpression","RecordCount"} ;
			object[] objParamValues = {whereCondition,orderByExpression,recordCount} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSystemLogsDynamic",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogsDynamic";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@WhereCondition", SqlDbType.NVarChar,500).Value=whereCondition;
//			base.AddParameter("@OrderByExpression", SqlDbType.NVarChar,250).Value=orderByExpression;
//			base.AddParameter("@RecordCount", SqlDbType.Int ).Value=recordCount;
//			//return the result reader
//			return base.ResultReader ;
//	
		}
		#endregion

		#region SelectSystemLogsDynamicPaged
		/// <summary>
		/// execute the spSelectListDynamicPaged sp and get the total count of the query
		/// </summary>
		/// <param name="strWhereStatement">where condition</param>
		/// <returns></returns>
		public int SelectSystemLogsDynamicPagedTotalCount( string strWhereStatement)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"WhereStatement","RecordCount","PageIndex","PageSize","DoCount"} ;
			object[] objParamValues = {strWhereStatement,0,0,0,0} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int} ;
			
			return (int)pDataAccess.ExecuteSPQueryObject("spSelectSystemLogsDynamicPaged",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogsDynamicPaged";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@WhereStatement", SqlDbType.NVarChar,2000).Value=strWhereStatement;
//			base.AddParameter("@RecordCount", SqlDbType.Int).Value=0;
//			base.AddParameter("@PageIndex", SqlDbType.Int).Value=0;
//			base.AddParameter("@PageSize", SqlDbType.Int).Value=0;
//			base.AddParameter("@DoCount", SqlDbType.Bit).Value=true;
//			//return the result count
//			return (int)base.ExecuteScalar ();
	
		}
		/// <summary>
		/// execute the spSelectSystemLogsDynamicPaged sp and get the sqldatareader result
		/// </summary>
		/// <param name="strWhereStatement"></param>
		/// <param name="RecordCount"></param>
		/// <param name="intPageIndex"></param>
		/// <param name="intPageSize"></param>
		/// <returns></returns>
		public SqlDataReader SelectSystemLogsDynamicPaged( string strWhereStatement,int RecordCount,int intPageIndex,int intPageSize)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"WhereStatement","RecordCount","PageIndex","PageSize","DoCount"} ;
			object[] objParamValues = {strWhereStatement,RecordCount,intPageIndex,intPageSize,false} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int,SqlDbType.Bit} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSystemLogsDynamicPaged",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogsDynamicPaged";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@WhereStatement", SqlDbType.NVarChar,2000).Value=strWhereStatement;
//			base.AddParameter("@RecordCount", SqlDbType.Int).Value=RecordCount;
//			base.AddParameter("@PageIndex", SqlDbType.Int).Value=intPageIndex;
//			base.AddParameter("@PageSize", SqlDbType.Int).Value=intPageSize;
//			base.AddParameter("@DoCount", SqlDbType.Bit).Value=false;
//			//return the result reader
//			return base.ResultReader;	
		}
		#endregion

		#region SelectListPaged2
		/// <summary>
		/// function to select one page's Department list.
		/// </summary>
		/// <param name="where">query condition</param>
		/// <param name="orderBy" ></param>
		/// <param name="ascOrDesc" ></param>
		/// <param name="recordCount">total Department count to fill query condition</param>
		/// <param name="pageIndex">required page</param>
		/// <param name="pageSize">record number per page</param>
		/// <returns>SqlDataReader with the result</returns>
		public SqlDataReader SelectSystemLogListPaged(string where,string orderBy,string ascOrDesc,int recordCount,int pageIndex,int pageSize)
		{
			string what=@" 
					[ID],
					[SessionID],
					SystemLog.[UserID],
					[LogTime],
					[ModuleID],
					[LogType],
					[LogDescription],
					[LogIP],
					[Platform],
					[BrowserVersion],
					[Language],
					isnull(BI_Employee.FullName,SystemLog.UserID) as UserName,
					BI_Employee.IDKey as UserID2
			";
			string from=@" 		
				SystemLog
				left outer join BI_Employee on SystemLog.UserID=BI_Employee.IDKey
			";
			
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPaged(what,from,where,orderBy,ascOrDesc,recordCount,pageIndex,pageSize);
		}

		/// <summary>
		/// get record count
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		public int SelectSystemLogListPagedTotalCount(string where)
		{
			
			string from=@" 				
				SystemLog
				left outer join BI_Employee on SystemLog.UserID=BI_Employee.IDKey
				";
		
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPagedTotalCount(from,where);
			
		}

		#endregion

		#region SelectSystemLogTypes Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSystemLogTypes 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="moduleID">变量 moduleID: 用于设置参数 '@ModuleID' 给存储过程spSelectSystemLogTypes </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSystemLogTypes  ] 如下：
		/// <code>
		/// --region [dbo].[spSelectSystemLogTypes]
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSystemLogTypes]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectSystemLogTypes]
		///	@ModuleID nvarchar(100) =  null
		///AS
		///
		///	if @ModuleID is null
		///	begin
		///		select distinct LogType from SystemLog
		///	end
		///	else
		///		select distinct LogType from SystemLog where ModuleID like @ModuleID+'%'
		///
		///--endregion
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString LogType,
		/// </remarks>
		public SqlDataReader SelectSystemLogTypes( string moduleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID"} ;
			object[] objParamValues = {moduleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSystemLogTypes",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogTypes";
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

		#region SelectSystemLogPlatforms Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSystemLogPlatforms 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="moduleID">变量 moduleID: 用于设置参数 '@ModuleID' 给存储过程spSelectSystemLogPlatforms </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSystemLogPlatforms  ] 如下：
		/// <code>
		/// 
		///
		///
		///
		///
		///--region [dbo].[spSelectSystemLogPlatforms]
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSystemLogPlatforms]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectSystemLogPlatforms]
		///	@ModuleID nvarchar(100) =  null
		///AS
		///
		///	if @ModuleID is null
		///	begin
		///		select distinct Platform from SystemLog
		///	end
		///	else
		///		select distinct Platform from SystemLog where ModuleID like @ModuleID+'%'
		///
		///--endregion
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString Platform,
		/// </remarks>
		public SqlDataReader SelectSystemLogPlatforms( string moduleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID"} ;
			object[] objParamValues = {moduleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSystemLogPlatforms",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogPlatforms";
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

		#region SelectSystemLogLanguages Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSystemLogLanguages 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="moduleID">变量 moduleID: 用于设置参数 '@ModuleID' 给存储过程spSelectSystemLogLanguages </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSystemLogLanguages  ] 如下：
		/// <code>
		/// 
		///
		///
		///--region [dbo].[spSelectSystemLogLanguages]
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSystemLogLanguages]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectSystemLogLanguages]
		///	@ModuleID nvarchar(100) =  null
		///AS
		///
		///	if @ModuleID is null
		///	begin
		///		select distinct Language from SystemLog
		///	end
		///	else
		///		select distinct Language from SystemLog where ModuleID like @ModuleID+'%'
		///
		///--endregion
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString Language,
		/// </remarks>
		public SqlDataReader SelectSystemLogLanguages( string moduleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID"} ;
			object[] objParamValues = {moduleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSystemLogLanguages",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogLanguages";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@ModuleID", SqlDbType.NVarChar,100).Value=moduleID;
//			//return the result reader
//			return base.ResultReader;
//	
		}
		#endregion

		#region SelectSystemLogBrowserVersions Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSystemLogBrowserVersions 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="moduleID">变量 moduleID: 用于设置参数 '@ModuleID' 给存储过程spSelectSystemLogBrowserVersions </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSystemLogBrowserVersions  ] 如下：
		/// <code>
		/// 
		///
		///
		///--region [dbo].[spSelectSystemLogBrowserVersions]
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSystemLogBrowserVersions]
		///-- Date Generated: 2005年6月8日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectSystemLogBrowserVersions]
		///	@ModuleID nvarchar(100) =  null
		///AS
		///
		///	if @ModuleID is null
		///	begin
		///		select distinct BrowserVersion from SystemLog
		///	end
		///	else
		///		select distinct BrowserVersion from SystemLog where ModuleID like @ModuleID+'%'
		///
		///--endregion
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString BrowserVersion,
		/// </remarks>
		public SqlDataReader SelectSystemLogBrowserVersions( string moduleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID"} ;
			object[] objParamValues = {moduleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSystemLogBrowserVersions",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSystemLogBrowserVersions";
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
	

		#region  DeleteSystemLogsDynamic Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteSystemLogsDynamic 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="whereCondition">变量 whereCondition: 用于设置参数 '@WhereCondition' 给存储过程spDeleteSystemLogsDynamic </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteSystemLogsDynamic  ] 如下：
		/// <code>
		/// CREATE PROCEDURE [dbo].[spDeleteSystemLogsDynamic]
		///	@WhereCondition nvarchar(1000)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///DECLARE @SQL nvarchar(3250)
		///SET @SQL='
		///insert into SystemLogBak
		///				(
		///				BakTime,
		///				[LogID],
		///				[SessionID],
		///				[UserID],
		///				[LogTime],
		///				[ModuleID],
		///				[LogType],
		///				[LogDescription],
		///				[LogIP],
		///				[Platform],
		///				[BrowserVersion],
		///				[Language]
		///				)
		///				select 
		///					GetDate(),
		///[ID],
		///[SessionID],
		///[UserID],
		///[LogTime],
		///[ModuleID],
		///[LogType],
		///[LogDescription],
		///[LogIP],
		///[Platform],
		///[BrowserVersion],
		///[Language]
		///FROM SystemLog
		///	where 1=1 '+ @WhereCondition
		///
		///SET @SQL = '
		///DELETE
		///FROM
		///	[dbo].[SystemLog]
		///WHERE 1=1 
		///	' + @WhereCondition
		///
		///EXEC sp_executesql @SQL
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int DeleteSystemLogsDynamic( string whereCondition)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"WhereCondition"} ;
			object[] objParamValues = {whereCondition} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			pDataAccess.ExecuteSP("spDeleteSystemLogsDynamic",sParams,objParamValues,paramTypes) ; 
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeleteSystemLogsDynamic";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@WhereCondition", SqlDbType.NVarChar,1000).Value=whereCondition;
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

		public DataTable SelectSystemLogListForReport(string where)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 		
			return pDataAccess.GetDataTable("select * from vwSystemLog where 1=1 "+where+" order by LogTime desc") ;
			
//			mCommandText="select * from vwSystemLog where 1=1 "+where+" order by LogTime desc";
//			mCommandType=CommandType.Text ;
//			base.ClearParameters() ;
//			return base.ResultDataTable ;
		}

	}
}
