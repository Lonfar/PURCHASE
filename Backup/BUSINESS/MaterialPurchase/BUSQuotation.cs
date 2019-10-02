using System;
using System.Data;
using DataEntity;
namespace Business.MaterialPurchase
{
	/// <summary>
	/// 页面: 报价单
	/// 类型: 业务实体类
	/// 操作时间: 2007-10-15
	/// 操作人员: 刘俊
	/// </summary>
	public class BUSQuotation : BUSBase
	{
        DAEServiceRequistion daeMR = new DAEServiceRequistion();
		/// <summary>
		/// 构造函数
		/// </summary>
		public BUSQuotation()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

        /// <param name="sCurrentUserID">登陆编号</param>
        /// <returns>popedomDepID</returns>

        public string GetUserDepartmentID(string sCurrentUserID)
        {
            string popedomDepID = string.Empty;
            //数据权限的控制
            if (daeMR.GetAllDepartmentID(sCurrentUserID) != null)
            {
                //取得登陆者的所在部门的ID（可能包含两种：一种他是部门的领导，一种他不是部门的领导）
                DataSet ds = daeMR.GetAllDepartmentID(sCurrentUserID);

                int nCount = ds.Tables.Count;
                //在这些部门中进行循环
                for (int i = 0; i < nCount; i++)
                {
                    int k = ds.Tables[i].Rows.Count;
                    for (int j = 0; j < k; j++)
                    {
                        //循环的时候发现ds.Tables[i].Rows[j][1].ToString()=="1" 表明是此部门的主管
                        if (ds.Tables[i].Rows[j][1] != System.DBNull.Value && ds.Tables[i].Rows[j][1].ToString() == "1")
                        {
                            //循环加上DepID
                            popedomDepID += "," + ds.Tables[i].Rows[j][0].ToString();
                        }
                    }
                }
            }
            return popedomDepID;
        }


		#region  计算总价

		/// <summary>
		/// 计算总价
		/// </summary>
		/// <param name="dtEdit">主表数据表</param>
		/// <param name="dtChild">子表数据表</param>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal mTotal = 0;				// 总价(未税)
			decimal mTotalWithTax = 0;		// 总价(含税)
			
			
			// 对本位币汇率
			decimal mTotalPriceNatural = Convert.ToDecimal ( dtEdit.Rows[0]["MR_QuotationPrice.ERNatural"].ToString() ) ;
			
			// 对核算币汇率
			decimal mTotalPriceStandard = Convert.ToDecimal ( dtEdit.Rows[0]["MR_QuotationPrice.ERStandarded"].ToString() ) ;
			
			
			foreach ( DataRow dr in dtChild.Rows )
			{
				if(dr.RowState != DataRowState.Deleted)
				{
//					if ( dr["MR_QuotationPriceMaterial.UnitPriceNoTax"] != DBNull.Value && dr["MR_QuotationPriceMaterial.MRQuantity"] != DBNull.Value )
//					{
//						mTotal += Convert.ToDecimal ( dr["MR_QuotationPriceMaterial.UnitPriceNoTax"] ) * Convert.ToDecimal ( dr["MR_QuotationPriceMaterial.MRQuantity"].ToString() );
//					}
					if ( dr["MR_QuotationPriceMaterial.UnitPriceTax"] != DBNull.Value && dr["MR_QuotationPriceMaterial.MRQuantity"] != DBNull.Value )
					{
						mTotalWithTax += Convert.ToDecimal ( dr["MR_QuotationPriceMaterial.UnitPriceTax"] ) * Convert.ToDecimal ( dr["MR_QuotationPriceMaterial.MRQuantity"].ToString() );
					}
				}
			}


			dtEdit.Rows[0]["MR_QuotationPrice.TotalPriceTax"] = mTotalWithTax ;
//			dtEdit.Rows[0]["MR_QuotationPrice.TotalPriceNoTax"] = mTotal ;

			dtEdit.Rows[0]["MR_QuotationPrice.TotalPriceTaxNatural"] = mTotalWithTax * mTotalPriceNatural;		// 含税本位币
			dtEdit.Rows[0]["MR_QuotationPrice.TotalPriceTaxStandarded"] = mTotalWithTax * mTotalPriceStandard;	// 含税核算币
	
		}
	
		#endregion

		#region 按汇率计算其他

		/// <summary>
		/// 按汇率计算其他
		/// </summary>
		/// <param name="dt">数据表</param>
		public void CalculateByER ( DataTable dt,DataTable dtMaterial )
		{
			// 对本位币汇率
			decimal mTotalPriceNatural = Convert.ToDecimal ( dt.Rows[0]["MR_QuotationPrice.ERNatural"].ToString() ) ;
			
			// 对核算币汇率
			decimal mTotalPriceStandard = Convert.ToDecimal ( dt.Rows[0]["MR_QuotationPrice.ERStandarded"].ToString() ) ;
			decimal mTotalWithTax = 0;		// 总价(含税)

			if ( dt.Rows[0]["MR_QuotationPrice.TotalPriceTax"] != DBNull.Value  )
			{
                mTotalWithTax = Convert.ToDecimal(dt.Rows[0]["MR_QuotationPrice.TotalPriceTax"]);
			}

			dt.Rows[0]["MR_QuotationPrice.TotalPriceTaxNatural"] = mTotalWithTax * mTotalPriceNatural;		// 含税本位币
			dt.Rows[0]["MR_QuotationPrice.TotalPriceTaxStandarded"] = mTotalWithTax * mTotalPriceStandard;	// 含税核算币
//
//			dt.Rows[0]["MR_QuotationPrice.TotalPriceNoTaxNatural"] = mTotal * mTotalPriceNatural ;			// 未税本位币
//			dt.Rows[0]["MR_QuotationPrice.TotalPriceNoTaxStandarded"] = mTotal * mTotalPriceStandard ;		// 未税核算币
		}

		#endregion

		#region Check QuotationNo

		/// <summary>
		/// 检查报价单编号是否唯一(因为已经为编辑状态,不对业务主键进行判断了)
		/// </summary>
		private string CheckQuotationNo ( DataTable dt )
		{
			string sErrorMsg = string.Empty;

			if (  dt.Rows.Count > 0 )
			{
				string sID = Convert.ToString( dt.Rows[0]["MR_QuotationPrice.QuotationPriceID"] );
				string sNo = Convert.ToString( dt.Rows[0]["MR_QuotationPrice.QuotationPriceNo"] );

				string sRetValue = ((DataEntity.MaterialPurchase.DAEQuotation)IEntity).GetQuotationIDByNo( sNo );
				if ( sRetValue.Length > 0 && sID != sRetValue )
				{
					sErrorMsg = "QuotationNoRepeat";
				}
			}

			return sErrorMsg;
		}

		#endregion

		#region 校验数据

		/// <summary>
		/// 数据校验
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_rule(DataTable dt, System.Collections.ArrayList fieldsList)
		{
			string sErrorMsg = string.Empty;

			if ( this.IEntity.TableName == "MR_QuotationPrice")
			{
				if ( dt.Rows.Count > 0 )
				{
					if ( dt.Rows[0]["MR_QuotationPrice.QuotationDate"] != DBNull.Value && dt.Rows[0]["MR_QuotationPrice.EnquiryDate"] != DBNull.Value )
					{
						// 报价时间不能早于或者等于询价时间
						if ( Convert.ToDateTime( dt.Rows[0]["MR_QuotationPrice.QuotationDate"] ) <   Convert.ToDateTime( dt.Rows[0]["MR_QuotationPrice.EnquiryDate"] )  )
						{
							sErrorMsg = "QuotationDate";
						}						
					}

					if ( sErrorMsg.Length == 0 )
					{
						sErrorMsg = CheckQuotationNo ( dt );
					}
				}
			}

//			if ( this.IEntity.TableName == "MR_QuotationPriceMaterial" && sErrorMsg.Length == 0 )
//			{
//				foreach ( DataRow dr in dt.Rows )
//				{
//					
//						if ( dr["MR_QuotationPriceMaterial.UnitPriceTax"] != DBNull.Value && dr["MR_QuotationPriceMaterial.UnitPriceNoTax"] != DBNull.Value )
//						{
//							// 未税单价不能大于含税单价
//							if ( Convert.ToDecimal( dr["MR_QuotationPriceMaterial.UnitPriceTax"] ) < Convert.ToDecimal( dr["MR_QuotationPriceMaterial.UnitPriceNoTax"] ) )
//							{
//								sErrorMsg = "UnitPriceTax";
//							}
//						}
//					
//				}
//			}

			return sErrorMsg;
		} 


		#endregion

		#region 当报价结束的时候同步更新MR中物资的状态

		/// <summary>
		/// 当报价结束的时候同步更新MR中物资的状态
		/// </summary>
		/// <param name="dt"></param>
		public string UpdateMRQuotationPriceState ( DataTable dt )
		{
			CEntityUitlity pEntityUitlity =new CEntityUitlity();
			String strState = dt.Rows[0]["MR_QuotationPrice.Status"] == null ? "" : dt.Rows[0]["MR_QuotationPrice.Status"].ToString();
			String quotationPriceID = dt.Rows[0]["MR_QuotationPrice.QuotationPriceID"] == null ? "" : dt.Rows[0]["MR_QuotationPrice.QuotationPriceID"].ToString();
			int intState = -1; 
			int intMRState;
			if ( strState != String.Empty )
			{
				intState = Convert.ToInt32(strState) ;
			}
			Type mRState = typeof(MRState);
			foreach(String str in Enum.GetNames(mRState))
			{
				intMRState = Int32.Parse(Enum.Format(mRState , Enum.Parse(mRState ,str) , "d")); 
				if(intState == intMRState)
				{
					return pEntityUitlity.UpdateMRQuotationPriceState((MRState)Enum.Parse(mRState ,str) , quotationPriceID);
				}
			}
			return "";
		}

		#endregion

		public void HandleDataTable(DataTable dtQuotationMaterial)
		{
			DataTable dtTempEnum = dtQuotationMaterial.Copy();
			foreach ( DataRow drQuotationMaterial in dtTempEnum.Rows )
			{
				if(drQuotationMaterial.RowState != DataRowState.Deleted)
				{
					AddMergerRow(dtQuotationMaterial,drQuotationMaterial);
				}
			}	
		}

		public void AddMergerRow(DataTable dtQuotationMaterial,DataRow drSearch)
		{
			
			decimal decMRQuantity = System.Convert.ToDecimal(drSearch["MRQuantity"].ToString()); ;
			string MRNO =drSearch["MRNO"].ToString();
			bool hasMerger = false ;
			foreach(DataRow drQuotationMaterial in dtQuotationMaterial.Rows)
			{
									
				if(drQuotationMaterial.RowState != DataRowState.Deleted && 
					drQuotationMaterial["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //保证是同种物资
					drQuotationMaterial["QuotationPriceMaterialID"].ToString() != drSearch["QuotationPriceMaterialID"].ToString() && //保证不是同一行
					drQuotationMaterial["RowAttribute"].ToString() == "Ordinary" //保证是没有合并过的
					)
				{
					hasMerger = true ;
					decMRQuantity += System.Convert.ToDecimal(drQuotationMaterial["MRQuantity"].ToString());
					MRNO += "," + drQuotationMaterial["MRNO"].ToString();
					drQuotationMaterial["RowAttribute"] = "Hide";
				}
			}
			if(hasMerger == true)
			{
				UpdateRowStatus(dtQuotationMaterial,drSearch);
				DataRow dr = dtQuotationMaterial.NewRow();
				dr["QuotationPriceMaterialID"] = System.Guid.NewGuid().ToString() ; 
				dr["MRNO"] = MRNO.Trim(",".ToCharArray());
				dr["ItemCode"] = drSearch["ItemCode"].ToString();
				dr["MaterialName"] = drSearch["MaterialName"].ToString();
				dr["ProductStandard"] = drSearch["ProductStandard"].ToString();
				dr["MFG"] =drSearch["MFG"].ToString();
				dr["PartNO"] = drSearch["PartNO"].ToString();
				dr["UOMID"] = drSearch["UOMID"].ToString();
				dr["MRQuantity"] = decMRQuantity;
				dr["UnitPriceTax"] = drSearch["UnitPriceTax"];
//				dr["UnitPriceNoTax"] = drSearch["UnitPriceNoTax"];
				dr["RowAttribute"] ="Merger";
				dtQuotationMaterial.Rows.Add(dr);
			}
			
		}

		public void UpdateRowStatus(DataTable dtQuotationMaterial,DataRow drSearch)
		{
			foreach(DataRow drQuotationMaterial in dtQuotationMaterial.Rows)
			{
				if(drQuotationMaterial.RowState != DataRowState.Deleted && 
					drQuotationMaterial["QuotationPriceMaterialID"].ToString() == drSearch["QuotationPriceMaterialID"].ToString() && //保证不是同一行
					drQuotationMaterial["RowAttribute"].ToString() == "Ordinary" //保证是没有合并过的
					)
				{
					drQuotationMaterial["RowAttribute"] = "Hide";
					break;
				}
			}
			
		}
	}
}
