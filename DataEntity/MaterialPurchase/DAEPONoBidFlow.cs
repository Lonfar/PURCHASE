using System;
using System.Data ; 
using System.Text ; 
namespace DataEntity
{
	/// <summary>
	/// DAEPONoBidFlow 的摘要说明。
	/// </summary>
	public class DAEPONoBidFlow : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		public DAEPONoBidFlow()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public int GetPurchaseOrderState(string strPkValue,string UserID,string popedomNew)
		{
			String strSql="SELECT * FROM PurchaseOrder WHERE charindex(','+convert(varchar,PurchaseOrder.ApproveStatus)+',',',"+popedomNew+",')>0 AND  PurchaseOrder.POID='"+strPkValue+"' AND PurchaseOrder.CreateBy= '"+ UserID +"'";
			DataTable dtPurchaseOrderState=_da.GetDataTable(strSql);
			return dtPurchaseOrderState.Rows.Count;

		}

		public DataTable GetPOMaterial(String strMRMaterialID )
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" SELECT MR_MaterialRequisition.MRNO,MR_Material.ItemCode,MR_Material.MaterialName, MR_Material.ProductStandard,")
				.Append("  MR_Material.MFG,MR_Material.PartNO,MR_Material.MaterialUomID,MaterialUOM.UOMID,MR_Material.MRQuantity,")
				.Append(" MR_Material.EstUnitPrice ,(MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) as CanPOQuantity  ")
				.Append(" From MR_Material join MR_MaterialRequisition on MR_Material.MRID = MR_MaterialRequisition.MRID")
				.Append(" join MaterialUOM on MaterialUOM.ItemCode = MR_Material.ItemCode and IsBaseUOM = 1")
				.Append(" WHERE MR_Material.MRMaterialID = '" + strMRMaterialID + "' And  MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0) <> 0 ");

//			StringBuilder strSql = new StringBuilder();
//			strSql.Append(" SELECT MR_MaterialRequisition.MRNO,MR_Material.ItemCode,MR_Material.MaterialName, MR_Material.ProductStandard,")
//				.Append(" MR_Material.MFG,MR_Material.PartNO,MR_Material.MaterialUomID,MaterialUOM.UOMID,MR_Material.MRQuantity,")
//				.Append(" MR_Material.EstUnitPrice ,(MR_Material.MRQuantity - isnull(a.HasPOQuantity,0)) as CanPOQuantity ")
//				.Append(" From MR_Material join MR_MaterialRequisition on MR_Material.MRID = MR_MaterialRequisition.MRID")
//				.Append(" join MaterialUOM on MaterialUOM.ItemCode = MR_Material.ItemCode and IsBaseUOM = 1")
//				.Append(" LEFT JOIN(Select sum(POQuantity) as HasPOQuantity,max(MRMaterialID) AS MRMaterialID from POMaterial Where  POMaterial.Status=" + (int)MRState.State_POApproved + "  group by MRMaterialID ")
//				.Append(" ) a on  a.MRMaterialID = MR_Material.MRMaterialID")
//				.Append(" WHERE MR_Material.MRMaterialID = '" + strMRMaterialID + "'  ");
						
			DataTable dt = _da.GetDataTable (strSql.ToString());
			return dt;
		
		}

		public DataTable GetPOMaterialRefresh(String strMRMaterialID )
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" SELECT MR_MaterialRequisition.MRNO,MR_Material.ItemCode,MR_Material.MaterialName, MR_Material.ProductStandard,")
				.Append("  MR_Material.MFG,MR_Material.PartNO,MR_Material.MaterialUomID,MaterialUOM.UOMID,MR_Material.MRQuantity,")
				.Append(" MR_Material.EstUnitPrice ,(MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) as CanPOQuantity  ")
				.Append(" From MR_Material join MR_MaterialRequisition on MR_Material.MRID = MR_MaterialRequisition.MRID")
				.Append(" join MaterialUOM on MaterialUOM.ItemCode = MR_Material.ItemCode and IsBaseUOM = 1")
				.Append(" WHERE MR_Material.MRMaterialID = '" + strMRMaterialID + "'");

						
			DataTable dt = _da.GetDataTable (strSql.ToString());
			return dt;
		
		}

		public DataTable GetPOMaterial(String strPOID , bool bolValue )
		{
			DataTable dt = new DataTable();
			if(bolValue)
			{
				String strSql = " Select POMaterial.ItemCode , POMaterial.MRNO From POMaterial Inner Join PurchaseOrder On POMaterial.POID = PurchaseOrder.POID Where PurchaseOrder.POID = '" + strPOID + "'" ;
				dt = _da.GetDataTable(strSql);
			}
			return dt ;
		}

		public DataTable CheckStateSubmit(String strPOID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Select MR_Material.MRMaterialID , MR_Material.MRQuantity , MR_Material.QuantityInPOConfirm , (POMaterial.POQuantity + IsNull(MR_Material.QuantityInPOConfirm,0)) as HasPOQuantity ")
				.Append(" From POMaterial Inner Join PurchaseOrder  On POMaterial.POID=PurchaseOrder.POID ")
				.Append(" Left Join MR_Material On POMaterial.MRMaterialID = MR_Material.MRMaterialID")
				.Append(" Where  PurchaseOrder.POID='" + strPOID + "' And  MR_Material.MRQuantity < (POMaterial.POQuantity + IsNull(MR_Material.QuantityInPOConfirm,0)) ");
//			StringBuilder strSql = new StringBuilder();
//			strSql.Append(" Select b.MRMaterialID , b.HasPOQuantity , MR_Material.MRQuantity ")
//				.Append(" From (Select POMaterial.MRMaterialID , POMaterial.ItemCode , POMaterial.POQuantity , ")
//				.Append(" (POMaterial.POQuantity + IsNull(a.POQuantity,0)) as HasPOQuantity")
//				.Append(" From POMaterial Inner Join PurchaseOrder On POMaterial.POID=PurchaseOrder.POID")
//				.Append(" Left Join (Select POMaterial.MRMaterialID , Sum(POMaterial.POQuantity) as POQuantity ")
//				.Append(" From POMaterial Where POMaterial.Status>=" + (int)MRState.State_POApproved + " Group By POMaterial.MRMaterialID) as a")
//				.Append(" ON POMaterial.MRMaterialID = a.MRMaterialID Where PurchaseOrder.POID='" + strPOID + "') as b ")
//				.Append(" Inner Join MR_Material On b.MRMaterialID =MR_Material.MRMaterialID ")
//				.Append(" Where b.HasPOQuantity > MR_Material.MRQuantity");
			DataTable dt = _da.GetDataTable(strSql.ToString());
			return dt ;
		}

//		public DataTable CheckStateCompare(String strPOID)
//		{
//			StringBuilder strSql = new StringBuilder();
//			strSql.Append(" Select b.MRMaterialID , b.HasPOQuantity , MR_Material.MRQuantity ")
//				.Append(" From (Select POMaterial.MRMaterialID , POMaterial.ItemCode , POMaterial.POQuantity , ")
//				.Append(" (POMaterial.POQuantity + IsNull(a.POQuantity,0)) as HasPOQuantity")
//				.Append(" From POMaterial Inner Join PurchaseOrder On POMaterial.POID=PurchaseOrder.POID")
//				.Append(" Left Join (Select POMaterial.MRMaterialID , Sum(POMaterial.POQuantity) as POQuantity ")
//				.Append(" From POMaterial Where POMaterial.Status>=" + (int)MRState.State_POApproved + " Group By POMaterial.MRMaterialID) as a")
//				.Append(" ON POMaterial.MRMaterialID = a.MRMaterialID Where PurchaseOrder.POID='" + strPOID + "') as b ")
//				.Append(" Inner Join MR_Material On b.MRMaterialID =MR_Material.MRMaterialID ")
//				.Append(" Where b.HasPOQuantity < MR_Material.MRQuantity");
//			DataTable dt = _da.GetDataTable(strSql.ToString());
//			return dt ;
//		}
		public DataTable GetBaseUom(String itemCode)
		{
			String strSql = " Select MaterialUOM.UOMID From MaterialUOM Where ItemCode = '" + itemCode + "' And MaterialUOM.IsBaseUOM =1" ;
			DataTable dt = _da.GetDataTable(strSql);
			return dt ; 
		
		}
		public String UpdateMRMaterialQuantity(String strPKValue)
		{
			String errMessage = String.Empty ; 
			string[] sParams = {"ID" ,"Type"} ;
			object[] objParamValues = {strPKValue , (int)QuantityType.QuantityInPOConfirm} ; 
			SqlDbType[] paramTypes = { SqlDbType.VarChar , SqlDbType.Int} ;
			bool executeResult = _da.ExecuteSP("spUpdateMRMaterialQuantity",sParams,objParamValues,paramTypes);
			if(!executeResult)
			{
				errMessage = "FailedExecuteSP";
			}
			return errMessage ; 

		}
	}
}
