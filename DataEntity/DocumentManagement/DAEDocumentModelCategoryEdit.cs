using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEDocumentModelCategoryEdit 的摘要说明。
	/// </summary>
	public class DAEDocumentModelCategoryEdit : DAEBase
	{
		public DAEDocumentModelCategoryEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public string GetId ( string sTableName , string sParentId )
		{
			string[] sParams = {"tbname","parentid"} ;
			object[] objParamValues = {sTableName,sParentId} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar,SqlDbType.NVarChar} ;

			DataTable dt = BaseDataAccess.ExecuteSPQueryDataTable ( 
				"sp_getTreeId" , sParams , objParamValues , paramTypes ) ;

			return dt.Rows[0][0].ToString() ;
		}
	}
}
