using System;
using DataEntity;
using System.Data;


namespace Business
{
	/// <summary>
	/// BUSMRStrategy ��ժҪ˵����
	/// </summary>
	public class BUSMRStrategy : BUSBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		DAEMRStrategy _daeMRStrategy = new DAEMRStrategy() ; 

		// Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ���
		public bool CheckState(String strTenderID , TenderState state)
		{
			DataTable dt = _daeMRStrategy.CheckState(strTenderID);
			int strState = -1 ; 
			int intState = (int)state ; 
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["CheckState"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["CheckState"]);
			}
			return  strState >= intState ;

		}

		//  Add by ZZH on 2008-1-21 �����֤�Ƿ����ɾ���ķ������ڵ㱻��һ�ڵ�����ʱ����Ӧ��ɾ��
		public bool CheckDeleteRecord(String strPKValue , TenderState state )
		{
			int strState = -1 ; 
			int intState = (int)state ; 
			DataTable dt = _daeMRStrategy.GetRecord(strPKValue) ;
			if( dt != null && dt.Rows.Count > 0 )
			{
				strState = dt.Rows[0]["State"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["State"]);
			}
			return  strState <= intState ;
		}
		//*********************************************************

		public string GetTypeID(string sTenderID)
		{
			string sSql = " SELECT MRTypeID FROM TCStrategy WHERE TenderID = '"+sTenderID+"'";
			DataTable dt =  _da.GetDataTable(sSql);
			if ( dt.Rows.Count > 0 )
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtMRStrategy"></param>
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
				DataTable dt = _da.GetDataTable(strSql);
				return dt.Rows[0][0] == DBNull.Value ? "" : dt.Rows[0][0].ToString();
			}
			return "";
		
		}

		/// <summary>
		/// ������ݱ���Ҫ��ɾ���Ĳ�������Ӧ��SRIDkey
		/// </summary>
		/// <param name="sTenderID"></param>
		/// <returns></returns>
		public string[] GetSelectMrIDkey(string sTenderID)
		{
			string strSql = @"
				SELECT MR_MRStrategy.MRStrategyID
				FROM TCStrategy INNER JOIN MR_MRStrategy
					ON TCStrategy.TenderID = MR_MRStrategy.TenderID
				WHERE TCStrategy.TenderID ='"+sTenderID+"'";
			DataTable dt = _da.GetDataTable(strSql);

			int n = dt.Rows.Count ;
			string[] strMrIDkey = new string[n] ;

			for ( int i = 0 ; i < n ; i ++ )
			{
				strMrIDkey[i] = dt.Rows[i][0].ToString() ;
			}

			return strMrIDkey ;
		}


		#region  MaterialRequisition �Ƿ��� TCstrategy �й�
		public bool HasRelation ( string sMRID )
		{
			string SelectSql = " SELECT 1 FROM MR_MRStrategy WHERE MRStrategyID = '"+ sMRID +"'";

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
			//			if ( sErrorMsg.Trim().Length == 0 )
			//			{
			//				// У��ҵ������           
			//				sErrorMsg = CheckMaterial ( dt );
			//				if ( sErrorMsg.Trim().Length > 0 )
			//				{ 
			//					return sErrorMsg;
			//				}
			//			}
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
					decimal iMRQuantity = Decimal.Parse(drMaterial["MR_Material.MRQuantity"].ToString()); 
					if (iMRQuantity < 0)
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
			if(dtChild.Rows.Count < 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}

		#endregion
	}
}
