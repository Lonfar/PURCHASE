using System;
using DataEntity;
using System.Data;
using System.Collections ;

namespace Business
{
	/// <summary>
	/// BUSMaterialRequest 的摘要说明。
	/// </summary>
	public class BUSEnquiryPrice : BUSBase
	{
		DAEServiceRequistion daeMR = new DAEServiceRequistion();
		DAEEnquiryPrice daeEnquiryPrice = new DAEEnquiryPrice();
		CEntityUitlity cEntityUitlity = new CEntityUitlity();
		/// <summary>
		/// 
		/// </summary>
		public BUSEnquiryPrice()
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
		public string CheckChildRows(DataTable dtEnquiryMaterial)
		{
			string sErrMsg = "";
			DataTable dtCheckRow = dtEnquiryMaterial.Copy();
			dtCheckRow.AcceptChanges();
			if(dtCheckRow.Rows.Count == 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}

		public string CheckChildEnquiryVendorRows(DataTable dtEnquiryVendor)
		{
			string sErrMsg = "";
			if(dtEnquiryVendor.Rows.Count <= 0)
			{
				sErrMsg= "NoEnquiryVendor" ;
			}
			return sErrMsg;
		}


		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// 错误信息
			string sErrorMsg = string.Empty;
			// 校验子表是否有数据
			sErrorMsg = CheckChildRows(dt);
//			if ( sErrorMsg.Trim().Length == 0 )
//			{
//				// 校验业务主键           
//				sErrorMsg = CheckRejectMaterial ( dt );
//				if ( sErrorMsg.Trim().Length > 0 )
//				{ 
//					return sErrorMsg;
//				}
//			}
			return sErrorMsg;
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

		public void UpdateEnquiryMaterial(DataTable dtEnquiryMaterial)
		{
			//两种相同物资合并后不允许再拆分
			foreach ( DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows )
			{
				if(drEnquiryMaterial.RowState != DataRowState.Deleted)
				{
					DataTable  dtTempInfo = daeEnquiryPrice.GetEnquiryMaterial(drEnquiryMaterial["MRMaterialID"].ToString());
					if (dtTempInfo.Rows.Count > 0 )
					{
						
						drEnquiryMaterial["MRNO"] = dtTempInfo.Rows[0]["MRNO"] ; 
						drEnquiryMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ; 
						drEnquiryMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ; 
						drEnquiryMaterial["ProductStandard"] = dtTempInfo.Rows[0]["ProductStandard"] ; 
						drEnquiryMaterial["MFG"] = dtTempInfo.Rows[0]["MFG"] ; 
						drEnquiryMaterial["PartNO"] = dtTempInfo.Rows[0]["PartNO"] ; 
						//当前单位
						drEnquiryMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ; 
						//基本单位
						drEnquiryMaterial["UOMID"] = dtTempInfo.Rows[0]["UOMID"] ; 
						//基本单位数量
						drEnquiryMaterial["MRQuantity"] = cEntityUitlity.ChangeToBaseUOM(dtTempInfo.Rows[0]["ItemCode"].ToString(),dtTempInfo.Rows[0]["MaterialUomID"].ToString(),System.Convert.ToDecimal(dtTempInfo.Rows[0]["CanEnquiryQuantity"]));
					}
				}
			}
		}

		public void HandleDataTable(DataTable dtEnquiryMaterial)
		{
			DataTable dtTempEnum = dtEnquiryMaterial.Copy();
			foreach ( DataRow drEnquiryMaterial in dtTempEnum.Rows )
			{
				if(drEnquiryMaterial.RowState != DataRowState.Deleted && drEnquiryMaterial["RowAttribute"].ToString() == "Ordinary" )
				{
					if(!MergerQuantity(dtEnquiryMaterial,drEnquiryMaterial))
					{
						MergerRow(dtEnquiryMaterial,drEnquiryMaterial,dtTempEnum);
					}
					else
					{
						continue;
					}
				}
			}	
		}

		private void UpdateRowAttribute(DataTable dtEnquiryMaterial,string EnquiryMaterialID)
		{
			foreach ( DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows )
			{
				if(drEnquiryMaterial.RowState != DataRowState.Deleted && 
					drEnquiryMaterial["RowAttribute"].ToString() == "Ordinary" &&
					drEnquiryMaterial["EnquiryMaterialID"].ToString() == EnquiryMaterialID )
				{
					drEnquiryMaterial["RowAttribute"] = "Hide";
				}
			}	
		}

		private bool MergerQuantity(DataTable dtEnquiryMaterial,DataRow drSearch)
		{
			foreach(DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows)
			{
									
				if(drEnquiryMaterial.RowState != DataRowState.Deleted && 
					drEnquiryMaterial["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //保证是同种物资
					drEnquiryMaterial["EnquiryMaterialID"].ToString() != drSearch["EnquiryMaterialID"].ToString() //保证不是同一行
					)
				{
					//找到合并行
					if(drEnquiryMaterial["RowAttribute"].ToString() == "Merger")
					{
						drEnquiryMaterial["MRQuantity"] =  System.Convert.ToDecimal(drSearch["MRQuantity"].ToString()) + System.Convert.ToDecimal(drEnquiryMaterial["MRQuantity"].ToString());
						drEnquiryMaterial["MRNO"] = drSearch["MRNO"].ToString() + "," + drEnquiryMaterial["MRNO"].ToString();
						drSearch["RowAttribute"] =  "Hide";
						UpdateRowStatus(dtEnquiryMaterial,drSearch);
						return true ;
					}
				}
			}
			return false;
		}


		public void MergerRow(DataTable dtEnquiryMaterial,DataRow drSearch,DataTable dtTempEnum)
		{
			
			decimal decMRQuantity = System.Convert.ToDecimal(drSearch["MRQuantity"].ToString()) ;
			string MRNO =drSearch["MRNO"].ToString();
			bool hasMerger = false ;
			foreach(DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows)
			{
									
				if(drEnquiryMaterial.RowState != DataRowState.Deleted && 
					drEnquiryMaterial["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //保证是同种物资
					drEnquiryMaterial["EnquiryMaterialID"].ToString() != drSearch["EnquiryMaterialID"].ToString() //保证不是同一行
					)
				{
					if(drEnquiryMaterial["RowAttribute"].ToString() == "Ordinary") //保证是没有合并过的
					{
						hasMerger = true ;
						decMRQuantity += System.Convert.ToDecimal(drEnquiryMaterial["MRQuantity"].ToString());
						MRNO += "," + drEnquiryMaterial["MRNO"].ToString();
						drEnquiryMaterial["RowAttribute"] = "Hide";
						UpdateRowAttribute(dtTempEnum,drEnquiryMaterial["EnquiryMaterialID"].ToString());
					}
				}
			}
			if(hasMerger == true)
			{
				UpdateRowStatus(dtEnquiryMaterial,drSearch);
				DataRow dr = dtEnquiryMaterial.NewRow();
				dr["EnquiryMaterialID"] = System.Guid.NewGuid().ToString() ; 
				dr["MRNO"] = MRNO.Trim(",".ToCharArray());
				dr["ItemCode"] = drSearch["ItemCode"].ToString();
				dr["MaterialName"] = drSearch["MaterialName"].ToString();
				dr["ProductStandard"] = drSearch["ProductStandard"].ToString();
				dr["MFG"] = drSearch["MFG"].ToString();
				dr["PartNO"] = drSearch["PartNO"].ToString();
				dr["UOMID"] = drSearch["UOMID"].ToString();
				dr["MRQuantity"] = decMRQuantity;
				dr["RowAttribute"] ="Merger";
				dtEnquiryMaterial.Rows.Add(dr);
			}
		}

		public void UpdateRowStatus(DataTable dtEnquiryMaterial,DataRow drSearch)
		{
			foreach(DataRow drEnquiryMaterial in dtEnquiryMaterial.Rows)
			{
				if(drEnquiryMaterial.RowState != DataRowState.Deleted && 
					drEnquiryMaterial["EnquiryMaterialID"].ToString() == drSearch["EnquiryMaterialID"].ToString() && //保证不是同一行
					drEnquiryMaterial["RowAttribute"].ToString() == "Ordinary" //保证是没有合并过的
					)
				{
					drEnquiryMaterial["RowAttribute"] = "Hide";
					break;
				}
			}
			
		}

		public void UpdateEnquiryVendor(DataTable dtEnquiryVendor)
		{
			foreach ( DataRow drEnquiryVendor in dtEnquiryVendor.Rows )
			{
				if(drEnquiryVendor.RowState != DataRowState.Deleted)
				{
					DataTable  dtTempInfo = daeEnquiryPrice.GetEnquiryVendor(drEnquiryVendor["VendorID"].ToString());
					if (dtTempInfo.Rows.Count > 0 )
					{
						drEnquiryVendor["VendorNO"] = dtTempInfo.Rows[0]["VendorNo"] ; 
						drEnquiryVendor["VendorName"] = dtTempInfo.Rows[0]["VendorName"] ; 
						drEnquiryVendor["VendorAdress"] = dtTempInfo.Rows[0]["Address"] ; 
						drEnquiryVendor["VendorTelphone"] = dtTempInfo.Rows[0]["Telphone"] ; 
						drEnquiryVendor["Fax"] = dtTempInfo.Rows[0]["Fax"] ; 
						drEnquiryVendor["Email"] = dtTempInfo.Rows[0]["Email"] ; 
					   
					}
				}
			}
		}
		// Add by ZZH on 2008-1-18 添加验证是否可以删除的方法
		public bool CheckState(string strMREnquiryPrice , MRState state)
		{
			DataTable dt = daeEnquiryPrice.CheckState(strMREnquiryPrice);
			int strState = -1 ; 
			int intState = (int)state ; 
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["CheckState"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CheckState"]);
			}
			return  strState >= intState ;

		}

		// 根据询价单ID取得报价单ID
		public Array GetQuotationPriceID(string strMREnquiryPrice)
		{
			DataTable dt = daeEnquiryPrice.GetQuotationPriceID(strMREnquiryPrice) ; 
			ArrayList list = new ArrayList();
			foreach( DataRow row in dt.Rows)
			{
				list.Add(row["QuotationPriceID"] == DBNull.Value ? "" : row["QuotationPriceID"].ToString() );
			}
			return list.ToArray(typeof(String));
		}


	}
}
