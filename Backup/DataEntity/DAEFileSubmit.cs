using System;
using  Cnwit.Utility ;
using Common;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEFileSubmit ��ժҪ˵����
	/// </summary>
	public class DAEFileSubmit:DAEBase
	{

		string strSql=string.Empty;
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEFileSubmit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		public  string  InsertApproveMember(string putinID,string objectType,string objectID)
		{
			
			string[] sParams = {"putinID","objectType","objectID"} ;
			object[] objParamValues = {putinID,objectType,objectID} ; 
			SqlDbType[] paramTypes = { SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar} ;
			bool result =  _da.ExecuteSP("spInsertApproveMember",sParams,objParamValues,paramTypes) ;
			if(result)
			{
				return "";
			}
			else
			{
				return "InsertApproveMember" ;
			}
		}

		#region �鿴ָ������������������Ա������ Added by Liujun at 11.3 

		/// <summary>
		/// ͨ���������̵�IDKey����ô��������̵���Ա
		/// </summary>
		/// <param name="strApproveFlowIDKey">��������IDKey</param>
		/// <returns>��Ա����</returns>
		public int GetApproveMemberNum ( string strApproveFlowIDKey )
		{
			int Num = 0 ;

			string SelectSql = "SELECT COUNT(*) AS Num FROM TI_ApproveFlowMember WHERE IDKey = '"+strApproveFlowIDKey+"'";

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					Num = Convert.ToInt32 ( dr["Num"] );
				}
			}

			return Num;
		}

		#endregion
	}
}
