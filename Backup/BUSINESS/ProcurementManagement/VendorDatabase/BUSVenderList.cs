using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;

namespace Business 
{
	/// <summary>
	/// ��Ӧ����¼ҵ���� ( Added By Liujun at 10.24 )
	/// </summary>
	public class BUSVenderList: BUSBase
	{
		public BUSVenderList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
