using System;
using System.Data;
using DataEntity;
namespace Business.MaterialPurchase
{
	/// <summary>
	/// ҳ��: ���۵�
	/// ����: ҵ��ʵ����
	/// ����ʱ��: 2007-10-15
	/// ������Ա: ����
	/// </summary>
	public class BUSQuotation : BUSBase
	{
        DAEServiceRequistion daeMR = new DAEServiceRequistion();
		/// <summary>
		/// ���캯��
		/// </summary>
		public BUSQuotation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

        /// <param name="sCurrentUserID">��½���</param>
        /// <returns>popedomDepID</returns>

        public string GetUserDepartmentID(string sCurrentUserID)
        {
            string popedomDepID = string.Empty;
            //����Ȩ�޵Ŀ���
            if (daeMR.GetAllDepartmentID(sCurrentUserID) != null)
            {
                //ȡ�õ�½�ߵ����ڲ��ŵ�ID�����ܰ������֣�һ�����ǲ��ŵ��쵼��һ�������ǲ��ŵ��쵼��
                DataSet ds = daeMR.GetAllDepartmentID(sCurrentUserID);

                int nCount = ds.Tables.Count;
                //����Щ�����н���ѭ��
                for (int i = 0; i < nCount; i++)
                {
                    int k = ds.Tables[i].Rows.Count;
                    for (int j = 0; j < k; j++)
                    {
                        //ѭ����ʱ����ds.Tables[i].Rows[j][1].ToString()=="1" �����Ǵ˲��ŵ�����
                        if (ds.Tables[i].Rows[j][1] != System.DBNull.Value && ds.Tables[i].Rows[j][1].ToString() == "1")
                        {
                            //ѭ������DepID
                            popedomDepID += "," + ds.Tables[i].Rows[j][0].ToString();
                        }
                    }
                }
            }
            return popedomDepID;
        }


		#region  �����ܼ�

		/// <summary>
		/// �����ܼ�
		/// </summary>
		/// <param name="dtEdit">�������ݱ�</param>
		/// <param name="dtChild">�ӱ����ݱ�</param>
		public void CalTotalAmount(DataTable dtEdit,DataTable dtChild)
		{
			decimal mTotal = 0;				// �ܼ�(δ˰)
			decimal mTotalWithTax = 0;		// �ܼ�(��˰)
			
			
			// �Ա�λ�һ���
			decimal mTotalPriceNatural = Convert.ToDecimal ( dtEdit.Rows[0]["MR_QuotationPrice.ERNatural"].ToString() ) ;
			
			// �Ժ���һ���
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

			dtEdit.Rows[0]["MR_QuotationPrice.TotalPriceTaxNatural"] = mTotalWithTax * mTotalPriceNatural;		// ��˰��λ��
			dtEdit.Rows[0]["MR_QuotationPrice.TotalPriceTaxStandarded"] = mTotalWithTax * mTotalPriceStandard;	// ��˰�����
	
		}
	
		#endregion

		#region �����ʼ�������

		/// <summary>
		/// �����ʼ�������
		/// </summary>
		/// <param name="dt">���ݱ�</param>
		public void CalculateByER ( DataTable dt,DataTable dtMaterial )
		{
			// �Ա�λ�һ���
			decimal mTotalPriceNatural = Convert.ToDecimal ( dt.Rows[0]["MR_QuotationPrice.ERNatural"].ToString() ) ;
			
			// �Ժ���һ���
			decimal mTotalPriceStandard = Convert.ToDecimal ( dt.Rows[0]["MR_QuotationPrice.ERStandarded"].ToString() ) ;
			decimal mTotalWithTax = 0;		// �ܼ�(��˰)

			if ( dt.Rows[0]["MR_QuotationPrice.TotalPriceTax"] != DBNull.Value  )
			{
                mTotalWithTax = Convert.ToDecimal(dt.Rows[0]["MR_QuotationPrice.TotalPriceTax"]);
			}

			dt.Rows[0]["MR_QuotationPrice.TotalPriceTaxNatural"] = mTotalWithTax * mTotalPriceNatural;		// ��˰��λ��
			dt.Rows[0]["MR_QuotationPrice.TotalPriceTaxStandarded"] = mTotalWithTax * mTotalPriceStandard;	// ��˰�����
//
//			dt.Rows[0]["MR_QuotationPrice.TotalPriceNoTaxNatural"] = mTotal * mTotalPriceNatural ;			// δ˰��λ��
//			dt.Rows[0]["MR_QuotationPrice.TotalPriceNoTaxStandarded"] = mTotal * mTotalPriceStandard ;		// δ˰�����
		}

		#endregion

		#region Check QuotationNo

		/// <summary>
		/// ��鱨�۵�����Ƿ�Ψһ(��Ϊ�Ѿ�Ϊ�༭״̬,����ҵ�����������ж���)
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

		#region У������

		/// <summary>
		/// ����У��
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
						// ����ʱ�䲻�����ڻ��ߵ���ѯ��ʱ��
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
//							// δ˰���۲��ܴ��ں�˰����
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

		#region �����۽�����ʱ��ͬ������MR�����ʵ�״̬

		/// <summary>
		/// �����۽�����ʱ��ͬ������MR�����ʵ�״̬
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
					drQuotationMaterial["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //��֤��ͬ������
					drQuotationMaterial["QuotationPriceMaterialID"].ToString() != drSearch["QuotationPriceMaterialID"].ToString() && //��֤����ͬһ��
					drQuotationMaterial["RowAttribute"].ToString() == "Ordinary" //��֤��û�кϲ�����
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
					drQuotationMaterial["QuotationPriceMaterialID"].ToString() == drSearch["QuotationPriceMaterialID"].ToString() && //��֤����ͬһ��
					drQuotationMaterial["RowAttribute"].ToString() == "Ordinary" //��֤��û�кϲ�����
					)
				{
					drQuotationMaterial["RowAttribute"] = "Hide";
					break;
				}
			}
			
		}
	}
}
