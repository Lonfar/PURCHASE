using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataEntity
{
	/// <summary>
	/// DAEBorrow ��ժҪ˵����
	/// </summary>
	public class DAEBorrow : DAEBase
	{
		public DAEBorrow()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���³��ⵥ��״̬

		public string UpdateWH_BorrowState ( string BorrowID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_Borrow SET Status = "+iState.ToString()+" WHERE BorrowID = '"+BorrowID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			//���β�����
//			if(sErrorMsg.Length == 0)
//			{			
//				if(state == ApproveState.State_Approved)
//				{
//					string sSql = "SELECT a.* , b.* FROM WH_Borrow a left join WH_BorrowMaterial b on a.BorrowID = b.BorrowID WHERE a.BorrowID = '"+BorrowID+"'";
//					DataTable dtBorrow = this.BaseDataAccess.GetDataTable ( sSql );
//					
//					foreach(DataRow drBorrowEdit in dtBorrow.Rows)
//					{
//						//����
//						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
//						CInStoreMaterialDetail pOutStore = new CInStoreMaterialDetail();
//						//
//						pOutStore.StoreOperateType = STOREOPERATETYPE.TYPE_OUT ;
//						pOutStore.OperateHistory = true;
//						pOutStore.InStockMaterialID = drBorrowEdit["InStockMaterialID"].ToString() ;		 
//						//
//						//pOutStore.QuantityInBinSet  =  cen.ChangeFromBaseUON(drBorrowEdit["ItemCode"].ToString(),drBorrowEdit["MaterialUomID"].ToString(),decimal.Parse(drIssueEdit["QuantityInBin"].ToString()));
//				 						
//						pInStoreMaterialDetailAccess.OperateStore(pOutStore);
//					}
//				}
//			}
			return sErrorMsg;
		}

		#endregion


		/// ���ύ���ʱ����У��
		/// </summary>
		/// <param name="sReceiveID">���ϵ����</param>
		/// <returns></returns>
		public string CheckNum ( string sBorrowID )
		{
			string sErrorMsg = string.Empty;

			string sSelectReceiveMaterial = @"select WH_InStoreMaterialDetail.ItemCode,
												WH_InStoreMaterialDetail.InStockMaterialID,
												QuantityBorrow,
												WH_InStoreMaterialDetail.QuantityInBin 
												from WH_BorrowMaterial 
												left join WH_InStoreMaterialDetail on WH_BorrowMaterial.InStockMaterialID 
												= WH_InStoreMaterialDetail.InStockMaterialID
												where 
												QuantityInBin - QuantityBorrow <0 AND BorrowID = '"+sBorrowID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectReceiveMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ sErrorMsg = dt.Rows[0]["ItemCode"].ToString();	}

			return sErrorMsg;
		}


	}
}
