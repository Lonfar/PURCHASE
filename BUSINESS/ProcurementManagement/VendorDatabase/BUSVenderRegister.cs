using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSVenderRegister 的摘要说明。
	/// Added by QSQ 10.18
	/// </summary>
	public class BUSVenderRegister:BUSBase
	{
		public BUSVenderRegister()
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
