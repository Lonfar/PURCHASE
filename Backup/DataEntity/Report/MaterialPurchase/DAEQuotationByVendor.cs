using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEQuotationByVendor 的摘要说明。
	/// </summary>
	public class DAEQuotationByVendor : DAEBase
	{
		public DAEQuotationByVendor()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			sSqlRep.Append(@"SELECT 
							MR_QuotationPrice.QuotationPriceID,
							MR_QuotationPrice.QuotationPriceNo
							,MR_QuotationPrice.QuotationDate
							,MR_EnquiryPrice.EnquiryPriceNo
							,MR_QuotationPrice.EnquiryDate
							,MR_QuotationPrice.BIDEvaluationDate
							,MR_QuotationPrice.VendorNo
							,MR_QuotationPrice.VendorName
							,MR_QuotationPrice.VendorAdress
							,MR_QuotationPrice.VendorTelphone
							,MR_QuotationPrice.Fax
							,MR_QuotationPrice.Email
							,MR_QuotationPrice.Contact

							,MR_QuotationPrice.TotalPriceNoTax as TotalPriceNoTax1
							,MR_QuotationPrice.TotalPriceTax as TotalPriceTax1

							,MR_QuotationPrice.CurrencyID
							,WH_BT_Incoterms.TypeDescription AS Incoterms
							,WH_BT_TransportTerm.TypeDescription AS TransportTerm
							,WH_BT_TransportMethod.TypeDescription AS TransportMethod
							,BT_Payments.TypeDescription AS Payments
							,MR_QuotationPriceMaterial.ItemCode
							,MR_QuotationPriceMaterial.MaterialName
							,MR_QuotationPriceMaterial.MRQuantity
							,MR_QuotationPriceMaterial.UOMID
							,MR_QuotationPriceMaterial.UnitPriceNoTax
							,(MR_QuotationPriceMaterial.MRQuantity * MR_QuotationPriceMaterial.UnitPriceNoTax ) AS TotalPriceNoTax
							,MR_QuotationPriceMaterial.UnitPriceTax
							,(MR_QuotationPriceMaterial.MRQuantity * MR_QuotationPriceMaterial.UnitPriceTax ) AS TotalPriceTax

							FROM MR_QuotationPrice

							INNER JOIN MR_EnquiryPrice ON MR_EnquiryPrice.EnquiryPriceID = MR_QuotationPrice.EnquiryPriceID
							INNER JOIN MR_QuotationPriceMaterial ON MR_QuotationPriceMaterial.QuotationPriceID = MR_QuotationPrice.QuotationPriceID
							Left JOIN WH_BT_Incoterms ON MR_QuotationPrice.IncotermsID = WH_BT_Incoterms.IDKey
							LEFT JOIN WH_BT_TransportTerm ON MR_QuotationPrice.TransportTermID = WH_BT_TransportTerm.IDKey
							LEFT JOIn WH_BT_TransportMethod ON MR_QuotationPrice.ShipID = WH_BT_TransportMethod.IDKey
							LEFT JOIn BT_Payments ON MR_QuotationPrice.PaymentTypeID = BT_Payments.IDKey");	
			if ( sWhere.Length > 0 )
			{
				sSqlRep.Append(" WHERE " + sWhere);
			}
					
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
