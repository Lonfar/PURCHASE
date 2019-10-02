using System;
using Cnwit.Utility;
using Common;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace DataEntity
{


	#region ��������Ŀ�����(����������¼����������)

	/// <summary>
	/// ��������Ŀ�����(����������¼����������)
	/// </summary>
	public class ApproveInfo
	{
		private string _personID ;

		/// <summary>
		/// ����˵�ID
		/// </summary>
		public string PersonID
		{
			get
			{
				return this._personID;
			}
			set 
			{
				this._personID = value;
			}
		}

		private string _putInID ;

		/// <summary>
		/// �ύ��¼ID
		/// </summary>
		public string PutInID
		{
			get
			{
				return this._putInID;
			}
			set
			{
				this._putInID = value;
			}
		}

		private int _approveLevel ;

		/// <summary>
		/// ��������
		/// </summary>
		public int ApproveLevel
		{
			get
			{
				return _approveLevel ;
			}
			set
			{
				_approveLevel = value;
			}
		}
	}

	#endregion

	/// <summary>
	/// ������ʵ���Ĺ��÷���
	/// </summary>
	public class CEntityUitlity
	{
		DataAcess _da = GetProjectDataAcess.GetDataAcess();
		
		string strSql=string.Empty;

		public CEntityUitlity()
		{
		}

		#region CheckExist, WriteBackStrategyPlan
		
		public bool CheckExist(string sTenderID,TCState _state)
		{

			string sSql  ="SELECT   1 FROM  TCStrategyPlan,TI_PlanActivity WHERE  TCStrategyPlan.TenderID = '"+sTenderID+"' AND TCStrategyPlan.IDKey = TI_PlanActivity.IDKey AND TI_PlanActivity.TenderCourseID = "+(int)_state+"";
			if(_da.GetDataTable(sSql).Rows.Count>0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void WriteBackStrategyPlan(string sTenderID,TCState _tcState,string sAccuBeginDate)
		{
			bool bHasExist = CheckExist(sTenderID,_tcState);

			if(bHasExist == true)
			{
				string sTenderCourseID = ((int)_tcState).ToString();
				string sSql = "Update TCStrategyPlan SET AccuBeginDate = '"+sAccuBeginDate+"' WHERE TenderID = '"+sTenderID+"' AND IDKey IN(SELECT IDKey FROM TI_PlanActivity WHERE TenderCourseID = '"+sTenderCourseID+"')";
				_da.ExecuteDMLSQL(sSql);
			}

		}

		#endregion

		#region ����PO��״̬

		public string UpdatePOState ( TenderState tContractState,string sPOID)
		{
			int nContractState = (int)tContractState ;
			if ( sPOID.IndexOf(",") > 0 )
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nContractState.ToString()+" WHERE POID in ("+sPOID+")";
			}
			else
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nContractState.ToString()+" WHERE POID = '"+sPOID.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
		}

		#endregion

		#region ����MRAndMRMaterial״̬
		/// <summary>
		/// ����MRAndMRMaterial״̬
		/// </summary>
		/// <param name="tMRState">ö�����͵�״ֵ̬</param>
		/// <param name="material">�������ʵ�MRNO,ItemCode��Ϣ��DataTable��</param>
		/// <returns></returns>
		public string  UpdateMRAndMRMaterialState(MRState tMRState,DataTable material)
		{
			String errMessage = String.Empty ; 
			if (material != null && material.Rows.Count > 0)
			{
				errMessage = UpdateMRState(tMRState ,material );
				if( errMessage.Length == 0 )
				{
					for ( int i = 0 ; i < material.Rows.Count ; i++ )
					{
						String mrNo = material.Rows[i]["MRNO"] == DBNull.Value ? "" : material.Rows[i]["MRNO"].ToString();
						String mrItemCode = material.Rows[i]["ItemCode"] == DBNull.Value ? "" : material.Rows[i]["ItemCode"].ToString();
						errMessage = UpdateMRMaterialState( tMRState , mrNo , mrItemCode );
					}
				}
			
			}
			return errMessage ; 
			

		}

		private String UpdateMRState(MRState tMRState , DataTable mrMaterial)
		{
			String errMessage = String.Empty;
			Hashtable materialTable = new Hashtable();
			String strKey = String.Empty;
			if(mrMaterial.Rows.Count > 0)
			{
				if(mrMaterial.Rows[0]["MRNO"] == DBNull.Value)return "MRNOIsNull";
				materialTable.Add(mrMaterial.Rows[0]["MRNO"].ToString().Trim() , new ArrayList());
				for(int i = 0 ; i < mrMaterial.Rows.Count ; i++)
				{
					if(mrMaterial.Rows[i]["MRNO"] == DBNull.Value)return "MRNOIsNull";
					strKey = mrMaterial.Rows[i]["MRNO"].ToString().Trim();
					if(HasKey(materialTable , strKey))
					{
						((ArrayList)materialTable[strKey]).Add(mrMaterial.Rows[i]["ItemCode"].ToString().Trim());
					
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add(mrMaterial.Rows[i]["ItemCode"].ToString().Trim());
						materialTable.Add(strKey,arrayList);
					}
					
				}
			
			

				foreach ( String keyStr in  materialTable.Keys )
				{
					ArrayList materialArrayList = materialTable[keyStr] as ArrayList;
					String itemCodeString = String.Empty;
					for(int l = 0 ; l< materialArrayList.Count ; l++)
					{
						itemCodeString = itemCodeString + ",'" + materialArrayList[l] + "'" ;
					}
					if(itemCodeString.Length > 0)
					{
						itemCodeString = itemCodeString.Trim().Substring(1);
					}

					int nMRState = (int)tMRState;
					if( !CheckMRState(tMRState,keyStr ,itemCodeString ) )
					{
						String strSql = "UPDATE MR_MaterialRequisition SET Status="+nMRState+" WHERE MRNO = '" + keyStr.Trim('\'') +"' ";
				
						errMessage = _da.ExecuteDMLSQL(strSql);
					
					}
				}
			}
			return errMessage ;
			
		}
		private bool HasKey(Hashtable hashtable , String strKey)
		{
			int state = 0 ;
			foreach( String  str  in hashtable.Keys )
			{
				if(strKey.Equals(str.Trim()))
				{
					state = 1 ;
				}
				
			}
			return state == 1 ;
		}

		private String UpdateMRMaterialState(MRState tMRState , String mrNo , String mrItemCode )
		{
			int nMRState = (int)tMRState;
			if( mrNo.Length > 0 && mrItemCode.Length > 0 )
			{
				String strSql = " Update MR_Material Set MR_Material.Status=" + nMRState + " From MR_Material Inner Join MR_MaterialRequisition On MR_MaterialRequisition.MRID = MR_Material.MRID Where MR_MaterialRequisition.MRNO='" + mrNo + "' And MR_Material.ItemCode = '" + mrItemCode + "'" ;
				return _da.ExecuteDMLSQL(strSql);
			}
			return "";
		}

		private bool CheckMRState(MRState tMRState , String mrNo ,String mrItemCode)
		{
			int nMRState = (int)tMRState;
			String sql = "Select MR_Material.Status From MR_MaterialRequisition Inner Join MR_Material On MR_MaterialRequisition.MRID = MR_Material.MRID Where MR_MaterialRequisition.MRNO = '" +  mrNo.Trim('\'') + "' And  MR_Material.Status < " + nMRState + " And MR_Material.ItemCode Not In(" + mrItemCode + ")";
			DataTable stateTable = _da.GetDataTable( sql );
			return  stateTable.Rows.Count > 0 ;
			
		}
		#endregion


		#region ����MRAndMRMaterial״̬�ڴ��б���еķ������õ�
		/// <summary>
		/// ����MRAndMRMaterial״̬
		/// </summary>
		/// <param name="tMRState">ö�����͵�״ֵ̬</param>
		/// <param name="material">�������ʵ�MRNO,ItemCode��Ϣ��DataTable��</param>
		/// <returns></returns>
		public string  UpdateMRAndMRMaterialState(TenderState tMRState,DataTable material)
		{
			String errMessage = String.Empty ; 
			if (material != null && material.Rows.Count > 0)
			{
				errMessage = UpdateMRState(tMRState ,material );
				if( errMessage.Length == 0 )
				{
					for ( int i = 0 ; i < material.Rows.Count ; i++ )
					{
						String mrNo = material.Rows[i]["MRNO"] == DBNull.Value ? "" : material.Rows[i]["MRNO"].ToString();
						String mrItemCode = material.Rows[i]["ItemCode"] == DBNull.Value ? "" : material.Rows[i]["ItemCode"].ToString();
						errMessage = UpdateMRMaterialState( tMRState , mrNo , mrItemCode );
					}
				}
			
			}
			return errMessage ; 
			

		}

		private String UpdateMRState(TenderState tMRState , DataTable mrMaterial)
		{
			String errMessage = String.Empty;
			Hashtable materialTable = new Hashtable();
			String strKey = String.Empty;
			if(mrMaterial.Rows.Count > 0)
			{
				if(mrMaterial.Rows[0]["MRNO"] == DBNull.Value)return "MRNOIsNull";
				materialTable.Add(mrMaterial.Rows[0]["MRNO"].ToString().Trim() , new ArrayList());
				for(int i = 0 ; i < mrMaterial.Rows.Count ; i++)
				{
					if(mrMaterial.Rows[i]["MRNO"] == DBNull.Value)return "MRNOIsNull";
					strKey = mrMaterial.Rows[i]["MRNO"].ToString().Trim();
					if(HasKey(materialTable , strKey))
					{
						((ArrayList)materialTable[strKey]).Add(mrMaterial.Rows[i]["ItemCode"].ToString().Trim());
					
					}
					else
					{
						ArrayList arrayList = new ArrayList();
						arrayList.Add(mrMaterial.Rows[i]["ItemCode"].ToString().Trim());
						materialTable.Add(strKey,arrayList);
					}
					
				}
			
			

				foreach ( String keyStr in  materialTable.Keys )
				{
					ArrayList materialArrayList = materialTable[keyStr] as ArrayList;
					String itemCodeString = String.Empty;
					for(int l = 0 ; l< materialArrayList.Count ; l++)
					{
						itemCodeString = itemCodeString + ",'" + materialArrayList[l] + "'" ;
					}
					if(itemCodeString.Length > 0)
					{
						itemCodeString = itemCodeString.Trim().Substring(1);
					}

					int nMRState = (int)tMRState;
					if( !CheckMRState(tMRState,keyStr ,itemCodeString ) )
					{
						String strSql = "UPDATE MR_MaterialRequisition SET Status="+nMRState+" WHERE MRNO = '" + keyStr.Trim('\'') +"' ";
				
						errMessage = _da.ExecuteDMLSQL(strSql);
					
					}
				}
			}
			return errMessage ;
			
		}
//		private bool HasKey(Hashtable hashtable , String strKey)
//		{
//			int state = 0 ;
//			foreach( String  str  in hashtable.Keys )
//			{
//				if(strKey.Equals(str.Trim()))
//				{
//					state = 1 ;
//				}
//				
//			}
//			return state == 1 ;
//		}

		private String UpdateMRMaterialState(TenderState tMRState , String mrNo , String mrItemCode )
		{
			int nMRState = (int)tMRState;
			if( mrNo.Length > 0 && mrItemCode.Length > 0 )
			{
				String strSql = " Update MR_Material Set MR_Material.Status=" + nMRState + " From MR_Material Inner Join MR_MaterialRequisition On MR_MaterialRequisition.MRID = MR_Material.MRID Where MR_MaterialRequisition.MRNO='" + mrNo + "' And MR_Material.ItemCode = '" + mrItemCode + "'" ;
				return _da.ExecuteDMLSQL(strSql);
			}
			return "";
		}

		private bool CheckMRState(TenderState tMRState , String mrNo ,String mrItemCode)
		{
			int nMRState = (int)tMRState;
			String sql = "Select MR_Material.Status From MR_MaterialRequisition Inner Join MR_Material On MR_MaterialRequisition.MRID = MR_Material.MRID Where MR_MaterialRequisition.MRNO = '" +  mrNo.Trim('\'') + "' And  MR_Material.Status < " + nMRState + " And MR_Material.ItemCode Not In(" + mrItemCode + ")";
			DataTable stateTable = _da.GetDataTable( sql );
			return  stateTable.Rows.Count > 0 ;
			
		}
		#endregion

		#region ����MR��״̬

		public string  UpdateMRState(MRState tMRState,string sMRID)
		{
			int nMRState = (int)tMRState;
			if(sMRID.IndexOf(",")>0)
			{
				strSql = "UPDATE MR_MaterialRequisition SET Status="+nMRState+" WHERE MRID in ("+sMRID+")";
			}
			else
			{
				strSql = "UPDATE MR_MaterialRequisition SET Status="+nMRState+" WHERE MRID = '"+sMRID.Trim('\'')+"'";
			}
			
			string sExecuteDMLSQL = _da.ExecuteDMLSQL(strSql);
			if ( sExecuteDMLSQL == "" )
			{
				if ( UpdateMRMaterialStatus(nMRState,sMRID) == "" )
				{
					return sExecuteDMLSQL;
				}
			}
			return "NoOperate";
		}

		public string  UpdateMRState(MRState tMRState,string sMRID,int n)
		{
			int iMRState = (int)tMRState;
			strSql = "UPDATE MR_MaterialRequisition SET Status="+iMRState+" WHERE SRID in ('"+sMRID+"')";
			string sExecuteDMLSQL = _da.ExecuteDMLSQL(strSql);
			if ( sExecuteDMLSQL == "" )
			{
				if ( UpdateMRMaterialStatus(iMRState,sMRID) == "" )
				{
					return sExecuteDMLSQL;
				}
			}
			return "NoOperate";

		}

		private string UpdateMRMaterialStatus(int iMRState,string sMRID)
		{
			if(sMRID.IndexOf(",")>0)
			{
				strSql = " UPDATE MR_Material SET MR_Material.Status=" + iMRState + " WHERE MRID in ("+sMRID+")";
			}
			else
			{
				strSql = " UPDATE MR_Material SET MR_Material.Status=" + iMRState + " WHERE MRID = '"+sMRID.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
		}

		#region ���� MR IsFinished ��״̬ <NoFlow>

//		public string  UpdateMRFinished(bool finished,string sMRID)
//		{
//			if(sMRID.IndexOf(",")>0)
//			{
//				strSql="UPDATE MR_MaterialRequisition SET Status="+(finished?"":"0")+" WHERE MRID in ("+sMRID+")";
//			}
//			else
//			{
//				strSql="UPDATE MR_MaterialRequisition SET Status="+(finished?"":"0")+" WHERE MRID = '"+sMRID.Trim('\'')+"'";
//			}
//			return _da.ExecuteDMLSQL(strSql);
//		}

		#endregion

		#endregion

		#region ����MR��EnquiryPrice״̬

		public string  UpdateMREnquiryPriceState(MRState tMRState,string SMRIDy)
		{
			String errMessage = String.Empty;
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE MR_EnquiryPrice SET Status="+nMRState+" WHERE EnquiryPriceID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE MR_EnquiryPrice SET Status="+nMRState+" WHERE EnquiryPriceID = '"+SMRIDy.Trim('\'')+"'";
			}
			errMessage =  _da.ExecuteDMLSQL(strSql);
			if(errMessage.Length == 0 )
			{
			
				errMessage = UpdateMREnquiryMaterialState(tMRState , SMRIDy);
			
			}
			if(errMessage.Length == 0 )
			{
				strSql = "Select MRNO , ItemCode From MR_EnquiryMaterial Where EnquiryPriceID='" + SMRIDy + "'";
				DataTable materialTable = _da.GetDataTable(strSql); 
				if(materialTable.Rows.Count > 0)
				{
					errMessage = UpdateMRAndMRMaterialState(tMRState , materialTable);
				}
			
			}
			return errMessage ; 

		}

		#endregion

		#region ����MR��EnquiryMaterial״̬

		public string  UpdateMREnquiryMaterialState(MRState tMRState,string SMRIDy)
		{
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE MR_EnquiryMaterial SET Status="+nMRState+" WHERE EnquiryPriceID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE MR_EnquiryMaterial SET Status="+nMRState+" WHERE EnquiryPriceID = '"+SMRIDy.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
			

		}

		#endregion

		#region ����MR��QuotationPrice״̬

		public string  UpdateMRQuotationPriceState(MRState tMRState,string SMRIDy)
		{
			String errMessage = String.Empty;
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE MR_QuotationPrice SET Status="+nMRState+" WHERE QuotationPriceID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE MR_QuotationPrice SET Status="+nMRState+" WHERE QuotationPriceID = '"+SMRIDy.Trim('\'')+"'";
			}
			errMessage =  _da.ExecuteDMLSQL(strSql);
			if(errMessage.Length == 0 )
			{
			
				errMessage = UpdateMRQuotationPriceMaterialState(tMRState , SMRIDy);
			
			}
			if(errMessage.Length == 0 )
			{
				strSql = "Select EnquiryPriceID From MR_QuotationPrice Where QuotationPriceID='" + SMRIDy + "'";
				DataTable materialTable = _da.GetDataTable(strSql); 
				if(materialTable.Rows.Count > 0)
				{
					String enquiryPriceId = materialTable.Rows[0][0] == DBNull.Value ? "" : materialTable.Rows[0][0].ToString();
					errMessage = UpdateMREnquiryPriceState(tMRState , enquiryPriceId);
				}
			
			}
			return errMessage ; 

		}

		#endregion

		#region ����QuotationPriceMaterial״̬
		public string UpdateMRQuotationPriceMaterialState(MRState tMRState,string SMRIDy)
		{
		
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE MR_QuotationPriceMaterial SET Status="+nMRState+" WHERE QuotationPriceID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE MR_QuotationPriceMaterial SET Status="+nMRState+" WHERE QuotationPriceID = '"+SMRIDy.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
		}
		#endregion

		#region ����MR��BIDEvaluation״̬

		public string  UpdateMRBIDEvaluationState(MRState tMRState,string SMRIDy)
		{
			String errMessage = String.Empty;
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE MR_BIDEvaluation SET Status="+nMRState+" WHERE BIDEvaluationID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE MR_BIDEvaluation SET Status="+nMRState+" WHERE BIDEvaluationID = '"+SMRIDy.Trim('\'')+"'";
			}
			errMessage =  _da.ExecuteDMLSQL(strSql);
			if(errMessage.Length == 0 )
			{
			
				errMessage = UpdateMRBIDSummaryState(tMRState , SMRIDy);
			
			}
			if(errMessage.Length == 0 )
			{
				String bIDEvaluationId = String.Empty ; 
				strSql = "Select MR_QuotationPrice.QuotationPriceID From MR_BIDEvaluation Inner Join MR_QuotationPrice ON MR_BIDEvaluation.EnquiryPriceID = MR_QuotationPrice.EnquiryPriceID Where MR_BIDEvaluation.BIDEvaluationID='"+SMRIDy+"'";
				DataTable ePTable = _da.GetDataTable(strSql);
				if(ePTable.Rows.Count > 0)
				{
					for( int i = 0 ; i < ePTable.Rows.Count ; i++)
					{
						bIDEvaluationId = ePTable.Rows[i][0] == DBNull.Value ? ""  : ePTable.Rows[i][0].ToString();
						errMessage = UpdateMRQuotationPriceState(tMRState , bIDEvaluationId);
					}
				}
			}
			return errMessage ; 

		}

		#endregion

		#region ����MR��BIDSummary״̬

		public string  UpdateMRBIDSummaryState(MRState tMRState,string SMRIDy)
		{
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE MR_BIDSummary SET Status="+nMRState+" WHERE BIDEvaluationID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE MR_BIDSummary SET Status="+nMRState+" WHERE BIDEvaluationID = '"+SMRIDy.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
			

		}
		#endregion

		#region ����POBidFlow״̬

		public string  UpdatePOBidFlowState(MRState tMRState,string SMRIDy)
		{
			/*
				����״̬��ִ������û�ִ���굥��״̬����ô��ִ���ʵ�״̬ʱ���ǰ����굥������Ϊ�������л�ִ��
				Ŀǰ���������ִ����������ֱ�ӻ�ִ��MR���ʼ�MR��״̬
				��������ֱ�ӵ�����UpdatePONoBidFlowState
			  */
//			String errMessage = String.Empty;
//			int nMRState = (int)tMRState;
//			if(SMRIDy.IndexOf(",")>0)
//			{
//				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
//			}
//			else
//			{
//				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
//			}
//			errMessage =  _da.ExecuteDMLSQL(strSql);
//			if(errMessage.Length == 0 )
//			{
//			
//				errMessage = UpdatePOMaterialState(tMRState , SMRIDy);
//			
//			}
//			if(errMessage.Length == 0 )
//			{
//				String bIDEvaluationId = String.Empty ; 
//				strSql = "Select PurchaseOrder.BIDEvaluationID From PurchaseOrder Where PurchaseOrder.POID='"+SMRIDy+"'";
//				DataTable ePTable = _da.GetDataTable(strSql);
//				if(ePTable.Rows.Count > 0)
//				{
//					bIDEvaluationId = ePTable.Rows[0][0] == DBNull.Value ? ""  : ePTable.Rows[0][0].ToString();
//					errMessage = this.UpdateMRBIDEvaluationState(tMRState , bIDEvaluationId);
//				}
//			}
//			return errMessage ; 
			return UpdatePONoBidFlowState(tMRState , SMRIDy);
		}

		public string  UpdatePOBidFlowState(MRState tMRState,string SMRIDy , bool bolValue)
		{
			String errMessage = String.Empty;
			if(bolValue)
			{
				int nMRState = (int)tMRState;
				if(SMRIDy.IndexOf(",")>0)
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
				}
				else
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
				}
				errMessage =  _da.ExecuteDMLSQL(strSql);
				if(errMessage.Length == 0 )
				{
			
					errMessage = UpdatePOMaterialState(tMRState , SMRIDy);
			
				}
			}
			return errMessage ; 

		}
		#endregion

		#region ����PONoBidFlow״̬

		public string  UpdatePONoBidFlowState(MRState tMRState,string SMRIDy)
		{
			String errMessage = String.Empty;
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
			}
			errMessage =  _da.ExecuteDMLSQL(strSql);
			if(errMessage.Length == 0 )
			{
			
				errMessage = UpdatePOMaterialState(tMRState , SMRIDy);
			
			}
			if(errMessage.Length == 0 )
			{
				strSql = "Select POMaterial.ItemCode , POMaterial.MRNO From POMaterial Inner Join  PurchaseOrder On POMaterial.POID = PurchaseOrder.POID  Where PurchaseOrder.POID='"+SMRIDy+"'";
				DataTable ePTable = _da.GetDataTable(strSql);
				if(ePTable.Rows.Count > 0)
				{
					errMessage = this.UpdateMRAndMRMaterialState(tMRState , ePTable);
				}
			}
			return errMessage ; 

		}

		public string  UpdatePONoBidFlowState(MRState tMRState,string SMRIDy , bool bolValue)
		{
			String errMessage = String.Empty;
			if(bolValue)
			{
				int nMRState = (int)tMRState;
				if(SMRIDy.IndexOf(",")>0)
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
				}
				else
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
				}
				errMessage =  _da.ExecuteDMLSQL(strSql);
				if(errMessage.Length == 0 )
				{
			
					errMessage = UpdatePOMaterialState(tMRState , SMRIDy);
			
				}
			}
			return errMessage ; 

		}
		#endregion

		#region ����POSign״̬(��POSign���õ�)

		public string  UpdatePOSignState(TenderState tMRState,string SMRIDy)
		{
			String errMessage = String.Empty;
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
			}
			errMessage =  _da.ExecuteDMLSQL(strSql);
			if(errMessage.Length == 0 )
			{
			
				errMessage = UpdatePOSignMaterialState(tMRState , SMRIDy);
			
			}
			if(errMessage.Length == 0 )
			{
				String bIDEvaluationId = String.Empty ; 
				strSql = "Select TenderID From PurchaseOrder Where PurchaseOrder.POID='"+SMRIDy+"'";
				DataTable dt = _da.GetDataTable(strSql);
				if ( dt != null && dt.Rows.Count > 0)
				{
					bIDEvaluationId = dt.Rows[0]["TenderID"].ToString();
				}
				errMessage = UpdateStrategyState(bIDEvaluationId,tMRState,SMRIDy);
				
			}

			return errMessage ; 
			

		}

		// Add by ZZH on 2008-2-3 ��������˶�MR����״̬�����¸�ֵ������״̬��ȡ������Ӧ�������е�״̬����Сֵ��Ϊ�˸Ķ���С������������������¸�ֵ�ķ���
		public String UpdateStrategyStateByStrategyMaterialState(String bIDEvaluationId )
		{
			String errMessage = String.Empty;
			String strSql = String.Empty ;
			strSql = " Select Min(Status) as Status From MR_MRStrategy Where TenderID = '" + bIDEvaluationId + "'" ; 
			DataTable dt = _da.GetDataTable(strSql) ;
			int state = -1 ; 
			if( dt != null && dt.Rows.Count > 0 ) 
			{
				state = dt.Rows[0]["Status"] == DBNull.Value ? -1 : Convert.ToInt32(dt.Rows[0]["Status"]);
			}
			if( state != -1 )
			{
				strSql = "UPDATE TCStrategy Set Status = "+state+" WHERE TenderID = '"+bIDEvaluationId+"'";
				errMessage = _da.ExecuteDMLSQL( strSql );
			}
				return errMessage ; 
		}
		//*****************************************************************************

		public string  UpdatePOSignState(TenderState tMRState,string SMRIDy , bool bolValue)
		{
			
			String errMessage = String.Empty;
			if( bolValue )
			{
				int nMRState = (int)tMRState;
				if(SMRIDy.IndexOf(",")>0)
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
				}
				else
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
				}
				errMessage =  _da.ExecuteDMLSQL(strSql);
				if(errMessage.Length == 0 )
				{
			
					errMessage = UpdatePOSignMaterialState(tMRState , SMRIDy);
			
				}
				
			}
			return errMessage ; 

		}

		public string  UpdatePOSignMaterialState(TenderState tMRState,string SMRIDy)
		{
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE POMaterial SET Status="+nMRState+" WHERE POID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE POMaterial SET Status="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
			

		}
		#endregion

		#region ����NoFlowPOSign״̬(��NoFlowPOSign���õ�)

		public string  UpdateNoFlowPOSignState(TenderState tMRState,string SMRIDy)
		{
			String errMessage = String.Empty;
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
			}
			errMessage =  _da.ExecuteDMLSQL(strSql);
			if(errMessage.Length == 0 )
			{
			
				errMessage = UpdatePOSignMaterialState(tMRState , SMRIDy);
			
			}

			if(errMessage.Length == 0 )
			{
				strSql = "Select POMaterial.ItemCode , POMaterial.MRNO From POMaterial Inner Join  PurchaseOrder On POMaterial.POID = PurchaseOrder.POID  Where PurchaseOrder.POID='"+SMRIDy+"'";
				DataTable ePTable = _da.GetDataTable(strSql);
				if(ePTable.Rows.Count > 0)
				{
					errMessage = this.UpdateMRAndMRMaterialState(tMRState , ePTable);
				}
			}

			return errMessage ; 

		}

		public string  UpdateNoFlowPOSignState(TenderState tMRState,string SMRIDy , bool bolValue)
		{
			String errMessage = String.Empty;
			if(bolValue)
			{
				int nMRState = (int)tMRState;
				if(SMRIDy.IndexOf(",")>0)
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID in ("+SMRIDy+")";
				}
				else
				{
					strSql="UPDATE PurchaseOrder SET ApproveStatus="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
				}
				errMessage =  _da.ExecuteDMLSQL(strSql);
				if(errMessage.Length == 0 )
				{
			
					errMessage = UpdatePOSignMaterialState(tMRState , SMRIDy);
			
				}
			}
			return errMessage ; 

		}
		#endregion

		#region ����POMaterial״̬

		public string  UpdatePOMaterialState(MRState tMRState,string SMRIDy)
		{
			int nMRState = (int)tMRState;
			if(SMRIDy.IndexOf(",")>0)
			{
				strSql="UPDATE POMaterial SET Status="+nMRState+" WHERE POID in ("+SMRIDy+")";
			}
			else
			{
				strSql="UPDATE POMaterial SET Status="+nMRState+" WHERE POID = '"+SMRIDy.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
			

		}
		#endregion



		#region ����SR��״̬
		/// <summary>
		/// ����SR��״̬
		/// </summary>
		/// <param name="tTenderState">Ŀ��״̬</param>
		/// <param name="SRID">SRID</param>
		/// <returns>������Ϣ</returns>
		public string  UpdateTenderState(TenderState tTenderState,string SRIDKey)
		{
			int nTenderState = (int)tTenderState;
			if(SRIDKey.IndexOf(",")>0)
			{
				strSql="UPDATE ServiceRequistion SET SRState="+nTenderState+" WHERE IDKey in ("+SRIDKey+")";
			}
			else
			{
				strSql="UPDATE ServiceRequistion SET SRState="+nTenderState+" WHERE IDKey = '"+SRIDKey.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
			

		}

		public string  UpdateTenderState(TenderState tTenderState,string SRID,int n)
		{
			int nTenderState = (int)tTenderState;
			strSql="UPDATE ServiceRequistion SET TenderState="+nTenderState+" WHERE SRID in ('"+SRID+"')";
			return _da.ExecuteDMLSQL(strSql);

		}

		// Added by Liujun ���²��Զ�Ӧ�����״̬
		public string  UpdateTenderStateInTCStrategySR(TenderState tTenderState,string strTCStrategySRIDKey )
		{
			int nTenderState = (int)tTenderState;
			strSql="UPDATE ServiceRequistion SET TenderState="+nTenderState+" WHERE IDKey in (SELECT SRID FROM TCStrategySR WHERE IDKey = '"+strTCStrategySRIDKey+"')";
			return _da.ExecuteDMLSQL(strSql);
		}
		#endregion

		#region ���� SR IsFinished ��״̬ <NoFlow>

		public string  UpdateSRFinished(bool finished,string SRIDKey)
		{
			if(SRIDKey.IndexOf(",")>0)
			{
				strSql="UPDATE ServiceRequistion SET IsFinished="+(finished?"1":"0")+" WHERE IDKey in ("+SRIDKey+")";
			}
			else
			{
				strSql="UPDATE ServiceRequistion SET IsFinished="+(finished?"1":"0")+" WHERE IDKey = '"+SRIDKey.Trim('\'')+"'";
			}
			return _da.ExecuteDMLSQL(strSql);
		}

		#endregion

		#region �鿴TCStrategy��״̬

		public bool CheckTCStrategy ( string sTenderID , TenderState tTenderState )
		{
			strSql = "SELECT * FROM TCStrategy WHERE status > " + (int)tTenderState 
				+ " AND TenderID = '" + sTenderID + "'" ;

			DataTable dtTenderState=_da.GetDataTable(strSql);

			if ( dtTenderState.Rows.Count > 0 )
				return true ;
			else
				return false ;
		}

		#endregion

		#region �鿴TCStrategy��״̬ (2)

		public bool CheckTCStrategy2 ( string sTenderID , TenderState tTenderState )
		{
			strSql = "SELECT * FROM TCStrategy WHERE status = " + (int)tTenderState 
				+ " AND TenderID = '" + sTenderID + "'" ;

			DataTable dtTenderState=_da.GetDataTable(strSql);

			if ( dtTenderState.Rows.Count > 0 )
				return true ;
			else
				return false ;
		}

		#endregion

		#region ���±���ITBDocument��״̬ Added by bincurd

		public string UpdateITBDocumentState ( string strTenderID , TenderState state )
		{
			int iState = Convert.ToInt32 ( state );
			
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE ITBDocument Set State = "+iState+" WHERE TenderID = '"+strTenderID+"'";

			strErrorMsg = _da.ExecuteDMLSQL( UpdateSql );

			/* ����ITBDocumentͬʱ,����TCStrategy */
			strErrorMsg += UpdateStrategyState ( strTenderID , state ) ;

			return strErrorMsg;
		}

		#endregion

		#region ���¼�����TechEvaluation��״̬ Added by bincurd

		public string UpdateTechEvaluationState ( string strTenderID , TenderState state )
		{
			int iState = Convert.ToInt32 ( state );
			
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE TechEvaluation Set State = "+iState+" WHERE TenderID = '"+strTenderID+"'";

			strErrorMsg = _da.ExecuteDMLSQL( UpdateSql );

			// ========= Modified by Liujun at 11.29 ================= //
			/* ����TechEvaluationͬʱ,����ITBDocument */
			strErrorMsg += UpdateITBDocumentState ( strTenderID , state ) ;

			return strErrorMsg;
		}

		#endregion

		#region ���������CommEvaluation��״̬ Added by bincurd

		public string UpdateCommEvaluationState ( string strTenderID , TenderState state )
		{
			int iState = Convert.ToInt32 ( state );
			
			string strErrorMsg = string.Empty;

			string UpdateSql = "UPDATE CommEvaluation Set State = "+iState+" WHERE TenderID = '"+strTenderID+"'";

			strErrorMsg = _da.ExecuteDMLSQL( UpdateSql );

			/* ����CommEvaluationͬʱ,����TechEvaluation */
			strErrorMsg += UpdateTechEvaluationState ( strTenderID , state ) ;

			return strErrorMsg;
		}

		#endregion

		#region ���غ�ع� add by wxc 2006/11/30
		/// <summary>
		/// ���غ�ع�
		/// </summary>
		/// <param name="objectID">��������ֵ</param>
		/// <returns>������Ϣ</returns>
		public string DeletePutin(string objectID)
		{
			string delMessage=string.Empty;
			//strSql="DELETE FROM PutIn WHERE ObjectiveID = '"+ objectID +"' AND state = -1 ";
			//����ͨ���Ļ��߱��˻صľ�ɾ������.
			string delSql="DELETE FROM ApproveMember WHERE ObjectiveID = '"+ objectID +"'";
			delMessage += _da.ExecuteDMLSQL(delSql);
			if(delMessage=="")
			{
				delSql="DELETE FROM Approved WHERE ObjectiveID = '"+ objectID +"'";
				delMessage += _da.ExecuteDMLSQL(delSql);
				if(delMessage.Length==0)
				{
					strSql="DELETE FROM PutIn WHERE ObjectiveID = '"+ objectID +"' ";
					delMessage+=_da.ExecuteDMLSQL(strSql);
				}
			}

			return delMessage;

		}

			#endregion

		#region ��DropDownList�ؼ�����ʾ����
		/// <summary>
		/// ��DropDownList�ؼ�����ʾ����,DropDownList�ؼ�Value,Textֵ���ֱ���ڵ���strValue,strTextֵ
		/// </summary>
		/// <param name="strValue">��DropDownList�ؼ�Valueֵ���Ӧ���ݿ���ֶ���</param>
		/// <param name="strText">��DropDownList�ؼ�Textֵ���Ӧ���ݿ���ֶ���</param>
		/// <param name="sql">sql</param>
		/// <param name="ddl">DropDownList�ؼ�id</param>
		public void BindDropDownList(string strValue,string strText,string strSql,System.Web.UI.WebControls.DropDownList ddl)
		{				
			ddl.DataSource=_da.GetDataTable(strSql).DefaultView;
			ddl.DataValueField =strValue;
			ddl.DataTextField=strText;
			ddl.DataBind();
		}

		public void BindDropDownList(string strValue,string strText,DataTable dt,System.Web.UI.WebControls.DropDownList ddl)
		{				
			ddl.DataSource=dt.DefaultView;
			ddl.DataValueField =strValue;
			ddl.DataTextField=strText;
			ddl.DataBind();
		}

		/// <summary>
		///  ͨ�����ݲ�������Ѿ���DropDownList�ؼ�Valueѡ��ֵ create by wxc 2006/12/1
		/// </summary>
		/// <param name="strValueField">����</param>
		/// <param name="ddl">DropDownList�ؼ�id</param>
		public void SelectDropDownListValue(string strValueField,System.Web.UI.WebControls.DropDownList ddl)
		{
			//ͨ�����ݲ�������Ѿ���DropDownList�ؼ�Valueѡ��ֵ
			if(ddl.SelectedItem!=null)
				ddl.Items[ddl.SelectedIndex].Selected=false;
			for (int i=0;i<ddl.Items.Count;i++) 
			{
				if (strValueField == ddl.Items[i].Value)
				{
					ddl.Items[i].Selected=true;
					break;
				}
			}
		}

		#endregion

		#region ��õ�ǰʹ�û��ҵ����� Added By Liujun at 11.2
		/// <summary>
		/// get current currency
		/// </summary>
		/// <returns></returns>
		public static string GetSysCurrency()
		{
			string sql="";
			DataAcess da=GetProjectDataAcess.GetDataAcess();
			sql="select StandardCurrencySymbol from BI_SysCurrency ";
			DataTable dt=da.GetDataTable(sql);
			if (dt.Rows.Count>0)
				return dt.Rows[0][0].ToString();
			else
				return "";
		}
		#endregion

		#region ���²��Ե�״̬ Added by Liujun at 11.27

		/// <summary>
		/// ���²���״̬
		/// </summary>
		/// <param name="state">״̬</param>
		/// <param name="strTenderID">����ID</param>
		/// <returns>������Ϣ</returns>
		public string UpdateStrategyState ( string tenderID , TenderState state )
		{
			String strSql  = String.Empty ; 
			int iState = Convert.ToInt32 ( state );
			
			string errorMsg = string.Empty;
			string UpdateSql = "UPDATE TCStrategy Set Status = "+iState+" WHERE TenderID = '"+tenderID+"'";
			errorMsg = _da.ExecuteDMLSQL( UpdateSql );
			int strategyType = GetStrategyType(tenderID);
			switch ( strategyType )
			{
				case (int)StrategyType.SR :
				{
					strSql = "Update ServiceRequistion set ServiceRequistion.SRState = "+iState+" WHERE ServiceRequistion.IDKey IN (SELECT SRID From TCStrategySR WHERE  TCStrategySR.TenderID = '"+tenderID+"')";
					errorMsg +=  _da.ExecuteDMLSQL ( strSql );
					break;
				}
				case (int)StrategyType.MR :
				{
					strSql = " UPDate MR_MRStrategy Set Status = "+iState+" Where TenderID='"+tenderID+"'";
					errorMsg +=  _da.ExecuteDMLSQL ( strSql );
					strSql = " Select MR_MRStrategy.MRMaterialID ,MR_MaterialRequisition.MRNO,MR_Material.ItemCode From MR_MRStrategy Inner Join MR_Material On MR_MRStrategy.MRMaterialID = MR_Material.MRMaterialID Inner Join MR_MaterialRequisition On MR_MaterialRequisition.MRID = MR_Material.MRID Where MR_MRStrategy.TenderID='"+tenderID+"'";
					DataTable dtMaterial = _da.GetDataTable(strSql);
					UpdateMRAndMRMaterialState(state ,dtMaterial);

					break;
				}
			}
			return errorMsg;
		}


		#endregion

		// Add by ZZH on 2008-2-3 ���²��Ե�״̬���POSign���������
		#region ���²��Ե�״̬ 

		/// <summary>
		/// ���²���״̬
		/// </summary>
		/// <param name="state">״̬</param>
		/// <param name="strTenderID">����ID</param>
		/// <returns>������Ϣ</returns>
		public string UpdateStrategyState ( string tenderID , TenderState state , String poIdStr)
		{
			String strSql  = String.Empty ; 
			int iState = Convert.ToInt32 ( state );
			string errorMsg = string.Empty;
			int strategyType = GetStrategyType(tenderID);
			switch ( strategyType )
			{
				case (int)StrategyType.SR :
				{
					string UpdateSql = "UPDATE TCStrategy Set Status = "+iState+" WHERE TenderID = '"+tenderID+"'";
					errorMsg = _da.ExecuteDMLSQL( UpdateSql );
					strSql = "Update ServiceRequistion set ServiceRequistion.SRState = "+iState+" WHERE ServiceRequistion.IDKey IN (SELECT SRID From TCStrategySR WHERE  TCStrategySR.TenderID = '"+tenderID+"')";
					errorMsg +=  _da.ExecuteDMLSQL ( strSql );

					break;
				}
				case (int)StrategyType.MR :
				{
					strSql = " UPDate MR_MRStrategy Set Status = "+iState+" From MR_MRStrategy Inner Join POMaterial On MR_MRStrategy.MRMaterialID=POMaterial.MRMaterialID Inner Join PurchaseOrder On PurchaseOrder.POID = POMaterial.POID Where PurchaseOrder.POID='"+poIdStr+"'";
					errorMsg +=  _da.ExecuteDMLSQL ( strSql );
					if(errorMsg.Length == 0)
					{
						errorMsg = UpdateStrategyStateByStrategyMaterialState(tenderID);
					}
					strSql = " Select MR_MRStrategy.MRMaterialID ,MR_MaterialRequisition.MRNO,MR_Material.ItemCode From MR_MRStrategy Inner Join MR_Material On MR_MRStrategy.MRMaterialID = MR_Material.MRMaterialID  Inner Join MR_MaterialRequisition On MR_MaterialRequisition.MRID = MR_Material.MRID Inner Join POMaterial On MR_MRStrategy.MRMaterialID = POMaterial.MRMaterialID Where POMaterial.POID='"+poIdStr+"'";
					DataTable dtMaterial = _da.GetDataTable(strSql);
					UpdateMRAndMRMaterialState(state ,dtMaterial);

					break;
				}
			}
			return errorMsg;
		}

		// �ض�ΪStrategyType.MR ��ɾ�����Զ�Ӧ������Ҫ��ִ״̬
		public string UpdateStrategyStateByStrategyMaterialState(TenderState state , DataTable dt , String strIdKey)
		{
			String errorMsg = String.Empty ; 
			String MRStrategyIDStr = String.Empty ; 
			int iState = Convert.ToInt32 ( state );
			if( dt != null && dt.Rows.Count > 0 )
			{
				foreach( DataRow row in dt.Rows)
				{
					MRStrategyIDStr = MRStrategyIDStr + ",'" + row["MRMaterialID"].ToString() + "'" ;  
				}
			}
			if( MRStrategyIDStr.Length > 0 ) MRStrategyIDStr= MRStrategyIDStr.Trim().Substring(1) ;
			strSql = " UPDate MR_MRStrategy Set Status = "+iState+" From MR_MRStrategy Where  TenderID = '"+strIdKey+"' And MRMaterialID In("+ MRStrategyIDStr +") ";
			errorMsg +=  _da.ExecuteDMLSQL ( strSql );
			if(errorMsg.Length == 0)
			{
				errorMsg = UpdateStrategyStateByStrategyMaterialState(strIdKey);
			}
			return errorMsg ; 
		}
		#endregion
		//************************************************************

        // Add by ZZH on 2008-6-26 ����PO���Ƿ����״̬
        public String UpDatePOReceiveState(String poId, POReceiveState poState)
        {
            String errorMsg = String.Empty; 
            int state = (int)poState ;
            strSql = " Update PurchaseOrder Set Status=" + state + " Where POID='"+poId+"'";
            errorMsg = _da.ExecuteDMLSQL(strSql);
            return errorMsg; 
        }
        //***********************************************

		#region ������� Added by Liujun at 12.12

		/// <summary>
		/// �������
		/// </summary>
		/// <param name="CurrencyIDFrom">Դ��������</param>
		/// <param name="CurrencyIDTo">Ŀ�Ļ�������</param>
		/// <param name="mCurrentMoney">���صĻ���</param>
		/// <returns></returns>
		public decimal GetCurrencyMoney(string CurrencyIDFrom,string CurrencyIDTo,decimal mCurrentMoney)
		{
			decimal rCurrentMoney = 0;
			DataTable dt = new DataTable();
			decimal dCurrent = 0;

			//���Դ�������ͺ�Ŀ�Ļ�������������ͬ
			if(CurrencyIDFrom.Trim() ==CurrencyIDTo.Trim())
			{
				dCurrent = 1;
			}
			else
			{
				//��ͬ
				string SelectSql = " SELECT  ExchangeRate FROM BI_CurrencyExchangeRate WHERE CurrencyIDFrom = '"+CurrencyIDFrom.Trim()+"' AND CurrencyIDTo='"+CurrencyIDTo.Trim()+"'";
				dt =   _da.GetDataTable(SelectSql);
				if ( dt.Rows.Count > 0 )
				{
					dCurrent = decimal.Parse(dt.Rows[0][0].ToString());
				}
			}
			//Ŀ�Ľ�� = Դ��� * ����
			rCurrentMoney = mCurrentMoney*dCurrent;

			// ���û�ж�Ӧ�Ļ����򷵻�-1
			if ( dt.Rows.Count == 0 && CurrencyIDFrom.Trim() != CurrencyIDTo.Trim() )
			{
				rCurrentMoney = -1;
			}

			return rCurrentMoney;
				
		}

		#endregion

		#region ͨ�����Ա���������Ŀ����

		/// <summary>
		/// ͨ�����Ա������÷�������ID����Ŀ����ID��Hashtable
		/// </summary>
		/// <param name="strTenderID"></param>
		/// <returns></returns>
		public string GetProjectName ( string strTenderID )
		{
			string SelectSql = "SELECT  ProjectName  FROM TCStrategy WHERE TenderID = '"+strTenderID+"'";

			if(_da.GetDataTable( SelectSql).Rows.Count>0)
			{
				return  _da.GetDataTable( SelectSql).Rows[0][0].ToString();
			}
			else
			{
				return "";
			}

	
		}

		#endregion

		#region  �ж��Ƿ���ڸ���
		public bool HasAttachments(string ModuleID,string strIDKey)
		{
			string SelectSql = " SELECT 1 FROM Attachments WHERE ObjectiveID = '"+strIDKey +"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region  �ҳ�ɾ��ǰ�ĸ�������Ϣ

		public DataTable GetAttachmentsInfo(string sObjectiveID)
		{
			string SelectSql = " SELECT * FROM Attachments WHERE ObjectiveID = '"+sObjectiveID +"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				return dataTable;
			}
			else
			{
				return null;
			}
		}
		#endregion

		#region  �ҳ�ɾ��ǰ�ĸ�������Ϣ

		public void PhysicalDelete(DataTable tblAttachment)
		{
			string pathConfig = ConfigurationSettings.AppSettings["SavefilePath"];
			string path =Path.Combine(pathConfig,tblAttachment.Rows[0]["AttachAddr"].ToString ());
			try
			{
				//File.Delete (Path.Combine(path,tblAttachment.Rows[i]["AttachName"].ToString 
				string oldFileName=tblAttachment.Rows[0]["AttachName"].ToString (); 
				int nLastO = oldFileName.LastIndexOf(".");
				string extension= oldFileName.Substring(nLastO);
				string fileName=tblAttachment.Rows[0]["IDKey"].ToString ()+extension;
				if(File.Exists(Path.Combine(path,fileName)))
				{
					File.Delete (Path.Combine(path,fileName));
				}
			}
			catch(Exception ex)
			{
				throw ex;
				//ɾ���ļ�ʧ��
			}
		}
		#endregion

		#region  ȡ���б���

		public string GetTenderID(string sTableCode,string sTenderIDCode,string sPKName,string sPKValue)
		{
			string sql= "SELECT "+sTenderIDCode+" FROM "+sTableCode+" WHERE "+sPKName+" = '"+sPKValue+"'";
			DataTable dt = _da.GetDataTable(sql);
			if ( dt.Rows.Count > 0)
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}

		}
		#endregion

		#region ����״̬

		/// <summary>
		/// ����Ŀ����״̬���Ҹ��¶�Ӧ�Ĳ��Լ�SR
		/// </summary>
		/// <param name="strTableName">Ŀ���</param>
		/// <param name="strStateColumn">״̬�ֶ�����</param>
		/// <param name="strTenderColumnName">���Ա���ֶ�����</param>
		/// <param name="strPKColumnName">�����ֶ�����</param>
		/// <param name="strPKValue">����ֵ</param>
		/// <param name="state">״̬</param>
		public void SetState ( string strTableName , string strStateColumn , string strTenderColumnName , string strPKColumnName ,string strPKValue , TenderState state )
		{
			string UpdateSql = " UPDATE "+strTableName+" SET " +strStateColumn+" = " + (int)state + " WHERE  " + strPKColumnName + " = '"+ strPKValue + "'" ;

			// ����Ŀ���
			_da.ExecuteDMLSQL ( UpdateSql );

			string SelectSql = " SELECT "+strTenderColumnName+" FROM "+strTableName + " WHERE  " + strPKColumnName + " = '"+ strPKValue + "'" ;

			// ��ò��Ա��
			DataTable dt = _da.GetDataTable ( SelectSql );

			if ( dt.Rows.Count > 0 )
			{
				// ���²��Լ���Ӧ��SR
				UpdateStrategyState ( dt.Rows[0][strTenderColumnName].ToString() , state ); 
			}
		}

		#endregion

		#region ���״̬

		/// <summary>
		/// ���״ֵ̬
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="strColumnName">״̬�ֶ���</param>
		/// <returns>״ֵ̬</returns>
		public int GetState ( string strTableName , string strColumnName , string pkName , string pKValue )
		{
			int iState = 0;

			string SelectSql = " SELECT " + strColumnName + " FROM " +strTableName + " WHERE " + pkName + "= '"+pKValue+"'";
			
			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			if ( dt_Temp.Rows.Count > 0 )
			{
				iState = Convert.ToInt32 ( dt_Temp.Rows[0][strColumnName] );
			}

			return iState;
		}

		#endregion

		#region ������Զ�ӦSR�ƻ������ۼ�ֵ

		public decimal GetSRPlanAmountByTCStrategy ( string strTenderID )
		{
            
			decimal dAmount = 0 ;
			string CurrencyIDTo = string.Empty;	// ��׼����
			string SelectSql = "SELECT PlanAmount , PlanCurrency FROM TCStrategySR WHERE TenderID = '"+strTenderID+"' ";

            CXmlReader pXmlReader = new CXmlReader();
			DataTable dt = _da.GetDataTable ( SelectSql );
			//CurrencyIDTo = System.Configuration.ConfigurationSettings.AppSettings["Currency"];
            CurrencyIDTo = pXmlReader.GetSingleNodeValue("System.xml", "configuration/SystemConfig/Currency", "value");

			foreach ( DataRow dr in dt.Rows )
			{
				if( dr["PlanAmount"]==System.DBNull.Value )
				{
					dAmount += 0 ;

				}
				else
				{
					if ( dr["PlanCurrency"] == System.DBNull.Value ) 
					{
						dAmount += decimal.Parse( dr["PlanAmount"].ToString()); 
					}
					else
					{
						dAmount += GetCurrencyMoney( dr["PlanCurrency"].ToString() , CurrencyIDTo , decimal.Parse( dr["PlanAmount"].ToString()) ) ;
					}
				}
			}

			return dAmount;
		}

		#endregion

		#region GetPutInIDKey - PutIn - Get IDKey by ObjectiveID

		public string GetPutInIDKeyByObjectiveID ( string ObjectiveID )
		{
			strSql = @"
				select IDKey from PutIn
				where ObjectiveID='" + ObjectiveID + "'" ;

			DataTable dt = _da.GetDataTable ( strSql ) ;

			if (dt.Rows.Count>0)
				return dt.Rows[0][0].ToString();
			else
				return "";
		}

		#endregion

		#region GetTCMeetingID - TCMeetingReport - Get ID by PutInIDKey

		public string GetTCMeetingID ( string PutInIDKey )
		{
			strSql = @"
				select ID from TCMeetingReport
				where PutINIDKey='" + PutInIDKey + "'" ;

			DataTable dt = _da.GetDataTable ( strSql ) ;

			if (dt.Rows.Count>0)
				return dt.Rows[0][0].ToString();
			else
				return "";
		}

		#endregion

		#region ���ָ�����ҶԱ�λ�ҵĻ���

		/// <summary>
		/// ���ָ�����ҶԱ�λ�ҵĻ���
		/// </summary>
		/// <param name="sCurrentCurrency">��ǰ����</param>
		/// <returns>��Ӧ����</returns>
		public decimal GetERFromCurrentToNatural ( string sCurrentCurrency )
		{
			string sSelectSql = @"SELECT ExchangeRate FROM BI_CurrencyExchangeRate WHERE CurrencyIDTo IN 
										(SELECT NaturalCurrencySymbol FROM BI_SysCurrency) AND CurrencyIDFROM = '"+sCurrentCurrency+"' and status =0";

			decimal mExchangeRate = 0;

			DataTable dt = this._da.GetDataTable ( sSelectSql );

			if ( dt.Rows.Count > 0 )
			{ 
				mExchangeRate = Convert.ToDecimal( dt.Rows[0][0]); 
			}
			else
			{
				mExchangeRate = 0;
			}

			return mExchangeRate;
		}

		#endregion

		#region ���ָ�����ҶԺ���ҵĻ���

		/// <summary>
		/// ���ָ�����ҶԺ���ҵĻ���
		/// </summary>
		/// <param name="sCurrentCurrency">��ǰ����</param>
		/// <returns>��Ӧ����</returns>
		public decimal GetERFromCurrentToStandard ( string sCurrentCurrency )
		{
			string sSelectSql = @"SELECT ExchangeRate FROM BI_CurrencyExchangeRate WHERE CurrencyIDTo IN 
											(SELECT StandardCurrencySymbol FROM BI_SysCurrency) AND CurrencyIDFROM = '"+sCurrentCurrency+"' and status =0";

			decimal mExchangeRate = 0;

			DataTable dt = this._da.GetDataTable ( sSelectSql );

			if ( dt.Rows.Count > 0 )
			{ 
				mExchangeRate = Convert.ToDecimal( dt.Rows[0][0]); 
			}
			else
			{
				mExchangeRate = 0;
			}

			return mExchangeRate;
		}

		#endregion

		#region ���ʵ�λת��

		/// <summary>
		/// �����������ɵ�ǰ��λת����������λ
		/// </summary>
		/// <param name="sItemCode">���ʱ���</param>
		/// <param name="sCurrentUOM">��ǰ��λ</param>
		/// <param name="fNum">����</param>
		/// <returns>������λ��Ӧ����</returns>
		public decimal ChangeToBaseUOM ( string sItemCode , string sCurrentUOM , decimal fNum )
		{
			decimal fNumToBase = fNum;
			string sSelectSql = "SELECT MultipleOfBaseUOM FROM MaterialUOM WHERE ItemCode = '"+sItemCode+"' AND MaterialUomID = '"+sCurrentUOM+"'";

			DataTable dt = this._da.GetDataTable ( sSelectSql );

			if ( dt.Rows.Count > 0 )
			{
				fNumToBase = fNum * Convert.ToDecimal( dt.Rows[0][0] ) ;
			}

			return fNumToBase;
		}

		/// <summary>
		/// �����������ɻ�����λת����Ŀ�굥λ
		/// </summary>
		/// <param name="sItemCode">���ʱ���</param>
		/// <param name="sDestUOM">Ŀ�굥λ</param>
		/// <param name="fNum">����</param>
		/// <returns>Ŀ�굥λ��Ӧ����</returns>
		public decimal ChangeFromBaseUON ( string sItemCode , string sDestUOM , decimal fNum )
		{
			decimal fNumFromBase = fNum;
			string sSelectSql = "SELECT MultipleOfBaseUOM FROM MaterialUOM WHERE ItemCode = '"+sItemCode+"' AND MaterialUomID = '"+sDestUOM+"'";

			DataTable dt = this._da.GetDataTable ( sSelectSql );

			if ( dt.Rows.Count > 0 )
			{
				fNumFromBase = fNum / Convert.ToDecimal( dt.Rows[0][0] ) ;
			}

			return fNumFromBase;
		}

		#endregion

		#region ��ò����ж�Ӧ���������

		/// <summary>
		/// ��ò����ж�Ӧ���������
		/// </summary>
		/// <param name="tenderID"></param>
		/// <returns>0:�����ݣ�1:MR��2:SR</returns>
		public int GetStrategyType(string tenderID)
		{
			string sSql = " SELECT MRTypeID FROM TCStrategy WHERE TenderID = '"+tenderID+"'";
			DataTable dt =  _da.GetDataTable(sSql);
			if ( dt.Rows.Count > 0 )
			{
				return Convert.ToInt32( dt.Rows[0][0] );
			}
			else
			{
				return 0;
			}
		}

		#endregion

		public string GetNextNo(string sCode)
		{
			string[] sParams = {"Name"} ;
			object[] objParamValues = {sCode} ; 
			SqlDbType[] paramTypes = {SqlDbType.NVarChar} ;
			object objNewCode =  _da.ExecuteSPQueryObject("p_GetBH",sParams,objParamValues,paramTypes) ; 
			return objNewCode.ToString();

		}

		#region �������ӱ���кϲ�( ��ʱ�������� Add by Liujun )

		public void HandleDataTable(DataTable dtMRStrategy)
		{
			DataTable dtTempEnum = dtMRStrategy.Copy();
			foreach ( DataRow drMRStrategy in dtTempEnum.Rows )
			{
				if(drMRStrategy.RowState != DataRowState.Deleted)
				{
					AddMergerRow(dtMRStrategy,drMRStrategy);
				}
			}	
		}

		private void AddMergerRow(DataTable dtMRStrategy,DataRow drSearch)
		{
			decimal decMRQuantity = System.Convert.ToDecimal(drSearch["MRQuantity"] == DBNull.Value ? 0 :drSearch["MRQuantity"] ); 
			string MRNO =drSearch["MRNO"].ToString();
			bool hasMerger = false ;
			foreach(DataRow drMRStrategy in dtMRStrategy.Rows)
			{
				if(drMRStrategy.RowState != DataRowState.Deleted && 
					drMRStrategy["ItemCode"].ToString() == drSearch["ItemCode"].ToString() && //��֤��ͬ������
					drMRStrategy["MRStrategyID"].ToString() != drSearch["MRStrategyID"].ToString() && //��֤����ͬһ��
					drMRStrategy["RowAttribute"].ToString() == "Ordinary" //��֤��û�кϲ�����
					)
				{
					hasMerger = true ;
					decMRQuantity += System.Convert.ToDecimal(drMRStrategy["MRQuantity"].ToString());
					MRNO += "," + drMRStrategy["MRNO"].ToString();
					drMRStrategy["RowAttribute"] = "Hide";
				}
			}
			if(hasMerger == true)
			{
				UpdateRowStatus(dtMRStrategy,drSearch);
				DataRow dr = dtMRStrategy.NewRow();
				dr["MRStrategyID"] = System.Guid.NewGuid().ToString() ; 
				dr["MRNO"] = MRNO.Trim(",".ToCharArray());
				dr["ItemCode"] = drSearch["ItemCode"].ToString();
				dr["MaterialName"] = drSearch["MaterialName"].ToString();
				dr["ProductStandard"] = drSearch["ProductStandard"].ToString();
				dr["MFG"] =drSearch["MFG"].ToString();
				dr["PartNO"] = drSearch["PartNO"].ToString();
				//				dr["UOMID"] = drSearch["UOMID"].ToString();
				dr["MR_MRStrategy__MaterialUomID"] =  GetBaseUom( dr["ItemCode"] == DBNull.Value ? "" : dr["ItemCode"].ToString() );
				dr["MRQuantity"] = decMRQuantity;
				dr["RowAttribute"] ="Merger";
				dr["Status"] = drSearch["Status"];
				dtMRStrategy.Rows.Add(dr);
			}
			
		}

		private void UpdateRowStatus(DataTable dtMRStrategy,DataRow drSearch)
		{
			foreach(DataRow drMRStrategy in dtMRStrategy.Rows)
			{
				if(drMRStrategy.RowState != DataRowState.Deleted && 
					drMRStrategy["MRStrategyID"].ToString() == drSearch["MRStrategyID"].ToString() && //��֤����ͬһ��
					drMRStrategy["RowAttribute"].ToString() == "Ordinary" //��֤��û�кϲ�����
					)
				{
					drMRStrategy["RowAttribute"] = "Hide";
					break;
				}
			}
			
		}

		public String GetBaseUom(String itemCode)
		{
			if(itemCode.Length > 0)
			{
				String strSql = " Select MaterialUOM.UOMID From MaterialUOM Where ItemCode = '" + itemCode + "' And MaterialUOM.IsBaseUOM =1" ;

				Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

				DataTable dt = _da.GetDataTable(strSql);
				return dt.Rows[0][0] == DBNull.Value ? "" : dt.Rows[0][0].ToString();
			}
			return "";
		}

		#endregion

		#region ����ҵ�񵥾����ͣ��������ŵ�ȱʡֵ ( Added By Liujun at 2008-5-13 )

		/// <summary>
		/// ����ҵ�񵥾����ͣ��������ŵ�ȱʡֵ
		/// ͳһ��Render�����һ��ʹ�á�
		/// </summary>
		/// <param name="BillType">��������</param>
		/// <returns>���</returns>
		public string GetNextCode ( string BillType )
		{
			string strSql = string.Empty;

			string strYearPart = DateTime.Now.Year.ToString().Substring(2,2);

			string strCode = string.Empty;

			switch ( BillType )
			{
				case "MRNO" :
				{
					strSql = "SELECT RIGHT(1001+ISNULL(MAX(RIGHT("+BillType+",3)),0),3)  FROM MR_MaterialRequisition WHERE LEFT("+BillType+",2) = (SELECT * FROM v_GetYear)";

					DataTable dt = _da.GetDataTable( strSql );

					if ( dt.Rows.Count > 0 )
					{
						// 3   ����MR ��������Ϊ�̶����֣�
						strCode =  strYearPart + "3" + dt.Rows[0][0].ToString();
					}

					break;
				}
				case "SRID" :
				{
					strSql = "SELECT RIGHT(1001+ISNULL(MAX(RIGHT("+BillType+",3)),0),3)  FROM ServiceRequistion WHERE LEFT("+BillType+",2) = (SELECT * FROM v_GetYear)";

					DataTable dt = _da.GetDataTable( strSql );

					if ( dt.Rows.Count > 0 )
					{
						// 6   ����SR ��������Ϊ�̶����֣�
						strCode =  strYearPart + "3" + dt.Rows[0][0].ToString();
					}

					break;
				}
				case "EnquiryPriceNo" :
				{
					strSql = "SELECT RIGHT(1001+ISNULL(MAX(RIGHT("+BillType+",3)),0),3)  FROM MR_EnquiryPrice WHERE LEFT("+BillType+",2) = (SELECT * FROM v_GetYear)";

					DataTable dt = _da.GetDataTable( strSql );

					if ( dt.Rows.Count > 0 )
					{
						// 4   ����Enquiry ��������Ϊ�̶����֣�
						strCode =  strYearPart + "4" + dt.Rows[0][0].ToString();
					}

					break;
				}
				case "TenderID" :
				{
					strSql = "SELECT RIGHT(1001+ISNULL(MAX(RIGHT(TenderID,3)),0),3)  FROM TCStrategy WHERE LEFT(TenderID,2) = (SELECT * FROM v_GetYear)";

					DataTable dt = _da.GetDataTable( strSql );

					if ( dt.Rows.Count > 0 )
					{
						// 4   ����Tender ��������Ϊ�̶����֣�
						strCode =  strYearPart + "4" + dt.Rows[0][0].ToString();
					}

					break;
				}
				case "POID" :
				{
					strSql = "SELECT RIGHT(1001+ISNULL(MAX(RIGHT(POID,3)),0),3)  FROM PurchaseOrder WHERE LEN(POID) = 7 AND LEFT(POID,2) = (SELECT * FROM v_GetYear)";

					DataTable dt = _da.GetDataTable( strSql );

					if ( dt.Rows.Count > 0 )
					{
						// 21 ����PO ��������Ϊ�̶����֣�
						strCode =  strYearPart + "21" + dt.Rows[0][0].ToString();
					}
					
					break;
				}
			}

			return strCode;
		}


		#endregion
		
	}


		
}
