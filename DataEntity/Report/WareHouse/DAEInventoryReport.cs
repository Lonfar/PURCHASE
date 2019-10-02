using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// InventoryReport 的摘要说明。
	/// </summary>
	public class DAEInventoryReport : DAEBase
	{
		public DAEInventoryReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region GetRptData_Inventory

		public DataTable GetRptData_Inventory ( string sWhere )
		{
			//一行
			string sSql = string.Empty;
			sSql = @"SELECT dbo.WH_InStoreMaterialDetail.ItemCode, dbo.Material.MaterialName, 
						dbo.WH_InStoreMaterialDetail.UOMID, 
						dbo.f_GetReceiveNum(dbo.WH_InStoreMaterialDetail.ItemCode, 
						dbo.WH_InStoreMaterialDetail.WHID) AS ReceiveNum, 
						dbo.f_GetWH2WHInNum(dbo.WH_InStoreMaterialDetail.ItemCode, 
						dbo.WH_InStoreMaterialDetail.WHID) AS WH2WHInNum, 
						dbo.f_GetReturnNum(dbo.WH_InStoreMaterialDetail.ItemCode, 
						dbo.WH_InStoreMaterialDetail.WHID) AS ReturnNum, 
						dbo.f_GetAdjustInNum(dbo.WH_InStoreMaterialDetail.ItemCode, 
						dbo.WH_InStoreMaterialDetail.WHID) AS AdjustInNum, 
						dbo.f_GetIssueNum(dbo.WH_InStoreMaterialDetail.ItemCode, 
						dbo.WH_InStoreMaterialDetail.WHID) AS IssueNum, 
						dbo.f_GetWH2WHOutNum(dbo.WH_InStoreMaterialDetail.ItemCode, 
						dbo.WH_InStoreMaterialDetail.WHID) AS WH2WHOutNum, 
						dbo.f_GetAdjustOutNum(dbo.WH_InStoreMaterialDetail.ItemCode, 
						dbo.WH_InStoreMaterialDetail.WHID) AS AdjustOutNum, 
						dbo.WH_InStoreMaterialDetail.QuantityInBin
					FROM dbo.WH_InStoreMaterialDetail INNER JOIN
						dbo.Material ON dbo.WH_InStoreMaterialDetail.ItemCode = dbo.Material.ItemCode
						INNER JOIN dbo.WH_BI_WareHouse
						ON dbo.WH_InStoreMaterialDetail.WHID = dbo.WH_BI_WareHouse.WHID";
			if(sWhere!=null&&sWhere.Length>0)
			{
				sSql += " WHERE "+sWhere+"";
			}
			return BaseDataAccess.GetDataTable(sSql);
		}

		#endregion
	}
}
