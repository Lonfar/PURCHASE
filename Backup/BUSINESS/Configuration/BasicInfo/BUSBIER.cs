using System;
using System.Data;
using System.Text;
using Common;

namespace Business
{
	/// <summary>
	/// BusSex 的摘要说明。
	/// </summary>
	public class BUSBIER:BUSBase
	{

        private Cnwit.Utility.DataAcess da = GetProjectDataAcess.GetDataAcess();
		public BUSBIER()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        public void ResumptionLastExchangeRate(string pkName)
        {
            DataTable dt = new DataTable();
            string sSql = "Select CurrencyIDFrom, CurrencyIDTo From  BI_CurrencyExchangeRate Where IDKey = '" + pkName + "' And Status = '0'";

            dt = da.GetDataTable(sSql);

            if (dt != null && dt.Rows.Count > 0)
            {
                string strCurrencyIDFrom = string.Empty;
                string strCurrencyIDTo = string.Empty;

                strCurrencyIDFrom = dt.Rows[0][0].ToString();
                strCurrencyIDTo = dt.Rows[0][1].ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append("Update BI_CurrencyExchangeRate Set Status = '0' ");
                sb.Append(" Where Status = '1' And CurrencyIDFrom='" + strCurrencyIDFrom + "' And CurrencyIDTo ='" + strCurrencyIDTo + "'");

                sb.Append(" And DateFrom = ( Select Top 1 DateFrom From  BI_CurrencyExchangeRate B Where CurrencyIDFrom = '" + strCurrencyIDFrom + "' And CurrencyIDTo = '" + strCurrencyIDTo + "' And Status = '1' Order By DateFrom DESC)");

                da.ExecuteDMLSQL(sb.ToString());

            }
        }


        public bool CompareCurrentExchangeRateDataFrom(string currencyIDFrom , string currencyIDTo,string strdataFrom)
        {
            DataTable dt = new DataTable();
            string sSql = "Select Top 1 DateFrom From  BI_CurrencyExchangeRate Where CurrencyIDFrom = '" + currencyIDFrom + "' And CurrencyIDTo = '" + currencyIDTo + "'  And Status = '0' Order By DateFrom DESC";

            dt = da.GetDataTable(sSql);

            if (dt != null && dt.Rows.Count > 0)
            {
                DateTime currencyDateFrom = Convert.ToDateTime(dt.Rows[0][0].ToString());
                DateTime dataFrom = Convert.ToDateTime(strdataFrom.ToString());

                TimeSpan days = dataFrom - currencyDateFrom;
                int d = days.Days;

                return d >= 0;

            }
            return true;
        }


        public void SetExchangeRate(string currencyIDFrom, string currencyIDTo)
        {
            string sSql = "Update BI_CurrencyExchangeRate Set Status = 1  Where CurrencyIDFrom = '" + currencyIDFrom + "' And CurrencyIDTo = '" + currencyIDTo + "' ";
            da.ExecuteDMLSQL(sSql);
        }
    }
}
