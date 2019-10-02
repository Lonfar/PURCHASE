using System;
using System.Data;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEABCClass 的摘要说明。
	/// </summary>
	public class DAEABCClass : DAEBase
	{
		Cnwit.Utility.DataAcess pDataAcess ;

		public DAEABCClass()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			pDataAcess = Common.GetProjectDataAcess.GetDataAcess() ;
		}

		public  DataTable GetABCISM(string sWHID ,string sCalssScale, string sCurrentUserWHIDs)
		{
			
			string[] sParams = {"CalssScale","WHID","CurrUserWhids"} ;
			object[] objParamValues = {sCalssScale,sWHID,sCurrentUserWHIDs} ; 
			SqlDbType[] paramTypes = { SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			return pDataAcess.ExecuteSPQueryDataTable("spSelectABCClass",sParams,objParamValues,paramTypes) ; 
		}
	}
}
