using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEDocumentModelCategoryEdit ��ժҪ˵����
	/// </summary>
	public class DAEDocumentModelCategoryEdit : DAEBase
	{
		public DAEDocumentModelCategoryEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
