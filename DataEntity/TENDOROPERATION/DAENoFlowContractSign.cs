using System;

namespace DataEntity
{
	/// <summary>
	/// DAENoFlowContractSign 的摘要说明。
	/// </summary>
	public class DAENoFlowContractSign:DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		public DAENoFlowContractSign()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public System.Data.DataTable GetVendorInfoByVendorID(string sIDkey)
		{
			string strSql = "SELECT Vendor.VendorNo,Vendor.VendorName,Vendor.Address,Vendor.Telphone,Vendor.Fax,Vendor.Email "+
				"FROM Vendor where  Vendor.IDKey  = '"+sIDkey+"'";

			System.Data.DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			{
				return dt;
			}
			else
			{
				return null;
			}

		}

		public System.Data.DataTable GetVendorInfo(string sIDkey)
		{
			string strSql = "SELECT Vendor.VendorNo,Vendor.VendorName,Vendor.Address,Vendor.Telphone,Vendor.Fax,Vendor.Email "+
				"FROM Vendor,Contract where Contract.IDkey = '"+sIDkey+"' AND Contract.VendorID =Vendor.IDKey ";

			System.Data.DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows.Count>0)
			{
				return dt;
			}
			else
			{
				return null;
			}

		}

		public string UpdateNoFlowTenderState(string sIDkey,int nState)
		{
			string sError = string.Empty ;
			string UpdateSql  = "Update Contract set State = "+nState+" where IDKey = '"+sIDkey+"'";
			sError += _da.ExecuteDMLSQL(UpdateSql);
			UpdateSql  = "UPDATE ServiceRequistion SET SRState = "+nState+" FROM ServiceRequistion SR JOIN  ContractRequistion CR ON SR.IDKey = CR.SRIDKey AND CR.ContractID ='"+sIDkey+"'";
			sError += _da.ExecuteDMLSQL(UpdateSql);
			return sError ;
		}

		public int GetContractState(string sIDKey,string nState)
		{
			string sql  = "SELECT 1 FROM Contract WHERE charindex(','+convert(varchar,Contract.State)+',',',"+nState+",')>0 AND IDKey ='"+sIDKey+"'";

			return _da.GetDataTable(sql).Rows.Count;

		}
		


	}
}
