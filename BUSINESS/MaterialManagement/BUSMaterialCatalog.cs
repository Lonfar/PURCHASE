using System;
using System.Data;
using System.Data.SqlClient;

namespace Business
{
	/// <summary>
	/// BUSMaterialCatalog ��ժҪ˵����
	/// </summary>
	public class BUSMaterialCatalog : BUSBase
	{

		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public BUSMaterialCatalog()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt">Edit��</param>
		/// <returns>sErrMsg</returns>
		public string CheckChildRows(DataTable dtChild)
		{
			string sErrMsg = "";
			if(dtChild.Rows.Count <= 0)
			{
				sErrMsg= "NoMaterialSelected" ;
			}
			return sErrMsg;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtEdit"></param>
		/// <param name="parentID"></param>
		/// <param name="sCode"></param>
		/// <param name="sViewParentCatalog"></param>
		/// <param name="dNowLevel"></param>
		/// <param name="dLevel"></param>
		public void SetTableField(DataTable dtEdit, string parentID, string sCode,string sViewParentCatalog, decimal dNowLevel, decimal dLevel)
		{
			//�������ݸ��ӵ�ʵ����ֵ������ҵ�����	
			if(parentID.ToString().Trim().Length != 1)
			{
				dtEdit.Rows[0]["MaterialCatalog.CatalogID"] = parentID + sCode;
			}
			dtEdit.Rows[0]["MaterialCatalog.CatalogDeep"] = GetNextDeep(parentID);
			
			if(parentID.ToString().Trim().Length == 0)
			{
				if (sViewParentCatalog.Length > 0)
				{				
					dtEdit.Rows[0]["MaterialCatalog.ParentCatalog"] = sViewParentCatalog;
				}
			}
			else
			{
				dtEdit.Rows[0]["MaterialCatalog.ParentCatalog"] = parentID;
			}

			//�ڵ��ж�
			if(dNowLevel == dLevel)
			{
				dtEdit.Rows[0]["MaterialCatalog.IsLeafCatalog"] = 1;
			}
			else
			{
				dtEdit.Rows[0]["MaterialCatalog.IsLeafCatalog"] = 0;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="CatalogID"></param>
		/// <returns></returns>
		public decimal GetNextDeep(string CatalogID)
		{
			DataTable dtGetDeep;
			decimal objReNum = 0;
			if(CatalogID != null)
			{
				if(CatalogID.Length != 0)
				{
					string  strSql="Select CatalogDeep From MaterialCatalog Where CatalogID ='"+CatalogID+"'";
					dtGetDeep = _da.GetDataTable(strSql);
				 
					if(dtGetDeep != null)
					{
						if(dtGetDeep.Rows.Count != 0)
						{
							objReNum = System.Convert.ToDecimal(dtGetDeep.Rows[0]["CatalogDeep"]);							
						}
					}				
				}		
			}
			return objReNum+1 ;
		}
	}
}
