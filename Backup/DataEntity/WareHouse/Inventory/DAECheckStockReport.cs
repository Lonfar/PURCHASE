using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using Common;
using Cnwit;

namespace DataEntity
{
	/// <summary>
	/// DAEInventoryReport ��ժҪ˵����
	/// </summary>
	public class DAECheckStockReport:DAEBase
	{
		Cnwit.Utility.DataAcess _da;

		public DAECheckStockReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			_da = Common.GetProjectDataAcess.GetDataAcess();
		}		

		#region ͨ����òο������ļ��б�
		/// <summary>
		/// ͨ����òο������ļ��б�
		/// </summary>
		/// <param name="strITBIDKey">������</param>
		/// <returns>���ݱ�</returns>
		public DataTable GetRefITBDocumentTable ( string strITBIDKey , string strFilter )
		{			
			// ѡ��������ݿ���״̬Ϊ��ʷ��¼��State=5�������б���ĸ��� 
			string SelectSql = @" SELECT 
										WH_CheckStock.CheckStockNO ,
										Attachments.IDKey, 
										Attachments.AttachName , 
										Attachments.AttachSize , 
										Attachments.UploadTime ,
										Attachments.AttachAddr 
										FROM WH_CheckStock,Attachments 
										WHERE WH_CheckStock.CheckStockID = Attachments.ObjectiveID " ;
			DataTable dt_Temp = _da.GetDataTable ( SelectSql );

			return dt_Temp;
		}
		#endregion		

	}
}
