using System;
using  Cnwit.Utility ;
using Common;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEFileSubmit 的摘要说明。
	/// </summary>
	public class DAEFileSubmit:DAEBase
	{

		string strSql=string.Empty;
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEFileSubmit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
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

		#region 查看指定审批流程中审批成员的数量 Added by Liujun at 11.3 

		/// <summary>
		/// 通过审批流程的IDKey来获得此审批流程的人员
		/// </summary>
		/// <param name="strApproveFlowIDKey">审批流程IDKey</param>
		/// <returns>人员数量</returns>
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
