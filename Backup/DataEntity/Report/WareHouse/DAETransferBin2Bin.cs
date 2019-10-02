using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// TransferBin2Bin 的摘要说明。
	/// </summary>
	public class DAETransferBin2Binf: DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT WH_TransferBin2BinMaterial.TransferBin2BinMaterialID, 
					WH_TransferBin2Bin.TransferBin2BinDate, 
					Material.ItemCode, Material.MaterialName, 
					WH_TransferBin2BinMaterial.POID, WH_TransferBin2BinMaterial.BINIDOld, 
					WH_TransferBin2BinMaterial.BINIDNew, Material.UOMID AS UOMID, 
					WH_TransferBin2BinMaterial.TransferQuantity, 
					WH_TransferBin2Bin.EmployeeID, WH_TransferBin2Bin.TransferBin2BinNO, 
					WH_TransferBin2BinMaterial.UnitPriceNatural, 
					WH_TransferBin2BinMaterial.UnitPriceStandard, 
					WH_TransferBin2BinMaterial.TransferQuantity * WH_TransferBin2BinMaterial.UnitPriceNatural
					AS TotalNatural, 
					WH_TransferBin2BinMaterial.TransferQuantity * WH_TransferBin2BinMaterial.UnitPriceStandard
					AS TotalStandard, WH_BI_WareHouse.WHID, 
					WH_InStoreMaterialDetail.InStockMaterialID, WH_InStoreMaterialDetail.MFG, 
					WH_InStoreMaterialDetail.PartNo
					FROM WH_TransferBin2BinMaterial INNER JOIN
					WH_TransferBin2Bin ON 
					WH_TransferBin2Bin.TransferBin2BinID = WH_TransferBin2BinMaterial.TransferBin2BinID
					INNER JOIN
					Material ON 
					WH_TransferBin2BinMaterial.ItemCode = Material.ItemCode INNER JOIN
					MaterialUOM ON 
					MaterialUOM.ItemCode = WH_TransferBin2BinMaterial.ItemCode INNER JOIN
					WH_BI_WareHouse ON 
					WH_BI_WareHouse.WHID = WH_TransferBin2Bin.WHID INNER JOIN
					BI_Employee ON 
					BI_Employee.IDKey = WH_TransferBin2Bin.EmployeeID INNER JOIN
					WH_InStoreMaterialDetail ON 
					WH_InStoreMaterialDetail.InStockMaterialID = WH_TransferBin2BinMaterial.InStockMaterialID"; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere+" AND WH_TransferBin2Bin.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			else
			{
				sSqlRep.Append(" WHERE WH_TransferBin2Bin.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
