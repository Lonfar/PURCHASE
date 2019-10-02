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
		/// �˺������ô洢���� spSelectSysInitStatus �����ذ�����¼�����ݶ�ȡ��.
		/// </summary>
		/// <returns>byte</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spSelectSysInitStatus  ] ���£�
		/// <code>
		/// 
		///--endregion
		///
		///--region [dbo].[spSelectSysInitStatus]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysInitStatus]
		///-- Date Generated: 2005��6��9��
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
		/// ������е��а���:
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
		/// �˺������ô洢���� spInsertUpdateSysInitStatus ���������ݿ�����Ӱ��ļ�¼��
		/// </summary>
		///<param name="initStatus">���� initStatus: �������ò��� '@InitStatus' ���洢����spInsertUpdateSysInitStatus </param>	
		/// <returns>��Ӱ��ļ�¼��</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spInsertUpdateSysInitStatus  ] ���£�
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spInsertUpdateSysInitStatus]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUpdateSysInitStatus]
		///-- Date Generated: 2005��6��13��
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
//				//Execute the sql command and return the ��Ӱ��ļ�¼��
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
//				//Execute the sql command and return the ��Ӱ��ļ�¼��
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
		/// �˺������ô洢���� spSelectSysCurrency �����ذ�����¼�����ݶ�ȡ��.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spSelectSysCurrency  ] ���£�
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spSelectSysCurrency]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysCurrency]
		///-- Date Generated: 2005��6��9��
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
		/// ������е��а���:
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
		/// �˺������ô洢���� spInsertUpdateSysCurrency ���������ݿ�����Ӱ��ļ�¼��
		/// </summary>
		///<param name="naturalCurrencyID">���� naturalCurrencyID: �������ò��� '@NaturalCurrencyID' ���洢����spInsertUpdateSysCurrency </param>	
		///<param name="standardCurrencyID">���� standardCurrencyID: �������ò��� '@StandardCurrencyID' ���洢����spInsertUpdateSysCurrency </param>	
		/// <returns>��Ӱ��ļ�¼��</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spInsertUpdateSysCurrency  ] ���£�
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spInsertUpdateSysCurrency]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUpdateSysCurrency]
		///-- Date Generated: 2005��6��13��
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
//				//Execute the sql command and return the ��Ӱ��ļ�¼��
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
		/// �˺������ô洢���� spSelectSysMaterialIDConfig �����ذ�����¼�����ݶ�ȡ��.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spSelectSysMaterialIDConfig  ] ���£�
		/// <code>
		/// 
		///--endregion
		///
		///--region [dbo].[spSelectSysMaterialIDConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysMaterialIDConfig]
		///-- Date Generated: 2005��6��9��
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
		/// ������е��а���:
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
		/// ��ȡ����Ԥ��������
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
		/// �˺������ô洢���� spInsertUpdateSysMaterialIDConfig ���������ݿ�����Ӱ��ļ�¼��
		/// </summary>
		///<param name="materialIDLength">���� materialIDLength: �������ò��� '@MaterialIDLength' ���洢����spInsertUpdateSysMaterialIDConfig </param>	
		///<param name="materialIDSegNumber">���� materialIDSegNumber: �������ò��� '@MaterialIDSegNumber' ���洢����spInsertUpdateSysMaterialIDConfig </param>	
		/// <returns>��Ӱ��ļ�¼��</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spInsertUpdateSysMaterialIDConfig  ] ���£�
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spInsertUpdateSysMaterialIDConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertUpdateSysMaterialIDConfig]
		///-- Date Generated: 2005��6��13��
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
//				//Execute the sql command and return the ��Ӱ��ļ�¼��
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
		/// �˺������ô洢���� spDeleteSysMaterialIDSegConfigsAll ���������ݿ�����Ӱ��ļ�¼��
		/// </summary>
		/// <returns>��Ӱ��ļ�¼��</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spDeleteSysMaterialIDSegConfigsAll  ] ���£�
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
		///-- Date Generated: 2005��6��13��
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
//				//Execute the sql command and return the ��Ӱ��ļ�¼��
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
		/// �˺������ô洢���� spSelectSysMaterialIDSegConfig �����ذ�����¼�����ݶ�ȡ��.
		/// </summary>
		/// <param name="segID" ></param>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spSelectSysMaterialIDSegConfig  ] ���£�
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
		///-- Date Generated: 2005��6��13��
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
		/// ������е��а���:
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
		/// �˺������ô洢���� spSelectSysMaterialIDLeafSegConfig �����ذ�����¼�����ݶ�ȡ��.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spSelectSysMaterialIDLeafSegConfig  ] ���£�
		/// <code>
		/// --endregion
		///
		///
		///--region [dbo].[spSelectSysMaterialIDLeafSegConfig]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectSysMaterialIDLeafSegConfig]
		///-- Date Generated: 2005��6��13��
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
		/// ������е��а���:
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
		/// �˺������ô洢���� spSelectSysMaterialIDSegConfigsAll �����ذ�����¼�����ݶ�ȡ��.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spSelectSysMaterialIDSegConfigsAll  ] ���£�
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
		///-- Date Generated: 2005��6��13��
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
		/// ������е��а���:
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
		/// �˺������ô洢���� spInsertSysMaterialIDSegConfig ���������ݿ�����Ӱ��ļ�¼��
		/// </summary>
		///<param name="segID">���� segID: �������ò��� '@SegID' ���洢����spInsertSysMaterialIDSegConfig </param>	
		///<param name="startIndex">���� startIndex: �������ò��� '@StartIndex' ���洢����spInsertSysMaterialIDSegConfig </param>	
		///<param name="length">���� length: �������ò��� '@Length' ���洢����spInsertSysMaterialIDSegConfig </param>	
		///<param name="segFormula">���� segFormula: �������ò��� '@SegFormula' ���洢����spInsertSysMaterialIDSegConfig </param>	
		///<param name="fullSegFormula">���� fullSegFormula: �������ò��� '@FullSegFormula' ���洢����spInsertSysMaterialIDSegConfig </param>	
		///<param name="segDescription">���� segDescription: �������ò��� '@SegDescription' ���洢����spInsertSysMaterialIDSegConfig </param>	
		///<param name="isLeafClassSeg">���� isLeafClassSeg: �������ò��� '@IsLeafClassSeg' ���洢����spInsertSysMaterialIDSegConfig </param>	
		///<param name="isLeafSeg">���� isLeafSeg: �������ò��� '@IsLeafSeg' ���洢����spInsertSysMaterialIDSegConfig </param>	
		/// <returns>��Ӱ��ļ�¼��</returns>
		/// <remarks>
		/// �洢����Դ�� [ �洢���� spInsertSysMaterialIDSegConfig  ] ���£�
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
		///-- Date Generated: 2005��6��13��
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
//				//Execute the sql command and return the ��Ӱ��ļ�¼��
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
