using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DataEntity
{
    public class DAESearchList : DAEBase
    {
        Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();

        public DataTable GetFileURL(string pkValue)
        {
            string sSql = " select * from V_InventoryReport where AttachmentIDKey = '" + pkValue + "' ";
            return _da.GetDataTable(sSql);
        }
        
    }
}
