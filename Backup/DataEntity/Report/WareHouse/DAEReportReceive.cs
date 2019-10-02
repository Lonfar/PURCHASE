using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEReportReceive 的摘要说明。
	/// </summary>
	public class DAEReportReceive:DAEBase
	{
		public DAEReportReceive()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		public DataTable GetRptData_ReportReceive(string sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT distinct WH_Receive.ReceiveNO, WH_Receive.ReceiveDate, 
					WH_ReceiveMaterial.BINID, WH_Receive.POID, WH_Receive.WHID,
					WH_Receive.CurrencyID, WH_ReceiveMaterial.UnitPrice, 
					WH_ReceiveMaterial.UnitPriceNatural, 
					WH_ReceiveMaterial.FactReceivedQuantity, 
					WH_ReceiveMaterial.UnitPriceStandard, 
					WH_ReceiveMaterial.UnitPrice * WH_ReceiveMaterial.FactReceivedQuantity AS
					SumPrice, 
					WH_ReceiveMaterial.UnitPriceNatural * WH_ReceiveMaterial.FactReceivedQuantity
					AS SumNatural, 
					WH_ReceiveMaterial.UnitPriceStandard * WH_ReceiveMaterial.FactReceivedQuantity
					AS SumStandard,MaterialUOM.UOMID,Material.MaterialName,Material.ItemCode
					FROM WH_ReceiveMaterial
					inner JOIN WH_Receive ON WH_ReceiveMaterial.ReceiveID = WH_Receive.ReceiveID 
					inner join MaterialUOM on WH_ReceiveMaterial.MaterialUomID=MaterialUOM.MaterialUomID 
					inner JOIN Material on Material.ItemCode = WH_ReceiveMaterial.ItemCode
					inner join WH_BI_WareHouse on WH_BI_WareHouse.WHID=WH_Receive.WHID
					inner join BI_Employee on BI_Employee.IDKey = WH_Receive.EmployeeID  "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere+" AND WH_Receive.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			else
			{
				sSqlRep.Append(" WHERE WH_Receive.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;

			#region
//			sSql +="SELECT CurrencyID,dbo.WH_ReceiveMaterial.*,dbo.WH_ReceiveMaterial.BINID, dbo.WH_Receive.POID,"+
//      "dbo.WH_ReceiveMaterial.FactReceivedQuantity, dbo.WH_Receive.EmployeeID, "+
//      "dbo.WH_ReceiveMaterial.UnitPriceNatural, dbo.WH_ReceiveMaterial.UnitPriceStandard, "+
//      "dbo.WH_ReceiveMaterial.ItemCode, dbo.Material.MaterialName, "+
//      "dbo.WH_Receive.ReceiveNo, dbo.WH_Receive.ReceiveDate, "+
//      " dbo.Material.UOMID, "+
//      "dbo.WH_ReceiveMaterial.FactReceivedQuantity * dbo.WH_ReceiveMaterial.UnitPriceNatural AS "+
//       "SumNatural, "+
//      "dbo.WH_ReceiveMaterial.FactReceivedQuantity * dbo.WH_ReceiveMaterial.UnitPriceStandard "+
//       "AS SumStandard, dbo.WH_BI_WareHouse.WHID, "+
//		  "dbo.WH_ReceiveMaterial.FactReceivedQuantity * dbo.WH_ReceiveMaterial.UnitPriceStandard  AS SumPrice "+
//		"FROM dbo.WH_ReceiveMaterial INNER JOIN "+
//      "dbo.Material ON "+
//      "dbo.WH_ReceiveMaterial.ItemCode = dbo.Material.ItemCode INNER JOIN "+
//      "dbo.WH_Receive ON "+
//      "dbo.WH_Receive.ReceiveID = dbo.WH_ReceiveMaterial.ReceiveID INNER JOIN "+
//      "dbo.MaterialUOM ON "+
//      "dbo.MaterialUOM.ItemCode = dbo.Material.ItemCode INNER JOIN "+
//      "dbo.WH_BI_WareHouse ON "+
//      "dbo.WH_BI_WareHouse.WHID = dbo.WH_Receive.WHID INNER JOIN "+
//      "dbo.BI_Employee ON dbo.BI_Employee.IDKey = dbo.WH_Receive.EmployeeID";
//			return BaseDataAccess.GetDataTable(sSql);
			#endregion
		
		}





	}
}
