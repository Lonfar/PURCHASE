using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAETenderSend_Edit 的摘要说明。
	/// </summary>
	public class DAETenderSend_Edit: DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql = string.Empty;
		public DAETenderSend_Edit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  void GetIsTCVendorList(DataTable dtTemp,string sTenderID)
		{
			 strSql = "SELECT Vendor.IDKey,Vendor.VendorName,ITBDocument.ITBIDKey FROM TCStrategyVendor,ITBDocument,Vendor "+
				" WHERE TCStrategyVendor.VendorCode = Vendor.IDkey AND ITBDocument.TenderID=TCStrategyVendor.TenderID AND "+
				" ITBDocument.TenderID= '"+sTenderID+"' ";
			DataTable dt = _da.GetDataTable(strSql);
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
				DataRow dr = dtTemp.NewRow();
				dr["ID"] = System.Guid.NewGuid().ToString();
				dr["ITBDocumentID"] = dt.Rows[i]["ITBIDKey"].ToString();
				dr["VendorID"] = dt.Rows[i]["IDKey"].ToString();
				dr["VendorName"] = dt.Rows[i]["VendorName"].ToString();
				dr["RowStatus"] = "New";
				dtTemp.Rows.Add(dr);
			}
		}

	
		public  string  GetTenderState(string SRID)
		{
			strSql = "SELECT BT_TenderState.TypeDescription FROM ServiceRequistion ,BT_TenderState WHERE ServiceRequistion.TenderState =BT_TenderState.IDKey AND  ServiceRequistion.SRID = '"+SRID+"'";
			DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows[0]["TypeDescription"] != System.DBNull.Value)
			{
				return dt.Rows[0]["TypeDescription"].ToString();
			}
			else
			{
				return "";
			}
		}

		public bool ITBIsExistsVendor(string sTenderID)
		{
			strSql = "SELECT COUNT(1) FROM ITBVendorList,ITBDocument WHERE ITBDocument.ITBIDKey =ITBVendorList.ITBDocumentID AND  ITBDocument.TenderID = '"+sTenderID+"'";
			DataTable dt = _da.GetDataTable(strSql);
			if(int.Parse(dt.Rows[0][0].ToString())>0)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public bool CanEditITB(string strTendorID,int nstate)
		{
			strSql ="SELECT State FROM ITBDocument WHERE TenderID ='"+strTendorID+"'";
			DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows[0][0].ToString().Trim()!=nstate.ToString())
			{
				//不可以编辑
				return false;
			}
			else
			{
				return true;
			}
																						 
		}

		
		public void UpdateTable(DataTable dt)
		{
			int nCount = dt.Rows.Count;
			if(nCount>0)
			{
				for(int i = 0;i<nCount;i++)
				{
					if(dt.Rows[i].RowState != DataRowState.Deleted)
					{
						string refTableKey = dt.Rows[i]["VendorID"].ToString();
						strSql = "SELECT VendorName FROM Vendor WHERE Vendor.IDKey = '"+ refTableKey +"'";
						DataTable dtrefTable = _da.GetDataTable(strSql);
						dt.Rows[i]["VendorName"] = dtrefTable.Rows[0]["VendorName"];
						
					}
				}
			}
		}
	}
}
