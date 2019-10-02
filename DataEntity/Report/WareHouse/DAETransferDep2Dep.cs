using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAETransferDep2Dep 的摘要说明。
	/// </summary>
	public class DAETransferDep2Depf : DAEBase
	{
		public  DataTable GetRptData(string  sWhere)
		{
			StringBuilder sSqlRep = new StringBuilder();
			string sSql = string.Empty;			
			sSql = @"SELECT DISTINCT WH_TransferDEP2DEPMaterial.*,  
				WH_TransferDEP2DEP.TransferDEP2DEPNO AS TransferDEP2DEPNO,  
				WH_TransferDEP2DEP.DepIDNew AS DepIDNew,  
				WH_TransferDEP2DEP.DepIDOld AS DepIDOld,  Material.UOMID AS UOMID, 
				Material.MaterialName AS MaterialName,  
				WH_TransferDEP2DEP.TransferDEP2DEPDate AS TransferDEP2DEPDate,  
				WH_TransferDEP2DEPMaterial.FactIssuedQuantity * WH_TransferDEP2DEPMaterial.UnitPriceNatural 
				AS TotalNatural,  
				WH_TransferDEP2DEPMaterial.FactIssuedQuantity * WH_TransferDEP2DEPMaterial.UnitPriceStandard 
				AS TotalStandard 
			    FROM WH_TransferDEP2DEPMaterial INNER JOIN 
				WH_TransferDEP2DEP ON  
				WH_TransferDEP2DEP.TransferDEP2DEPID = WH_TransferDEP2DEPMaterial.TransferDEP2DEPID 
				INNER JOIN 
				Material ON  
				Material.ItemCode = WH_TransferDEP2DEPMaterial.ItemCode INNER JOIN 
				MaterialUOM ON  
				MaterialUOM.MaterialUomID = WH_TransferDEP2DEPMaterial.MaterialUomID 
				INNER JOIN 
				WH_InStoreMaterialDetail ON  
				WH_InStoreMaterialDetail.ItemCode = WH_TransferDEP2DEPMaterial.ItemCode 
				INNER JOIN
				WH_BI_WareHouse ON 
				WH_BI_WareHouse.WHID = WH_InStoreMaterialDetail.WHID INNER JOIN 
				BI_Department ON 
				BI_Department.IDKey = WH_TransferDEP2DEP.DepIDOld "; 
			sSqlRep.Append(sSql);
			if(sWhere != null && sWhere.Length > 0)
			{
				sSqlRep.Append(" WHERE "+sWhere+" AND WH_TransferDEP2DEP.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			else
			{
				sSqlRep.Append(" WHERE WH_TransferDEP2DEP.Status = '"+(int)ApproveState.State_Approved+"'");
			}
			DataTable dt = this.BaseDataAccess.GetDataTable(sSqlRep.ToString());
			return dt;
		}
	}
}
