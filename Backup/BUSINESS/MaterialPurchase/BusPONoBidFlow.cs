using System;
using DataEntity;
using System.Data;

namespace Business
{
	/// <summary>
	/// BusPONoBidFlow ��ժҪ˵����
	/// </summary>
	public class BusPONoBidFlow : BUSBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		DAEPONoBidFlow daePONoBidFlow = new DAEPONoBidFlow();
		public BusPONoBidFlow()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����������Ϣ
		/// <summary>
		/// У���ӱ��Ƿ�������
		/// </summary>
		/// <param name="dt"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// ������Ϣ
			string sErrorMsg = string.Empty;
			// У���ӱ��Ƿ�������
			sErrorMsg = CheckChildRows(dt);
			if ( sErrorMsg.Trim().Length == 0 )
			{
				// У��ҵ������           
				sErrorMsg = CheckMaterial ( dt );
				if ( sErrorMsg.Trim().Length > 0 )
				{ 
					return sErrorMsg;
				}
			}
			return sErrorMsg;
		}

		/// <summary>
		/// �ж������Ƿ�С��0
		/// </summary>
		/// <param name="dtMaterial"></param>
		/// <returns></returns>
		public string CheckMaterial ( DataTable dtMaterial )
		{
			foreach(DataRow drMaterial in dtMaterial.Rows)
			{
				if (drMaterial.RowState != DataRowState.Deleted)
				{
					decimal dPOQuantity = Convert.ToDecimal(drMaterial["POMaterial.POQuantity"] == DBNull.Value ? 0 : drMaterial["POMaterial.POQuantity"]); 
					decimal dCanPOQuantity = Convert.ToDecimal(drMaterial["POMaterial.CanPOQuantity"] == DBNull.Value ? 0 : drMaterial["POMaterial.CanPOQuantity"]); 
					if (dCanPOQuantity < dPOQuantity)
					{	
						return "Error01" ;
					}
				}
			}

			return string.Empty ;
		}

		private string CheckChildRows(DataTable dtChild)
		{
			string sErrMsg = "";
			DataTable dtCheckRow = dtChild.Copy();
			dtCheckRow.AcceptChanges();
			if(dtCheckRow.Rows.Count == 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}

		#endregion

		public int GetPurchaseOrderStatus(string sPOID,string nStatus)
		{
			string sql  = "SELECT 1 FROM PurchaseOrder WHERE charindex(','+convert(varchar,PurchaseOrder.ApproveStatus)+',',',"+nStatus+",')>0 AND POID ='"+sPOID+"'";

			return _da.GetDataTable(sql).Rows.Count;

		}
		public bool CheckState(String strPOID)
		{
			return (!CheckStateSubmit(strPOID)) ;
		}

		public bool CheckStateSubmit(String strPOID )
		{
			DataTable dtMaterial = daePONoBidFlow.CheckStateSubmit(strPOID);
			return dtMaterial.Rows.Count > 0 ;
		}
//		public bool CheckStateCompare(String strPOID)
//		{
//			DataTable dtMaterial = daePONoBidFlow.CheckStateCompare(strPOID);
//			
//			if(dtMaterial.Rows.Count > 0 )
//			{
//				return false ;
//			}
//			else
//			{
//				return true ;
//			}
//			
//		}

		public void UpdatePOMaterial(DataTable dtPOMaterial , bool bolValue )
		{
			if(dtPOMaterial != null)
			{
				if(bolValue)
				{
					foreach ( DataRow drPOMaterial in dtPOMaterial.Rows )
					{
						if(drPOMaterial.RowState != DataRowState.Deleted)
						{
							DataTable  dtTempInfo = daePONoBidFlow.GetPOMaterial(drPOMaterial["MRMaterialID"].ToString() );
							if (dtTempInfo.Rows.Count > 0 )
							{
						
								//������	
								drPOMaterial["MRNO"] = dtTempInfo.Rows[0]["MRNO"] ;
								//���ʱ���	
								drPOMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ; 
								//��������
								drPOMaterial["MaterialDescription"] = dtTempInfo.Rows[0]["MaterialName"] ; 
								//��Ʒ���
								drPOMaterial["PartNo"] = dtTempInfo.Rows[0]["PartNo"] ;
								//��λ
								drPOMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
								//						drMaterial["MR_Material__MaterialUomID"] = dtTempInfo.Rows[0]["UomID"] ;			
								//��ǩ����
								drPOMaterial["CanPOQuantity"] = dtTempInfo.Rows[0]["CanPOQuantity"] ;
//								//����
//								drPOMaterial["POQuantity"] = drPOMaterial["CanPOQuantity"] ;

								drPOMaterial["POMaterial__MaterialUomID"] =  dtTempInfo.Rows[0]["UOMID"].ToString();
					
								//����
								drPOMaterial["UnitPrice"] = DBNull.Value ;
								//�ܼ�
								if( drPOMaterial["POQuantity"] != DBNull.Value && drPOMaterial["UnitPrice"] != DBNull.Value)
								{
									drPOMaterial["TotalCost"] = Convert.ToDecimal(drPOMaterial["POQuantity"]) * Convert.ToDecimal(drPOMaterial["UnitPrice"] );
								}
								else
								{
									drPOMaterial["TotalCost"] = DBNull.Value ; 
								}
							}
						}
					}
				}
			}
		}
		public void RefreshPOMaterial(DataTable dtTemp)
		{
			String strMRMaterialID = String.Empty ; 
			DataTable dtCopy = dtTemp.Copy() ; 
			DataTable dt = new DataTable();
			foreach( DataRow row in dtTemp.Rows )
			{
				if( row.RowState != DataRowState.Deleted )
				{
					if(row["RowAttribute"].ToString() != "Merger") 
					{
						strMRMaterialID = row["MRMaterialID"].ToString();
						dt = daePONoBidFlow.GetPOMaterialRefresh(strMRMaterialID );
						if(dt.Rows.Count > 0)
						{
							row["CanPOQuantity"] = dt.Rows[0]["CanPOQuantity"];
						}
					}
				}
			}
//			dt.AcceptChanges();
//			for( int i = 0 ; i < dtCopy.Rows.Count ; i++ )
//			{
//				if(dtCopy.Rows[i]["RowAttribute"].ToString() == "Merger") 
//				{
//					dtCopy.Rows.RemoveAt(i);
//				}
//			}
//			foreach( DataRow row in dtCopy.Rows )
//			{
//				if( row.RowState != DataRowState.Deleted )
//				{
//					strMRMaterialID = row["MRMaterialID"].ToString();
//					dt = daePONoBidFlow.GetPOMaterial(strMRMaterialID );
//					if(dt.Rows.Count > 0)
//					{
//						row["CanPOQuantity"] = dt.Rows[0]["CanPOQuantity"];
//					}
//				}
//			}
//			
//			dtTemp = dtCopy ; 

		}
		public void UpdatePOMaterial(DataTable dtPOMaterial )
		{
			HandleDataTable(dtPOMaterial);
		}

		public void HandleDataTable(DataTable dtPOMaterial)
		{
			DataTable dtTempEnum = dtPOMaterial.Copy();
			foreach ( DataRow drPOMaterial in dtTempEnum.Rows )
			{
				if(drPOMaterial.RowState != DataRowState.Deleted)
				{
					AddMergerRow(dtPOMaterial,drPOMaterial);
				}
			}	
		}

		public void AddMergerRow(DataTable dtPOMaterial,DataRow drSearch)
		{
			
			decimal decPOQuantity = System.Convert.ToDecimal(drSearch["POQuantity"] == DBNull.Value ? 0 :drSearch["POQuantity"] );
			decimal decPOTotalCost =  System.Convert.ToDecimal(drSearch["TotalCost"] == DBNull.Value ? 0 : drSearch["TotalCost"] );
			decimal canPOQuantity =  System.Convert.ToDecimal(drSearch["CanPOQuantity"] == DBNull.Value ? 0 : drSearch["CanPOQuantity"] );
			string MRNO =drSearch["MRNO"].ToString();
			bool hasMerger = false ;
			foreach(DataRow drPOMaterial in dtPOMaterial.Rows)
			{
									
				if(drPOMaterial.RowState != DataRowState.Deleted && 
					drPOMaterial["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //��֤��ͬ������
					drPOMaterial["POMaterialID"].ToString() != drSearch["POMaterialID"].ToString() && //��֤����ͬһ��
					drPOMaterial["RowAttribute"].ToString() == "Ordinary" //��֤��û�кϲ�����
					)
				{
					hasMerger = true ;
					decPOQuantity += System.Convert.ToDecimal(drPOMaterial["POQuantity"] == DBNull.Value ? 0 : drPOMaterial["POQuantity"] );
					decPOTotalCost += System.Convert.ToDecimal(drPOMaterial["TotalCost"]  == DBNull.Value ? 0 : drPOMaterial["TotalCost"] );
					canPOQuantity += System.Convert.ToDecimal(drPOMaterial["CanPOQuantity"] == DBNull.Value ? 0 : drPOMaterial["CanPOQuantity"] );
					MRNO += "," + drPOMaterial["MRNO"].ToString();
					drPOMaterial["POQuantity"] = drPOMaterial["CanPOQuantity"] ;
					drPOMaterial["RowAttribute"] = "Hide";
				}
			}
			if(hasMerger == true)
			{
				UpdateRowStatus(dtPOMaterial,drSearch);
				DataRow dr = dtPOMaterial.NewRow();
				dr["POMaterialID"] = System.Guid.NewGuid().ToString() ; 
				dr["MRNO"] = MRNO.Trim(",".ToCharArray());
				dr["ItemCode"] = drSearch["ItemCode"].ToString();
				dr["MaterialDescription"] = drSearch["MaterialDescription"].ToString();
				dr["PartNO"] = drSearch["PartNO"].ToString();
				dr["POMaterial__MaterialUomID"] =  GetBaseUom( dr["ItemCode"] == DBNull.Value ? "" : dr["ItemCode"].ToString() );
				dr["POQuantity"] = canPOQuantity;
				dr["CanPOQuantity"] = canPOQuantity;
				dr["UnitPrice"] = drSearch["UnitPrice"];

				dr["TotalCost"] = decPOTotalCost ;
				dr["RowAttribute"] ="Merger";
				dtPOMaterial.Rows.Add(dr);
			}
			
		}
	
		public String GetBaseUom(String itemCode)
		{
			if(itemCode.Length > 0)
			{
				DataTable dt = daePONoBidFlow.GetBaseUom(itemCode);
				return dt.Rows[0][0] == DBNull.Value ? "" : dt.Rows[0][0].ToString();
			}
			return "";
		
		}
		public void UpdateRowStatus(DataTable dtPOMaterial,DataRow drSearch)
		{
			foreach(DataRow drPOMaterial in dtPOMaterial.Rows)
			{
				if(drPOMaterial.RowState != DataRowState.Deleted && 
					drPOMaterial["POMaterialID"].ToString() == drSearch["POMaterialID"].ToString() && //��֤����ͬһ��
					drPOMaterial["RowAttribute"].ToString() == "Ordinary" //��֤��û�кϲ�����
					)
				{
					drPOMaterial["POQuantity"] = drPOMaterial["CanPOQuantity"] ;
					drPOMaterial["RowAttribute"] = "Hide";
					break;
				}
			}
			
		}

		public DataTable GetPOMaterial(String strPOID)
		{
			DataTable dt = daePONoBidFlow.GetPOMaterial(strPOID , true) ; 
			return dt ; 
			
		}

	}
}
