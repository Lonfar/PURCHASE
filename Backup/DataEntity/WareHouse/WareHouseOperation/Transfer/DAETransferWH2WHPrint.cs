using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAETransferWH2WHPrint 的摘要说明。
	/// </summary>
	public class DAETransferWH2WHPrint: DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT 
					WH_TransferWH2WH.TransferWH2WHNO, 
					WH_TransferWH2WH.TransferWH2WHDate,
					(SELECT WHName FROM WH_BI_WareHouse
						WHERE WH_BI_WareHouse.WHID = WH_TransferWH2WH.WHIDOld) AS OldWHID,
					(SELECT WHName FROM WH_BI_WareHouse
						WHERE WH_BI_WareHouse.WHID = WH_TransferWH2WH.WHIDNew) AS NewWHID,
					(SELECT FullName FROM BI_Employee
						WHERE BI_Employee.IDKey = WH_TransferWH2WH.EmployeeIDTransfer) AS TransFullName,
					(SELECT FullName FROM BI_Employee
						WHERE BI_Employee.IDKey = WH_TransferWH2WH.EmployeeIDReceive) AS ReceiveFullName, 
					(SELECT FullName FROM BI_Employee
					WHERE BI_Employee.IDKey = WH_TransferWH2WH.CreateBy) AS CreateName,
					WH_TransferWH2WH.TotalPriceStandard, 
					WH_TransferWH2WH.TotalPriceNatural, WH_TransferWH2WH.CreateBy, 
					WH_TransferWH2WH.CreateDate, WH_TransferWH2WHMaterial.ItemCode, 
					Material.MaterialName, WH_TransferWH2WHMaterial.POID, 
					WH_TransferWH2WHMaterial.TransferQuantity, 
					WH_TransferWH2WHMaterial.UnitPriceStandard, 
					WH_TransferWH2WHMaterial.UnitPriceNatural, MaterialUOM.UOMID,
					(SELECT BINID FROM WH_BI_BIN
						WHERE WH_BI_BIN.BINID = WH_TransferWH2WHMaterial.BINIDOld) AS OldBINID,
					(SELECT BINID FROM WH_BI_BIN
						WHERE WH_BI_BIN.BINID = WH_TransferWH2WHMaterial.BINIDNew) AS NewBINID      
				FROM WH_TransferWH2WH 
				LEFT OUTER JOIN WH_TransferWH2WHMaterial ON WH_TransferWH2WH.TransferWH2WHID = WH_TransferWH2WHMaterial.TransferWH2WHID 
				LEFT OUTER JOIN Material ON Material.ItemCode = WH_TransferWH2WHMaterial.ItemCode  
				LEFT OUTER JOIN MaterialUOM ON MaterialUOM.MaterialUomID = WH_TransferWH2WHMaterial.MaterialUomID ";  
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE WH_TransferWH2WH.TransferWH2WHID='"+sWhere+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
