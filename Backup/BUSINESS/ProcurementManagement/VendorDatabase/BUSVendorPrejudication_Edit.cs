using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;

namespace Business
{
	/// <summary>
	/// ��Ӧ��Ԥ��ı༭ʵ����
	/// </summary>
	public class BUSVendorPrejudication_Edit:BUSBase
	{

		/// <summary>
		/// �Ƿ���WH_MaterialVendor���д���
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string sItemCode,DataTable dtMaterial )
		{
			foreach(DataRow dr in dtMaterial.Rows)
			{
				if ( sItemCode == dr["ItemCode"].ToString()) return true;
			}
			return false;
		}


		/// <summary>
		/// ����ʵ����
		/// </summary>
		private DataEntity.DAEVendorPrejudication_Edit dataEntity;

		public BUSVendorPrejudication_Edit()
		{
			dataEntity = new DAEVendorPrejudication_Edit();
		}

		/// <summary>
		/// �Ƿ�����ͬ�ĵ�¼��
		/// </summary>
		/// <param name="strLoginName">��¼��</param>
		/// <param name="strIDKey">��½���</param>
		/// <returns>true:���ظ�,false:���ظ�</returns>
		public bool IsRepeatLoginName ( string strLoginName , string strIDKey )
		{
			// ͨ�����ݲ��ô˵�¼����Ӧ������
			DataTable dataTable = dataEntity.GetNumByLoginName( strLoginName );

			bool IsRepeat = false;

			if ( dataTable.Rows.Count > 1 )
			{
				IsRepeat = true;
			}
			else if ( dataTable.Rows.Count == 1 )
			{
				if ( dataTable.Rows[0]["IDKey"].ToString() != strIDKey )
				{
					IsRepeat = true;
				}
			}

			return IsRepeat;
		 }

        //public override string CheckBusinessLogic_rule (DataTable dt, System.Collections.ArrayList fieldsList)
        //{
        //    string strErrMsg = string.Empty;

        //    if ( IEntity.TableName == "Vendor" )
        //    {
        //        if ( IsRepeatLoginName ( dt.Rows[0]["Vendor.LoginName"].ToString() , dt.Rows[0]["Vendor.IDKey"].ToString() ))
        //        {
        //            strErrMsg = "HasRepeatLoginName";
        //        }
        //    }

        //    return strErrMsg;
        //}

        public string CheckChildRows(System.Data.DataTable dataTable)
        {
            string returnString = string.Empty;

            if (dataTable.Rows.Count == 0)
                return "NoCountry";

            foreach (DataRow dr in dataTable.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    returnString = "";
                    break;
                }
            }
            return returnString;
        }

        public string CheckBusinessData(DataTable dataTable)
        {
            return CheckChildRows(dataTable);
        }

	}
}
