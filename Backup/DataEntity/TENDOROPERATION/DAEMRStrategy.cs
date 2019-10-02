using System;
using System.Data;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// DAEMRStrategy 的摘要说明。
	/// </summary>
	public class DAEMRStrategy : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAEMRStrategy()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
		public DataTable CheckState(String strTenderID )
		{
			String strSql = " Select Max(PurchaseOrder.ApproveStatus) as CheckState From TCStrategy Inner Join PurchaseOrder On TCStrategy.TenderID = PurchaseOrder.TenderID Where TCStrategy.TenderID ='" + strTenderID + "'" ;
			DataTable dt = _da.GetDataTable(strSql) ; 
			return dt ; 
		}

		public DataTable GetRecord(String strPKValue)
		{
			String strSql = " Select Status as State From TCStrategy Where TenderID='" + strPKValue + "'" ;
			DataTable dt = BaseDataAccess.GetDataTable(strSql) ; 
			return dt ; 
		}
		//****************************************************

		/// <summary>
		/// 根据服务申请编号获得服务申请其他信息
		/// </summary>
		/// <param name="dataTable"></param>
		public void UpdataDataTable_RelatMR ( DataTable dataTable )
		{
			string SelectSql = string.Empty;
			DataTable dt_Temp ;

			foreach ( DataRow dr in dataTable.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					StringBuilder strSql = new StringBuilder();
					strSql.Append(" SELECT MR_Material.MRMaterialID,MaterialUOM.MaterialUomID,MaterialUOM.UOMID,MR_Material.ItemCode,");
					strSql.Append("	MR_Material.MRQuantity ,MR_Material.ProductStandard, ");
					strSql.Append(" MR_MaterialRequisition.MRNO,MR_Material.PartNO ,MR_Material.MFG, MR_Material.MaterialName ");
					strSql.Append(" FROM MR_MaterialRequisition ");
					strSql.Append(" inner join MR_Material on MR_Material.MRID = MR_MaterialRequisition.MRID ");
					strSql.Append(" LEFT JOIN MaterialUOM on MaterialUOM.MaterialUomID = MR_Material.MaterialUomID ");
					strSql.Append(" Where MR_Material.MRMaterialID = '"+dr["MRMaterialID"].ToString()+"' ");

					dt_Temp = _da.GetDataTable ( strSql.ToString() );

					if ( dt_Temp.Rows.Count > 0 )
					{
						dr["MRNO"] = dt_Temp.Rows[0]["MRNO"]  == DBNull.Value ? "" : dt_Temp.Rows[0]["MRNO"].ToString();
						dr["ItemCode"] = dt_Temp.Rows[0]["ItemCode"] == DBNull.Value ? "" : dt_Temp.Rows[0]["ItemCode"].ToString(); 
						dr["MaterialName"] = dt_Temp.Rows[0]["MaterialName"] == DBNull.Value ? "" : dt_Temp.Rows[0]["MaterialName"].ToString();
						dr["ProductStandard"] = dt_Temp.Rows[0]["ProductStandard"] == DBNull.Value ? "" : dt_Temp.Rows[0]["ProductStandard"].ToString(); 
						dr["MFG"] = dt_Temp.Rows[0]["MFG"] == DBNull.Value ? "" : dt_Temp.Rows[0]["MFG"].ToString();
						dr["PartNO"] = dt_Temp.Rows[0]["PartNO"] == DBNull.Value ? "" : dt_Temp.Rows[0]["PartNO"].ToString();
     					dr["MaterialUomID"] = dt_Temp.Rows[0]["MaterialUomID"] == DBNull.Value ? "" : dt_Temp.Rows[0]["MaterialUomID"].ToString();
						dr["MR_MRStrategy__MaterialUomID"] =  dt_Temp.Rows[0]["UomID"] == DBNull.Value ? "" : dt_Temp.Rows[0]["UomID"].ToString();
						dr["MRQuantity"] = dt_Temp.Rows[0]["MRQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(dt_Temp.Rows[0]["MRQuantity"]);
					}
				}
			}
		}

		public String GetBaseUom(String itemCode)
		{
			if(itemCode.Length > 0)
			{
				String strSql = " Select MaterialUOM.UOMID From MaterialUOM Where ItemCode = '" + itemCode + "' And MaterialUOM.IsBaseUOM =1" ;
				DataTable dt = _da.GetDataTable(strSql);
				return dt.Rows[0][0] == DBNull.Value ? "" : dt.Rows[0][0].ToString();
			}
			return "";
		
		}

		public DataTable GetMRStrategy(String tenderId)
		{
			String strSql = " Select MR_MRStrategy.MRMaterialID , MR_MRStrategy.ItemCode , MR_MRStrategy.MRNO From TCStrategy Inner Join MR_MRStrategy On TCStrategy.TenderID = MR_MRStrategy.TenderID And TCStrategy.TenderID='" + tenderId + "'";
			DataTable dt = _da.GetDataTable(strSql);
			return dt ; 
		}
	}
}