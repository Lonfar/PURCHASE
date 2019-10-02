using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;

namespace Business
{
	/// <summary>
	/// BUSCommEvaluation 的摘要说明。
	/// </summary>
	public class BUSCommEvaluation:BUSBase
	{
		/// <summary>
		/// 商务评标的数据实体类
		/// </summary>
		DAECommEvaluation dataEntity = new DAECommEvaluation();

		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public BUSCommEvaluation()
		{
			dataEntity = new DAECommEvaluation();
		}

		// Add by ZZH on 2008-1-21 添加验证是否可以删除的方法
		public bool CheckState(String strTenderID , TenderState state)
		{
			DataTable dt = dataEntity.CheckState(strTenderID);
			int strState = -1 ; 
			int intState = (int)state ; 
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["CheckState"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CheckState"]);
			}
			return  strState >= intState ;

		}

		//  Add by ZZH on 2008-1-21 添加验证是否可以删除的方法当节点被下一节点引用时，不应被删除
		public bool CheckDeleteRecord(String strPKValue , TenderState state )
		{
			int strState = -1 ; 
			int intState = (int)state ; 
			DataTable dt = dataEntity.GetRecord(strPKValue) ;
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["State"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["State"]);
			}
			return  strState <= 0 ;
		}
		//*********************************************************

		/// <summary>
		/// 是否在SeePerson表中存在
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtSeePerson )
		{
			foreach(DataRow dr in dtSeePerson.Rows)
			{
				if ( IDKey == dr["CommEvaluationView.ViewerID"].ToString()) return true;
			}
			return false;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool CheckCommGroupExist( string IDKey,DataTable dtCommEvaluationGroup )
		{
			foreach(DataRow dr in dtCommEvaluationGroup.Rows)
			{
				if ( IDKey == dr["CommEvaluationGroup.personID"].ToString()) return true;
			}
			return false;
		}

		/// <summary>
		/// 至少有一个投标商中标
		/// </summary>
		/// <param name="dt_Vendors">投标商子表</param>
		/// <returns>是否符合条件</returns>
		public bool bHasOneTenderAtLeast ( DataTable dt_Vendors )
		{
			bool IsOk = false;

			foreach ( DataRow dr in dt_Vendors.Rows )
			{
				if ( Convert.ToInt32( dr["CommResult.Passed"] ) == 1 )
				{
					IsOk = true;
					break;
				}
			}

			return IsOk;
		}


		public string UpdateMRStatus(string sStatus,string sTenderID,string sName)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append( "Update TCStrategy SET Status = '"+sStatus+"' WHERE TenderID='"+sTenderID+"'; ");
			sb.Append( "Update MR_MRStrategy SET Status = '"+sStatus+"' WHERE TenderID='"+sTenderID+"'; ");
			sb.Append( "Update MR_MaterialRequisition SET Status = '"+sStatus+"' ");
			sb.Append( "where MRID = (Select MRID From MR_Material Inner Join MR_MRStrategy On MR_MRStrategy.MRMaterialID = MR_Material.MRMaterialID ");
			sb.Append( "where tenderid='"+sTenderID+"' And MR_MaterialRequisition.ReceiveBy='"+sName+"')");
			return _da.ExecuteDMLSQL(sb.ToString());
		}


		/// <summary>
		/// 检验当前操作员所承办的SR中是否存在相同的供应商如果存在查看是否已经完成合同签订
		/// </summary>
		/// <param name="dt_Vendors"></param>
		/// <param name="User"></param>
		/// <returns></returns>
		public string IsAllVendorGetContract ( DataTable dt_Vendors , TopisUser User )
		{
			string strVendorName = string.Empty;

			foreach ( DataRow dr in dt_Vendors.Rows )
			{
				if ( Convert.ToInt32( dr["CommResult.Passed"] ) == 1 )
				{
					strVendorName = dataEntity.IsVendorFinishContract ( dr["CommResult.VendorID"].ToString() , User.EmployeeID );
					if ( strVendorName.Length > 0 )
					{
						break;
					}
				}
			}

			return strVendorName;
		}
	}
}
