using System;
using Cnwit.Utility ;
namespace Business.SystemConfig
{
	/// <summary>
	/// CEmployeeManager ��ժҪ˵����
	/// </summary>
	public class CBLEmployeeManager:CBLBase
	{
		public CBLEmployeeManager():base("SC_Employee")
		{
			
		}

		public Common.CResultInfo SynUserInfo(System.Data.DataTable dtInfo,string sUserID,string sUserName)
		{
			Common.CResultInfo pResultInfo = new Common.CResultInfo() ;
			string sEmployeeID = dtInfo.Rows[0]["sc_employee.vEmployeeID"].ToString().Trim() ;
			string sMobile = dtInfo.Rows[0]["sc_employee.vMobile"].ToString().Trim();
			string sTelephone = dtInfo.Rows[0]["sc_employee.vTelephone"].ToString().Trim() ;
			string sEmail = dtInfo.Rows[0]["sc_employee.vEmail"].ToString().Trim() ;
			try
			{				
				if(sUserID.Trim() == "")
				{
					if(Common.CCommonCheck.CheckFieldValueExist("sc_user","vUserID",sUserID))
					{//�û��Ѿ����ڣ�����ɾ����ֻ�ܽ��г�������
						pResultInfo.bSuccess = false ;
						pResultInfo.ErrorResID = "UserIDExist" ;
						return pResultInfo ;
					}
					else
					{//������Ӳ���,��ʾ����Ϊ��
						pResultInfo.bSuccess = false ;
						pResultInfo.ErrorResID = "UserIDNotNull" ;
						return pResultInfo ;
					}
				}
				if(sUserName.Trim() == "")
				{
					pResultInfo.bSuccess = false ;
					pResultInfo.ErrorResID = "UserNameNotNull" ;
					return pResultInfo ;
				}
				string sPK = System.Guid.NewGuid().ToString() ;
				string sSql = "Insert into sc_user(PK_SC_User,vUserID,vUserName,vMobile,vTelephone,vEmail,vEmployeeid) values('" + sPK + "','" + sUserID + "','" + sUserName + "','" + sMobile + "','" + sTelephone + "','" + sEmail + "','" + sEmployeeID + "')" ;
				DataAcess pDataAccess = Common.GetProjectDataAcess.GetDataAcess() ;  
				pDataAccess.scExecuteQuery(sSql) ; 
				pResultInfo.bSuccess = true ;
			}
			catch(System.Data.SqlClient.SqlException sqlErr)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorID = sqlErr.Number.ToString() ;
			}
			catch(System.Exception err)
			{
				pResultInfo.bSuccess = false ;
				pResultInfo.ErrorDesc = err.Message ;				
			}
			return pResultInfo ;
		}
	}
}
