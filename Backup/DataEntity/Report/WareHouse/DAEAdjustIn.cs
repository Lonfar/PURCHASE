using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEAdjustIn 的摘要说明。
	/// </summary>
	public class DAEAdjustInf : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT  WH_AdjustIN.AdjustInNO,  WH_AdjustIN.AdjustDate, 
					(SELECT WHName FROM WH_BI_WareHouse  WHERE WH_BI_WareHouse.WHID = WH_AdjustIn.WHID) AS WHID,           
					(SELECT FullName FROM BI_Employee WHERE BI_Employee.IDKey = WH_AdjustIn.EmployeeID) AS TransFullName,        
					WH_AdjustIN.TotalPriceStandard,  WH_AdjustIN.TotalPriceNatural,WH_AdjustIN.CreateBy,  WH_AdjustIN.CreateDate,        
					WH_AdjustInMaterial.ItemCode,  Material.MaterialName,WH_AdjustInMaterial.PartNO,  WH_AdjustInMaterial.AdjustInQuantity,         
					WH_AdjustInMaterial.UnitPriceStandard,WH_AdjustInMaterial.UnitPriceNatural,  MaterialUOM.UOMID, WH_AdjustInMaterial.POID,           
					(SELECT BINID FROM WH_BI_BIN WHERE WH_BI_BIN.BINID = WH_AdjustInMaterial.BINID) AS BINID 
					FROM  WH_AdjustIN 
					LEFT OUTER JOIN  WH_AdjustInMaterial ON WH_AdjustIN.AdjustInID =  WH_AdjustInMaterial.AdjustInID 
					LEFT OUTER JOIN  Material ON Material.ItemCode =  WH_AdjustInMaterial.ItemCode 
					LEFT OUTER JOIN  MaterialUOM ON MaterialUOM.MaterialUomID =  WH_AdjustInMaterial.MaterialUomID 
					LEFT OUTER JOIN  WH_BI_WareHouse ON WH_BI_WareHouse.WHID = WH_AdjustIN.WHID 
                    LEFT OUTER JOIN  WH_BI_BIN ON  WH_AdjustInMaterial.BINID= WH_BI_BIN.BINID "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere+" AND WH_AdjustIN.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			else
			{
				sSqlRep.Append(" WHERE WH_AdjustIN.Status = '"+(int)ApproveState.State_Approved+"'");
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
