using System;
using System.Data;

namespace DataEntity.MaterialPurchase
{
	/// <summary>
	/// DAEQuotation ��ժҪ˵����
	/// </summary>
	public class DAEQuotation : DAEBase
	{
		public DAEQuotation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��ȡQuotation��ӡ��������

		/// <summary>
		/// ��ȡMR��ӡ��������
		/// </summary>
		/// <param name="sMRID"></param>
		/// <returns></returns>
		public DataTable GetPrintData ( string sQuotationPriceID )
		{
			string sSelectSql = "SELECT * FROM v_Report_QuotationPrint WHERE QuotationPriceID = '"+sQuotationPriceID+"'";
			string sErrorMsg = string.Empty;

			DataTable dtData = this.BaseDataAccess.GetDataTable ( sSelectSql );

			return dtData;
		}

		#endregion

		#region Get QuotationID By QuotationNo

		/// <summary>
		/// ��鱨�۵�����Ƿ����
		/// </summary>
		/// <param name="sQuotationNo"></param>
		/// <returns></returns>
		public string GetQuotationIDByNo ( string sQuotationNo )
		{
			string sQuotationID = string.Empty;
			string sSelect = " SELECT QuotationPriceID FROM MR_QuotationPrice WHERE QuotationPriceNO = '"+sQuotationNo+"'";

			DataTable dt = BaseDataAccess.GetDataTable ( sSelect );

			if ( dt.Rows.Count > 0 )
			{
				sQuotationID = Convert.ToString( dt.Rows[0][0] );
			}

			return sQuotationID;
		}

		#endregion
	}
}
