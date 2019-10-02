using System;
using Cnwit.Utility ;
namespace Business.SystemConfig
{
	/// <summary>
	/// CEmployeeManager 的摘要说明。
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
					{//用户已经存在，不能删除，只能进行撤销工作
						pResultInfo.bSuccess = false ;
						pResultInfo.ErrorResID = "UserIDExist" ;
						return pResultInfo ;
					}
					else
					{//属于添加操作,提示不能为空
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
