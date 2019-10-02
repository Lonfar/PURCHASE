/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Date Generated: 2005.6.7
-- Version List
--  Version 1.0 2005.7.1
Version 2.0 2005.7.21 update many functions with the sp changed( add EntranceAuthority)
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data.SqlClient;
using System.Data;
using Cnwit.Utility ;
/// Data access namespace about TOPIS system, mainly about
/// module.                                               
namespace Business.TopisSystem
{
	/// <summary>
	/// Summary description for CDAModule.
	/// </summary>
	public class CDAModule
	{
		/// <summary>
		/// constructor
		/// </summary>
		public CDAModule()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		#region SelectModulesForTree Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectModulesForTree 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>包含记录集的数据表</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectModulesForTree  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectModulesForTree]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectModulesForTree]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectModulesForTree]
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///--select all the 
		///SELECT
		///	[ModuleID],
		///	[ParentModuleID],
		///	[SubID],
		///	[IndexNo],
		///	[ModuleName],
		///	[ModuleUrl],
		///	[IsLeafModule],
		///	[IsInTree],
		///	EntranceAuthority,
		///	[HelpUrl],
		///ModuleStatus
		///FROM
		///	[dbo].[Module]
		///WHERE
		///	IsInTree = 1
		///ORDER BY
		///	ParentModuleID,IndexNo
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString ModuleID, SqlString ParentModuleID, SqlString SubID, SqlDecimal IndexNo, SqlString ModuleName, SqlString ModuleUrl, SqlBoolean IsLeafModule, SqlBoolean IsInTree, SqlBoolean EntranceAuthority,SqlString HelpUrl,
		/// </remarks>
		public DataTable SelectModulesForTree()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectModulesForTree",sParams,objParamValues,paramTypes) ; 
			//			//set the commandText
			//			mCommandText = "spSelectModulesForTree";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			//return the result Table
			//			return base.ResultDataTable;
	
		}
		#endregion

		#region SelectModule Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectModule 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="moduleID">变量 moduleID: 用于设置参数 '@ModuleID' 给存储过程spSelectModule </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectModule  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectModule]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectModule]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectModule]
		///	@ModuleID nvarchar(100)
		///AS
		///SELECT
		///	[ModuleID],
		///	[ParentModuleID],
		///	[SubID],
		///	[IndexNo],
		///	[ModuleName],
		///	[ModuleUrl],
		///	[IsLeafModule],
		///	[IsInTree],
		///	EntranceAuthority,
		///	[HelpUrl],
		///ModuleStatus
		///FROM
		///	[dbo].[Module]
		///WHERE
		///	ModuleID=@ModuleID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString ModuleID, SqlString ParentModuleID, SqlString SubID, SqlDecimal IndexNo, SqlString ModuleName, SqlString ModuleUrl, SqlBoolean IsLeafModule, SqlBoolean IsInTree, SqlString HelpUrl,
		/// </remarks>
		public DataTable SelectModule( string moduleID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID"} ;
			object[] objParamValues = {moduleID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectModule",sParams,objParamValues,paramTypes) ; 
			//			//set the commandText
			//			mCommandText = "spSelectModule";
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


		#region SelectModulesAll Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectModulesAll 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectModulesAll  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectModulesAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectModulesAll]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectModulesAll]	
		///AS
		///SELECT
		///	[ModuleID],
		///	[ParentModuleID],
		///	[SubID],
		///	[IndexNo],
		///	[ModuleName],
		///	[ModuleUrl],
		///	[IsLeafModule],
		///	[IsInTree],
		///	EntranceAuthority,
		///	[HelpUrl],
		///ModuleStatus
		///FROM
		///	[dbo].[Module]
		///order by
		///	ParentModuleID,IndexNo
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString ModuleID, SqlString ParentModuleID, SqlString SubID, SqlDecimal IndexNo, SqlString ModuleName, SqlString ModuleUrl, SqlBoolean IsLeafModule, SqlBoolean IsInTree, SqlString HelpUrl,
		/// </remarks>
		public SqlDataReader SelectModulesAll()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectModulesAll",sParams,objParamValues,paramTypes) ;
			//			//set the commandText
			//			mCommandText = "spSelectModulesAll";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			//return the result reader
			//			return base.ResultReader;
	
		}
		#endregion

		#region SelectModulesAllOrderByModuleID Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectModulesAllOrderByModuleID 并返回包含查询结果的数据集
		/// </summary>
		/// <returns>包含记录集的数据表</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectModulesAllOrderByModuleID  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectModulesAllOrderByModuleID]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectModulesAllOrderByModuleID]
		///-- Date Generated: 2005年6月19日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectModulesAllOrderByModuleID]	
		///AS
		///SELECT
		///	[ModuleID],
		///	[ParentModuleID],
		///	[SubID],
		///	[IndexNo],
		///	[ModuleName],
		///	[ModuleUrl],
		///	[IsLeafModule],
		///	[IsInTree],
		///	EntranceAuthority,
		///	[HelpUrl],
		///ModuleStatus
		///FROM
		///	[dbo].[Module]
		///order by
		///	ModuleID
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString ModuleID, SqlString ParentModuleID, SqlString SubID, SqlDecimal IndexNo, SqlString ModuleName, SqlString ModuleUrl, SqlBoolean IsLeafModule, SqlBoolean IsInTree, SqlString HelpUrl,
		/// </remarks>
		public DataTable SelectModulesAllOrderByModuleID()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectModulesAllOrderByModuleID",sParams,objParamValues,paramTypes) ;
			//			//set the commandText
			//			mCommandText = "spSelectModulesAllOrderByModuleID";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			//return the result reader
			//			return base.ResultDataTable ;
	
		}
		#endregion

		public DataTable SelectModulesAllOrderByModuleIDWithStatusName()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectModulesAllOrderByModuleID2",sParams,objParamValues,paramTypes) ;
			//			//set the commandText
			//			mCommandText = "spSelectModulesAllOrderByModuleID2";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			return base.ResultDataTable;
	
		}

		public DataTable SelectTreeModules()
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectTreeModules",sParams,objParamValues,paramTypes) ;
			//			mCommandText = "spSelectTreeModules";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			return base.ResultDataTable;

		}
		public DataTable SelectTreeForOneModules(string ModleID,string ParentID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID","ParentID"} ;
			object[] objParamValues = {ModleID,ParentID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectTreeForOneModule",sParams,objParamValues,paramTypes) ; 
			//			mCommandText = "spSelectTreeForOneModule";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			base.AddParameter("@ModuleID", SqlDbType.NVarChar,100).Value=ModleID;
			//			base.AddParameter("@ParentID", SqlDbType.NVarChar,100).Value=ParentID;
			//
			//			return base.ResultDataTable;


		}
		public DataTable SelectTreeForContract()
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectTreeForContract",sParams,objParamValues,paramTypes) ; 
			//			mCommandText = "spSelectTreeForContract";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//
			//			return base.ResultDataTable;


		}
		
		public DataTable SelectTreeForOneModules3Layer(string ModleID,string ParentID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ModuleID","ParentID"} ;
			object[] objParamValues = {ModleID,ParentID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectTreeForOneModule3Layer",sParams,objParamValues,paramTypes) ; 
			//			mCommandText = "spSelectTreeForOneModule3Layer";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			base.AddParameter("@ModuleID", SqlDbType.NVarChar,100).Value=ModleID;
			//			base.AddParameter("@ParentID", SqlDbType.NVarChar,100).Value=ParentID;
			//
			//
			//			return base.ResultDataTable;


		}
		#region SelectModulesAllIDAndName Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectModulesAllIDAndName 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectModulesAllIDAndName  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectModulesAllIDAndName]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectModulesAllIDAndName]
		///-- Date Generated: 2005年6月7日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectModulesAllIDAndName]	
		///AS
		///SELECT
		///	[ModuleID],
		///	[ModuleName]
		///FROM
		///	[dbo].[Module]
		///order by
		///	ModuleID
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString ModuleID, SqlString ModuleName,
		/// </remarks>
		public SqlDataReader SelectModulesAllIDAndName()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectModulesAllIDAndName",sParams,objParamValues,paramTypes) ; 
			//			//set the commandText
			//			mCommandText = "spSelectModulesAllIDAndName";
			//			//set the CommandType
			//			mCommandType = CommandType.StoredProcedure ;
			//			//Clear all the parameters
			//			base.ClearParameters();
			//			//add and set the parameters
			//			//return the result reader
			//			return base.ResultReader;
	
		}
		#endregion
	}
}
