using System;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAECancelPreserveEdit ��ժҪ˵����
	/// </summary>
	public class DAECancelPreserveEdit : DAEBase
	{
		public DAECancelPreserveEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public void UpdatePreserveMaterial(DataTable dtPreserveMaterial,string sDepID)
		{
			foreach ( DataRow drPreserveMaterial in dtPreserveMaterial.Rows )
			{
				if(drPreserveMaterial.RowState != DataRowState.Deleted)
				{
					string sSql =" SELECT WH_InStoreMaterialDetail.*,MaterialUOM.MaterialUomID, "+
						" ISNULL(FactIssuedPreQuantity,0) AS  FactIssuedPreQuantity, "+
						" ISNULL(PreserveQuantitySum,0) - ISNULL(FactIssuedPreQuantity,0) as CanBackPreQuantity,"+
						" ISNULL(WH_InStoreMaterialDetail.QuantityInBin,0) - ISNULL(WH_InStoreMaterialDetail.PreserveQuantity,0 ) as CanPreQuantity,"+
						" ISNULL(PreserveQuantitySum,0) AS  PreserveQuantitySum, Material.MaterialName 	 "+
						" From WH_InStoreMaterialDetail 	LEFT JOIN  MaterialUOM  	 "+
						" on WH_InStoreMaterialDetail.UOMID =  MaterialUOM.UOMID   	 "+
						" 	AND  MaterialUOM.ItemCode = WH_InStoreMaterialDetail.ItemCode  	AND MaterialUOM.IsBaseUOM = 1  "+
						" 	Left JOIN Material ON Material.ItemCode = WH_InStoreMaterialDetail.ItemCode  	 "+
						" left JOIN  	 "+
						" 	(SELECT InStockMaterialID, 	 "+
						" 	SUM(PreserveQuantityInFact)  "+
						" 	AS FactIssuedPreQuantity  "+ 	
						" 	FROM WH_IssueMaterial   "+
						" 	left join WH_Issue on WH_IssueMaterial.IssueID = WH_Issue.IssueID "+
						" 	where DepID = '"+sDepID+"' AND WH_Issue.Status =  "+(int)ApproveState.State_Approved+" "+
						" 	GROUP BY  InStockMaterialID "+
						" 	) a  	 "+
						" 	ON  "+
						" 	WH_InStoreMaterialDetail.InStockMaterialID = a.InStockMaterialID  	 "+
						" 	left JOIN  	 "+
						" 	( "+
						" 	SELECT InStockMaterialID,SUM(CASE WH_Preserve.IsPreserve WHEN 1 THEN QuantityPreserve ELSE -QuantityByCanceled END) AS PreserveQuantitySum  "+
						" 	FROM WH_PreserveMaterial  	left join WH_Preserve "+
						" 	on WH_PreserveMaterial.PreserveID = WH_Preserve.PreserveID "+
						" 	where DepID = '"+sDepID+"' AND WH_Preserve.Status =  "+(int)ApproveState.State_Approved+" "+
						" 	GROUP BY  InStockMaterialID "+
						" 	) b	 "+
						" 	ON WH_InStoreMaterialDetail.InStockMaterialID = b.InStockMaterialID   "+	
						"	WHERE WH_InStoreMaterialDetail.InStockMaterialID = '"+drPreserveMaterial["InStockMaterialID"].ToString()+"'";
				

					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{
						//ԭ��λ	
						drPreserveMaterial["BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						drPreserveMaterial["WH_PreserveMaterial__BINID"] = dtTempInfo.Rows[0]["BINID"] ;
						//��λ	
						drPreserveMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drPreserveMaterial["WH_PreserveMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UOMID"] ;
						//�������	
						drPreserveMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ;
						drPreserveMaterial["WH_PreserveMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ;
						//Ԥ������	
//						drPreserveMaterial["PreserveQuantity"] = dtTempInfo.Rows[0]["PreserveQuantitySum"] ;
						//ʵ������	
						drPreserveMaterial["QuantityIssuedFact"] = dtTempInfo.Rows[0]["FactIssuedPreQuantity"] ;	
						//�ɳ�������
						drPreserveMaterial["QuantityByPreserved"] = dtTempInfo.Rows[0]["CanBackPreQuantity"] ;
						//���ʱ���
						drPreserveMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;	
						//��������
						drPreserveMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ;
					}
				}
			}
		}

		#region �����������ܴ���Ԥ������
		public void CalTotalAmount(DataTable dtPreserveMaterial ,ref  decimal iQuantityByCanceled,ref decimal iQuantityByPreserved)
		{
			foreach(DataRow drPreserveMaterial in dtPreserveMaterial.Rows)
			{
				if(drPreserveMaterial.RowState != DataRowState.Deleted)
				{
					iQuantityByCanceled =  Convert.ToDecimal(drPreserveMaterial["WH_PreserveMaterial.QuantityByCanceled"].ToString());
					iQuantityByPreserved =  Convert.ToDecimal(drPreserveMaterial["WH_PreserveMaterial.QuantityByPreserved"].ToString());
				}
			}
		}
		#endregion


		#region ���ύ���ʱ����У��( ʵ�������Ƿ���ڿ������� )

		/// <summary>
		/// ���ύ���ʱ����У��
		/// </summary>
		/// <param name="sReceiveID">Ԥ�����ʱ��</param>
		/// <returns></returns>
		public string CheckNum ( string sPreserveID )
		{
			string sErrorMsg = string.Empty;

			string sSelectPreserveMaterial = @"select WH_InStoreMaterialDetail.ItemCode,WH_InStoreMaterialDetail.InStockMaterialID,QuantityPreserve,
														WH_InStoreMaterialDetail.QuantityInBin 
														from WH_PreserveMaterial 
														left join WH_InStoreMaterialDetail on WH_PreserveMaterial.InStockMaterialID 
														= WH_InStoreMaterialDetail.InStockMaterialID
														where 
														WH_InStoreMaterialDetail.QuantityByPreserved - WH_PreserveMaterial.QuantityByCanceled < 0 AND PreserveID = '"+sPreserveID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectPreserveMaterial );
			
			if ( dt.Rows.Count > 0 )
			{ 
				sErrorMsg = dt.Rows[0]["ItemCode"].ToString();
			}

			return sErrorMsg;
		}

		#endregion


		#region ����Ԥ�����ʵ�״̬
		
//		public string UpdatePreserveState ( string sPreserveID , ApproveState state )
//		{
//			int iState = Convert.ToInt32( state );
//			string sErrorMsg = string.Empty;
//			string sUpdateSql = "UPDATE WH_Preserve SET Status = "+iState.ToString()+" WHERE PreserveID = '"+sPreserveID+"' ";
//			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
//			if(sErrorMsg.Length == 0)
//			{			
//				if(state == ApproveState.State_Approved)
//				{
//					string sSql = "SELECT a.* , b.WHID,b.IsPreserve FROM WH_PreserveMaterial a left join WH_Preserve b on a.PreserveID = b.PreserveID WHERE a.PreserveID = '"+sPreserveID+"'";
//					DataTable dtPreserveMaterial = this.BaseDataAccess.GetDataTable ( sSql );
//					foreach(DataRow drPreserveMaterial in dtPreserveMaterial.Rows)
//					{
//						CInStoreMaterialDetailAccess pInStoreMaterialDetailAccess = new CInStoreMaterialDetailAccess();
//						//���
//						CInStoreMaterialDetail pInStore = new CInStoreMaterialDetail();
//						pInStore.StoreOperateType = STOREOPERATETYPE.TYPE_OUT ;
//						pInStore.OperateHistory = false;
//						pInStore.InStockMaterialID = drPreserveMaterial["InStockMaterialID"].ToString() ;
//						pInStore.QuantityInBinSet  = 0;
//						pInStore.PreserveQuantitySet  = Decimal.Parse(drPreserveMaterial["QuantityByCanceled"].ToString()) ;
//						pInStoreMaterialDetailAccess.OperateStore(pInStore);					
//					}
//				}
//			}
//			return sErrorMsg;
//		}		

		#endregion
	}
}
