using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// ��λ����ʵ����
	/// </summary>
	public class DAEBIBIN:DAEBase
	{
		public DAEBIBIN()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���زֿ�ڵ�ID
		/// </summary>
		/// <param name="BINID"></param>
		/// <returns></returns>
		public string  GetHouseID(string sBinid)
		{	
			DataTable dtHouseID =null;

			if(sBinid != null)
			{
				if(sBinid.Length!=0)
				{
					string strSql ="Select WHID from WH_BI_BIN where  BINID = '"+sBinid+"'";

					dtHouseID =  this.BaseDataAccess.GetDataTable(strSql);

					if(dtHouseID != null)
					{
						if(dtHouseID.Rows.Count > 0)
						{
							return dtHouseID.Rows[0]["WHID"].ToString();

						}

					}

				}

			}
			return "";
		  
		}

		/// <summary>
		/// ����Ƿ����ӽڵ�
		/// </summary>
		/// <param name="sBinid"></param>
		/// <returns></returns>
		public bool CheckChildren(string sBinid)
		{
			bool bCheck = false;

			if(sBinid != null)
			{
				if(sBinid.Length != 0)
				{
					string strSql ="Select BINID From WH_BI_BIN Where BINParentID= '"+sBinid+"'";
					DataTable dtChildren = this.BaseDataAccess.GetDataTable(strSql);
					if(dtChildren != null)
					{
						if(dtChildren.Rows.Count != 0)
						{
							bCheck = true;

						}

					}

				}

			}
			return bCheck;

		}
		#region ��ÿⷿ����Ӧ�����п�λ��Ϣ

		/// <summary>
		/// ��ÿⷿ����Ӧ�����п�λ��Ϣ
		/// </summary>
		/// <param name="sWHID">�ⷿID</param>
		/// <returns>��Ӧ���п�λID</returns>
		public string GetAllBINIDByWHID ( string sWHID )
		{
			string sSelectSql = " SELECT BINID FROM WH_BI_BIN WHERE WHID = '"+sWHID+"'";
			string sBINID = string.Empty;
			StringBuilder sbBINID = new StringBuilder();
			
			DataTable dtBINID = this.BaseDataAccess.GetDataTable ( sSelectSql );


			foreach ( DataRow dr in dtBINID.Rows )
			{
				sbBINID.Append ( " '" + dr["BINID"].ToString() + "',"); 
			}

			if ( sbBINID.Length > 0 )
			{
				if ( sbBINID[sbBINID.Length-1] == ',' )
				{
					sBINID = sbBINID.Remove( sbBINID.Length-1 , 1 ).ToString();
				}
			}

			return sBINID; // ����ֵ��: '02��λ','03��λ','05��λ'
		}

		#endregion

		#region ����п�λ�ĿⷿID

		/// <summary>
		/// ����п�λ�ĿⷿID
		/// </summary>
		/// <returns></returns>
		public string GetWHHasBIN ()
		{
			string sSelectSql = @"SELECT WH_BI_WareHouse.WHID , COUNT(WH_BI_BIN.BINID) FROM WH_BI_WareHouse 
										INNER JOIN WH_BI_BIN ON WH_BI_WareHouse.WHID = WH_BI_BIN.WHID
										GROUP BY WH_BI_WareHouse.WHID";
			string sWHIDs = string.Empty;
			StringBuilder sbWHID = new StringBuilder();

			DataTable dtWHID = this.BaseDataAccess.GetDataTable ( sSelectSql );

			foreach ( DataRow dr in dtWHID.Rows )
			{
				sbWHID.Append ( " '"+dr["WHID"].ToString() + "'," );
			}

			if ( sbWHID.Length > 0 )
			{
				if ( sbWHID[sbWHID.Length-1] == ',' )
				{
					sWHIDs = sbWHID.Remove( sbWHID.Length-1 , 1 ).ToString();
				}
			}

			return sWHIDs ;	// ����ֵ��: '01�ⷿ','03�ⷿ'
		}

		#endregion

		#region Ϊָ���ⷿ����Ĭ�Ͽ�λ

		/// <summary>
		/// Ϊָ���ⷿ����Ĭ�Ͽ�λ Liujun add at 2007-7-4
		/// </summary>
		/// <param name="sWHID">�ⷿID</param>
		/// <returns></returns>
		public string AddDefaultBIN ( string sWHID )
		{
			

			string sSelectSql = "SELECT * FROM WH_BI_BIN WHERE Status = "+(int)DataEntity.BINState.State_Default+" AND WHID = '"+sWHID+"'";
			string sInsertSql = "INSERT INTO WH_BI_BIN (BINID,WHID,Note,Status,BINDescription) VALUES ('"+sWHID+"-Default','"+sWHID+"','Ĭ�Ͽ�λ',"+(int)DataEntity.BINState.State_Default+",'Default bin')";
			string sErrorMsg = string.Empty;

			// ���Ȳ鿴�Ƿ����Ĭ�Ͽ�λ
			int iNum = this.BaseDataAccess.GetDataTableCount( sSelectSql );
 
			if ( iNum == 0 )
			{
				// ���������������
				sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sInsertSql );
			}
			return sErrorMsg;
		}

		#endregion

		#region ���ָ���ⷿ��Ĭ�Ͽ�λ

		/// <summary>
		/// ���ָ���ⷿ��Ĭ�Ͽ�λ Liujun add at 2007-7-5
		/// </summary>
		/// <param name="sWHID"></param>
		/// <returns></returns>
		public string GetDefaultBIN ( string sWHID )
		{
			string sSelectSql = "SELECT BINID FROM WH_BI_BIN WHERE Status = "+(int)DataEntity.BINState.State_Default+" AND WHID = '"+sWHID+"'  ";
			string sBINID = string.Empty;

			DataTable dt = this.BaseDataAccess.GetDataTable( sSelectSql );

			if ( dt.Rows.Count > 0 )
			{
				sBINID = dt.Rows[0]["BINID"].ToString();
			}

			return sBINID ;
		}

		#endregion

		#region ���ϵͳ�����λ

		/// <summary>
		/// ����ϵͳ�����λ Liujun add at 2007-7-5
		/// </summary>
		/// <returns></returns>
		public string GetVirtualBIN ()
		{
			string sSelectSql = "SELECT BINID FROM WH_BI_BIN WHERE Status = "+(int)DataEntity.BINState.State_Virtual;
			string sBINID = string.Empty;

			DataTable dt = this.BaseDataAccess.GetDataTable( sSelectSql );

			if ( dt.Rows.Count > 0 )
			{
				sBINID = dt.Rows[0]["BINID"].ToString();
			}

			return sBINID ;
		}

		#endregion


		public DataTable GetDefaultWareHouse()
		{
		  string  sql="select WHID,WHName from WH_BI_WareHouse";

		  return this.BaseDataAccess.GetDataTable(sql);
		}

	}

}
