/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Module Name: BusyAccess.SystemConfig.CDADepartment
-- Date Generated: 2005.6.6
-- Version List
--  Version 1.0 2005.7.1
Version 1.1 2005.7.15 Dou Zhi-cheng add Select...Paged functions
Version 1.2 2005.7.28 Dou Zhi-cheng add function GetDepartmentName;
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data;
using System.Data.SqlClient  ;
using Cnwit.Utility ;
namespace Business.SystemConfig
{
	/// <summary>
	/// database access class from department table
	/// </summary>
	public class CDADepartment
	{
		/// <summary>
		/// constructor
		/// </summary>
		public CDADepartment()
		{
			
		}

		#region SelectDepartmentsAll Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectDepartmentsAll 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>包含记录集的数据表</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectDepartmentsAll  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectDepartmentsAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///-- Procedure Name: [dbo].[spSelectDepartmentsAll]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectDepartmentsAll]
		///AS
		///
		///SET NOCOUNT ON
		///
		///SELECT
		///	[DepartmentID],
		///	[DepartmentName],
		///	[DepartmentDescription],
		///	[Principal],
		///	[Address],
		///	[Fax],
		///	[Tel],
		///	[Email],
		///	[Contact],
		///	[Status]
		///FROM
		///	[dbo].[Department]
		///ORDER BY
		///	DepartmentID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString DepartmentID, SqlString DepartmentName, SqlString DepartmentDescription, SqlString Principal, SqlString Address, SqlString Fax, SqlString Tel, SqlString Email, SqlString Contact, SqlByte Status,
		/// </remarks>
		public DataTable SelectDepartmentsAll()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectDepartmentsAll",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectDepartmentsAll";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultDataTable ;
	
		}
		#endregion


		#region SelectDepartmentsDynamic Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectDepartmentsDynamic 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="keyword">变量 keyword: 用于设置参数 '@Keyword' 给存储过程spSelectDepartmentsDynamic </param>	
		/// <returns>包含记录集的数据表</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectDepartmentsDynamic  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectDepartmentsDynamic]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Procedure Name: [dbo].[spSelectDepartmentsDynamic]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectDepartmentsDynamic]
		///(
		///	@Keyword nvarchar(50)
		///)
		///AS
		///
		///SELECT
		///	[DepartmentID],
		///	[DepartmentName],
		///	[DepartmentDescription],
		///	[Principal],
		///	[Address],
		///	[Fax],
		///	[Tel],
		///	[Email],
		///	[Contact],
		///	[Status]
		///FROM
		///	[dbo].[Department]
		///WHERE
		///	DepartmentID like '%'+@Keyword+'%'
		///	or DepartmentName like '%'+@Keyword+'%'
		///	or DepartmentDescription like '%'+@Keyword+'%'
		///order by DepartmentID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString DepartmentID, SqlString DepartmentName, SqlString DepartmentDescription, SqlString Principal, SqlString Address, SqlString Fax, SqlString Tel, SqlString Email, SqlString Contact, SqlByte Status,
		/// </remarks>
		public DataTable SelectDepartmentsDynamic( string keyword)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"Keyword"} ;
			object[] objParamValues = {keyword} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectDepartmentsDynamic",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectDepartmentsDynamic";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@Keyword", SqlDbType.NVarChar,50).Value=keyword;
//			//return the result reader
//			return base.ResultDataTable;
	
		}
		#endregion

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
		public SqlDataReader SelectDepartmentListPaged(string where,string orderBy,string ascOrDesc,int recordCount,int pageIndex,int pageSize)
		{
			string what=@" 
			DepartmentID,
			DepartmentName,			
			Principal,
			Address,
			Fax,
			Tel,
			Email,
			Contact,
			[Status]
		";
			string from=" Department ";
			
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPaged(what,from,where,orderBy,ascOrDesc,recordCount,pageIndex,pageSize);
		}

		/// <summary>
		/// get record count
		/// </summary>
		/// <param name="where"></param>
		/// <returns></returns>
		public int SelectDepartmentListPagedTotalCount(string where)
		{
			
			string from=" Department ";
		
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPagedTotalCount(from,where);
			
		}

		#region SelectDepartmentsIDAndName Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectDepartmentsIDAndName 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectDepartmentsIDAndName  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectDepartmentsAll]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///-- Procedure Name: [dbo].[spSelectDepartmentsIDAndName]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectDepartmentsIDAndName]
		///AS
		///
		///SET NOCOUNT ON
		///
		///SELECT
		///	[DepartmentID],
		///	[DepartmentName],
		///	[Status]
		///FROM
		///	[dbo].[Department]
		///ORDER BY
		///	DepartmentID
		///
		///--endregion
		///
		///
		/// </code>code>
		/// 结果集中的列包括:
		/// SqlString DepartmentID, SqlString DepartmentName, SqlByte Status,
		/// </remarks>
		public SqlDataReader SelectDepartmentsIDAndName()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectBI_Department",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectDepartmentsIDAndName";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectDepartment Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectDepartment 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="departmentID">变量 departmentID: 用于设置参数 '@DepartmentID' 给存储过程spSelectDepartment </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectDepartment  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectDepartment]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///-- Procedure Name: [dbo].[spSelectDepartment]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectDepartment]
		///	@DepartmentID nvarchar(50)
		///AS
		///
		///SET NOCOUNT ON
		///SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///SELECT
		///	[DepartmentID],
		///	[DepartmentName],
		///	[DepartmentDescription],
		///	[Principal],
		///	[Address],
		///	[Fax],
		///	[Tel],
		///	[Email],
		///	[Contact],
		///	[Status]
		///FROM
		///	[dbo].[Department]
		///WHERE
		///	[DepartmentID] = @DepartmentID
		///
		///--endregion
		///
		///
		/// </code>code>
		/// 结果集中的列包括:
		/// SqlString DepartmentID, SqlString DepartmentName, SqlString DepartmentDescription, SqlString Principal, SqlString Address, SqlString Fax, SqlString Tel, SqlString Email, SqlString Contact, SqlByte Status,
		/// </remarks>
		public SqlDataReader SelectDepartment( string departmentID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"DepartmentID"} ;
			object[] objParamValues = {departmentID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectDepartment",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectDepartment";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=departmentID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region GetDepartmentName
		public string GetDepartmentName(string depID)
		{
			string depName="";
			SqlDataReader dr= SelectDepartment(depID);
			if(dr.Read() )
			{
				depName=(string)dr["DepartmentName"];
			}
			dr.Close();
				
			return depName;
		}
		#endregion

		#region  InsertDepartment Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertDepartment 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="departmentID">变量 departmentID: 用于设置参数 '@DepartmentID' 给存储过程spInsertDepartment </param>	
		///<param name="departmentName">变量 departmentName: 用于设置参数 '@DepartmentName' 给存储过程spInsertDepartment </param>	
		///<param name="departmentDescription">变量 departmentDescription: 用于设置参数 '@DepartmentDescription' 给存储过程spInsertDepartment </param>	
		///<param name="principal">变量 principal: 用于设置参数 '@Principal' 给存储过程spInsertDepartment </param>	
		///<param name="address">变量 address: 用于设置参数 '@Address' 给存储过程spInsertDepartment </param>	
		///<param name="fax">变量 fax: 用于设置参数 '@Fax' 给存储过程spInsertDepartment </param>	
		///<param name="tel">变量 tel: 用于设置参数 '@Tel' 给存储过程spInsertDepartment </param>	
		///<param name="email">变量 email: 用于设置参数 '@Email' 给存储过程spInsertDepartment </param>	
		///<param name="contact">变量 contact: 用于设置参数 '@Contact' 给存储过程spInsertDepartment </param>	
		///<param name="status">变量 status: 用于设置参数 '@Status' 给存储过程spInsertDepartment </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertDepartment  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spInsertDepartment]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///-- Procedure Name: [dbo].[spInsertDepartment]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spInsertDepartment]
		///	@DepartmentID nvarchar(50),
		///	@DepartmentName nvarchar(255),
		///	@DepartmentDescription ntext,
		///	@Principal nvarchar(64),
		///	@Address nvarchar(512),
		///	@Fax nvarchar(64),
		///	@Tel nvarchar(64),
		///	@Email nvarchar(64),
		///	@Contact nvarchar(64),
		///	@Status tinyint
		///AS
		///
		///SET NOCOUNT ON
		///
		///INSERT INTO [dbo].[Department] (
		///	[DepartmentID],
		///	[DepartmentName],
		///	[DepartmentDescription],
		///	[Principal],
		///	[Address],
		///	[Fax],
		///	[Tel],
		///	[Email],
		///	[Contact],
		///	[Status]
		///) VALUES (
		///	@DepartmentID,
		///	@DepartmentName,
		///	@DepartmentDescription,
		///	@Principal,
		///	@Address,
		///	@Fax,
		///	@Tel,
		///	@Email,
		///	@Contact,
		///	@Status
		///)
		///
		///--endregion
		///
		///
		/// </code>code>
		/// </remarks>
		public int InsertDepartment( string departmentID, string departmentName, string departmentDescription, string principal, string address, string fax, string tel, string email, string contact, byte status)
		{

			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"DepartmentID","DepartmentName","DepartmentDescription","Principal","Address","Fax","Tel","Email","Contact","Status"} ;
			object[] objParamValues = {departmentID,departmentName,departmentDescription,principal,address,fax,tel,email,contact,status} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NText,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.TinyInt} ;
			pDataAccess.ExecuteSP("spInsertDepartment",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertDepartment";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=departmentID;
//			base.AddParameter("@DepartmentName", SqlDbType.NVarChar,255).Value=departmentName;
//			base.AddParameter("@DepartmentDescription", SqlDbType.NText).Value=departmentDescription;
//			base.AddParameter("@Principal", SqlDbType.NVarChar,64).Value=principal;
//			base.AddParameter("@Address", SqlDbType.NVarChar,512).Value=address;
//			base.AddParameter("@Fax", SqlDbType.NVarChar,64).Value=fax;
//			base.AddParameter("@Tel", SqlDbType.NVarChar,64).Value=tel;
//			base.AddParameter("@Email", SqlDbType.NVarChar,64).Value=email;
//			base.AddParameter("@Contact", SqlDbType.NVarChar,64).Value=contact;
//			base.AddParameter("@Status", SqlDbType.TinyInt).Value=status;
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

		
		#region  UpdateDepartment Methods
		/// <summary>
		/// 此函数调用存储过程 spUpdateDepartment 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="departmentID">变量 departmentID: 用于设置参数 '@DepartmentID' 给存储过程spUpdateDepartment </param>	
		///<param name="departmentName">变量 departmentName: 用于设置参数 '@DepartmentName' 给存储过程spUpdateDepartment </param>	
		///<param name="departmentDescription">变量 departmentDescription: 用于设置参数 '@DepartmentDescription' 给存储过程spUpdateDepartment </param>	
		///<param name="principal">变量 principal: 用于设置参数 '@Principal' 给存储过程spUpdateDepartment </param>	
		///<param name="address">变量 address: 用于设置参数 '@Address' 给存储过程spUpdateDepartment </param>	
		///<param name="fax">变量 fax: 用于设置参数 '@Fax' 给存储过程spUpdateDepartment </param>	
		///<param name="tel">变量 tel: 用于设置参数 '@Tel' 给存储过程spUpdateDepartment </param>	
		///<param name="email">变量 email: 用于设置参数 '@Email' 给存储过程spUpdateDepartment </param>	
		///<param name="contact">变量 contact: 用于设置参数 '@Contact' 给存储过程spUpdateDepartment </param>	
		///<param name="status">变量 status: 用于设置参数 '@Status' 给存储过程spUpdateDepartment </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spUpdateDepartment  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spUpdateDepartment]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///-- Procedure Name: [dbo].[spUpdateDepartment]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spUpdateDepartment]
		///	@DepartmentID nvarchar(50),
		///	@DepartmentName nvarchar(255),
		///	@DepartmentDescription ntext,
		///	@Principal nvarchar(64),
		///	@Address nvarchar(512),
		///	@Fax nvarchar(64),
		///	@Tel nvarchar(64),
		///	@Email nvarchar(64),
		///	@Contact nvarchar(64),
		///	@Status tinyint
		///AS
		///
		///SET NOCOUNT ON
		///
		///UPDATE [dbo].[Department] SET
		///	[DepartmentName] = @DepartmentName,
		///	[DepartmentDescription] = @DepartmentDescription,
		///	[Principal] = @Principal,
		///	[Address] = @Address,
		///	[Fax] = @Fax,
		///	[Tel] = @Tel,
		///	[Email] = @Email,
		///	[Contact] = @Contact,
		///	[Status] = @Status
		///WHERE
		///	[DepartmentID] = @DepartmentID
		///
		///--endregion
		///
		///
		/// </code>code>
		/// </remarks>
		public int UpdateDepartment( string departmentID, string departmentName, string departmentDescription, string principal, string address, string fax, string tel, string email, string contact, byte status)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"DepartmentID","DepartmentName","DepartmentDescription","Principal","Address","Fax","Tel","Email","Contact","Status"} ;
			object[] objParamValues = {departmentID,departmentName,departmentDescription,principal,address,fax,tel,email,contact,status} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NText,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.TinyInt} ;
			pDataAccess.ExecuteSP("spUpdateDepartment",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spUpdateDepartment";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=departmentID;
//			base.AddParameter("@DepartmentName", SqlDbType.NVarChar,255).Value=departmentName;
//			base.AddParameter("@DepartmentDescription", SqlDbType.NText,1073741823).Value=departmentDescription;
//			base.AddParameter("@Principal", SqlDbType.NVarChar,64).Value=principal;
//			base.AddParameter("@Address", SqlDbType.NVarChar,512).Value=address;
//			base.AddParameter("@Fax", SqlDbType.NVarChar,64).Value=fax;
//			base.AddParameter("@Tel", SqlDbType.NVarChar,64).Value=tel;
//			base.AddParameter("@Email", SqlDbType.NVarChar,64).Value=email;
//			base.AddParameter("@Contact", SqlDbType.NVarChar,64).Value=contact;
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

		#region  DeleteDepartment Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteDepartment 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="departmentID">变量 departmentID: 用于设置参数 '@DepartmentID' 给存储过程spDeleteDepartment </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteDepartment  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spDeleteDepartment]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///
		///
		///-- Procedure Name: [dbo].[spDeleteDepartment]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spDeleteDepartment]
		///	@DepartmentID nvarchar(50)
		///AS
		///
		///SET NOCOUNT ON
		///
		///DELETE FROM [dbo].[Department]
		///WHERE
		///	[DepartmentID] = @DepartmentID
		///
		///--endregion
		///
		///
		/// </code>code>
		/// </remarks>
		public int DeleteDepartment( string departmentID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"DepartmentID"} ;
			object[] objParamValues = {departmentID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			pDataAccess.ExecuteSP("spDeleteDepartment",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeleteDepartment";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=departmentID;
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
