using System;
using System.Data;
using Cnwit.Utility;
using Common;
namespace DataEntity
{
	/// <summary>
	/// DAEBUSBITables ��ժҪ˵����
	/// </summary>
	public class DAEBasicInfoBin:DAEBase
	{
		DataAcess _da=GetProjectDataAcess.GetDataAcess();
		public DAEBasicInfoBin()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		///  wether one node has children
		/// </summary>
		/// <param name="idKey">Bi_department's pk</param>
		/// <returns></returns>
		public bool HashChildren(string idKey)
		{
			string sql="";
			sql="select count(*) from BI_Bin where ParentBinID='"+idKey+"'";
			DataTable dt=_da.GetDataTable(sql);
			if ((int)dt.Rows[0][0]>0)
			{
				dt.Dispose();
				return true;
			}	
			else
			{
				dt.Dispose();
				return false;
			}
			
		}
		public bool isWareHouse(string idKey)
		{
			string sql="";
			sql="select count(*) from bi_bin where parentbinid=warehouseid  and   idkey='"+idKey+"'";
			DataTable dt=_da.GetDataTable(sql);
			if ((int)dt.Rows[0][0]>0)
			{
				dt.Dispose();
				return true;
			}
					
			else
			{
				dt.Dispose();
				return false;
			}
			
		
	   }
		public bool isWare(string idKey)
		{
			string sql="";
			sql="select count(*) from bi_bin where    idkey='"+idKey+"'";
			DataTable dt=_da.GetDataTable(sql);
			if ((int)dt.Rows[0][0]>0)
			{
					dt.Dispose();
						 
				 return true;
			}
			else
			{
				dt.Dispose();
				return false;
			}
			
		
		}
	}
}
