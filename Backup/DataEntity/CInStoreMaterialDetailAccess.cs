using System;
using Cnwit.Utility;
using System.Data;
using System.Collections ;
using System.Text ;
using Common;


namespace DataEntity
{
	/// <summary>
	/// CInStoreMaterialDetailAccess 的摘要说明。
	/// </summary>
	public class CInStoreMaterialDetailAccess
	{
		private DataAcess pDataAcess = GetProjectDataAcess.GetDataAcess();
		public CInStoreMaterialDetailAccess()
		{}

		public virtual  bool OperateStore(CInStoreMaterialDetail pInStoreMaterialDetail)
		{
			string[] sParams = {"InStockMaterialID","BINID","POID","ItemCode","WHID","VendorID","QuantityInBinSet","PreserveQuantitySet","MFG","PartNo","UnitPricePONatural","UnitPricePOStandard","Status","Comment","flag","OperateHistory"} ;
			object[] objParamValues = {pInStoreMaterialDetail.InStockMaterialID,pInStoreMaterialDetail.BINID,pInStoreMaterialDetail.POID,pInStoreMaterialDetail.ItemCode,pInStoreMaterialDetail.WHID,pInStoreMaterialDetail.VendorID,pInStoreMaterialDetail.QuantityInBinSet,pInStoreMaterialDetail.PreserveQuantitySet,pInStoreMaterialDetail.MFG,pInStoreMaterialDetail.PartNo,pInStoreMaterialDetail.UnitPricePONatural,pInStoreMaterialDetail.UnitPricePOStandard,pInStoreMaterialDetail.Status,pInStoreMaterialDetail.Comment,pInStoreMaterialDetail.StoreOperateType,pInStoreMaterialDetail.OperateHistory} ; 
			SqlDbType[] paramTypes = { SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Real,SqlDbType.Real,SqlDbType.NVarChar,SqlDbType.NVarChar,SqlDbType.Money, SqlDbType.Money,SqlDbType.Int,SqlDbType.NVarChar,SqlDbType.Int,SqlDbType.Bit} ;
			return pDataAcess.ExecuteSP("spOperateInStoreMaterialDetail",sParams,objParamValues,paramTypes) ; 
		}

	}
}
