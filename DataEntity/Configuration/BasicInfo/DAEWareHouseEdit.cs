using System;
using System.Data;
using Cnwit.Utility;
using Common;

namespace DataEntity
{
	/// <summary>
	/// DAEWareHouseEdit ��ժҪ˵����
	/// </summary>
	public class DAEWareHouseEdit:DAEBase
	{
		public DAEWareHouseEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		/// <summary>
		/// �ڲ���ⷿ��ͬʱ����λ�����һ��������¼
		/// </summary>
		/// <param name="WarehouseID">WarehouseID��ֵ</param>
		public  void InsertWH_BI_BIN(string WarehouseID)
		{
			string strSql = "INSERT INTO WH_BI_BIN (BinID,WHID,BINParentID,BINDescription,BINSubID,Note,Status,Deep) VALUES ('" +WarehouseID+ "','" +WarehouseID+ "',null,null,null,null,null,null)";
			this.BaseDataAccess.ExecuteDMLSQL(strSql);
		}

		#region ɾ��Ĭ�Ͽ�λ Added by Liujun at 2007-7-18

		/// <summary>
		/// ָ���ⷿ��,���������������λ��ɾ��Ĭ�Ͽ�λ
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

				// ֻ����û��������λ����²�ɾ��Ĭ�Ͽ�λ
				if ( iNormalBinNum == 0 )
				{
					// ���Ĭ�Ͽ�λҲ��ʹ��,�򲻷��ش���
					sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sDeleteSql );
				}
			}

			return sErrorMsg;
		}
		
		#endregion
	}
}
