using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// TransferWH2WHReport 的摘要说明。
	/// </summary>
	public class DAETransferWH2WHReport : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT WH_TransferWH2WH.TransferWH2WHNO, WH_TransferWH2WH.WHIDOld, WH_TransferWH2WH.WHIDNew, WH_TransferWH2WH.TransferWH2WHDate,  
					WH_TransferWH2WHMaterial.ItemCode, WH_TransferWH2WHMaterial.BINIDOld, WH_TransferWH2WHMaterial.BINIDNew, Material.UOMID,  
					Material.MaterialName, WH_TransferWH2WHMaterial.UnitPriceNatural, WH_TransferWH2WHMaterial.UnitPriceStandard,  
					WH_TransferWH2WHMaterial.TransferQuantity, Material.PartNo, 
					WH_TransferWH2WHMaterial.TransferQuantity * WH_TransferWH2WHMaterial.UnitPriceNatural AS TotalNatural,  
					WH_TransferWH2WHMaterial.TransferQuantity * WH_TransferWH2WHMaterial.UnitPriceStandard AS TotalStandard 
					FROM  WH_TransferWH2WH 
					INNER JOIN WH_TransferWH2WHMaterial ON WH_TransferWH2WH.TransferWH2WHID = WH_TransferWH2WHMaterial.TransferWH2WHID 
					INNER JOIN Material ON WH_TransferWH2WHMaterial.ItemCode = Material.ItemCode 
					INNER JOIN MaterialUOM ON  MaterialUOM.MaterialUomID = WH_TransferWH2WHMaterial.MaterialUomID 
					INNER JOIN WH_BI_WareHouse ON WH_BI_WareHouse.WHID = WH_TransferWH2WH.WHIDOld 
					INNER JOIN BI_Employee ON BI_Employee.IDKey = WH_TransferWH2WH.EmployeeIDTransfer 
					INNER JOIN WH_BI_BIN ON  WH_BI_BIN.BINID = WH_TransferWH2WHMaterial.BINIDOld "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere+" AND WH_TransferWH2WH.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			else
			{
				sSqlRep.Append(" WHERE WH_TransferWH2WH.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
