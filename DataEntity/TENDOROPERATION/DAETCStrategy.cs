using System;
using System.Data ; 

namespace DataEntity
{
	/// <summary>
	/// TCStrategy ��ժҪ˵����
	/// </summary>
	public class DAETCStrategy:DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;
		public DAETCStrategy()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public DataTable CheckState(String strTenderID )
		{
			String strSql = " Select Max(Contract.State) as CheckState From TCStrategy Inner Join Contract On TCStrategy.TenderID = Contract.TenderNumber Where TCStrategy.TenderID ='" + strTenderID + "'" ;
			DataTable dt = _da.GetDataTable(strSql) ; 
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select Status as State From TCStrategy Where TenderID='" + strPKValue + "'" ;
			DataTable dt = BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}
		//****************************************************

		/// <summary>
		/// ������ݱ���Ҫ��ɾ���Ĳ�������Ӧ��SRIDkey
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string[] GetSelectSrIDkey(string sTenderID)
		{
			System.Text.StringBuilder strBuilder =  new System.Text.StringBuilder();
			strSql = @"
				SELECT TCStrategySR.SRID
				FROM TCStrategy INNER JOIN TCStrategySR
					ON TCStrategy.TenderID = TCStrategySR.TenderID
				WHERE TCStrategy.TenderID ='"+sTenderID+"'";
			System.Data.DataTable dt = _da.GetDataTable(strSql);

			int n = dt.Rows.Count ;
			string[] strSrIDkey = new string[n] ;

			for ( int i = 0 ; i < n ; i ++ )
			{
				strSrIDkey[i] = dt.Rows[i][0].ToString() ;
			}

			return strSrIDkey ;
		}

		#region  ServerRequistion �Ƿ��� TCstrategy �й�
		public bool HasRelation ( string SRID )
		{
			string SelectSql = " SELECT 1 FROM TCStrategySR WHERE SRID = '"+ SRID +"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion


	}
}
