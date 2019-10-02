using System;
using System.Collections;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// 招标公告(编辑页面)的数据实体类
	/// </summary>
	public class DAETenderBulletin_Edit : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETenderBulletin_Edit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 通过采办策略编号来获得服务申请编号,合同模式编号,项目名称
		/// </summary>
		/// <param name="tenderID">采办策略编号</param>
		/// <returns>包含服务申请编号,合同模式编号,项目名称的Hashtable</returns>
		public Hashtable GetDetialByTenderID ( string tenderID )
		{
			//string SelectSql = "SELECT SRIDKey , ContractMode , ProjectName FROM TCStrategy WHERE TenderID = '"+tenderID+"'";
			//Modified by QSQ 12.15 修改显示SRName
			string SelectSql = @"SELECT   ProjectName FROM TCStrategy
									WHERE TenderID = '"+tenderID+"'";
			Hashtable hashtable = new Hashtable();

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					hashtable.Add ( "ProjectName" , Convert.ToString ( dr["ProjectName"] ) );
				}
			}

			return hashtable;
		}

//		/// <summary>
//		/// 通过招标公告的ID来获得:服务申请编号,合同模式,采办策略编号,项目名称
//		/// </summary>
//		/// <param name="publishID"></param>
//		/// <returns></returns>
//		public Hashtable GetDetailByPublishID ( string publishID )
//		{
//			string SelectSql = "SELECT TCStrategy.SRID , TCStrategy.ContractMode , TCStrategy.ProjectName , TCStrategy.TenderID FROM TCStrategy JOIN BidPlacard on TCStrategy.TenderID = BidPlacard.TenderID WHERE BidPlacard.PublishID = '"+publishID+"'";
//
//			Hashtable hashtable = new Hashtable();
//
//			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
//			{
//				while ( dr.Read() )
//				{
//					hashtable.Add ( "SRID" , Convert.ToString ( dr["SRID"] ));
//					hashtable.Add ( "ContractMode" , Convert.ToString ( dr["ContractMode"] ) );
//					hashtable.Add ( "ProjectName" , Convert.ToString ( dr["ProjectName"] ) );
//					hashtable.Add ( "TenderID" , Convert.ToString ( dr["TenderID"] ));
//				}
//			}
//
//			return hashtable ;
//		}

		/// <summary>
		/// 更新招标公告的状态
		/// </summary>
		/// <param name="TenderState"></param>
		/// <param name="IDKey"></param>
		/// <returns></returns>
		public string UpdateState ( DataEntity.TenderState state , string PublishID )
		{
			int nTenderState = (int)state;
			return _da.ExecuteDMLSQL ( "UPDATE BidPlacard Set State = "+nTenderState+ " WHERE PublishID = '"+PublishID+"'");
		}
		/// <summary>
		/// 得到ServiceRequistion 的IDKEY
		/// </summary>
		/// <param name="SIDkey"></param>
		/// <returns></returns>
		public string  GetTenderID(string PublishID)
		{
			string	strSql = @"select TenderID from BidPlacard
						where  BidPlacard.PublishID ='"+PublishID+"'";
			DataTable dt = _da.GetDataTable(strSql);
			if(dt.Rows.Count >0)
			{
				return dt.Rows[0]["TenderID"].ToString();
			}else return "";

		}

		/// <summary>
		/// 更新SR的状态
		/// </summary>
		/// <param name="TenderState"></param>
		/// <param name="IDKey"></param>
		/// <returns></returns>
		public string UpdateTenderState ( DataEntity.TenderState state , string IDKey )
		{
			int nTenderState = (int)state;
			return _da.ExecuteDMLSQL ( "UPDATE ServiceRequistion Set SRState = "+nTenderState+ " WHERE IDKey = '"+IDKey+"'");
		}

		/// <summary>
		/// 得到SR状态
		/// </summary>
		/// <param name="SRIDKey"></param>
		/// <returns></returns>
		public string GetTenderState ( string SRIDKey )
		{
			string strSql = "select TenderState from ServiceRequistion where IDKey = '"+SRIDKey+"'";
			DataTable dt = _da.GetDataTable(strSql);
			if (dt.Rows.Count>0)
			{
				return dt.Rows[0]["TenderState"].ToString();
			}else return "0";
		}
		
		/// <summary>
		/// 得到SR状态
		/// </summary>
		/// <param name="SRIDKey"></param>
		/// <returns></returns>
		public string GetBidState(string publishID )
		{
			string strSql = "select State from BidPlacard where PublishID = '"+publishID+"'";
			DataTable dt = _da.GetDataTable(strSql);
			if (dt.Rows.Count>0)
			{
				return dt.Rows[0]["State"].ToString();
			}
			else return "0";
		}

	}
}
