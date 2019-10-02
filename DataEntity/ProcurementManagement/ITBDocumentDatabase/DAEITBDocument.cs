using System;
using System.Data;
using System.Collections;
using Common;
using Cnwit;

namespace DataEntity
{
	/// <summary>
	/// DAEITBDocument ��ժҪ˵����
	/// </summary>
	public class DAEITBDocument:DAEBase
	{
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		string strSql=string.Empty;
		public DAEITBDocument()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public string GetSelectTenderID(string ITBpkvalue)
		{
			string SelectSql = " SELECT TenderID FROM ITBDocument WHERE ITBIDKey = '"+ITBpkvalue+"'";
			DataTable dt_Temp = _da.GetDataTable ( SelectSql );
			if (dt_Temp.Rows.Count >0)
			{
				return dt_Temp.Rows[0][0].ToString();
			}
			else
				return "";
		}


		

		/// <summary>
		/// ͨ�����Ա������÷�������ID����Ŀ����ID��Hashtable
		/// </summary>
		/// <param name="strTenderID"></param>
		/// <returns></returns>
		public Hashtable GetSRIDKeyAndProjectName ( string strTenderID )
		{
			string SelectSql = "SELECT SRIDKey , ProjectName , DepartmentID , Currency , PlanAmount  FROM TCStrategy WHERE TenderID = '"+strTenderID+"'";

			DataTable dt_Temp = _da.GetDataTable( SelectSql );

			Hashtable hashtable = new Hashtable(5);

			if ( dt_Temp.Rows.Count > 0 )
			{
				hashtable.Add ( "SRIDKey" , dt_Temp.Rows[0]["SRIDKey"]	);
				hashtable.Add ( "ProjectName" , dt_Temp.Rows[0]["ProjectName"] );
				hashtable.Add ( "DepartmentID" , dt_Temp.Rows[0]["DepartmentID"] );
				hashtable.Add ( "Currency" , dt_Temp.Rows[0]["Currency"] );
				hashtable.Add ( "PlanAmount" , dt_Temp.Rows[0]["PlanAmount"] );
			}

			return hashtable;
		}


		/// <summary>
		/// ѡ��ɰ첿�����в���ID
		/// </summary>
		/// <returns></returns>
		public string GetDepartmentID ()
		{
			string SelectSql = "SELECT IDKey FROM BI_Department WHERE IsMisDepartment = 1";
			string strDepartmentIDs = string.Empty;
			System.Text.StringBuilder sbuilder = new System.Text.StringBuilder();

			using ( System.Data.SqlClient.SqlDataReader dr = _da.GetDataReader ( SelectSql ) )
			{
				while ( dr.Read() )
				{
					sbuilder.Append (" '");
					sbuilder.Append (dr["IDKey"].ToString());
					sbuilder.Append ("',");
				}
			}

			if ( sbuilder.Length > 0 )
			{
				if ( sbuilder[sbuilder.Length-1] == ',' )
				{
					// �����һ��","ȥ��
					strDepartmentIDs = sbuilder.Remove( sbuilder.Length - 1 , 1 ).ToString();
				}
			}

			return strDepartmentIDs;
		}
	}
}
