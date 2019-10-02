using System;
using System.Data;
namespace Business
{
	/// <summary>
	/// BUSPreserve ��ժҪ˵����
	/// </summary>
	public class BUSPreserve : BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSPreserve()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
