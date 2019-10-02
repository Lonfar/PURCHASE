using System;
using System.Data;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEMaterialRequest 的摘要说明。
	/// </summary>
	public class DAEPOSign : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAEPOSign ()
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
		public  DataTable GetPOMaterialbyEvaluationID(string bidEvaluationID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Select MR_Material.MRMaterialID,MaterialUOM.MaterialUomID,MaterialUOM.UOMID,MR_Material.ItemCode, MR_Material.MRQuantity ,")
				.Append(" (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) as CanPOQuantity ,")
				.Append(" MR_MRStrategy.MRNO , MR_Material.PartNO , MR_Material.MaterialName ")
				.Append(" From TCStrategy Inner Join MR_MRStrategy On TCStrategy.TenderID = MR_MRStrategy.TenderID")
				.Append(" Inner Join MR_Material On MR_MRStrategy.MRMaterialID = MR_Material.MRMaterialID ")
				.Append(" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID")
				.Append(" Where TCStrategy.TenderID='" + bidEvaluationID + "'")
				.Append(" And (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) <> 0");

//			String strSql = "  Select MR_Material.MRMaterialID," +
//				" MAX(MaterialUOM.MaterialUomID) as MaterialUomID ,"+
//				" max(MaterialUOM.UOMID) as UOMID,"+
//				" max(MR_Material.ItemCode) as ItemCode, "+
//				" max(MR_Material.MRQuantity) as MRQuantity , "+
//				" (max(MR_Material.MRQuantity) - isnull(max(MR_Material.QuantityInPOConfirm),0)) as CanPOQuantity , "+
//				" max(MR_EnquiryMaterial.MRNO) as MRNO, "+
//				" max(MR_Material.PartNO) as PartNO, "+
//				" max(MR_Material.MaterialName) as MaterialName, "+
//				" max(MR_QuotationPriceMaterial.UnitPriceNoTax) as UnitPriceNoTax"+
//				" From MR_BIDEvaluation Inner Join MR_BIDSummary "+
//				" On MR_BIDEvaluation.BIDEvaluationID = MR_BIDSummary.BIDEvaluationID Inner Join MR_EnquiryPrice "+
//				" On MR_EnquiryPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID Inner Join  MR_EnquiryMaterial "+
//				" ON MR_EnquiryPrice.EnquiryPriceID = MR_EnquiryMaterial.EnquiryPriceID "+
//				" Inner Join MR_Material On MR_EnquiryMaterial.MRMaterialID = MR_Material.MRMaterialID  "+
//				" Inner Join MR_QuotationPrice On MR_QuotationPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID "+
//				" And  MR_QuotationPrice.VendorNo = MR_BIDSummary.VendorNo Inner Join  MR_QuotationPriceMaterial "+
//				" On MR_QuotationPrice.QuotationPriceID=MR_QuotationPriceMaterial.QuotationPriceID And "+
//				" MR_QuotationPriceMaterial.EnquiryMaterialID = MR_EnquiryMaterial.EnquiryMaterialID"+
//				" And MR_QuotationPriceMaterial.MRNO=MR_EnquiryMaterial.MRNO "+
//				" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID "+
//				" Where MR_BIDEvaluation.BIDEvaluationID='"+bidEvaluationID+"' "+
//				" and (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) <> 0 "+
//				" group by MR_Material.MRMaterialID";
//			StringBuilder strSql = new StringBuilder();
//			strSql.Append(" Select MR_Material.MRMaterialID,MaterialUOM.MaterialUomID,MaterialUOM.UOMID,MR_Material.ItemCode, MR_Material.MRQuantity ,")
//				.Append(" (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) as CanPOQuantity ,")
//				.Append(" MR_EnquiryMaterial.MRNO , MR_Material.PartNO , MR_Material.MaterialName , MR_QuotationPriceMaterial.UnitPriceNoTax")
//				.Append(" From MR_BIDEvaluation Inner Join MR_BIDSummary On MR_BIDEvaluation.BIDEvaluationID = MR_BIDSummary.BIDEvaluationID")
//				.Append(" Inner Join MR_EnquiryPrice On MR_EnquiryPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID Inner Join ")
//				.Append(" MR_EnquiryMaterial ON MR_EnquiryPrice.EnquiryPriceID = MR_EnquiryMaterial.EnquiryPriceID")
//				.Append(" Inner Join MR_Material On MR_EnquiryMaterial.MRMaterialID = MR_Material.MRMaterialID ")
//				.Append(" Inner Join MR_QuotationPrice On MR_QuotationPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID And ")
//				.Append(" MR_QuotationPrice.VendorNo = MR_BIDSummary.VendorNo Inner Join ")
//				.Append(" MR_QuotationPriceMaterial On MR_QuotationPrice.QuotationPriceID=MR_QuotationPriceMaterial.QuotationPriceID And")
//				.Append(" MR_QuotationPriceMaterial.EnquiryMaterialID = MR_EnquiryMaterial.EnquiryMaterialID And MR_QuotationPriceMaterial.MRNO=MR_EnquiryMaterial.MRNO")
//				.Append(" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID")
//				.Append(" Where MR_BIDEvaluation.BIDEvaluationID='" + bidEvaluationID + "'")
//				.Append(" and (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) <> 0");

//			StringBuilder strSql = new StringBuilder();
//			strSql.Append(" Select MR_Material.MRMaterialID,MaterialUOM.MaterialUomID,MaterialUOM.UOMID,MR_Material.ItemCode, MR_Material.MRQuantity ,")
//				.Append(" (MR_Material.MRQuantity - isnull(a.HasPOQuantity,0)) as CanPOQuantity ,")
//				.Append(" MR_EnquiryMaterial.MRNO , MR_Material.PartNO , MR_Material.MaterialName , MR_QuotationPriceMaterial.UnitPriceNoTax")
//				.Append(" From MR_BIDEvaluation Inner Join MR_BIDSummary On MR_BIDEvaluation.BIDEvaluationID = MR_BIDSummary.BIDEvaluationID")
//				.Append(" Inner Join MR_EnquiryPrice On MR_EnquiryPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID Inner Join ")
//				.Append(" MR_EnquiryMaterial ON MR_EnquiryPrice.EnquiryPriceID = MR_EnquiryMaterial.EnquiryPriceID")
//				.Append(" Inner Join MR_Material On MR_EnquiryMaterial.MRMaterialID = MR_Material.MRMaterialID ")
//				.Append(" Inner Join MR_QuotationPrice On MR_QuotationPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID And ")
//				.Append(" MR_QuotationPrice.VendorNo = MR_BIDSummary.VendorNo Inner Join ")
//				.Append(" MR_QuotationPriceMaterial On MR_QuotationPrice.QuotationPriceID=MR_QuotationPriceMaterial.QuotationPriceID And")
//				.Append(" MR_QuotationPriceMaterial.EnquiryMaterialID = MR_EnquiryMaterial.EnquiryMaterialID And MR_QuotationPriceMaterial.MRNO=MR_EnquiryMaterial.MRNO")
//				.Append(" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID")
//				.Append(" LEFT JOIN(Select sum(POQuantity) as HasPOQuantity,max(MRMaterialID) AS MRMaterialID from POMaterial left join PurchaseOrder ")
//				.Append(" on PurchaseOrder.POID = POMaterial.POID where POMaterial.Status>='" + (int)MRState.State_POApproved + "' group by MRMaterialID ")
//				.Append(" ) a on  a.MRMaterialID = MR_Material.MRMaterialID")
//				//.Append(" left JOIN  MaterialUOM On MaterialUOM.ItemCode = MR_Material.ItemCode And MaterialUOM.IsBaseUOM =1")
//				.Append(" Where MR_BIDEvaluation.BIDEvaluationID='" + bidEvaluationID + "'")
//				.Append(" And MR_BIDSummary.VendorID='" + vendorID + "' and (MR_Material.MRQuantity - isnull(a.HasPOQuantity,0)) <> 0");
			DataTable dt = _da.GetDataTable(strSql.ToString());
			return dt;
			

		}

		public DataTable GetRefreshMaterial(String bidEvaluationID , String strMRMaterialID )
		{

			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Select MR_Material.MRMaterialID,MaterialUOM.MaterialUomID,MaterialUOM.UOMID,MR_Material.ItemCode, MR_Material.MRQuantity ,")
				.Append(" (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) as CanPOQuantity ,")
				.Append(" MR_MRStrategy.MRNO , MR_Material.PartNO , MR_Material.MaterialName ")
				.Append(" From TCStrategy Inner Join MR_MRStrategy On TCStrategy.TenderID = MR_MRStrategy.TenderID")
				.Append(" Inner Join MR_Material On MR_MRStrategy.MRMaterialID = MR_Material.MRMaterialID ")
				.Append(" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID")
				.Append(" Where TCStrategy.TenderID='" + bidEvaluationID + "'")
				.Append(" And MR_Material.MRMaterialID = '" + strMRMaterialID + "'");

//			StringBuilder strSql = new StringBuilder();
//			strSql.Append(" Select MR_Material.MRMaterialID,MaterialUOM.MaterialUomID,MaterialUOM.UOMID,MR_Material.ItemCode, MR_Material.MRQuantity ,")
//				.Append(" (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) as CanPOQuantity ,")
//				.Append(" MR_EnquiryMaterial.MRNO , MR_Material.PartNO , MR_Material.MaterialName , MR_QuotationPriceMaterial.UnitPriceNoTax")
//				.Append(" From MR_BIDEvaluation Inner Join MR_BIDSummary On MR_BIDEvaluation.BIDEvaluationID = MR_BIDSummary.BIDEvaluationID")
//				.Append(" Inner Join MR_EnquiryPrice On MR_EnquiryPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID Inner Join ")
//				.Append(" MR_EnquiryMaterial ON MR_EnquiryPrice.EnquiryPriceID = MR_EnquiryMaterial.EnquiryPriceID")
//				.Append(" Inner Join MR_Material On MR_EnquiryMaterial.MRMaterialID = MR_Material.MRMaterialID ")
//				.Append(" Inner Join MR_QuotationPrice On MR_QuotationPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID And ")
//				.Append(" MR_QuotationPrice.VendorNo = MR_BIDSummary.VendorNo Inner Join ")
//				.Append(" MR_QuotationPriceMaterial On MR_QuotationPrice.QuotationPriceID=MR_QuotationPriceMaterial.QuotationPriceID And")
//				.Append(" MR_QuotationPriceMaterial.EnquiryMaterialID = MR_EnquiryMaterial.EnquiryMaterialID And MR_QuotationPriceMaterial.MRNO=MR_EnquiryMaterial.MRNO")
//				.Append(" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID")
//				.Append(" Where MR_BIDEvaluation.BIDEvaluationID='" + bidEvaluationID + "'")
//				.Append(" and (MR_Material.MRQuantity - isnull(MR_Material.QuantityInPOConfirm,0)) <> 0")
//				.Append(" And MR_Material.MRMaterialID = '" + strMRMaterialID + "'");
//			StringBuilder strSql = new StringBuilder();
//			strSql.Append(" Select MR_Material.MRMaterialID,MaterialUOM.MaterialUomID,MaterialUOM.UOMID,MR_Material.ItemCode, MR_Material.MRQuantity ,")
//				.Append(" (MR_Material.MRQuantity - isnull(a.HasPOQuantity,0)) as CanPOQuantity ,")
//				.Append(" MR_EnquiryMaterial.MRNO , MR_Material.PartNO , MR_Material.MaterialName , MR_QuotationPriceMaterial.UnitPriceNoTax")
//				.Append(" From MR_BIDEvaluation Inner Join MR_BIDSummary On MR_BIDEvaluation.BIDEvaluationID = MR_BIDSummary.BIDEvaluationID")
//				.Append(" Inner Join MR_EnquiryPrice On MR_EnquiryPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID Inner Join ")
//				.Append(" MR_EnquiryMaterial ON MR_EnquiryPrice.EnquiryPriceID = MR_EnquiryMaterial.EnquiryPriceID")
//				.Append(" Inner Join MR_Material On MR_EnquiryMaterial.MRMaterialID = MR_Material.MRMaterialID ")
//				.Append(" Inner Join MR_QuotationPrice On MR_QuotationPrice.EnquiryPriceID = MR_BIDEvaluation.EnquiryPriceID And ")
//				.Append(" MR_QuotationPrice.VendorNo = MR_BIDSummary.VendorNo Inner Join ")
//				.Append(" MR_QuotationPriceMaterial On MR_QuotationPrice.QuotationPriceID=MR_QuotationPriceMaterial.QuotationPriceID And")
//				.Append(" MR_QuotationPriceMaterial.EnquiryMaterialID = MR_EnquiryMaterial.EnquiryMaterialID And MR_QuotationPriceMaterial.MRNO=MR_EnquiryMaterial.MRNO")
//				.Append(" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID")
//				.Append(" LEFT JOIN(Select sum(POQuantity) as HasPOQuantity,max(MRMaterialID) AS MRMaterialID from POMaterial left join PurchaseOrder ")
//				.Append(" on PurchaseOrder.POID = POMaterial.POID where  POMaterial.Status >= '" + (int)MRState.State_POApproved + "'group by MRMaterialID ")
//				.Append(" ) a on  a.MRMaterialID = MR_Material.MRMaterialID")
//				//.Append(" left JOIN  MaterialUOM On MaterialUOM.ItemCode = MR_Material.ItemCode And MaterialUOM.IsBaseUOM =1")
//				.Append(" Where MR_BIDEvaluation.BIDEvaluationID='" + bidEvaluationID + "'")
//				.Append(" And MR_BIDSummary.VendorID='" + vendorID + "' and (MR_Material.MRQuantity - isnull(a.HasPOQuantity,0)) <> 0 ")
//				.Append(" And MR_Material.MRMaterialID = '" + strMRMaterialID + "'");
			DataTable dt = _da.GetDataTable(strSql.ToString());
			return dt;
		}

		public DataTable GetBIDEvaluationIDInPO(String strBIDEvaluationID)
		{
			String strSql = " Select PurchaseOrder.POID , PurchaseOrder.BIDEvaluationID From PurchaseOrder Where PurchaseOrder.BIDEvaluationID = '" + strBIDEvaluationID + "'" ;
			DataTable dt = _da.GetDataTable(strSql);
			return dt ;
		}

		public DataTable GetPOMaterial(string materialMRId,string itemCode,string bidEvaluationID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Select POMaterial.POQuantity From PurchaseOrder Inner Join POMaterial On PurchaseOrder.POID=POMaterial.POID ")
				.Append(" Where PurchaseOrder.BIDEvaluationID='" + bidEvaluationID + "' And POMaterial.MRMaterialID='" + materialMRId + "' ")
				.Append(" And POMaterial.itemCode='" + itemCode + "'");
			DataTable dt=_da.GetDataTable(strSql.ToString());
			return dt;

		}
		public DataTable GetPOMaterial(string bidEvaluationID)
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append(" Select POMaterial.POQuantity From PurchaseOrder Inner Join POMaterial On PurchaseOrder.POID=POMaterial.POID ")
				.Append(" Where PurchaseOrder.BIDEvaluationID='" + bidEvaluationID + "'");
			DataTable dt=_da.GetDataTable(strSql.ToString());
			return dt;

		}

		public DataTable GetPORecordByPOID(String strPOID)
		{
			String strSql = "Select PurchaseOrder.BIDEvaluationID , PurchaseOrder.VendorID From PurchaseOrder Where PurchaseOrder.POID = '" + strPOID + "'";
			DataTable dt=_da.GetDataTable(strSql);
			return dt;
		}

		public String GetTenderIdByPOID(String strPOID)
		{
			String tenderID = String.Empty ; 
			String strSql = "Select PurchaseOrder.TenderID From PurchaseOrder Where PurchaseOrder.POID = '" + strPOID + "'";
			DataTable dt=_da.GetDataTable(strSql);
			if(dt != null && dt.Rows.Count > 0)
			{
				tenderID = dt.Rows[0]["TenderID"] == DBNull.Value ? "" : dt.Rows[0]["TenderID"].ToString() ;
			}
			return tenderID ;
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

		public DataTable GetMaterialByMRMaterialID(String strMRMaterialID)
		{
			String strSql = "Select POMaterial.MRMaterialID , POMaterial.ItemCode , POMaterial.POQuantity From POMaterial Where POMaterial.MRMaterialID = '" + strMRMaterialID + "' And POMaterial.Status=" + (int)MRState.State_POApproved + "";
			DataTable dt=_da.GetDataTable(strSql);
			return dt;
		}

		public DataTable GetBaseUom(String itemCode)
		{
			String strSql = " Select MaterialUOM.UOMID From MaterialUOM Where ItemCode = '" + itemCode + "' And MaterialUOM.IsBaseUOM =1" ;
			DataTable dt = _da.GetDataTable(strSql);
			return dt ; 
		
		}

		public DataTable GetPOMaterialByPOID(string strPOID )
		{
			StringBuilder strSql = new StringBuilder();
			strSql.Append("Select POMaterial.POMaterialID ,POMaterial.ItemCode , POMaterial.POQuantity , POMaterial.MRNO , POMaterial.MRMaterialID From POMaterial Where POID='" + strPOID + "'");
			DataTable dt = _da.GetDataTable(strSql.ToString());
			return dt ;
		}
		/// <summary>
		/// 得到所有查看人信息
		/// </summary>
		/// <returns></returns>
		public DataTable GetSeePerson( string pkValue )
		{
			string strSql = "";
			strSql = @"select distinct BI_Employee.FullName as [BI_Employee.FullName], 
										BI_PositionType.PositionName as [BI_PositionType.PositionName], 
										'Edit' RowStatus, 
										MR_Viewer.ViewerID as [MR_Viewer.ViewerID], 
										MR_Viewer.ViewerName as [MR_Viewer.ViewerName], 
										MR_Viewer.ObjectiveID as [MR_Viewer.ObjectiveID],
										MR_Viewer.ObjectiveType as [MR_Viewer.ObjectiveType], 
										MR_Viewer.ViewerPosition as [MR_Viewer.ViewerPosition],
										BI_DepartmentEmployee.DepartmentID as [MR_Viewer.ViewerDept] 
						from  MR_Viewer,BI_Employee,BI_DepartmentEmployee,BI_PositionType 
						where MR_Viewer.ViewerName = BI_Employee.IDKey 
						and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID 
						and MR_Viewer.ViewerDept = BI_DepartmentEmployee.DepartmentID 
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey 
						and MR_Viewer.ObjectiveID = '"+pkValue+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			
			return dt;
		}

		public void DelViewer(string strIDKey)
		{
			string strSql = "delete MR_Viewer where ViewerID ='"+strIDKey+"'";
			_da.GetDataTable(strSql);
		}

		/// <summary>
		/// 根据BI_DepartmentEmployee.IDKey 得到BI_Employee.IDKey
		/// </summary>
		/// <returns></returns>
		public string GetBI_EmployeeID( string pkValue )
		{
			string strSql = "";
			strSql = @"select	BI_Employee.IDKey							  
						from 	BI_Employee, BI_DepartmentEmployee
						where 	BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						        and BI_DepartmentEmployee.IDKey = '"+pkValue+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			if(dt.Rows.Count > 0)
			{
				return dt.Rows[0]["IDKey"].ToString();
			}
			else return "";
		}

		/// <summary>
		/// 得到单个用户信息
		/// </summary>
		/// <param name="UserID">用户ID(暂时保存为部门员工的IDKey，以后可能会有变动)</param>
		/// <returns></returns>
		public DataTable GetSeePerson_User( string UserID )
		{
			string strSql = "";
			strSql = @"SELECT BI_Employee.FullName,                 
							  BI_PositionType.PositionName,
							  BI_Employee.IDKey,
							  BI_DepartmentEmployee.DepartmentID

						FROM  BI_Employee , 
							  BI_DepartmentEmployee,
							  BI_PositionType

						where BI_DepartmentEmployee.IDKey='"+UserID+"'";
			strSql += @"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// 得到指定部门的人员信息
		/// </summary>
		/// <param name="DepartmentID">部门ID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Department( string DepartmentID )
		{
			string strSql = "";
			strSql = @"select  BI_Employee.FullName,
							   BI_PositionType.PositionName,
							   BI_Employee.IDKey,
							   BI_DepartmentEmployee.DepartmentID

						from BI_Employee,
							 BI_Department,
							 BI_DepartmentEmployee,
							 BI_PositionType
						where BI_DepartmentEmployee.DepartmentID = '"+DepartmentID+"'";
			strSql += @"and BI_Department.IDKey = BI_DepartmentEmployee.DepartmentID
					   and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
					   and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey
						";

			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// 得到指定组的人员信息
		/// </summary>
		/// <param name="UserID">GroupID</param>
		/// <returns></returns>
		public DataTable GetSeePerson_Group( string GroupID )
		{
			string strSql = "";
			strSql = @"select  
							BI_Employee.FullName,
							BI_PositionType.PositionName,
  							BI_Employee.IDKey,
							BI_DepartmentEmployee.DepartmentID

						from TI_DefaultGroupMember,
							 BI_Employee,
							 BI_PositionType,
							 BI_DepartmentEmployee

						where TI_DefaultGroupMember.IDKEY = '"+GroupID+"'";
			strSql +=@"	and BI_Employee.IDKey = BI_DepartmentEmployee.EmployeeID
						and TI_DefaultGroupMember.GroupUserID = BI_DepartmentEmployee.IDKey
						and BI_DepartmentEmployee.PositionID = BI_PositionType.IDKey";

			System.Data.DataTable dt = _da.GetDataTable ( strSql );
			return dt;
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
