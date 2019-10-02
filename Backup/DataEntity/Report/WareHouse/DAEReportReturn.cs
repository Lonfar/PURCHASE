using System;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEReportReturn 的摘要说明。
	/// </summary>
	public class DAEReportReturn:DAEBase
	{
		public DAEReportReturn()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public DataTable GetRptData_ReportReturn(string  sWhere)
		{
			string sSql = string.Empty;
			sSql +="SELECT dbo.WH_ReturnMaterial.BINID,"+
      "dbo.WH_ReturnMaterial.POID, "+
      "dbo.WH_ReturnMaterial.FactReturnQuantity, "+
      "dbo.WH_Return.EmployeeReceive, "+
      "dbo.WH_ReturnMaterial.UnitPriceNatural,"+
      "dbo.WH_ReturnMaterial.UnitPriceStandard,"+
      "dbo.WH_ReturnMaterial.ItemCode, "+
      "dbo.Material.MaterialName, "+
      "dbo.WH_Return.ReturnNo,"+
      "dbo.WH_Return.ReturnDate, "+
      "dbo.BI_Department.DepartmentName, "+
       "dbo.Material.UOMID, "+
      "dbo.WH_ReturnMaterial.FactReturnQuantity * dbo.WH_ReturnMaterial.UnitPriceNatural AS TotalNatural, "+
      "dbo.WH_ReturnMaterial.FactReturnQuantity * dbo.WH_ReturnMaterial.UnitPriceStandard AS TotalStandard,"+
      "dbo.WH_BI_WareHouse.WHID "+
		"FROM "+
	  "dbo.WH_ReturnMaterial INNER JOIN dbo.Material ON  dbo.WH_ReturnMaterial.ItemCode = dbo.Material.ItemCode "+
                      " INNER JOIN dbo.WH_Return ON dbo.WH_Return.ReturnID = dbo.WH_ReturnMaterial.ReturnID "+
                      " INNER JOIN dbo.BI_Department ON dbo.BI_Department.IDKey = dbo.WH_Return.DepID "+
                      " INNER JOIN dbo.MaterialUOM ON  dbo.MaterialUOM.ItemCode = dbo.Material.ItemCode "+
                      " INNER JOIN dbo.WH_BI_WareHouse ON dbo.WH_BI_WareHouse.WHID = dbo.WH_Return.WHIDIssue "+
                      " INNER JOIN dbo.BI_Employee ON dbo.BI_Employee.IDKey = dbo.WH_Return.EmployeeReceive";
					
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
