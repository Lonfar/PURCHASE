using System;
using System.Data;
namespace DataEntity
{
	/// <summary>
	/// ��������(�༭ҳ��)������ʵ����
	/// </summary>
	public class DAEApproveProcessing_Edit : DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAEApproveProcessing_Edit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����SR��״̬
		/// </summary>
		/// <param name="State">״̬</param>
		/// <param name="IDKey">SR����</param>
		public void UpdateTenderState ( string IDKey , string State )
		{
			_da.ExecuteDMLSQL ( "UPDATE ServiceRequistion SET SRState = '"+State+"'WHERE ServiceRequistion.IDKey = '"+IDKey+"'" );
		}

		/// <summary>
		/// �����ύ���״̬
		/// </summary>
		/// <param name="ObjectiveID">����ID</param>
		/// <param name="ObjectiveType">��������</param>
		/// <param name="State">����״̬</param>
		public void UpdatePutInState ( string IDKey , int State )
		{
			_da.ExecuteDMLSQL ( "UPDATE PutIn SET State = "+State+" WHERE IDKey = '"+IDKey+"'" );
		}

		/// <summary>
		/// ͨ��PubishID�õ� title��content��releaseDate��Ϣ
		/// </summary>
		public DataTable GetBidPlacardInfo (string publishID)
		{
			string strSql = @" select Title ,Contents ,PublishDate from BidPlacard where publishID = '"+publishID+"'";
			DataTable dt = _da.GetDataTable( strSql );
			return dt;
		}

		#region �鿴ָ���û��Ƿ�Ϊָ�����̵����������

		/// <summary>
		/// �鿴ָ���û��Ƿ�Ϊָ�����̵����������
		/// </summary>
		/// <param name="personID">������</param>
		/// <param name="PutID">��������</param>
		/// <returns></returns>
		public bool IsLastApprove ( string PutInID , int ApproveLevel_CurrentUser )
		{
			bool IsLast = false ;

			string SelectSql = @"SELECT MAX(ApproveLevel) AS Level
								FROM ApproveMember 
								WHERE PutInID = '"+PutInID+"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				if ( Convert.ToInt32 ( dataTable.Rows[0]["Level"] ) == ApproveLevel_CurrentUser )
				{
					IsLast = true;
				}
			}

			return IsLast;
		}

		#endregion

		
		#region ���ָ��SR�ĵ�ǰ��˼���
		/// <summary>
		/// �����ύID ������Approved��PutIn��ѡȡApproved���У�״̬Ϊͨ���ģ������SR��Ӧ�ĵ�ǰ��˼���
		/// �鲻����¼������1��
		/// </summary>
		/// <param name="objType"></param>
		/// <param name="objID"></param>
		/// <returns></returns>
		public int GetCurrentSRApproveLevel(string objType,string objID)
		{
			int iApproveLevel = 1;

			string SelectSql = @"SELECT MAX( Approved.CurrApproveLevel ) AS Level FROM Approved 
								INNER JOIN PutIn 
								ON Approved.PutInID = PutIn.IDKey
								WHERE PutIn.ObjectiveType = '"+objType+"' AND PutIn.ObjectiveID = '"+objID+"' AND PutIn.State = 0 ";
		
			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					if ( dr["Level"] != DBNull.Value )
					{
						iApproveLevel = Convert.ToInt32 ( dr["Level"] );
					}
				}
			}

			return iApproveLevel;
		}
		#endregion
	}
}
