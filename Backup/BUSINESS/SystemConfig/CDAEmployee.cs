/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Module Name: BusyAccess.SystemConfig.CDAEmployee
-- Date Generated: 2005.6.6
-- Version List
--Version 1.0 2005.7.1
--Version 1.1 2007.7.12 Dou Zhi-cheng add GetEmployeeName method
Version 1.2 2005.7.15 Dou Zhi-cheng add Select...Paged functions

------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data;
using System.Data.SqlClient;
using Cnwit.Utility ;
namespace Business.SystemConfig
{
	/// <summary>
	/// Summary description for CDAEmployee.
	/// </summary>
	public class CDAEmployee 
	{
		/// <summary>
		/// constructor
		/// </summary>
		public CDAEmployee()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region SelectEmployeesDynamic Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectEmployeesDynamic 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="departmentID">变量 departmentID: 用于设置参数 '@DepartmentID' 给存储过程spSelectEmployeesDynamic </param>	
		///<param name="keyword">变量 keyword: 用于设置参数 '@Keyword' 给存储过程spSelectEmployeesDynamic </param>	
		/// <returns>包含记录集的数据表</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectEmployeesDynamic  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectEmployeesDynamic]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectEmployeesDynamic]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectEmployeesDynamic]
		///	@DepartmentID nvarchar(50),
		///	@Keyword nvarchar(50)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///if @DepartmentID ='' or @DepartmentID is null
		///begin
		///	SELECT
		///		[EmployeeID],
		///		[EmployeeName],
		///		[FullName],
		///		[EmployeeDescription],
		///		Employee.[DepartmentID],
		///		Department.DepartmentName,
		///		Employee.[Fax],
		///		Employee.[Tel],
		///		Employee.[Email],
		///		Employee.[Mobile],
		///		Employee.[Status]
		///	FROM
		///		[dbo].[Employee]
		///	INNER JOIN Department
		///	ON	Department.DepartmentID = Employee.DepartmentID
		///	WHERE
		///		EmployeeID like '%'+@Keyword+'%'
		///		or EmployeeName like '%'+@Keyword+'%'
		///		or FullName like '%'+@Keyword+'%'
		///		or EmployeeDescription like '%'+@Keyword+'%'
		///	
		///end
		///else
		///begin
		///	SELECT
		///		[EmployeeID],
		///		[EmployeeName],
		///		[FullName],
		///		[EmployeeDescription],
		///		Employee.[DepartmentID],
		///		Department.DepartmentName,
		///		Employee.[Fax],
		///		Employee.[Tel],
		///		Employee.[Email],
		///		Employee.[Mobile],
		///		Employee.[Status]
		///	FROM
		///		[dbo].[Employee]
		///	INNER JOIN Department
		///	ON	Department.DepartmentID = Employee.DepartmentID
		///	WHERE
		///		Employee.DepartmentID = @DepartmentID
		///		and
		///		(
		///		EmployeeID like '%'+@Keyword+'%'
		///		or EmployeeName like '%'+@Keyword+'%'
		///		or FullName like '%'+@Keyword+'%'
		///		or EmployeeDescription like '%'+@Keyword+'%'
		///		)
		///
		///end
		///
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString EmployeeID, SqlString EmployeeName, SqlString FullName, SqlString EmployeeDescription, SqlString DepartmentID, SqlString DepartmentName, SqlString Fax, SqlString Tel, SqlString Email, SqlString Mobile, SqlByte Status,
		/// </remarks>
		public DataTable SelectEmployeesDynamic( string departmentID, string keyword)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"DepartmentID","Keyword"} ;
			object[] objParamValues = {departmentID,keyword} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectEmployeesDynamic",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectEmployeesDynamic";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=departmentID;
//			base.AddParameter("@Keyword", SqlDbType.NVarChar,50).Value=keyword;
//			//return the result reader
//			return base.ResultDataTable ;
	
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
				[EmployeeID],
				[EmployeeName],
				[FullName],
				[EmployeeDescription],
				Employee.[DepartmentID],
				Department.DepartmentName,
				Employee.[Fax],
				Employee.[Tel],
				Employee.[Email],
				Employee.[Mobile],
				Employee.[PositionType],
				TypePosition.TypeDescription as Position,
				Employee.[Sex],
				Employee.[Status]
			";
			string from=@" 		
				Employee
				INNER JOIN Department ON Department.DepartmentID = Employee.DepartmentID
				INNER JOIN TypePosition ON 	TypePosition.TypeID=Employee.PositionType
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
		public int SelectDepartmentListPagedTotalCount(string where)
		{
			
			string from=@" 				
				Employee
				INNER JOIN Department ON Department.DepartmentID = Employee.DepartmentID
				INNER JOIN TypePosition ON 	TypePosition.TypeID=Employee.PositionType
				";
		
			//invoke CDACommon.SelectListPaged to return the SqlDataReader
			Business.CDACommon dac=new  CDACommon();
			return dac.SelectListPagedTotalCount(from,where);
			
		}
		public SqlDataReader SelectEmployeesByDepartment(string DepartmentID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"DepartmentID"} ;
			object[] objParamValues = {DepartmentID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectEmployeeByDepartment",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectEmployeeByDepartment";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=DepartmentID;
//			//return the result reader
//			return base.ResultReader;
	
		}

		#region SelectEmployeesAllIDAndName Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectEmployeesAllIDAndName 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectEmployeesAllIDAndName  ] 如下：
		/// <code>
		/// 
		///
		///--region [dbo].[spSelectEmployeesAllIDAndName]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectEmployeesAllIDAndName]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectEmployeesAllIDAndName]
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[EmployeeID],
		///	[EmployeeName]
		///FROM
		///	[dbo].[Employee]
		///ORDER BY
		///	EmployeeName
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString EmployeeID, SqlString EmployeeName,
		/// </remarks>
		public SqlDataReader SelectEmployeesAllIDAndName()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectEmployeesAllIDAndName",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectEmployeesAllIDAndName";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion

		#region SelectEmployee Methods
		/// <summary>
		/// 此函数调用存储过程 spSelectEmployee 并返回包含记录的数据读取器.
		/// </summary>
		///<param name="employeeID">变量 employeeID: 用于设置参数 '@EmployeeID' 给存储过程spSelectEmployee </param>	
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spSelectEmployee  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spSelectEmployee]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spSelectEmployee]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spSelectEmployee]
		///	@EmployeeID nvarchar(32)
		///AS
		///
		///--SET NOCOUNT ON
		///--SET TRANSACTION ISOLATION LEVEL READ COMMITTED
		///
		///
		///SELECT
		///	[EmployeeID],
		///	[EmployeeName],
		///	[FullName],
		///	[EmployeeDescription],
		///	[DepartmentID],
		///	[Fax],
		///	[Tel],
		///	[Email],
		///	[Mobile],
		///	[Status]
		///FROM
		///	[dbo].[Employee]
		///WHERE
		///	[EmployeeID] = @EmployeeID
		///
		///--endregion
		///
		///
		/// </code>
		/// 结果集中的列包括:
		/// SqlString EmployeeID, SqlString EmployeeName, SqlString FullName, SqlString EmployeeDescription, SqlString DepartmentID, SqlString Fax, SqlString Tel, SqlString Email, SqlString Mobile, SqlByte Status,
		/// </remarks>
		public SqlDataReader SelectEmployee( string employeeID)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"EmployeeID"} ;
			object[] objParamValues = {employeeID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectEmployee",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectEmployee";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@EmployeeID", SqlDbType.NVarChar,32).Value=employeeID;
//			//return the result reader
//			return base.ResultReader;
	
		}
		#endregion


		#region  InsertEmployee Methods
		/// <summary>
		/// 此函数调用存储过程 spInsertEmployee 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="employeeID">变量 employeeID: 用于设置参数 '@EmployeeID' 给存储过程spInsertEmployee </param>	
		///<param name="employeeName">变量 employeeName: 用于设置参数 '@EmployeeName' 给存储过程spInsertEmployee </param>	
		///<param name="fullName">变量 fullName: 用于设置参数 '@FullName' 给存储过程spInsertEmployee </param>	
		///<param name="employeeDescription">变量 employeeDescription: 用于设置参数 '@EmployeeDescription' 给存储过程spInsertEmployee </param>	
		///<param name="departmentID">变量 departmentID: 用于设置参数 '@DepartmentID' 给存储过程spInsertEmployee </param>	
		///<param name="fax">变量 fax: 用于设置参数 '@Fax' 给存储过程spInsertEmployee </param>	
		///<param name="tel">变量 tel: 用于设置参数 '@Tel' 给存储过程spInsertEmployee </param>	
		///<param name="email">变量 email: 用于设置参数 '@Email' 给存储过程spInsertEmployee </param>	
		///<param name="mobile">变量 mobile: 用于设置参数 '@Mobile' 给存储过程spInsertEmployee </param>	
		///<param name="status">变量 status: 用于设置参数 '@Status' 给存储过程spInsertEmployee </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spInsertEmployee  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spInsertEmployee]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spInsertEmployee]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spInsertEmployee]
		///	@EmployeeID nvarchar(32),
		///	@EmployeeName nvarchar(64),
		///	@FullName nvarchar(512),
		///	@EmployeeDescription nvarchar(1024),
		///	@DepartmentID nvarchar(50),
		///	@Fax nvarchar(64),
		///	@Tel nvarchar(64),
		///	@Email nvarchar(64),
		///	@Mobile nvarchar(64),
		///	@Status tinyint
		///AS
		///
		///--SET NOCOUNT ON
		///
		///INSERT INTO [dbo].[Employee] (
		///	[EmployeeID],
		///	[EmployeeName],
		///	[FullName],
		///	[EmployeeDescription],
		///	[DepartmentID],
		///	[Fax],
		///	[Tel],
		///	[Email],
		///	[Mobile],
		///	[Status]
		///) VALUES (
		///	@EmployeeID,
		///	@EmployeeName,
		///	@FullName,
		///	@EmployeeDescription,
		///	@DepartmentID,
		///	@Fax,
		///	@Tel,
		///	@Email,
		///	@Mobile,
		///	@Status
		///)
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int InsertEmployee( string employeeID, string employeeName, string fullName, string employeeDescription, string departmentID, string fax, string tel, string email, string mobile,int PositionType,bool Sex, byte status)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"EmployeeID","EmployeeName","FullName","EmployeeDescription","DepartmentID","Fax","Tel","Email","Mobile","PositionType","Sex","Status"} ;
			object[] objParamValues = {employeeID,employeeName,fullName,employeeDescription,departmentID,fax,tel,email,mobile,PositionType,Sex,status} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Bit,SqlDbType.TinyInt} ;
			pDataAccess.ExecuteSP("spInsertEmployee",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertEmployee";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@EmployeeID", SqlDbType.NVarChar,32).Value=employeeID;
//			base.AddParameter("@EmployeeName", SqlDbType.NVarChar,64).Value=employeeName;
//			base.AddParameter("@FullName", SqlDbType.NVarChar,512).Value=fullName;
//			base.AddParameter("@EmployeeDescription", SqlDbType.NVarChar,1024).Value=employeeDescription;
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=departmentID;
//			base.AddParameter("@Fax", SqlDbType.NVarChar,64).Value=fax;
//			base.AddParameter("@Tel", SqlDbType.NVarChar,64).Value=tel;
//			base.AddParameter("@Email", SqlDbType.NVarChar,64).Value=email;
//			base.AddParameter("@Mobile", SqlDbType.NVarChar,64).Value=mobile;
//			base.AddParameter("@PositionType", SqlDbType.Int).Value=PositionType;
//			base.AddParameter("@Sex", SqlDbType.Bit).Value=Sex;
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


		#region  UpdateEmployee Methods
		/// <summary>
		/// 此函数调用存储过程 spUpdateEmployee 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="employeeID">变量 employeeID: 用于设置参数 '@EmployeeID' 给存储过程spUpdateEmployee </param>	
		///<param name="employeeName">变量 employeeName: 用于设置参数 '@EmployeeName' 给存储过程spUpdateEmployee </param>	
		///<param name="fullName">变量 fullName: 用于设置参数 '@FullName' 给存储过程spUpdateEmployee </param>	
		///<param name="employeeDescription">变量 employeeDescription: 用于设置参数 '@EmployeeDescription' 给存储过程spUpdateEmployee </param>	
		///<param name="departmentID">变量 departmentID: 用于设置参数 '@DepartmentID' 给存储过程spUpdateEmployee </param>	
		///<param name="fax">变量 fax: 用于设置参数 '@Fax' 给存储过程spUpdateEmployee </param>	
		///<param name="tel">变量 tel: 用于设置参数 '@Tel' 给存储过程spUpdateEmployee </param>	
		///<param name="email">变量 email: 用于设置参数 '@Email' 给存储过程spUpdateEmployee </param>	
		///<param name="mobile">变量 mobile: 用于设置参数 '@Mobile' 给存储过程spUpdateEmployee </param>	
		///<param name="status">变量 status: 用于设置参数 '@Status' 给存储过程spUpdateEmployee </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spUpdateEmployee  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spUpdateEmployee]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spUpdateEmployee]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spUpdateEmployee]
		///	@EmployeeID nvarchar(32),
		///	@EmployeeName nvarchar(64),
		///	@FullName nvarchar(512),
		///	@EmployeeDescription nvarchar(1024),
		///	@DepartmentID nvarchar(50),
		///	@Fax nvarchar(64),
		///	@Tel nvarchar(64),
		///	@Email nvarchar(64),
		///	@Mobile nvarchar(64),
		///	@Status tinyint
		///AS
		///
		///--SET NOCOUNT ON
		///
		///UPDATE [dbo].[Employee] SET
		///	[EmployeeName] = @EmployeeName,
		///	[FullName] = @FullName,
		///	[EmployeeDescription] = @EmployeeDescription,
		///	[DepartmentID] = @DepartmentID,
		///	[Fax] = @Fax,
		///	[Tel] = @Tel,
		///	[Email] = @Email,
		///	[Mobile] = @Mobile,
		///	[Status] = @Status
		///WHERE
		///	[EmployeeID] = @EmployeeID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int UpdateEmployee( string employeeID, string employeeName, string fullName, string employeeDescription, string departmentID, string fax, string tel, string email, string mobile,int PositionType,bool Sex, byte status)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"EmployeeID","EmployeeName","FullName","EmployeeDescription","DepartmentID","Fax","Tel","Email","Mobile","PositionType","Sex","Status"} ;
			object[] objParamValues = {employeeID,employeeName,fullName,employeeDescription,departmentID,fax,tel,email,mobile,PositionType,Sex,status} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Bit,SqlDbType.TinyInt} ;
			pDataAccess.ExecuteSP("spUpdateEmployee",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spUpdateEmployee";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@EmployeeID", SqlDbType.NVarChar,32).Value=employeeID;
//			base.AddParameter("@EmployeeName", SqlDbType.NVarChar,64).Value=employeeName;
//			base.AddParameter("@FullName", SqlDbType.NVarChar,512).Value=fullName;
//			base.AddParameter("@EmployeeDescription", SqlDbType.NVarChar,1024).Value=employeeDescription;
//			base.AddParameter("@DepartmentID", SqlDbType.NVarChar,50).Value=departmentID;
//			base.AddParameter("@Fax", SqlDbType.NVarChar,64).Value=fax;
//			base.AddParameter("@Tel", SqlDbType.NVarChar,64).Value=tel;
//			base.AddParameter("@Email", SqlDbType.NVarChar,64).Value=email;
//			base.AddParameter("@Mobile", SqlDbType.NVarChar,64).Value=mobile;
//			base.AddParameter("@PositionType", SqlDbType.Int).Value=PositionType;
//			base.AddParameter("@Sex", SqlDbType.Bit).Value=Sex;
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

		#region  DeleteEmployee Methods
		/// <summary>
		/// 此函数调用存储过程 spDeleteEmployee 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="employeeID">变量 employeeID: 用于设置参数 '@EmployeeID' 给存储过程spDeleteEmployee </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spDeleteEmployee  ] 如下：
		/// <code>
		/// 
		///--region [dbo].[spDeleteEmployee]
		///
		///------------------------------------------------------------------------------------------------------------------------
		///-- Create by:      Dou Zhi-Cheng
		///-- Procedure Name: [dbo].[spDeleteEmployee]
		///-- Date Generated: 2005年6月6日
		///------------------------------------------------------------------------------------------------------------------------
		///
		///CREATE PROCEDURE [dbo].[spDeleteEmployee]
		///	@EmployeeID nvarchar(32)
		///AS
		///
		///--SET NOCOUNT ON
		///
		///DELETE FROM [dbo].[Employee]
		///WHERE
		///	[EmployeeID] = @EmployeeID
		///
		///--endregion
		///
		///
		/// </code>
		/// </remarks>
		public int DeleteEmployee( string employeeID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"EmployeeID"} ;
			object[] objParamValues = {employeeID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			pDataAccess.ExecuteSP("spDeleteEmployee",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeleteEmployee";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("@EmployeeID", SqlDbType.NVarChar,32).Value=employeeID;
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

		public string GetEmployeeName(string empID)
		{
			string empName="";
			SqlDataReader r=this.SelectEmployee(empID) ;
			if(r.Read() )
			{
				empName=(string)r["EmployeeName"];
			}
			r.Close() ;
			return empName;
		}
	}
}
