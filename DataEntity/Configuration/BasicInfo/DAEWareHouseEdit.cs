using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEWareHouseEdit 的摘要说明。
	/// </summary>
	public class DAEWareHouseEdit:DAEBase
	{
		public DAEWareHouseEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		/// <summary>
		/// 在插入库房的同时往库位表插入一条父结点记录
		/// </summary>
		/// <param name="WarehouseID">WarehouseID的值</param>
		public  void InsertWH_BI_BIN(string WarehouseID)
		{
			string strSql = "INSERT INTO WH_BI_BIN (BinID,WHID,BINParentID,BINDescription,BINSubID,Note,Status,Deep) VALUES ('" +WarehouseID+ "','" +WarehouseID+ "',null,null,null,null,null,null)";
			this.BaseDataAccess.ExecuteDMLSQL(strSql);
		}

		#region 删除默认库位 Added by Liujun at 2007-7-18

		/// <summary>
		/// 指定库房下,如果不存在正常库位则删除默认库位
		/// </summary>
		/// <param name="sWHID"></param>
		/// <returns></returns>
		public string DeleteDefaultBin ( string sWHID )
		{
			string sSelectSql = "SELECT COUNT(*) FROM WH_BI_BIN WHERE WHID = '"+sWHID+"' AND Status = 0 ";
			string sDeleteSql = "DELETE FROM WH_BI_BIN WHERE WHID = '"+sWHID+"' AND Status = 1 ";
			string sErrorMsg = string.Empty;
			int iNormalBinNum = 0;
			
			DataTable dt = this.BaseDataAccess.GetDataTable ( sSelectSql );
			if ( dt.Rows.Count > 0 )
			{
				iNormalBinNum = Convert.ToInt32( dt.Rows[0][0] );

				// 只有在没有正常库位情况下才删除默认库位
				if ( iNormalBinNum == 0 )
				{
					// 如果默认库位也背使用,则不返回错误
					sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sDeleteSql );
				}
			}

			return sErrorMsg;
		}
		
		#endregion
	}
}
