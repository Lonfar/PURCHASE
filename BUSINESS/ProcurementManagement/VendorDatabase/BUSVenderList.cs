using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;

namespace Business 
{
	/// <summary>
	/// 供应商名录业务类 ( Added By Liujun at 10.24 )
	/// </summary>
	public class BUSVenderList: BUSBase
	{
		public BUSVenderList()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

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
