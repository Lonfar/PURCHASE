using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAETransferBin2BinPrint 的摘要说明。
	/// </summary>
	public class DAETransferBin2BinPrint:DAEBase
	{
		public DAETransferBin2BinPrint()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// <summary>
		/// 取得报表数据
		/// </summary>
		/// <param name="sPkValue"></param>
		/// <returns></returns>
		public  DataTable GetRptData(string  sPkValue)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"
					SELECT DISTINCT 
      dbo.WH_TransferBin2BinMaterial.ItemCode, dbo.WH_TransferBin2BinMaterial.POID, 
      dbo.WH_TransferBin2BinMaterial.BINIDOld, 
      dbo.WH_TransferBin2BinMaterial.BINIDNew, 
      dbo.WH_TransferBin2BinMaterial.TransferQuantityOld, 
      dbo.WH_TransferBin2BinMaterial.TransferQuantity, dbo.MaterialUOM.UOMID, 
      dbo.WH_TransferBin2BinMaterial.TransferQuantity * dbo.WH_TransferBin2BinMaterial.UnitPriceStandard
       AS TotalPrices, dbo.WH_TransferBin2BinMaterial.UnitPriceStandard, 
      dbo.WH_TransferBin2BinMaterial.MaterialName, 
      dbo.WH_TransferBin2Bin.TransferBin2BinNO, dbo.WH_TransferBin2Bin.CreateBy, 
      CONVERT(varchar(10), dbo.WH_TransferBin2Bin.CreateDate, 126) AS CreateDate, dbo.WH_TransferBin2Bin.WHID, 
      dbo.WH_BI_WareHouse.WHName, dbo.BI_Employee.FullName,
       (SELECT FullName FROM BI_Employee WHERE IDKey = WH_TransferBin2Bin.EmployeeID) AS TransferBy, 
      dbo.WH_TransferBin2Bin.TransferBin2BinDate
FROM dbo.WH_TransferBin2Bin INNER JOIN
      dbo.WH_BI_WareHouse ON 
      dbo.WH_TransferBin2Bin.WHID = dbo.WH_BI_WareHouse.WHID INNER JOIN
      dbo.BI_Employee ON 
      dbo.WH_TransferBin2Bin.EmployeeID = dbo.BI_Employee.IDKey LEFT OUTER JOIN
      dbo.WH_TransferBin2BinMaterial ON 
      dbo.WH_TransferBin2Bin.TransferBin2BinID = dbo.WH_TransferBin2BinMaterial.TransferBin2BinID
       LEFT OUTER JOIN
      dbo.Material ON 
      dbo.WH_TransferBin2BinMaterial.ItemCode = dbo.Material.ItemCode LEFT OUTER JOIN
      dbo.MaterialUOM ON 
      dbo.WH_TransferBin2BinMaterial.MaterialUomID = dbo.MaterialUOM.MaterialUomID AND
       dbo.Material.ItemCode = dbo.MaterialUOM.ItemCode
					"; 
			sSqlRep.Append(sSql);
			if(sPkValue != null && sPkValue.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_TransferBin2Bin.TransferBin2BinID='"+sPkValue+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
