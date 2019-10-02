using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAETransferDEP2DEPEdit ��ժҪ˵����
	/// </summary>
	public class DAETransferDEP2DEPEdit:DAEBase
	{
		public DAETransferDEP2DEPEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public void UpdateTransferDEP2DEPMaterial(DataTable dtTransferDEP2DEPMaterial)
		{
			foreach ( DataRow drTransferDEP2DEPMaterial in dtTransferDEP2DEPMaterial.Rows )
			{
				if (drTransferDEP2DEPMaterial.RowState != DataRowState.Deleted)
				{
					string sSql = @"SELECT WH_IssueMaterial.*,(WH_IssueMaterial.FactIssuedQuantity -isnull(t.FactIssuedQuantity,0)) as CanTransferNum, 
									MaterialUOM.UOMID,Material.MaterialName From WH_IssueMaterial 
									LEFT JOIN  MaterialUOM 
									on WH_IssueMaterial.MaterialUomID =  MaterialUOM.MaterialUomID  
									left join Material 
									ON WH_IssueMaterial.ItemCode = Material.ItemCode
									left join 
									(select isnull(sum(FactIssuedQuantity),0) as FactIssuedQuantity ,WH_TransferDEP2DEPMaterial.IssueMaterialID 
									from WH_TransferDEP2DEPMaterial join WH_TransferDEP2DEP
									on WH_TransferDEP2DEP.TransferDEP2DEPID = WH_TransferDEP2DEPMaterial.TransferDEP2DEPID
									where WH_TransferDEP2DEP.Status = 4
									group by WH_TransferDEP2DEPMaterial.IssueMaterialID 
									) t on t.IssueMaterialID = WH_IssueMaterial.IssueMaterialID
									WHERE WH_IssueMaterial.IssueMaterialID = '"+drTransferDEP2DEPMaterial["IssueMaterialID"].ToString()+"'";

					DataTable  dtTempInfo = BaseDataAccess.GetDataTable (sSql);
					if (dtTempInfo.Rows.Count > 0 )
					{
						drTransferDEP2DEPMaterial["ItemCode"] = dtTempInfo.Rows[0]["ItemCode"] ;
						drTransferDEP2DEPMaterial["MaterialName"] = dtTempInfo.Rows[0]["MaterialName"] ;
						//��ת����
						drTransferDEP2DEPMaterial["IssueQuantity"] = dtTempInfo.Rows[0]["CanTransferNum"] ;
						//��λ	
						drTransferDEP2DEPMaterial["MaterialUomID"] = dtTempInfo.Rows[0]["MaterialUomID"] ;
						drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial__MaterialUomID"] = dtTempInfo.Rows[0]["UOMID"] ;
						//�������	
						drTransferDEP2DEPMaterial["POID"] = dtTempInfo.Rows[0]["POID"] ;
						drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial__POID"] = dtTempInfo.Rows[0]["POID"] ;
						//������λ����(��)	
						drTransferDEP2DEPMaterial["UnitPriceNatural"] = dtTempInfo.Rows[0]["UnitPriceNatural"] ;
						//������λ����(��)	
						drTransferDEP2DEPMaterial["UnitPriceStandard"] = dtTempInfo.Rows[0]["UnitPriceStandard"] ;					
					}
				}
			}
		}

		#region ���ύ���ʱ����У��( ʵ�������Ƿ���ڿ������� )

		/// <summary>
		/// ���ύ���ʱ����У��
		/// </summary>
		/// <param name="sTransferDEP2DEPID"></param>
		/// <returns></returns>
		public string CheckNum ( string sTransferDEP2DEPID )
		{
			string sErrorMsg = string.Empty;

			string sSql = @"select WH_TransferDEP2DEPMaterial.ItemCode from WH_TransferDEP2DEPMaterial
							left join WH_IssueMaterial
							ON  WH_TransferDEP2DEPMaterial.IssueMaterialID = WH_IssueMaterial.IssueMaterialID
							where 
							WH_IssueMaterial.FactIssuedQuantity - WH_TransferDEP2DEPMaterial.FactIssuedQuantity <0 AND TransferDEP2DEPID = '"+sTransferDEP2DEPID+"'";
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSql );
			
			if ( dt.Rows.Count > 0 )
			{ 
				sErrorMsg = dt.Rows[0]["ItemCode"].ToString();
			}

			return sErrorMsg;
		}

		#endregion

		#region ���¿���ת�ϵ���״̬

		public string UpdateTransferDEP2DEPState ( string sTransferDEP2DEPID , ApproveState state )
		{
			int iState = Convert.ToInt32( state );
			string sErrorMsg = string.Empty;
			string sUpdateSql = "UPDATE WH_TransferDEP2DEP SET Status = "+iState.ToString()+" WHERE TransferDEP2DEPID = '"+sTransferDEP2DEPID+"' ";
			sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sUpdateSql );
			return sErrorMsg;
		}

		#endregion

		public int GetState(string strPkValue,string UserID,string popedomNew)
		{
			string strSql = "SELECT * FROM WH_TransferDEP2DEP WHERE charindex(','+convert(varchar,WH_TransferDEP2DEP.Status)+',',',"+popedomNew+",')>0 AND "+
				" WH_TransferDEP2DEP.TransferDEP2DEPID='"+strPkValue+"' AND WH_TransferDEP2DEP.CreateBy= '"+ UserID +"'";
			DataTable dtState = BaseDataAccess.GetDataTable(strSql);
			return dtState.Rows.Count;

		}



		public void CalTotalAmount(DataTable dtTransferDEP2DEPMaterial,ref  decimal decTotalAmountStandard,ref decimal decTotalAmountNatural)
		{
			foreach(DataRow drTransferDEP2DEPMaterial in dtTransferDEP2DEPMaterial.Rows)
			{
				if(drTransferDEP2DEPMaterial.RowState != DataRowState.Deleted)
				{
					decimal decUnitPriceStandard =  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.UnitPriceStandard"].ToString());
					decimal decUnitPriceNatural =  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.UnitPriceNatural"].ToString());
					decimal decFactIssuedQuantity =  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.FactIssuedQuantity"].ToString());
					decimal decDepreciationRate=  Convert.ToDecimal(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.depreciationRate"].ToString());
					decTotalAmountStandard += decUnitPriceStandard*decFactIssuedQuantity*decDepreciationRate ;
					//----------------add wudi 2007-07-20---------------------------
					decTotalAmountNatural += decUnitPriceNatural*decFactIssuedQuantity*decDepreciationRate;
				}
			}

		}

	}
}
