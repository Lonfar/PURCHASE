using System;
using System.Data;
using System.Data.SqlClient;
using Cnwit.Utility ;

namespace Business.SystemConfig
{
	/// <summary>
	/// CDAPM ��ժҪ˵����
	/// </summary>
	public class CDAPM
	{
		public CDAPM()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public int AddPM(string UserID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ManagerID"} ;
			object[] objParamValues = {UserID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			pDataAccess.ExecuteSP("spInsertPM",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spInsertPM";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			base.AddParameter("@ManagerID",SqlDbType.NVarChar,32).Value=UserID;
//
//			return base.ExecuteNonQuery();
		}

		public int SetValid(string UserID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ManagerID"} ;
			object[] objParamValues = {UserID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			pDataAccess.ExecuteSP("spSetValid",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spSetValid";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			base.AddParameter("@ManagerID",SqlDbType.NVarChar,32).Value=UserID;
//
//			return base.ExecuteNonQuery();
		}

		public int DeletePM(string UserID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ManagerID"} ;
			object[] objParamValues = {UserID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			pDataAccess.ExecuteSP("spDeletePM",sParams,objParamValues,paramTypes) ;
			return 1 ;
//			//set the commandText
//			mCommandText = "spDeletePM";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			base.AddParameter("@ManagerID",SqlDbType.NVarChar,32).Value=UserID;
//
//			return base.ExecuteNonQuery();
		}

		public bool IsPMRepeated(string UserID)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"ManagerID"} ;
			object[] objParamValues = {UserID} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectUser",sParams,objParamValues,paramTypes).Rows.Count > 0 ; 
//			//set the commandText
//			mCommandText = "spSelectUser";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			base.AddParameter("@ManagerID",SqlDbType.NVarChar,32).Value=UserID;

//			return base.ResultDataTable.Rows.Count>0;
		}
	}
}
