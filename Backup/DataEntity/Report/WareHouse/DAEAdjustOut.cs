using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEAdjustOut 的摘要说明。
	/// </summary>
	public class DAEAdjustOutf: DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT WH_AdjustOutMaterial.*, MaterialUOM.UOMID AS UOMID, 
					WH_AdjustOut.WHID AS WHID, 
					WH_AdjustOut.AdjustOutNO AS AdjustOutNO, 
					WH_AdjustOut.AdjustDate AS AdjustDate, 
					WH_AdjustOutMaterial.QuantityReject * WH_AdjustOutMaterial.UnitPriceNatural
					AS TotalNatural, 
					WH_AdjustOutMaterial.QuantityReject * WH_AdjustOutMaterial.UnitPriceStandard
					AS TotalStandard 
					FROM WH_AdjustOutMaterial   
					INNER JOIN WH_AdjustOut ON WH_AdjustOutMaterial.AdjustOutID = WH_AdjustOut.AdjustOutID  
					INNER JOIN Material ON Material.ItemCode = WH_AdjustOutMaterial.ItemCode  
					INNER JOIN MaterialUOM ON MaterialUOM.MaterialUomID = WH_AdjustOutMaterial.MaterialUomID  
					INNER JOIN WH_BI_WareHouse ON WH_BI_WareHouse.WHID = WH_AdjustOut.WHID  
					INNER JOIN WH_BI_BIN ON WH_BI_BIN.BINID = WH_AdjustOutMaterial.BINID "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere+" AND WH_AdjustOut.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			else
			{
				sSqlRep.Append(" WHERE WH_AdjustOut.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}

		public DataTable GetWHIDStatus( string sBINID)
		{
			string sSql = " SELECT WHID FROM WH_BI_BIN WHERE BINID = '"+sBINID+"'";
			DataTable dt = this.BaseDataAccess.GetDataTable(sSql);
			return dt;
		}
	}
}
