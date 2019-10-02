using System;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAESealConfig ��ժҪ˵����
	/// </summary>
	public class DAESealConfig:DAEBase
	{
		/// <summary>
		/// ���ݴ洢��
		/// </summary>
		private Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

		public DAESealConfig()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// �̳��˻���ı��淽������ʹ����������ʽ���и��������ļ�¼
		/// </summary>
		/// <returns></returns>
		public override string Save()
		{
			// ������Ϣ
			string ErrorMessage = string.Empty;
			
			if ( CurDataTable.Rows.Count > 0 )
			{
				ErrorMessage = base.Save();

				// �������Ϊ��Ч�ܷ��,������������Ч����Ϊ��
				if ( Convert.ToInt32( CurDataTable.Rows[0]["TI_SealConfig.IsValid"] ) == 1 )
				{
					string UpdateSql = "UPDATE TI_SealConfig SET IsValid = 0 WHERE IDKey <> '"+Convert.ToString( CurDataTable.Rows[0]["TI_SealConfig.IDKey"] )+"'";
				
					if ( ErrorMessage == "" )
					{
						ErrorMessage += _da.ExecuteDMLSQL ( UpdateSql );
					}
				}
			}

			return ErrorMessage;
		}
	
		/// <summary>
		/// �����Ч���ܷ����Ϣ(IDKey)
		/// </summary>
		/// <returns></returns>
		public System.Data.SqlClient.SqlDataReader GetValidData ()
		{
			string SelectSql = "SELECT IDKey FROM TI_SealConfig WHERE IsValid = 1 ";

			System.Data.SqlClient.SqlDataReader drInfo = Common.GetProjectDataAcess.GetDataAcess().GetDataReader(SelectSql) ; 
				
			return drInfo;
		}
	}
}
