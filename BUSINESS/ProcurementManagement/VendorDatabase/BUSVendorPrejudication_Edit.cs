using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;

namespace Business
{
	/// <summary>
	/// 供应商预审的编辑实体类
	/// </summary>
	public class BUSVendorPrejudication_Edit:BUSBase
	{

		/// <summary>
		/// 是否在WH_MaterialVendor表中存在
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
		/// 数据实体类
		/// </summary>
		private DataEntity.DAEVendorPrejudication_Edit dataEntity;

		public BUSVendorPrejudication_Edit()
		{
			dataEntity = new DAEVendorPrejudication_Edit();
		}

		/// <summary>
		/// 是否有相同的登录名
		/// </summary>
		/// <param name="strLoginName">登录名</param>
		/// <param name="strIDKey">登陆编号</param>
		/// <returns>true:有重复,false:无重复</returns>
		public bool IsRepeatLoginName ( string strLoginName , string strIDKey )
		{
			// 通过数据层获得此登录名对应的数量
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
