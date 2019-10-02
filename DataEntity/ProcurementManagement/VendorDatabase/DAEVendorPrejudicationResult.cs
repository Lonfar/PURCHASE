using System;
using System.Data.SqlClient;
using Cnwit.Utility;
using System.Data;

namespace DataEntity
{
	/// <summary>
	/// ��Ӧ��Ԥ��������ʵ���� Added by Liujun at 10.20
	/// </summary>
	public class DAEVendorPrejudicationResult : DAEBase
	{
		public DAEVendorPrejudicationResult()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���ݷ���ʵ��
		/// </summary>
		DataAcess da = Common.GetProjectDataAcess.GetDataAcess();

		/// <summary>
		/// ͨ����Ӧ��ID��ù�Ӧ�̱��(Added By Liujun at 10.20)
		/// </summary>
		/// <param name="VendorIDKey">��Ӧ��ID</param>
		/// <returns>��Ӧ�̱��</returns>
		public string GetVendorNo ( string VendorIDKey )
		{
			string strVendorNo = string.Empty;

			string SelectSql = "Select dbo.GenerateCode ( '"+VendorIDKey+"' ) AS Code";

			using ( SqlDataReader dr = da.GetDataReader ( SelectSql ) )
			{
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        strVendorNo = Convert.ToString(dr["Code"]);
                    }
                }
			}

			return strVendorNo;
		}

		/// <summary>
		/// ͨ����Ӧ��ID���ɹ�Ӧ�̱��( Added By Liujun at 1.11 )
		/// </summary>
		/// <param name="VendorIDKey">��Ӧ��ID</param>
		/// <returns>��Ӧ�̱��,������������򷵻ر��,�������Ϊ�����ʾ����ʧ��</returns>
		public string UpdateNo ( string VendorIDKey )
		{
			string strVendorNo = string.Empty;

			strVendorNo = GetVendorNo ( VendorIDKey );

			if ( strVendorNo.Length > 0 )
			{
				da.ExecuteDMLSQL ( "UPDATE Vendor SET VendorNo = '"+strVendorNo+"'  WHERE IDKey = '"+VendorIDKey+"'" );	
			}

			return strVendorNo;
		}

		public void  UpdateNo ( string VendorIDKey , string VendorNo )
		{
			da.ExecuteDMLSQL ( "UPDATE Vendor SET VendorNo = '"+VendorNo+"'  WHERE IDKey = '"+VendorIDKey+"'" );
		}

        public string JugeVendorNo(string VendorNo)
        {
            string sSql = "select * from Vendor where VendorNo = '" + VendorNo + "' ";
            DataTable dt = da.GetDataTable(sSql);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Status"].ToString() == "2")
                {
                    return "";
                }
                return "RepeateName";
            }
            else
            {
                return "";
            }
        }
	}
}
