using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEMisDepApprove ��ժҪ˵����
	/// </summary>
	public class DAEMisDepApprove : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;

		public DAEMisDepApprove()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
