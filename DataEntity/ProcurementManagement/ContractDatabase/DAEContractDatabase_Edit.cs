using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// ContractDatabase_Edit ��ժҪ˵����
	/// </summary>
	public class DAEContractDatabase_Edit:DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		public DAEContractDatabase_Edit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ͨ�����(���������)������ӱ������*****�����VoucherEdit�ؼ�������Ϊ�ӿؼ���Bug
		/// <param name="ParentKey"></param>
		/// <returns></returns>
		public string GetIDKey ( string ParentKeyFieldName , string ParentValue , string ChildTableName )
		{
			string IDKey = string.Empty;

			string SelectSql = " SELECT ID FROM "+ChildTableName+" WHERE "+ParentKeyFieldName +" = '"+ ParentValue+"'";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				IDKey = Convert.ToString ( dataTable.Rows[0][0] );
			}

			return IDKey ;
		}

		public bool HasProtocol(string sContractID)
		{
			//��Э����ڵ�
			string SelectSql = " SELECT * FROM Contract WHERE RelatingContractID = '"+sContractID+"' AND IsAddProtocol = 1 ";

			System.Data.DataTable dataTable = _da.GetDataTable ( SelectSql );

			if ( dataTable.Rows.Count > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public DataTable GetContract (string ContractIDKey)
		{
			string sql = @"SELECT * FROM Contract WHERE IDKey = '" + ContractIDKey + "'" ;
			return _da.GetDataTable ( sql ) ;

		}

		// �Ƿ� ContractID �Ѿ�����
		public bool ContractExist ( string ContractID )
		{
			string sql = @"SELECT ContractID FROM Contract WHERE ContractID = '" + ContractID + "'" ;
			DataTable dt = _da.GetDataTable ( sql ) ;

			if ( dt.Rows.Count > 0 )
			{
				return true ;
			}
			else
			{
				return false ;
			}
		}

		public bool ContractExist ( string ContractID , string IDKey )
		{
			string sql = @"SELECT ContractID FROM Contract WHERE IDKey <> '" + IDKey + "' AND ContractID = '" + ContractID + "'" ;
			DataTable dt = _da.GetDataTable ( sql ) ;

			if ( dt.Rows.Count > 0 )
			{
				return true ;
			}
			else
			{
				return false ;
			}
		}

	}
}
