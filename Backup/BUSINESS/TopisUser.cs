/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Module Name: Topis.DataAccess.SystemConfig.CDAAFE
-- Date Generated: 2005.6.8
-- Version List
--  Version 1.0 2005.7.1
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data.SqlClient ;
namespace Business
{
	/// <summary>
	/// This object represents the properties and methods of a User.
	/// </summary>
	public class TopisUser
	{
		/// <summary>
		/// _userID
		/// </summary>
		protected string _userID = String.Empty;


		protected string _loginname = String.Empty;
		/// <summary>
		/// _userName
		/// </summary>
		protected string _userName = String.Empty;
		/// <summary>
		/// _userDescription
		/// </summary>
		protected string _userDescription = String.Empty;
		/// <summary>
		/// _employeeID
		/// </summary>
		protected string _employeeID = String.Empty;


		/// <summary>
		/// _positionID
		/// </summary>
		protected string _positionID = String.Empty;


		/// <summary>
		/// _positionName
		/// </summary>
		protected string _positionName = String.Empty;



		/// <summary>
		/// _departmentID
		/// </summary>
		protected string _departmentID = String.Empty;


		/// <summary>
		/// _departmentName
		/// </summary>
		protected string _departmentName = String.Empty;

		/// <summary>
		/// _warehouseids ËùÊô¿â·¿
		/// </summary>
		protected string _warehouseids = String.Empty;

		
        
		/// <summary>
		/// constructor of TopisUser
		/// </summary>              
		public TopisUser()
		{

		}
		/// <summary>
		/// constructor, construct a user by user id. If the user id not
		/// exists, throw a ApplicationException with message "User does
		/// not exist."
		/// </summary>
		/// <param name="vUserID">User ID</param>
		/// 
		/// <remarks>
		/// invoke function <see cref="Topis.DataAccess.SystemConfig.CDAUserRoleAuthority.SelectUserInfo@string" text="SelectUserInfo"/>
		/// method in class DataAccess.SystemConfig.CDAUserRoleAuthority
		/// </remarks>                                                                                                                  
		public TopisUser(string vUserID)
		{
			Business.SystemConfig.CDAUserRoleAuthority dau=new Business.SystemConfig.CDAUserRoleAuthority ();


			SqlDataReader reader = dau.SelectUserInfo (vUserID);
			
			if (reader.Read()) 
			{
				this.LoadFromReader(reader);
				reader.Close();
			}
			else
			{
				if (!reader.IsClosed) reader.Close();
				throw new ApplicationException("User does not exist.");
			}
		}
		/// <summary>
		/// constructor,read user info from database reader
		/// </summary>
		/// <param name="reader">reader contains user data </param>
		/// 
		/// <remarks>
		/// This constructor invokes <see cref="Topis.Components.BaseData.TopisUser.LoadFromReader@SqlDataReader" text="LoadFromReader(reader)"/>method
		/// 
		/// </remarks>                                                                                                                                 
		public TopisUser(SqlDataReader reader)
		{
			this.LoadFromReader(reader);
		}

		/// <summary>
		/// load the user information from the database reader
		/// </summary>
		/// <param name="reader">the reader contains the user data</param>
		protected void LoadFromReader(SqlDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
			{
				_userID = (string)reader["UserID"];
				_loginname = (string)reader["LoginName"];
				_userName = (string)reader["UserName"];	
				if(reader["PositionID"]!= System.DBNull.Value)
				{
					this._positionID = (string)reader["PositionID"];
				}
				if(reader["PositionType"]!= System.DBNull.Value)
				{
					this._positionName = (string)reader["PositionType"];
				}
				if(reader["DepartmentID"]!= System.DBNull.Value)
				{
					this._departmentID = (string)reader["DepartmentID"];
				}
				if(reader["DepartmentName"]!= System.DBNull.Value)
				{
					this._departmentName = (string)reader["DepartmentName"];
				}
				if(reader["WareHouseIDs"]!= System.DBNull.Value)
				{
					this._warehouseids = (string)reader["WareHouseIDs"];
				}

				_employeeID = _userID ;				
			}
		}


		/// <summary>
		/// static method to get a instance of user
		/// </summary>
		/// <param name="vUserID">user id</param>
		/// <returns>
		/// TopisUser instance
		/// </returns>                             
		public static TopisUser GetUser(string vUserID)
		{
			return new TopisUser(vUserID);
		}

		#region Public Properties

		/// <summary>
		/// UserID
		/// </summary>
		public string UserID
		{
			get {
				return _userID;
			}
		}
		/// <summary>
		/// UserName
		/// </summary>
		public string UserName
		{
			get
			{
				return _userName;
			}
		}

		public string LoginName
		{
			get
			{
				return _loginname;
			}
		}
		/// <summary>
		/// UserDescription
		/// </summary>
		public string UserDescription
		{
			get
			{
				return _userDescription;
			}
		}
		/// <summary>
		/// EmployeeID
		/// </summary>
		public string EmployeeID
		{
			get
			{
				return _employeeID;
			}
		}

		/// <summary>
		/// PositionID
		/// </summary>
		public string PositionID
		{
			get
			{
				return _positionID;
			}
		}
			

		/// <summary>
		/// DepartmentID
		/// </summary>
		public string DepartmentID
		{
			get
			{
				return _departmentID;
			}
		}

		/// <summary>
		/// DepartmentID
		/// </summary>
		public string WareHouseIDs
		{
			get
			{
				return _warehouseids;
			}
		}




		/// <summary>
		/// PositionName
		/// </summary>
		public string PositionName
		{
			get
			{
				return _positionName;
			}
		}
			

		/// <summary>
		/// DepartmentName
		/// </summary>
		public string DepartmentName
		{
			get
			{
				return _departmentName;
			}
		}
		#endregion
	}
}
