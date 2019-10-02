using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using System.Text;

namespace DataEntity
{
	/// <summary>
	/// 库位数据实体类
	/// </summary>
	public class DAEBIBIN:DAEBase
	{
		public DAEBIBIN()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 返回仓库节点ID
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
		/// 检查是否有子节点
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
		#region 获得库房所对应得所有库位信息

		/// <summary>
		/// 获得库房所对应得所有库位信息
		/// </summary>
		/// <param name="sWHID">库房ID</param>
		/// <returns>对应所有库位ID</returns>
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

			return sBINID; // 返回值如: '02库位','03库位','05库位'
		}

		#endregion

		#region 获得有库位的库房ID

		/// <summary>
		/// 获得有库位的库房ID
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

			return sWHIDs ;	// 返回值如: '01库房','03库房'
		}

		#endregion

		#region 为指定库房新增默认库位

		/// <summary>
		/// 为指定库房新增默认库位 Liujun add at 2007-7-4
		/// </summary>
		/// <param name="sWHID">库房ID</param>
		/// <returns></returns>
		public string AddDefaultBIN ( string sWHID )
		{
			

			string sSelectSql = "SELECT * FROM WH_BI_BIN WHERE Status = "+(int)DataEntity.BINState.State_Default+" AND WHID = '"+sWHID+"'";
			string sInsertSql = "INSERT INTO WH_BI_BIN (BINID,WHID,Note,Status,BINDescription) VALUES ('"+sWHID+"-Default','"+sWHID+"','默认库位',"+(int)DataEntity.BINState.State_Default+",'Default bin')";
			string sErrorMsg = string.Empty;

			// 首先查看是否存在默认库位
			int iNum = this.BaseDataAccess.GetDataTableCount( sSelectSql );
 
			if ( iNum == 0 )
			{
				// 如果不存在则新增
				sErrorMsg = this.BaseDataAccess.ExecuteDMLSQL ( sInsertSql );
			}
			return sErrorMsg;
		}

		#endregion

		#region 获得指定库房的默认库位

		/// <summary>
		/// 获得指定库房的默认库位 Liujun add at 2007-7-5
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

		#region 获得系统虚拟库位

		/// <summary>
		/// 查找系统虚拟库位 Liujun add at 2007-7-5
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
