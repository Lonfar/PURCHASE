using System;
using System.Data;
using System.Collections;

using Common;
using Cnwit.Utility;


namespace DataEntity
{
	/// <summary>
	/// ���������ʵ����
	/// </summary>
	public class DAETenderSend : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();	// ���ݷ���
		CEntityUitlity cEntity= new CEntityUitlity();

		public  DAETenderSend()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//

		}


		public  void GetVendorList(DataTable dtTemp,string sTenderID)
		{
			string strSQL = "SELECT ITBVendorList.VendorID FROM TCStrategyVendor,Vendor WHERE TCStrategyVendor.VendorCode = Vendor.ITBIDKey AND ITBDocument.TenderID = '"+sTenderID+"'";
			DataTable dt = _da.GetDataTable(strSQL);
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
				DataRow dr = dtTemp.NewRow();
				dr["IDKey"] = System.Guid.NewGuid().ToString();
				dr["VendorID"] = dt.Rows[i][0].ToString();
				dr["RowStatus"] = "New";
				dtTemp.Rows.Add(dr);

			}

		}

		/// <summary>
		/// ��д�б�ƻ����е�"����ʱ��"
		/// </summary>
		/// <returns>������Ϣ</returns>
		public string SetTenderSendDate ( string strTendorIDKey , string TendorSendDate )
		{
			string UpdateSql = "UPDATE TCStrategyPlan SET PlanBeginDate = '"+TendorSendDate+"' WHERE TenderID = '"+strTendorIDKey+"'";

			return _da.ExecuteDMLSQL( UpdateSql );
		}

		/// <summary>
		/// ���±����״̬
		/// </summary>
		/// <param name="strITBDocumentID">�����IDKey</param>
		/// <param name="State">״̬������Ӧ��Ϊ2���ر�׶Σ�</param>
		/// <returns>������Ϣ</returns>
		public string SetITBDocumentState ( string strITBDocumentID , int State )
		{
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE ITBDocument SET State = "+State+" WHERE ITBIDKey = '"+strITBDocumentID+"'" ;

			strErrorMsg =  _da.ExecuteDMLSQL ( UpdateSql );

			return strErrorMsg;
		}

		/// <summary>
		/// ��ñ������ϸ����
		/// </summary>
		/// <param name="ITBNumber">������</param>
		/// <returns></returns>
		public Hashtable GetITBInfo ( string ITBNumber )
		{
			string SelectSql = "SELECT TenderID , ObjectName  FROM ITBDocument WHERE ITBIDKey = '"+ITBNumber+"'";

			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			Hashtable ht = new Hashtable(3);

			if ( dt_Temp.Rows.Count > 0 )
			{
				ht.Add("TenderID" ,Convert.ToString( dt_Temp.Rows[0]["TenderID"] ) );
				ht.Add("ObjectName" ,Convert.ToString( dt_Temp.Rows[0]["ObjectName"] ) );
			}

			return ht;
		}
	}
}

