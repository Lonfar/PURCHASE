using System;
using System.Data;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEABCClass ��ժҪ˵����
	/// </summary>
	public class DAEABCClass : DAEBase
	{
		Cnwit.Utility.DataAcess pDataAcess ;

		public DAEABCClass()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
