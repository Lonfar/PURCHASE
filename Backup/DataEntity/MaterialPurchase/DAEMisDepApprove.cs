using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEMisDepApprove 的摘要说明。
	/// </summary>
	public class DAEMisDepApprove : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;

		public DAEMisDepApprove()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public string GetMaxID(string MRPlanType)
		{
//			strSql = "SELECT CategoryID FROM MR_BI_Category WHERE CategoryID = '"+MRPlanType+"'";
//			DataTable dt = _da.GetDataTable(strSql);
			strSql="SELECT dbo.f_GetMrNO() ";
			return _da.GetDataTable(strSql).Rows[0][0].ToString();
		}

        public string GetMaxID()
        {
            strSql = "SELECT dbo.f_GetMrNO() ";
            return _da.GetDataTable(strSql).Rows[0][0].ToString();
        }
	}
}
