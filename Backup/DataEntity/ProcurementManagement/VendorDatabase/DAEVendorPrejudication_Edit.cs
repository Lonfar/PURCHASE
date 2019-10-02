using System;
using Common;
using Cnwit.Utility;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// DAEVendorPrejudication_Edit ��ժҪ˵����
	/// </summary>
	public class DAEVendorPrejudication_Edit:DAEBase
	{
		DataAcess _da = GetProjectDataAcess.GetDataAcess();

		public string InsertMaterialVendor(string sItemCode,string sMaterialName,string sVendorID,string sComment,string sVendorName)
		{
			string errorMessage = "";
			string sSql = "insert into WH_MaterialVendor(MaterialVendorID,ItemCode,VendorID,MaterialName,Comment,VendorName) values ('"+System.Guid.NewGuid().ToString() +"',"+
						  "'"+sItemCode+"','"+sVendorID+"','"+sMaterialName+"','"+sComment+"','"+sVendorName+"')";
			errorMessage = _da.ExecuteDMLSQL(sSql);
			return errorMessage;
		}

		/// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="strIDKey"></param>
		public string DeleteMaterialVendor(string sItemCode)
		{
			string errorMessage = "";
			
			string sSql;
			if (sItemCode != null && sItemCode.Length > 0)
			{
				sSql = "delete WH_MaterialVendor where ItemCode ='"+sItemCode+"'";
			}
			else
			{
				sSql = "delete WH_MaterialVendor where 1 > 2 ";
			}
			errorMessage = _da.ExecuteDMLSQL(sSql);
			return errorMessage;
		}

		public DataTable GetMaterial( string sIDKey )
		{
			string strSql = "";
			if (sIDKey != null && sIDKey.Length > 0)
			{
				strSql = @"select * from WH_MaterialVendor where WH_MaterialVendor.VendorID = '"+sIDKey+"'";
			}
			else
			{
				strSql = @"select * from WH_MaterialVendor where 1 > 2 ";
			}
			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		public DataTable GetItemCode(string sItemCode)
		{
			string strSql = @"select * from Material where Material.ItemCode = '"+sItemCode+"'";
			DataTable dt = _da.GetDataTable ( strSql );
			return dt;
		}

		/// <summary>
		/// ���¹�Ӧ��״̬
		/// </summary>
		/// <param name="status">״̬</param>
		/// <returns></returns>
		public string UpdateVendorStatus ( int status , string IDKey )
		{
			string errorMessage = "";

			errorMessage = _da.ExecuteDMLSQL ( "UPDATE Vendor SET Status = "+status + " WHERE IDKey = '"+IDKey+"'");

			return errorMessage;
		}


		public string  GetVendorDataListReturnSt (string strIDkey)
		{
			System.Data.DataTable  dt = _da.GetDataTable ( "select distinct(dbo.f_GetVendorDataList('"+strIDkey+"')) as VendorDataList FROM VendorDataList ");

			return dt.Rows[0][0].ToString();
		}

		public System.Data.DataTable   GetVendorDataListReturnDt ()
		{
			System.Data.DataTable  dt = _da.GetDataTable ( "select TypeDescription  FROM VendorDataList ");

			return dt;
		}

		/// <summary>
		/// ��VendorAssociationName������ԭ����ID ���Ҷ�Ӧ��ID
		/// </summary>
		/// <returns></returns>
		public string GetAssociattionID (string IDKey)
		{
			string SelectSql = "SELECT VendorAssociationID FROM VendorAssociationName WHERE IDKey = '"+IDKey+"'";
			string AssociattionID = string.Empty;
			using (System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader(SelectSql))
			{
				while (dr.Read())
				{
					AssociattionID = dr["VendorAssociationID"].ToString();
				}
			}
				
			return AssociattionID;
		}
	
		/// <summary>
		/// ͨ��ID�����ָ����Ӧ�̵�״̬
		/// </summary>
		/// <param name="IDKey"></param>
		/// <returns>״̬</returns>
		public int GetVendorStatus ( string IDKey )
		{
			string SelectSql = "SELECT Status FROM Vendor WHERE IDKey = '"+IDKey+"'";	

			int iStatus = 0;

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader( SelectSql ))
			{
				while ( dr.Read() )
				{
					iStatus = Convert.ToInt32( dr["Status"] );
				}
			}

			return iStatus;
		}

		/// <summary>
		/// ��ô˵�½����Ӧ��Ӧ����Ϣ(Added by Liujun at 1114)
		/// </summary>
		/// <param name="strLoginName"></param>
		/// <returns></returns>
		public System.Data.DataTable GetNumByLoginName ( string strLoginName )
		{
			string SelectSql  = "SELECT * FROM Vendor WHERE LoginName = '"+strLoginName+"'";

			return _da.GetDataTable( SelectSql );
		}

		#region �����Ƿ�Ϊ������ҵ��ȷ����Ҫ�ϴ����ļ������б� ( Added by Liujun at 0209 )

		/// <summary>
		/// �����Ƿ�Ϊ������ҵ��ȷ����Ҫ�ϴ����ļ������б� ( Added by Liujun at 0209 )
		/// </summary>
		/// <param name="iIsColony"></param>
		/// <returns></returns>
		public System.Data.DataTable GetVendorDataListByIsColony ( int iIsColony )
		{
			string SelectSql = string.Empty;
			System.Data.DataTable dt_Data = new System.Data.DataTable();

			switch ( iIsColony )
			{
					// ��������ҵ
				case 0 :
				{
					SelectSql = "SELECT TypeDescription FROM VendorDataList WHERE BT_VendorCorTypeID = 2 OR BT_VendorCorTypeID = 3 ";
					break;
				}


					// ������ҵ
				case 1 :
				{
					SelectSql = "SELECT TypeDescription FROM VendorDataList WHERE BT_VendorCorTypeID = 1 OR BT_VendorCorTypeID = 3 ";
					break;
				}
			}

			dt_Data = _da.GetDataTable ( SelectSql );

			return dt_Data;
		}

		#endregion
	}
}
