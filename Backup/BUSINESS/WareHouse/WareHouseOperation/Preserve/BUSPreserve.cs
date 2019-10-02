using System;
using System.Data;
namespace Business
{
	/// <summary>
	/// BUSPreserve 的摘要说明。
	/// </summary>
	public class BUSPreserve : BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSPreserve()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public string CheckChildRows(DataTable dtPOMaterial)
		{
			foreach(DataRow dr in dtPOMaterial.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoMaterialSelected" ;
		}

		public string CheckChildData(DataTable dtPreserveMaterial)
		{
			if (dtPreserveMaterial.Rows.Count > 0)
			{
				foreach ( DataRow drPreserveMaterial in dtPreserveMaterial.Rows )
				{
					if(drPreserveMaterial.RowState != DataRowState.Deleted)
					{
						Decimal iQuantityPreserve = Decimal.Parse(drPreserveMaterial["WH_PreserveMaterial.QuantityPreserve"].ToString()); 
						Decimal iQuantityInBin = Decimal.Parse(drPreserveMaterial["WH_PreserveMaterial.QuantityInBin"].ToString()) ; 
						if (iQuantityPreserve > iQuantityInBin)
						{		
							
							return "ErrQuantity";
						}
					}
				}
			}
			return "";
		}
	}
}
