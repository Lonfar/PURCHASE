using System;
using System.Data;


namespace DataEntity
{
	/// <summary>
	/// DAEMaterialRequest 的摘要说明。
	/// </summary>
	public class DAEMaterialRequest : DAEBase
	{
		public DAEMaterialRequest()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 更新出库单的状态

		public string Update_MR_MaterialRequisition_State ( string sMRID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE MR_MaterialRequisition SET Status = "+iState.ToString()+" WHERE MRID = '"+sMRID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			return sErrorMsg;
		}

		#endregion

		#region 获取MR打印单据数据

		/// <summary>
		/// 获取MR打印单据数据
		/// </summary>
		/// <param name="sMRID"></param>
		/// <returns></returns>
		public DataTable GetPrintData ( string sMRID )
		{
			string sSelectSql = "SELECT * FROM v_Report_MRPrint WHERE MRID = '"+sMRID+"'";
			string sErrorMsg = string.Empty;

			DataTable dtData = this.BaseDataAccess.GetDataTable ( sSelectSql );

			return dtData;
		}

		#endregion

		#region GetMaterialUomIDByItemCodeAndUomID
		public String GetMaterialUomIDByItemCodeAndUomID(String itemCode , String strUomID)
		{
			String materialUomId = String.Empty  ; 
			String strSql = " Select MaterialUomID From MaterialUOM Where ItemCode='" + itemCode + "' And UOMID='" + strUomID + "'" ; 
			DataTable dt = this.BaseDataAccess.GetDataTable(strSql);
			if( dt != null && dt.Rows.Count > 0 )
			{
				materialUomId = dt.Rows[0]["MaterialUomID"].ToString() ; 
			}
			return materialUomId ;
		}
		#endregion

		#region CheckItemCode
		public bool CheckItemCode(String strItemCode)
		{
			String strSql = " Select ItemCode From Material Where ItemCode='" + strItemCode + "'" ;
			DataTable dt = this.BaseDataAccess.GetDataTable(strSql);
			if( dt != null )
			{
				return (dt.Rows.Count > 0) ;
			}
			return false ;
		
		}
		#endregion


		public DataTable IsModifyMRNo(string strMRKey)
		{			
			string strSql = " Select *  From MR_MaterialRequisition Where MR_MaterialRequisition.MRID = '" + strMRKey +"'";
			return this.BaseDataAccess.GetDataTable( strSql );
		}

		public DataTable CheckMRHasExist( string strMRNo)
		{
			string message = string.Empty;
			string strSql = " Select *  From MR_MaterialRequisition Where MR_MaterialRequisition.MRNO = '" + strMRNo +"'";
			return this.BaseDataAccess.GetDataTable( strSql );
		}

		// Add by ZZH on 2008-1-18 添加验证是否可以删除的方法
		public DataTable CheckState(String strMRID )
		{
			String strSql = " Select Max(PurchaseOrder.ApproveStatus) as CheckState From MR_Material Left Join POMaterial On MR_Material.MRMaterialID = POMaterial.MRMaterialID " +
							" Left Join PurchaseOrder On PurchaseOrder.POID = POMaterial.POID  Where MR_Material.MRID = '" + strMRID + "'" ;
			DataTable dt = this.BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select Max(MR_Material.Status) as State From MR_Material Inner Join  MR_MaterialRequisition On MR_Material.MRID=MR_MaterialRequisition.MRID Where MR_MaterialRequisition.MRID='" + strPKValue + "'" ;
			DataTable dt = BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}
		//****************************************************
	}
}
