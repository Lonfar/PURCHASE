/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Date Generated: 2005.6.8
-- Version List
--  Version 1.0 2005.7.1
--  Version 1.0.1 change GetProject()->SelectProject()
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data;
using System.Data.SqlClient ;
using Cnwit.Utility ;
/// Data access namespace about system initialization.
namespace Business.SystemInit
{
	/// <summary>
	/// Database access functions about system initialization
	/// </summary>
	public class CDASystemInit
	{
		/// <summary>
		/// database access class about system init
		/// </summary>
		public CDASystemInit()
		{
			
		}

		#region SelectSysInitStatus Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSysInitStatus 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>byte</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSysInitStatus  ] 如下：
		/// <code>
		/// 
		///--endregion
		///
		///--region [dbo].[spSelectSysInitStatus]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysInitStatus]
		///-- Date Generated: 2005年6月9日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///create procedure spSelectSysInitStatus
		///as
		///select 
		///	top 1 
		///	InitStatus 
		///	from 
		///	SysInitStatus
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlByte InitStatus,
		/// </remarks>
		public byte SelectSysInitStatus()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			SqlDataReader r = pDataAccess.ExecuteSPQueryReader("spSelectSysInitStatus",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSysInitStatus";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			
			byte initStatus = 0;
//			SqlDataReader r=base.ResultReader;
			if(r.Read ())
			{
				initStatus=(byte)r["InitStatus"];
			}
			r.Close ();
			return initStatus;
	
		}

//		public byte SelectSysInitMaterialConfigType()		
//		{
//			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
//			string[] sParams = {} ;
//			object[] objParamValues = {} ; 
//			SqlDbType[] paramTypes = {} ;
//			
//			SqlDataReader r = pDataAccess.ExecuteSPQueryReader("spSelectSysInitStatus",sParams,objParamValues,paramTypes) ; 
////			//set the commandText
////			mCommandText = "spSelectSysInitStatus";
////			//set the CommandType
////			mCommandType = CommandType.StoredProcedure ;
////			//Clear all the parameters
////			base.ClearParameters();
////			//add and set the parameters
////			
//			byte initStatus = 0;
////			SqlDataReader r=base.ResultReader;
//			if(r.Read ())
//			{
//				initStatus=(byte)r["MaterialConfigType"];
//			}
//			r.Close ();
//			return initStatus;
//	
//		}

		public byte SelectSysInitPriceype()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			SqlDataReader r = pDataAccess.ExecuteSPQueryReader("spSelectSysInitStatus",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSysInitStatus";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			
			byte initStatus = 0;
//			SqlDataReader r=base.ResultReader;
			if(r.Read ())
			{
				initStatus=(byte)r["PriceType"];
			}
			r.Close ();
			return initStatus;
	
		}
		#endregion

		#region IsSystemInitFinished
		/// <summary>
		/// function to get wether the system init has finished
		/// </summary>
		/// <returns></returns>
		public bool IsSystemInitFinished()
		{
			byte InitStatus=SelectSysInitStatus();
			if(InitStatus==1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region  InsertUpdateSysInitStatus Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertUpdateSysInitStatus 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="initStatus">变量 initStatus: 用于设置参数 '@InitStatus' 给存储过程spInsertUpdateSysInitStatus </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertUpdateSysInitStatus  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spInsertUpdateSysInitStatus]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUpdateSysInitStatus]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///--
		///create procedure spInsertUpdateSysInitStatus
		///	@InitStatus tinyint
		///as
		///if exists (select * from SysInitStatus)
		///begin
		///	update  
		///		SysInitStatus 
		///	set
		///		InitStatus = @InitStatus
		///end
		///else
		///begin
		///	insert into SysInitStatus(InitStatus)
		///	values(@InitStatus)
		///end
		///
		///
		/// </code>
		/// </remarks>
		public int InsertUpdateSysInitStatus( byte initStatus,byte priceType)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"InitStatus","PriceType"} ;
			object[] objParamValues = {initStatus,priceType} ; 
			SqlDbType[] paramTypes = {SqlDbType.TinyInt,SqlDbType.TinyInt} ;
			pDataAccess.ExecuteSP("spInsertUpdateSysInitStatus",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertUpdateSysInitStatus";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@InitStatus", SqlDbType.TinyInt,0).Value=initStatus;
//			base.AddParameter("@MaterialConfigType", SqlDbType.TinyInt,0).Value=materialConfigType;
//			base.AddParameter("@PriceType", SqlDbType.TinyInt,0).Value=priceType;
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

		public int FinishSystemInit(bool importMaterialCatalog)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ImportMaterialCatalog"} ;
			object[] objParamValues = {importMaterialCatalog} ; 
			SqlDbType[] paramTypes = {SqlDbType.TinyInt} ;
			object objReturn = new object() ; 
			pDataAccess.ExecuteSPReturnParam("spFinishSysInit",sParams,objParamValues,paramTypes,SqlDbType.Int,ref objReturn) ;

			return (int)objReturn ;
//			//set the commandText
//			mCommandText = "spFinishSysInit";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@MaterialConfigType", SqlDbType.TinyInt,0).Value=materialConfigType;
//			base.AddParameter("@ImportMaterialCatalog", SqlDbType.Bit ).Value=importMaterialCatalog;
//			base.AddParameter("@ReturnValue",SqlDbType.Int).Direction=ParameterDirection.ReturnValue ; 
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the 受影响的记录数
//				base.ExecuteNonQuery();
//				return (int)base.GetParameterValue("@ReturnValue") ;
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
	
		}

		#region SelectSysCurrency Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSysCurrency 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSysCurrency  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spSelectSysCurrency]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysCurrency]
		///-- Date Generated: 2005年6月9日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///create procedure spSelectSysCurrency
		///as
		///select 
		///	top 1 
		///	NaturalCurrencyID,
		///	StandardCurrencyID 
		///from 
		///	SysCurrency
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString NaturalCurrencyID, SqlString StandardCurrencyID,
		/// </remarks>
		public SqlDataReader SelectSysCurrency()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSysCurrency",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSysCurrency";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region  InsertUpdateSysCurrency Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertUpdateSysCurrency 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="naturalCurrencyID">变量 naturalCurrencyID: 用于设置参数 '@NaturalCurrencyID' 给存储过程spInsertUpdateSysCurrency </param>	
		///<param name="standardCurrencyID">变量 standardCurrencyID: 用于设置参数 '@StandardCurrencyID' 给存储过程spInsertUpdateSysCurrency </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertUpdateSysCurrency  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spInsertUpdateSysCurrency]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUpdateSysCurrency]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///create procedure spInsertUpdateSysCurrency
		///	@NaturalCurrencyID nvarchar(10),
		///	@StandardCurrencyID nvarchar(10)
		///as
		///if exists(select * from SysCurrency)
		///begin
		///update
		///	SysCurrency
		///set
		///	NaturalCurrencyID=@NaturalCurrencyID,
		///	StandardCurrencyID=@StandardCurrencyID
		///	
		///end
		///else
		///begin
		///	insert into 
		///		SysCurrency(NaturalCurrencyID,StandardCurrencyID)
		///	values
		///		(@NaturalCurrencyID,@StandardCurrencyID)	
		///end
		///
		///
		/// </code>
		/// </remarks>
		public int InsertUpdateSysCurrency( string naturalCurrencyID, string standardCurrencyID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"NaturalCurrencyID","StandardCurrencyID"} ;
			object[] objParamValues = {naturalCurrencyID,standardCurrencyID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			pDataAccess.ExecuteSP("spInsertUpdateSysCurrency",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertUpdateSysCurrency";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@NaturalCurrencyID", SqlDbType.NVarChar,10).Value=naturalCurrencyID;
//			base.AddParameter("@StandardCurrencyID", SqlDbType.NVarChar,10).Value=standardCurrencyID;
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

		#region SelectSysMaterialIDConfig Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSysMaterialIDConfig 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSysMaterialIDConfig  ] 如下：
		/// <code>
		/// 
		///--endregion
		///
		///--region [dbo].[spSelectSysMaterialIDConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysMaterialIDConfig]
		///-- Date Generated: 2005年6月9日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///create procedure spSelectSysMaterialIDConfig
		///as
		///select  top 1 
		///	MaterialIDLength,
		///	MaterialIDSegNumber
		///	from 
		///	SysMaterialIDConfig
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt16 MaterialIDLength, SqlInt16 MaterialIDSegNumber,
		/// </remarks>
		public SqlDataReader SelectSysMaterialIDConfig()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSysMaterialIDConfig",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSysMaterialIDConfig";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectSysMaterialIDConfigBakVersion Methods
	
		/// <summary>
		/// 获取内置预留的配置
		/// </summary>
		/// <returns></returns>
		public SqlDataReader SelectSysMaterialIDConfigBakVersion()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;
			return pDataAccess.GetDataReader("select top 1 MaterialIDLength,MaterialIDSegNumber from SysMaterialIDConfigBakVersion") ; 
//			//set the commandText
//			mCommandText = @"
//			select  top 1 
//			MaterialIDLength,
//			MaterialIDSegNumber
//			from 
//			SysMaterialIDConfigBakVersion";
//			//set the CommandType
//			mCommandType = CommandType.Text  ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region  InsertUpdateSysMaterialIDConfig Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertUpdateSysMaterialIDConfig 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="materialIDLength">变量 materialIDLength: 用于设置参数 '@MaterialIDLength' 给存储过程spInsertUpdateSysMaterialIDConfig </param>	
		///<param name="materialIDSegNumber">变量 materialIDSegNumber: 用于设置参数 '@MaterialIDSegNumber' 给存储过程spInsertUpdateSysMaterialIDConfig </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertUpdateSysMaterialIDConfig  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spInsertUpdateSysMaterialIDConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUpdateSysMaterialIDConfig]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///--
		///create procedure spInsertUpdateSysMaterialIDConfig
		///	@MaterialIDLength smallint,
		///	@MaterialIDSegNumber smallint
		///as
		///if exists (select * from SysMaterialIDConfig)
		///begin
		///	update  
		///		SysMaterialIDConfig 
		///	set
		///		MaterialIDLength = @MaterialIDLength,
		///		MaterialIDSegNumber = @MaterialIDSegNumber
		///end
		///else
		///begin
		///	insert into SysMaterialIDConfig(MaterialIDLength,MaterialIDSegNumber)
		///	values(@MaterialIDLength,@MaterialIDSegNumber)
		///end
		///
		///
		/// </code>
		/// </remarks>
		public int InsertUpdateSysMaterialIDConfig( short materialIDLength, short materialIDSegNumber)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"MaterialIDLength","MaterialIDSegNumber"} ;
			object[] objParamValues = {materialIDLength,materialIDSegNumber} ; 
			SqlDbType[] paramTypes = {SqlDbType.SmallInt,SqlDbType.SmallInt} ;
			pDataAccess.ExecuteSP("spInsertUpdateSysMaterialIDConfig",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertUpdateSysMaterialIDConfig";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@MaterialIDLength", SqlDbType.SmallInt,0).Value=materialIDLength;
//			base.AddParameter("@MaterialIDSegNumber", SqlDbType.SmallInt,0).Value=materialIDSegNumber;
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

		#region  DeleteSysMaterialIDSegConfigsAll Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteSysMaterialIDSegConfigsAll 并返回数据库中受影响的记录数
		/// </summary>
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteSysMaterialIDSegConfigsAll  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///
		///--region [dbo].[spDeleteSysMaterialIDSegConfigsAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spDeleteSysMaterialIDSegConfigsAll]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///--
		///create procedure spDeleteSysMaterialIDSegConfigsAll
		///	
		///as
		///if exists (select * from SysMaterialIDSegConfig)
		///begin
		///	delete from SysMaterialIDSegConfig
		///end
		///
		/// </code>
		/// </remarks>
		public int DeleteSysMaterialIDSegConfigsAll()
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			pDataAccess.ExecuteSP("spDeleteSysMaterialIDSegConfigsAll",sParams,objParamValues,paramTypes) ;
//			//set the commandText
//			mCommandText = "spDeleteSysMaterialIDSegConfigsAll";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
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
			return 0 ;
		}
		#endregion

		#region SelectSysMaterialIDSegConfig Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSysMaterialIDSegConfig 并返回包含记录的数据读取器.
		/// </summary>
		/// <param name="segID" ></param>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSysMaterialIDSegConfig  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///
		///
		///--region [dbo].[spSelectSysMaterialIDSegConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysMaterialIDSegConfig]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///--
		///create procedure spSelectSysMaterialIDSegConfig
		///					 @SegID smallint
		///								as
		///select
		///	SegID,
		///	StartIndex,
		///	Length,
		///	SegFormula,
		///	FullSegFormula,
		///	SegDescription,
		///	IsLeafClassSeg,
		///	IsLeafSeg
		///	from
		///	SysMaterialIDSegConfig
		///		where
		///		SegID=@SegID
		///				  go
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt16 SegID, SqlInt16 StartIndex, SqlInt16 Length, SqlString SegFormula, SqlString FullSegFormula, SqlString SegDescription, SqlBoolean IsLeafClassSeg, SqlBoolean IsLeafSeg,
		/// </remarks>
		public SqlDataReader SelectSysMaterialIDSegConfig(short segID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"SegID"} ;
			object[] objParamValues = {segID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSysMaterialIDSegConfig",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSysMaterialIDSegConfig";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			base.AddParameter ("@SegID",SqlDbType.SmallInt).Value=segID;
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectSysMaterialIDLeafSegConfig Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSysMaterialIDLeafSegConfig 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSysMaterialIDLeafSegConfig  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spSelectSysMaterialIDLeafSegConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysMaterialIDLeafSegConfig]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///--
		///create procedure spSelectSysMaterialIDLeafSegConfig
		///	
		///as
		///select
		///	SegID,
		///	StartIndex,
		///	Length,
		///	SegFormula,
		///	FullSegFormula,
		///	SegDescription,
		///	IsLeafClassSeg,
		///	IsLeafSeg
		///from
		///	SysMaterialIDSegConfig
		///where
		///	IsLeafSeg=1
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt16 SegID, SqlInt16 StartIndex, SqlInt16 Length, SqlString SegFormula, SqlString FullSegFormula, SqlString SegDescription, SqlBoolean IsLeafClassSeg, SqlBoolean IsLeafSeg,
		/// </remarks>
		public SqlDataReader SelectSysMaterialIDLeafSegConfig()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSysMaterialIDLeafSegConfig",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSysMaterialIDLeafSegConfig";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion
		
		#region SelectSysMaterialIDSegConfigsAll Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectSysMaterialIDSegConfigsAll 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectSysMaterialIDSegConfigsAll  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///
		///
		///--region [dbo].[spSelectSysMaterialIDSegConfigsAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysMaterialIDSegConfigsAll]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///--
		///create procedure spSelectSysMaterialIDSegConfigsAll
		///	
		///as
		///select
		///	SegID,
		///	StartIndex,
		///	Length,
		///	SegFormula,
		///	FullSegFormula,
		///	SegDescription,
		///	IsLeafClassSeg,
		///	IsLeafSeg
		///from
		///	SysMaterialIDSegConfig
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlInt16 SegID, SqlInt16 StartIndex, SqlInt16 Length, SqlString SegFormula, SqlString FullSegFormula, SqlString SegDescription, SqlBoolean IsLeafClassSeg, SqlBoolean IsLeafSeg,
		/// </remarks>
		public SqlDataReader SelectSysMaterialIDSegConfigsAll()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectSysMaterialIDSegConfigsAll",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectSysMaterialIDSegConfigsAll";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectSysMaterialIDSegConfigsBakVersionAll Methods
		
		///select
		///	SegID,
		///	StartIndex,
		///	Length,
		///	SegFormula,
		///	FullSegFormula,
		///	SegDescription,
		///	IsLeafClassSeg,
		///	IsLeafSeg
		///from
		///	SysMaterialIDSegConfig
		public SqlDataReader SelectSysMaterialIDSegConfigsBakVersionAll()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			return pDataAccess.GetDataReader("select SegID,StartIndex,	Length,SegFormula,FullSegFormula,SegDescription,IsLeafClassSeg,	IsLeafSeg from	SysMaterialIDSegConfigBakVersion") ; 
//			//set the commandText
//			mCommandText = @"			
//	select
//			SegID,
//			StartIndex,
//			Length,
//			SegFormula,
//			FullSegFormula,
//			SegDescription,
//			IsLeafClassSeg,
//			IsLeafSeg
//	from
//			SysMaterialIDSegConfigBakVersion";
//			//set the CommandType
//			mCommandType = CommandType.Text  ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region  InsertSysMaterialIDSegConfig Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertSysMaterialIDSegConfig 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="segID">变量 segID: 用于设置参数 '@SegID' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		///<param name="startIndex">变量 startIndex: 用于设置参数 '@StartIndex' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		///<param name="length">变量 length: 用于设置参数 '@Length' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		///<param name="segFormula">变量 segFormula: 用于设置参数 '@SegFormula' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		///<param name="fullSegFormula">变量 fullSegFormula: 用于设置参数 '@FullSegFormula' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		///<param name="segDescription">变量 segDescription: 用于设置参数 '@SegDescription' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		///<param name="isLeafClassSeg">变量 isLeafClassSeg: 用于设置参数 '@IsLeafClassSeg' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		///<param name="isLeafSeg">变量 isLeafSeg: 用于设置参数 '@IsLeafSeg' 给存储过程spInsertSysMaterialIDSegConfig </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertSysMaterialIDSegConfig  ] 如下：
		/// <code>
		/// --endregion
		///
		///
		///
		///
		///--region [dbo].[spInsertSysMaterialIDSegConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertSysMaterialIDSegConfig]
		///-- Date Generated: 2005年6月13日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///--
		///create procedure spInsertSysMaterialIDSegConfig
		///	@SegID smallint,
		///	@StartIndex smallint,
		///	@Length smallint,
		///	@SegFormula nvarchar(32),
		///	@FullSegFormula nvarchar(32),
		///	@SegDescription nvarchar(100),
		///	@IsLeafClassSeg bit,
		///	@IsLeafSeg bit
		///	
		///as
		///insert into SysMaterialIDSegConfig
		///(
		///	SegID,
		///	StartIndex,
		///	Length,
		///	SegFormula,
		///	FullSegFormula,
		///	SegDescription,
		///	IsLeafClassSeg,
		///	IsLeafSeg
		///)
		///values
		///(
		///	@SegID,
		///	@StartIndex,
		///	@Length,
		///	@SegFormula,
		///	@FullSegFormula,
		///	@SegDescription,
		///	@IsLeafClassSeg,
		///	@IsLeafSeg
		///)
		///
		/// </code>
		/// </remarks>
		public int InsertSysMaterialIDSegConfig( short segID, short startIndex, short length, string segFormula, string fullSegFormula, string segDescription, bool isLeafClassSeg, bool isLeafSeg)
		{

			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"SegID","StartIndex","Length","SegFormula","FullSegFormula","SegDescription","IsLeafClassSeg","IsLeafSeg"} ;
			object[] objParamValues = {segID,startIndex,length,segFormula,fullSegFormula,segDescription,isLeafClassSeg,isLeafSeg} ; 
			SqlDbType[] paramTypes = {SqlDbType.SmallInt,SqlDbType.SmallInt,SqlDbType.SmallInt,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Bit,SqlDbType.Bit} ;
			pDataAccess.ExecuteSP("spInsertSysMaterialIDSegConfig",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertSysMaterialIDSegConfig";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@SegID", SqlDbType.SmallInt,0).Value=segID;
//			base.AddParameter("@StartIndex", SqlDbType.SmallInt,0).Value=startIndex;
//			base.AddParameter("@Length", SqlDbType.SmallInt,0).Value=length;
//			base.AddParameter("@SegFormula", SqlDbType.NVarChar,32).Value=segFormula;
//			base.AddParameter("@FullSegFormula", SqlDbType.NVarChar,32).Value=fullSegFormula;
//			base.AddParameter("@SegDescription", SqlDbType.NVarChar,100).Value=segDescription;
//			base.AddParameter("@IsLeafClassSeg", SqlDbType.Bit,0).Value=isLeafClassSeg;
//			base.AddParameter("@IsLeafSeg", SqlDbType.Bit,0).Value=isLeafSeg;
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

	}
}
