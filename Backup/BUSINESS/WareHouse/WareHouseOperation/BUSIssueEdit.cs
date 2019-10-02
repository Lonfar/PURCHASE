using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSIssueEdit ��ժҪ˵����
	/// </summary>
	public class BUSIssueEdit:BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSIssueEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="sErrMsg"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dt)
		{
			string sErrMsg = "";
			foreach(DataRow row in dt.Rows)
			{
				if(row.RowState != DataRowState.Deleted)
				{
					//ʵ�ʷ�������
					decimal decFactIssuedQuantity=Convert.ToDecimal(row["WH_IssueMaterial.FactIssuedQuantity"].ToString());
					//�ɷ�����
					decimal decCanIssuedQuantity=Convert.ToDecimal(row["WH_IssueMaterial.CanIssuedQuantity"].ToString());
					//����Ԥ������
					decimal decPreserveQuantityInFact = (row["WH_IssueMaterial.PreserveQuantityInFact"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WH_IssueMaterial.PreserveQuantityInFact"].ToString()));
					//Ԥ������
					decimal decPreserveQuantity = (row["WH_IssueMaterial.PreserveQuantity"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WH_IssueMaterial.PreserveQuantity"].ToString()));


					//ʵ������ӦС�ڿɷ�����
					if(decFactIssuedQuantity > decCanIssuedQuantity)
					{
						sErrMsg ="CheckErrMsg1";
						break;

					}
					//����Ԥ������ӦС��ʵ������
					if(decPreserveQuantityInFact > decFactIssuedQuantity )
					{
						sErrMsg ="CheckErrMsg2";
						break;
					}
					
					//����Ԥ������ӦС��Ԥ������
					if(decPreserveQuantityInFact > decPreserveQuantity)
					{
						sErrMsg ="CheckErrMsg3";
						break;
					}

					//����Ԥ����������д����
					if(decPreserveQuantityInFact < decFactIssuedQuantity -(decCanIssuedQuantity - decPreserveQuantity))
					{
						sErrMsg ="CheckErrMsg4";
						break;
					}
				}
			}	
			return sErrMsg;
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtBorrowMaterial"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtBorrowMaterial)
		{
			foreach(DataRow dr in dtBorrowMaterial.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoMaterialSelected" ;
		}


	}
}
