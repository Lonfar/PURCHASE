using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEReportPreserve 的摘要说明。
	/// </summary>
	public class DAEReportPreserve: DAEBase
	{
		public DAEReportPreserve()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetRptData_ReportPreserve(string sWhere)
		{
			//一行
			string sSql = string.Empty;

			sSql +="SELECT dbo.Material.UOMID, dbo.Material.MaterialName, "+
      "dbo.BI_Department.DepartmentName, dbo.WH_Preserve.PreserveDate, "+
      "dbo.WH_Preserve.PreserveNO, dbo.WH_PreserveMaterial.ItemCode,"+ 
      "dbo.WH_PreserveMaterial.POID, dbo.WH_PreserveMaterial.BINID, "+
      "dbo.WH_PreserveMaterial.QuantityPreserve, dbo.WH_BI_WareHouse.WHID,"+ 
      "dbo.POMaterial.UnitPrice, dbo.POMaterial.UnitPrice AS SumPrice, "+
      "dbo.POMaterial.UnitPrice * Z.StandardExchangeRate AS StandardUnitPrice,"+ 
      "dbo.POMaterial.UnitPrice * Z.NaturalExchangeRate AS NaturalUnitPrice, "+
      "dbo.WH_PreserveMaterial.QuantityPreserve * dbo.POMaterial.UnitPrice * Z.StandardExchangeRate "+
       "AS StandardSumPrice,  "+
      "dbo.WH_PreserveMaterial.QuantityPreserve * dbo.POMaterial.UnitPrice * Z.NaturalExchangeRate "+
       "AS NaturalSumPrice "+
"FROM dbo.WH_PreserveMaterial INNER JOIN "+
      "dbo.Material ON  "+
      "dbo.WH_PreserveMaterial.ItemCode = dbo.Material.ItemCode INNER JOIN "+
      "dbo.WH_Preserve ON  "+
      "dbo.WH_Preserve.PreserveID = dbo.WH_PreserveMaterial.PreserveID INNER JOIN "+
      "dbo.POMaterial ON dbo.WH_PreserveMaterial.POID = dbo.POMaterial.POID AND  "+
      "dbo.WH_PreserveMaterial.ItemCode = dbo.POMaterial.ItemCode INNER JOIN "+
      "dbo.MaterialUOM ON  "+
      "dbo.MaterialUOM.ItemCode = dbo.Material.ItemCode INNER JOIN "+
      "dbo.WH_BI_WareHouse ON  "+
      "dbo.WH_BI_WareHouse.WHID = dbo.WH_Preserve.WHID INNER JOIN "+
      "dbo.BI_Department ON  "+
      "dbo.BI_Department.IDKey = dbo.WH_Preserve.DepID INNER JOIN  "+
      "dbo.BI_Employee ON  "+
      "dbo.BI_Employee.IDKey = dbo.WH_Preserve.EmployeeID LEFT OUTER JOIN "+
       "   (SELECT A.ItemCode, A.POID, B.StandardExchangeRate,  "+
       "        C.NaturalExchangeRate "+
        " FROM (SELECT POMaterial.ItemCode, PurchaseOrder.POID,  "+
         "              PurchaseOrder.ContractTotalCostCUR, POMaterial.UnitPrice "+
          "       FROM POMaterial LEFT JOIN "+
           "            PurchaseOrder ON PurchaseOrder.POID = POMaterial.POID) A LEFT  "+
            "   JOIN "+
            "       (SELECT BI_CurrencyExchangeRate.CurrencyIDFrom,  "+
             "           BI_CurrencyExchangeRate.ExchangeRate AS StandardExchangeRate "+
             "     FROM BI_SysCurrency LEFT JOIN  "+
             "           BI_CurrencyExchangeRate ON  "+
              "          BI_CurrencyExchangeRate.CurrencyIDTo = BI_SysCurrency.StandardCurrencySymbol) "+
               " B ON A.ContractTotalCostCUR = B.CurrencyIDFrom LEFT JOIN "+
               "    (SELECT BI_CurrencyExchangeRate.CurrencyIDFrom,  "+
                "        BI_CurrencyExchangeRate.ExchangeRate AS NaturalExchangeRate "+
                "  FROM BI_SysCurrency LEFT JOIN "+
                 "       BI_CurrencyExchangeRate ON "+ 
                  "      BI_CurrencyExchangeRate.CurrencyIDTo = BI_SysCurrency.NaturalCurrencySymbol) "+
               " C ON A.ContractTotalCostCUR = C.CurrencyIDFrom) Z ON  "+
      " dbo.WH_PreserveMaterial.ItemCode = Z.ItemCode AND  "+
      " dbo.WH_PreserveMaterial.POID = Z.POID "; 
					
			if(sWhere!=null&&sWhere.Length>0)
			{
				sSql += " WHERE "+sWhere+"";
			}
			//
			//			if ( sShowCount != null && sShowCount.Length != 0 )
			//			{
			//				try
			//				{
			//					Convert.ToInt32 ( sShowCount ) ;
			//					sSql = "SELECT TOP " + sShowCount + " * FROM ( " + sSql + " ) Contract" ;
			//				}
			//				catch
			//				{}
			//			}
			//
			//			sSql += " ORDER BY Contract.ContractTotal DESC";

			return BaseDataAccess.GetDataTable(sSql);

		
		}





	}
}
