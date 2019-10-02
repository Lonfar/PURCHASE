using System;
using DataEntity;
using System.Data;


namespace Business
{
	/// <summary>
	/// BUSMaterialRequest 的摘要说明。
	/// </summary>
	public class BUSPOBidFlow : BUSBase
	{
		DAEServiceRequistion daeMR = new DAEServiceRequistion();
		DAEPOBidFlow daePOBidFlow = new DAEPOBidFlow();
		CEntityUitlity cEntityUitlity = new CEntityUitlity();
		/// <summary>
		/// 
		/// </summary>
		public BUSPOBidFlow ()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 验证借料子表中借料的数量是否大于库存数量
		/// </summary>
		/// <param name="dt">Edit表</param>
		/// <returns>sErrMsg</returns>
		public string CheckChildRows(DataTable dtPOMaterial)
		{
			string sErrMsg = "";
			DataTable dtCheckRow = dtPOMaterial.Copy();
			dtCheckRow.AcceptChanges();
			if(dtCheckRow.Rows.Count == 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}

		public String CompareValue(DataTable dtPOMaterial)
		{
			string sErrMsg = "";
			foreach(DataRow drPOMaterial in dtPOMaterial.Rows)
			{
				if(drPOMaterial.RowState != DataRowState.Deleted)
				{
					decimal decCanPOQuantity = Convert.ToDecimal(drPOMaterial["POMaterial.CanPOQuantity"].ToString());
					decimal decPOQuantity = Convert.ToDecimal(drPOMaterial["POMaterial.POQuantity"].ToString());
					if(decPOQuantity > decCanPOQuantity)
					{
						sErrMsg ="POQuantityMoreThanCanPOQuantity";
						break;
					}
				}
			}
			return sErrMsg;
		}

		public void GetPOMaterialList(DataTable childTable,string bidEvaluationID)
		{
			DataTable dt = daePOBidFlow.GetPOMaterialbyEvaluationID(bidEvaluationID);
			childTable.Rows.Clear();
			int nCount =dt.Rows.Count;
			for(int i =0;i<nCount;i++)
			{
					DataRow dr = childTable.NewRow();
					dr["POMaterialID"] = System.Guid.NewGuid().ToString();
					dr["MaterialUomID"] = dt.Rows[i]["MaterialUomID"] == DBNull.Value ? "" : dt.Rows[i]["MaterialUomID"].ToString();
					dr["ItemCode"]  = dt.Rows[i]["ItemCode"] == DBNull.Value  ? "" : dt.Rows[i]["ItemCode"].ToString();
					dr["PartNo"] = dt.Rows[i]["PartNo"] == DBNull.Value  ? "" : dt.Rows[i]["PartNo"].ToString();
					dr["MaterialDescription"] = dt.Rows[i]["MaterialName"] == DBNull.Value  ? "" : dt.Rows[i]["MaterialName"].ToString();
					dr["MRNO"] = dt.Rows[i]["MRNO"] == DBNull.Value  ? "" : dt.Rows[i]["MRNO"].ToString();
					dr["MRMaterialID"] = dt.Rows[i]["MRMaterialID"] == DBNull.Value  ? "" : dt.Rows[i]["MRMaterialID"].ToString();
					dr["POMaterial__MaterialUomID"] =  dt.Rows[i]["UOMID"].ToString();
				
//					if(dt.Rows[i]["UnitPriceNoTax"] == DBNull.Value)
//					{
//						dr["UnitPrice"] = DBNull.Value ;
//					}
//					else
//					{
//						dr["UnitPrice"] = Convert.ToDecimal(dt.Rows[i]["UnitPriceNoTax"].ToString());
//					}
					dr["UnitPrice"] = DBNull.Value ;

					if(dt.Rows[i]["CanPOQuantity"] == DBNull.Value )
					{
						dr["CanPOQuantity"] = DBNull.Value ; 
				
					}
					else
					{
						dr["CanPOQuantity"] =  Convert.ToDecimal(dt.Rows[i]["CanPOQuantity"]) ;//GetQuantity(dt.Rows[i],bidEvaluationID , vendorID);
					}
					dr["POQuantity"] = dr["CanPOQuantity"] ;
				
					if(dr["UnitPrice"] != DBNull.Value && dr["POQuantity"] != DBNull.Value)
					{
						dr["TotalCost"] = Convert.ToDecimal(dr["UnitPrice"]) * Convert.ToDecimal(dr["POQuantity"]);
					}
					else
					{
						dr["TotalCost"] = DBNull.Value;
					}

					dr["RowStatus"] = "NEW" ;
					dr["RowAttribute"] = "Ordinary";
					childTable.Rows.Add(dr);
			}
		}

		public void RefreshPOMaterial(DataTable dtTemp,string bidEvaluationID , string vendorID)
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
						dt = daePOBidFlow.GetRefreshMaterial(bidEvaluationID,strMRMaterialID );
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
//					dt = daePOBidFlow.GetRefreshMaterial(bidEvaluationID,strMRMaterialID );
//					if(dt.Rows.Count > 0)
//					{
//						row["CanPOQuantity"] = dt.Rows[0]["CanPOQuantity"];
//					}
//					
//					row["RowAttribute"] = "Ordinary";
//				}
//			}
//			dtTemp = dtCopy ; 

		}

		public Decimal CalculationTotal(String strBIDEvaluationID , String strVendorID)
		{
			Decimal quantity = 0 ;
			DataTable dt = daePOBidFlow.GetPOMaterialbyEvaluationID(strBIDEvaluationID);
			if(dt != null)
			{
				for ( int i= 0 ; i < dt.Rows.Count ; i++)
				{
					quantity += Convert.ToDecimal(dt.Rows[i]["MRQuantity"] == DBNull.Value ? 0 : dt.Rows[i]["MRQuantity"]) ;
				}
			}
			return quantity ;
		}
		

		public bool CheckState(String strPOID)
		{
			return (!CheckStateSubmit(strPOID)) ;
		}

		public bool CheckStateSubmit(String strPOID )
		{
			DataTable dtMaterial = daePOBidFlow.CheckStateSubmit(strPOID);
			return dtMaterial.Rows.Count > 0 ;
		}

//		public bool CheckStateCompare(String strPOID)
//		{
//			DataTable dtMaterial = daePOBidFlow.CheckStateCompare(strPOID);
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
//
//		public bool CompareValue(String strPOQuantity , String strMRMaterialID )
//		{
//			Decimal decPOQuantity = Convert.ToDecimal(strPOQuantity);
//			Decimal decValue = 0 ;
//
//			DataTable dtMaterial = daePOBidFlow.GetMaterialByMRMaterialID(strMRMaterialID);
//			foreach(DataRow row in dtMaterial.Rows)
//			{
//				decValue += Convert.ToDecimal(row["POQuantity"] == DBNull.Value ? 0 : row["POQuantity"]);
//			}
//			return decPOQuantity > decValue;
//		
//		}
		public Decimal SingedTotal(String strBIDEvaluationID)
		{
			Decimal quantity = 0 ;
			DataTable dt = daePOBidFlow.GetPOMaterial(strBIDEvaluationID);
			if(dt != null)
			{
				for ( int i= 0 ; i < dt.Rows.Count ; i++)
				{
					quantity += Convert.ToDecimal(dt.Rows[i]["POQuantity"] == DBNull.Value ? 0 : dt.Rows[i]["POQuantity"]) ;
				}
			}
			return quantity ;
		
		}
		
//		public Decimal GetQuantity(DataRow rowMaterial, String bidEvaluationID , String vendorID)
//		{
//			String materialMRId = rowMaterial["MRMaterialID"] == DBNull.Value ? "" : rowMaterial["MRMaterialID"].ToString();
//			String itemCode = rowMaterial["ItemCode"] == DBNull.Value  ? "" : rowMaterial["ItemCode"].ToString();
//			Decimal quantity = Convert.ToDecimal(rowMaterial["MRQuantity"] == DBNull.Value ? 0 : rowMaterial["MRQuantity"]);
//
//			DataTable dtPOMaterial = daePOBidFlow.GetPOMaterial(materialMRId , itemCode , bidEvaluationID );
//			for( int i = 0 ; i < dtPOMaterial.Rows.Count ; i++ )
//			{
//				quantity = quantity - Convert.ToDecimal(dtPOMaterial.Rows[i]["POQuantity"] == DBNull.Value ? 0 : dtPOMaterial.Rows[i]["POQuantity"]);
//			}
//			return quantity;
//		}

		public String GetBIDEvaluationID(string strPOID)
		{
			String str = String.Empty;
			DataTable dt = daePOBidFlow.GetPORecordByPOID(strPOID) ; 
			if(dt != null)
			{
				if(dt.Rows.Count > 0)
				{
					str = dt.Rows[0]["BIDEvaluationID"] == DBNull.Value ? "" : dt.Rows[0]["BIDEvaluationID"].ToString();
					
				}
			}
			return str ;
		
		}

		public bool CheckBIDEvaluationIDInPO(String strBIDEvaluationID)
		{
			DataTable dt = daePOBidFlow.GetBIDEvaluationIDInPO(strBIDEvaluationID) ; 
			if ( dt != null )
			{
				if(dt.Rows.Count > 0) return true ;
			}
			return false ; 
			
		}

		public void UpdatePOMaterial(DataTable dtPOMaterial)
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
					drPOMaterial["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //保证是同种物资
					drPOMaterial["POMaterialID"].ToString() != drSearch["POMaterialID"].ToString() && //保证不是同一行
					drPOMaterial["RowAttribute"].ToString() == "Ordinary" //保证是没有合并过的
					)
				{
					hasMerger = true ;
					decPOQuantity += System.Convert.ToDecimal(drPOMaterial["POQuantity"] == DBNull.Value ? 0 : drPOMaterial["POQuantity"] );
					decPOTotalCost += System.Convert.ToDecimal(drPOMaterial["TotalCost"]  == DBNull.Value ? 0 : drPOMaterial["TotalCost"] );
					canPOQuantity += System.Convert.ToDecimal(drPOMaterial["CanPOQuantity"] == DBNull.Value ? 0 : drPOMaterial["CanPOQuantity"] );
					MRNO += "," + drPOMaterial["MRNO"].ToString();
					
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
				DataTable dt = daePOBidFlow.GetBaseUom(itemCode);
				return dt.Rows[0][0] == DBNull.Value ? "" : dt.Rows[0][0].ToString();
			}
			return "";
		
		}
		public void UpdateRowStatus(DataTable dtEnquiryMaterial,DataRow drSearch)
		{
			foreach(DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows)
			{
				if(drEnquiryMaterial.RowState != DataRowState.Deleted && 
					drEnquiryMaterial["POMaterialID"].ToString() == drSearch["POMaterialID"].ToString() && //保证不是同一行
					drEnquiryMaterial["RowAttribute"].ToString() == "Ordinary" //保证是没有合并过的
					)
				{
					drEnquiryMaterial["RowAttribute"] = "Hide";
					break;
				}
			}
			
		}

		/// <summary>
		/// 是否在SeePerson表中存在
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string IDKey,DataTable dtSeePerson )
		{
			foreach(DataRow dr in dtSeePerson.Rows)
			{
				if ( IDKey == dr["MR_Viewer.ViewerName"].ToString()) return true;
			}
			return false;
		}

		/// <param name="sCurrentUserID">登陆编号</param>
		/// <returns>popedomDepID</returns>

		public string GetUserDepartmentID(string sCurrentUserID)
		{
			string popedomDepID=string.Empty;
			//数据权限的控制
			if(daeMR.GetAllDepartmentID(sCurrentUserID) != null)
			{
				//取得登陆者的所在部门的ID（可能包含两种：一种他是部门的领导，一种他不是部门的领导）
				DataSet ds = daeMR.GetAllDepartmentID(sCurrentUserID);
				
				int nCount = ds.Tables.Count;
				//在这些部门中进行循环
				for(int i=0;i<nCount;i++ )
				{
					int k = ds.Tables[i].Rows.Count;
					for(int j = 0; j<k ;j++)
					{
						//循环的时候发现ds.Tables[i].Rows[j][1].ToString()=="1" 表明是此部门的主管
						if(ds.Tables[i].Rows[j][1] != System.DBNull.Value && ds.Tables[i].Rows[j][1].ToString() == "1")
						{
							//循环加上DepID
							popedomDepID += ","+ds.Tables[i].Rows[j][0].ToString();
						}
					}
				}
			}
			return popedomDepID;
		}

		public DataTable GetDefaultValue(String bidEvaluationValue , MRState mrState)
		{
			DataTable dt = daePOBidFlow.GetDefaultValue(bidEvaluationValue,mrState);
			if( dt != null && dt.Rows.Count == 1 )return dt ; 
			return null ;

		}

		public DataTable GetDefaultValueByVendorOnQuotationPrice(String bidEvaluationValue , String vendorID )
		{
			DataTable dt = daePOBidFlow.GetDefaultValueByVendorOnQuotationPrice( bidEvaluationValue ,vendorID );
			return dt ; 
		}
	}
}
