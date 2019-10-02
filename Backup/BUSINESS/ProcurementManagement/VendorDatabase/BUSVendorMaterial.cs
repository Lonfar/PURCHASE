using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;


namespace Business
{
	/// <summary>
	/// BUSVendorMaterial ��ժҪ˵����
	/// </summary>
	public class BUSVendorMaterial : BUSBase
	{
		/// <summary>
		/// �Ƿ���WH_MaterialVendor���д���
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
