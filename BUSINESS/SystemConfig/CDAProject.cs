/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Module Name: BusyAccess.SystemConfig.CDAProject
-- Date Generated: 2005.6.6
-- Version List
--  Version 1.0 2005.7.1
--  version 1.1 Dou Zhi-cheng 2005.7.9 
    Rename method GetProject to SelectProject
    add method GetProjectName and GetProjectShortName
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data ;
using System.Data.SqlClient ;
using Cnwit.Utility ;
namespace Business.SystemConfig
{
	/// <summary>
	/// Data Access Class of Table 'Project',Maintain the Project(System) Infor
	/// 
	/// </summary>
	/// <remarks>
	/// 项目指的是使用该系统的项目，即系统用户。因此一个系统中有且仅有一条项目信息
	/// 该信息在系统安装时加入，在系统运行时只能进行联系方式等附加信息的修改，不能修改项目编号
	/// </remarks>
	public class CDAProject
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public CDAProject()
		{		
	
		}
		#region SelectProject Methods
		/// <summary>
		/// 此函数调用存储过程 spGetProject 并返回包含记录的数据读取器.
		/// </summary>
		/// <returns>SqlDataReader</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spGetProject  ] 如下：
		/// <code>
		/// --endregion
		///--region [dbo].[spGetProject]
		///
		///CREATE PROCEDURE [dbo].[spGetProject]
		///AS
		///SET NOCOUNT ON
		///
		///SELECT
		///	top 1	
		///	[ProjectID],
		///	[ProjectName],
		///	[ShortName],
		///	[Address],
		///	[Fax],
		///	[Tel],
		///	[Email],
		///	[Contact]
		///FROM
		///	[dbo].[Project]
		///
		///
		/// </code>code>
		/// 结果集中的列包括:
		/// SqlString ProjectID, SqlString ProjectName, SqlString ShortName, SqlString Address, SqlString Fax, SqlString Tel, SqlString Email, SqlString Contact,
		/// </remarks>
		public SqlDataReader SelectProject()		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {} ;
			object[] objParamValues = {} ; 
			SqlDbType[] paramTypes = {} ;
			
			return pDataAccess.ExecuteSPQueryReader("spGetProject",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spGetProject";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			//return the result reader
//			return base.ResultReader ;
	
		}
		#endregion

		#region GetProjectName 

		/// <summary>
		/// get the project name
		/// </summary>
		/// <returns></returns>
		public string GetProjectName()
		{
			SqlDataReader r=SelectProject();
			string projectName="Unknown";
			if(r.Read() )
			{
				projectName=(string)r["ProjectName"];
			}
			r.Close() ;
			return projectName;
		}

		#endregion

		#region GetProjectShortName 

		/// <summary>
		/// get the project name
		/// </summary>
		/// <returns></returns>
		public string GetProjectShortName()
		{
			SqlDataReader r=SelectProject();
			string projectName="Unknown";
			if(r.Read() )
			{
				projectName=(string)r["ShortName"];
			}
			r.Close() ;
			return projectName;
		}

		#endregion
		
		//add by hbing 2006-04-24 for get the project's local currency and check currency
		public SqlDataReader GetProjectCurrency()
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
//			return base.ResultReader ;
		}
		//end add
		
		//add by hbing 2006-04-24 本位币
		public string GetLocalCurrency()
		{
			string sLocalCurrency = "" ;
			SqlDataReader drCurrency = GetProjectCurrency() ;
			if(drCurrency.Read())
			{
				sLocalCurrency = drCurrency["NaturalCurrencyID"].ToString() ; 
			}
			drCurrency.Close() ;
			return sLocalCurrency ;
		}
		//end add
		//add by hbing 2006-04-24 核算货币
		public string GetCheckCurrency()
		{
			string sCheckCurrency = "" ;
			SqlDataReader drCurrency = GetProjectCurrency() ;
			if(drCurrency.Read())
			{
				sCheckCurrency = drCurrency["StandardCurrencyID"].ToString() ; 
			}
			drCurrency.Close() ;
			return sCheckCurrency ;
		}



		#region AddOrUpdateProject Methods
		/// <summary>
		/// 此函数调用存储过程 spAddOrUpdateProject 并返回数据库中受影响的记录数
		/// </summary>
		///<param name="projectID">变量 projectID: 用于设置参数 '@ProjectID' 给存储过程spAddOrUpdateProject </param>	
		///<param name="projectName">变量 projectName: 用于设置参数 '@ProjectName' 给存储过程spAddOrUpdateProject </param>	
		///<param name="shortName">变量 shortName: 用于设置参数 '@ShortName' 给存储过程spAddOrUpdateProject </param>	
		///<param name="address">变量 address: 用于设置参数 '@Address' 给存储过程spAddOrUpdateProject </param>	
		///<param name="fax">变量 fax: 用于设置参数 '@Fax' 给存储过程spAddOrUpdateProject </param>	
		///<param name="tel">变量 tel: 用于设置参数 '@Tel' 给存储过程spAddOrUpdateProject </param>	
		///<param name="email">变量 email: 用于设置参数 '@Email' 给存储过程spAddOrUpdateProject </param>	
		///<param name="contact">变量 contact: 用于设置参数 '@Contact' 给存储过程spAddOrUpdateProject </param>	
		/// <returns>受影响的记录数</returns>
		/// <remarks>
		/// 存储过程源码 [ 存储过程 spAddOrUpdateProject  ] 如下：
		/// <code>
		/// create procedure spAddOrUpdateProject
		///	@ProjectID nvarchar(32),
		///	@ProjectName nvarchar(512),
		///	@ShortName nvarchar(256),
		///	@Address nvarchar(512),
		///	@Fax nvarchar(64),
		///	@Tel nvarchar(64),
		///	@Email nvarchar(64),
		///	@Contact nvarchar(64)
		///as
		///	if exists (select * from Project)
		///		update Project
		///		set ProjectID=@ProjectID,ProjectName=@ProjectName,
		///		ShortName=@ShortName,Address=@Address,Fax=@Fax,
		///		Tel=@Tel,Email=@Email,Contact=@Contact
		///	else
		///		insert into Project(ProjectID,ProjectName,ShortName,Address,Fax,
		///		Tel,Email,Contact)
		///		values(@ProjectID,@ProjectName,@ShortName,@Address,@Fax,
		///		@Tel,@Email,@Contact)
		///
		/// </code>code>
		/// </remarks>
		public int AddOrUpdateProject(
			string projectID,		
			string projectName,		
			string shortName,		
			string address,		
			string fax,		
			string tel,		
			string email,		
			string contact,
			string Bank,
			string AccountNo,
			string TaxNo,
			string Attorney,
			string Deputy
			)		
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ProjectID","ProjectName","ShortName","Address","Fax","Tel","Email","Contact","Bank","AccountNo","TaxNo","Attorney","Deputy"} ;
			object[] objParamValues = {projectID,projectName,shortName,address,fax,tel,email,contact,Bank,AccountNo,TaxNo,Attorney,Deputy} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			pDataAccess.ExecuteSP("spAddOrUpdateProject",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			mCommandText = "spAddOrUpdateProject";
//			mCommandType = CommandType.StoredProcedure ;
//			base.ClearParameters();
//
//			base.AddParameter("@ProjectID", SqlDbType.NVarChar,32).Value=projectID;
//			base.AddParameter("@ProjectName", SqlDbType.NVarChar,512).Value=projectName;
//			base.AddParameter("@ShortName", SqlDbType.NVarChar,256).Value=shortName;
//			base.AddParameter("@Address", SqlDbType.NVarChar,512).Value=address;
//			base.AddParameter("@Fax", SqlDbType.NVarChar,64).Value=fax;
//			base.AddParameter("@Tel", SqlDbType.NVarChar,64).Value=tel;
//			base.AddParameter("@Email", SqlDbType.NVarChar,64).Value=email;
//			base.AddParameter("@Contact", SqlDbType.NVarChar,64).Value=contact;
//			base.AddParameter("@Bank", SqlDbType.NVarChar,100).Value=Bank;
//			base.AddParameter("@AccountNo", SqlDbType.NVarChar,100).Value=AccountNo;
//			base.AddParameter("@TaxNo", SqlDbType.NVarChar,100).Value=TaxNo;
//			base.AddParameter("@Attorney", SqlDbType.NVarChar,50).Value=Attorney;
//			base.AddParameter("@Deputy", SqlDbType.NVarChar,50).Value=Deputy;
//
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
