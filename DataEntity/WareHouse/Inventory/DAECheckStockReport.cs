using System;
using System.Data;
using System.Collections;
using System.Web.UI;
using Common;
using Cnwit;

namespace DataEntity
{
	/// <summary>
	/// DAEInventoryReport 的摘要说明。
	/// </summary>
	public class DAECheckStockReport:DAEBase
	{
		Cnwit.Utility.DataAcess _da;

		public DAECheckStockReport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			_da = Common.GetProjectDataAcess.GetDataAcess();
		}		

		#region 通过获得参考标书文件列表
		/// <summary>
		/// 通过获得参考标书文件列表
		/// </summary>
		/// <param name="strITBIDKey">标书编号</param>
		/// <returns>数据表</returns>
		public DataTable GetRefITBDocumentTable ( string strITBIDKey , string strFilter )
		{			
			// 选择标书数据库中状态为历史纪录（State=5）的所有标书的附件 
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
