using System;
using System.Collections;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// �б깫��(�༭ҳ��)������ʵ����
	/// </summary>
	public class DAETenderBulletin_Edit : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAETenderBulletin_Edit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ͨ���ɰ���Ա������÷���������,��ͬģʽ���,��Ŀ����
		/// </summary>
		/// <param name="tenderID">�ɰ���Ա��</param>
		/// <returns>��������������,��ͬģʽ���,��Ŀ���Ƶ�Hashtable</returns>
		public Hashtable GetDetialByTenderID ( string tenderID )
		{
			//string SelectSql = "SELECT SRIDKey , ContractMode , ProjectName FROM TCStrategy WHERE TenderID = '"+tenderID+"'";
			//Modified by QSQ 12.15 �޸���ʾSRName
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
//		/// ͨ���б깫���ID�����:����������,��ͬģʽ,�ɰ���Ա��,��Ŀ����
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
		/// �����б깫���״̬
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
		/// �õ�ServiceRequistion ��IDKEY
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
		/// ����SR��״̬
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
		/// �õ�SR״̬
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
		/// �õ�SR״̬
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
