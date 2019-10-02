using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;


namespace Business
{
	/// <summary>
	/// BUSVendorMaterial 的摘要说明。
	/// </summary>
	public class BUSVendorMaterial : BUSBase
	{
		/// <summary>
		/// 是否在WH_MaterialVendor表中存在
		/// </summary>
		/// <returns></returns>
		public bool CheckExist( string sItemCode,DataTable dtMaterial )
		{
			foreach(DataRow dr in dtMaterial.Rows)
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					if ( sItemCode == dr["ItemCode"].ToString()) return true;
				}
			}
			return false;
		}
	}
}
