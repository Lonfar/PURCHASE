/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Date Generated: 2005.7.6
-- Version List
--  Version 1.0 2005.7.6
--  Version 1.1 2005.7.7 add SelectListPagedTotalCount method
--  Version 1.1.1 2005.7.8 modify comment of SelectListPagedTotalCount method
--  Version 1.1.2 2005.7.25 add SelectMaterialQuantityInStoreByUOMAndBin method
------------------------------------------------------------------------------------------------------------------------
*/
using System;
using System.Data;
using System.Data.SqlClient;
using Cnwit.Utility ; 

/// <summary>
/// Common function to access database
/// </summary>
namespace Business
{
	/// <summary>
	/// Summary description for CDACommon.
	/// </summary>
	public class CDACommon
	{
		/// <summary>
		/// construnction
		/// do nothing
		/// </summary>
		public CDACommon()
		{
			
		}
		
		public SqlDataReader SelectListPaged( string strSelectStatement,string strFromStatement,string strWhereStatement,string strOrderByExpression,string ascOrDesc,int intRecordCount,int intPageIndex,int intPageSize)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"SelectStatement","FromStatement","WhereStatement","OrderByExpression","AscOrDesc","RecordCount","PageIndex","PageSize","DoCount"} ;
			object[] objParamValues = {strSelectStatement,strFromStatement,strWhereStatement,strOrderByExpression,ascOrDesc,intRecordCount,intPageIndex,intPageSize,false} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int,SqlDbType.Bit} ;
			
			return pDataAccess.ExecuteSPQueryReader("spSelectListDynamicPaged2",sParams,objParamValues,paramTypes) ; 
			//set the commandText
//			mCommandText = "spSelectListDynamicPaged2";
			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("SelectStatement", SqlDbType.NVarChar,2000).Value=strSelectStatement;
//			base.AddParameter("FromStatement", SqlDbType.NVarChar,2000).Value=strFromStatement;
//			base.AddParameter("WhereStatement", SqlDbType.NVarChar,2000).Value=strWhereStatement;
//			base.AddParameter("OrderByExpression", SqlDbType.NVarChar,500).Value=strOrderByExpression;
//			base.AddParameter("AscOrDesc",SqlDbType.NVarChar, 10).Value=ascOrDesc; 
//			base.AddParameter("RecordCount", SqlDbType.Int).Value=intRecordCount;
//			base.AddParameter("PageIndex", SqlDbType.Int).Value=intPageIndex;
//			base.AddParameter("PageSize", SqlDbType.Int).Value=intPageSize;
//			base.AddParameter("DoCount", SqlDbType.Bit).Value=false;
			//return the result reader
//			return base.ResultReader;
	
		}
		/// <summary>
		/// Get the total record count 
		/// </summary>
		/// <param name="strFromStatement"></param>
		/// <param name="strWhereStatement"></param>
		/// <returns>the total record count</returns>
		public DataTable SelectListPaged2( string strSelectStatement,string strFromStatement,string strWhereStatement,string strOrderByExpression,string ascOrDesc,int intRecordCount,int intPageIndex,int intPageSize)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"SelectStatement","FromStatement","WhereStatement","OrderByExpression","AscOrDesc","RecordCount","PageIndex","PageSize","DoCount"} ;
			object[] objParamValues = {strSelectStatement,strFromStatement,strWhereStatement,strOrderByExpression,ascOrDesc,intRecordCount,intPageIndex,intPageSize,false} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int,SqlDbType.Bit} ;
			
			return pDataAccess.ExecuteSPQueryDataTable("spSelectListDynamicPaged2",sParams,objParamValues,paramTypes) ; 
			
//			//set the commandText
//			mCommandText = "spSelectListDynamicPaged2";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("SelectStatement", SqlDbType.NVarChar,2000).Value=strSelectStatement;
//			base.AddParameter("FromStatement", SqlDbType.NVarChar,2000).Value=strFromStatement;
//			base.AddParameter("WhereStatement", SqlDbType.NVarChar,2000).Value=strWhereStatement;
//			base.AddParameter("OrderByExpression", SqlDbType.NVarChar,500).Value=strOrderByExpression;
//			base.AddParameter("AscOrDesc",SqlDbType.NVarChar, 10).Value=ascOrDesc; 
//			base.AddParameter("RecordCount", SqlDbType.Int).Value=intRecordCount;
//			base.AddParameter("PageIndex", SqlDbType.Int).Value=intPageIndex;
//			base.AddParameter("PageSize", SqlDbType.Int).Value=intPageSize;
//			base.AddParameter("DoCount", SqlDbType.Bit).Value=false;
//			//return the result reader
//			return base.ResultDataTable;
	
		}
		/// <summary>
		/// Get the total record count 
		/// </summary>
		/// <param name="strFromStatement"></param>
		/// <param name="strWhereStatement"></param>
		/// <returns>the total record count</returns>
		public int SelectListPagedTotalCount( string strFromStatement,string strWhereStatement)
		{
			DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ; 			
			string[] sParams = {"SelectStatement","FromStatement","WhereStatement","OrderByExpression","AscOrDesc","RecordCount","PageIndex","PageSize","DoCount"} ;
			object[] objParamValues = {DBNull.Value,strFromStatement,strWhereStatement,DBNull.Value,DBNull.Value,DBNull.Value,DBNull.Value,DBNull.Value,true} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Int,SqlDbType.Int,SqlDbType.Bit} ;
			
			return (int)pDataAccess.ExecuteSPQueryObject("spSelectListDynamicPaged2",sParams,objParamValues,paramTypes) ; 
//			//set the commandText
//			mCommandText = "spSelectListDynamicPaged2";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("SelectStatement", SqlDbType.NVarChar,2000).Value=DBNull.Value ;
//			base.AddParameter("FromStatement", SqlDbType.NVarChar,2000).Value=strFromStatement;
//			base.AddParameter("WhereStatement", SqlDbType.NVarChar,2000).Value=strWhereStatement;
//			base.AddParameter("OrderByExpression", SqlDbType.NVarChar,500).Value=DBNull.Value ;
//			base.AddParameter("AscOrDesc",SqlDbType.NVarChar, 10).Value=DBNull.Value ; 
//			base.AddParameter("RecordCount", SqlDbType.Int).Value=DBNull.Value ;
//			base.AddParameter("PageIndex", SqlDbType.Int).Value=DBNull.Value ;
//			base.AddParameter("PageSize", SqlDbType.Int).Value=DBNull.Value ;
//			base.AddParameter("DoCount", SqlDbType.Bit).Value=true;
//			//return the total count
//			return (int)base.ExecuteScalar() ;
		}

//		/*
//		 * Update: Liu Dian-Xin 2005-07-25
//		 * add SelectMaterialQuantityInStoreByUOMAndBin method
//		 */
//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="strWarehouseID"></param>
//		/// <param name="strMaterialID"></param>
//		/// <param name="strUOMID"></param>
//		/// <param name="strBinID"></param>
//		/// <returns></returns>
//		public double SelectMaterialQuantityInStoreByUOMAndBin(string strWarehouseID,string strMaterialID,string strUOMID,string strBinID)
//		{
//			//set the commandText
//			mCommandText = "spSelectMaterialQuantityInStoreByUOMAndBin";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("WarehouseID", SqlDbType.NVarChar,50).Value=strWarehouseID;
//			base.AddParameter("MaterialID", SqlDbType.NVarChar,32).Value=strMaterialID;
//			base.AddParameter("UOMID", SqlDbType.NVarChar,50).Value=strUOMID;
//			base.AddParameter("BinID", SqlDbType.NVarChar,50).Value=strBinID;
//
//			//return the result QuantityInStore
//			object returnvalue = base.ExecuteScalar();
//			if(returnvalue==null)
//			{
//				return 0.0;
//			}
//			else
//			{
//				return (double)returnvalue;
//			}
//
//		}
//
//
//		/*
//		 * Update: Liu Dian-Xin 2005-10-7
//		 * add SelectConfirmRemark method,InsertConfirmRemark method
//		 */
//		/// <summary>
//		/// 
//		/// </summary>
//		/// <param name="strVoucherName"></param>
//		/// <param name="strVoucherID"></param>
//		/// <returns></returns>
//		public DataTable SelectConfirmRemark(string strVoucherName,string strVoucherID)
//		{
//			//set the commandText
//			mCommandText = "spSelectConfirmRemark";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("VoucherName", SqlDbType.NVarChar,50).Value=strVoucherName;
//			base.AddParameter("VoucherID", SqlDbType.NVarChar,50).Value=strVoucherID;
//
//			return base.ResultDataTable;
//		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="voucherName"></param>
		/// <param name="voucherID"></param>
		/// <param name="confirmDate"></param>
		/// <param name="confirmUserID"></param>
		/// <param name="confirmRemark"></param>
		/// <param name="isPassed"></param>
		/// <param name="status"></param>
		/// <returns></returns>
//		public int InsertConfirmRemark( string voucherName, string voucherID, DateTime confirmDate, string confirmUserID, string confirmRemark, bool isPassed, byte status)
//		{
//			//set the commandText
//			mCommandText = "spInsertConfirmRemark";
//			//set the CommandType
//			mCommandType = CommandType.StoredProcedure ;
//			//Clear all the parameters
//			base.ClearParameters();
//			//add and set the parameters
//			base.AddParameter("VoucherName", SqlDbType.NVarChar,50).Value=voucherName;
//			base.AddParameter("VoucherID", SqlDbType.NVarChar,50).Value=voucherID;
//			base.AddParameter("ConfirmDate", SqlDbType.DateTime,0).Value=confirmDate;
//			base.AddParameter("ConfirmUserID", SqlDbType.NVarChar,32).Value=confirmUserID;
//			base.AddParameter("ConfirmRemark", SqlDbType.NVarChar,1024).Value=confirmRemark;
//			base.AddParameter("IsPassed", SqlDbType.Bit,0).Value=isPassed;
//			base.AddParameter("Status", SqlDbType.TinyInt,0).Value=status;
//			//return the result reader
//			try
//			{
//				//Execute the sql command and return the effected record count
//				return base.ExecuteNonQuery();
//			}
//			catch
//			{
//				//throw the exception out
//				throw;
//			}
//	
//		}
	}
}
