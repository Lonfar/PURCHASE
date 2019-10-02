using System;
using System.Data;
namespace Business
{
	/// <summary>
	/// BUSCancelPreserve ��ժҪ˵����
	/// </summary>
	public class BUSCancelPreserve : BUSBase
	{
		public BUSCancelPreserve()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region �����������ܴ���Ԥ������
		public void CalTotalAmount(DataTable dtPreserveMaterial ,ref  decimal iQuantityByCanceled,ref decimal iQuantityByPreserved)
		{
			foreach(DataRow drPreserveMaterial in dtPreserveMaterial.Rows)
			{
				if(drPreserveMaterial.RowState != DataRowState.Deleted)
				{
					iQuantityByCanceled =  Convert.ToDecimal(drPreserveMaterial["WH_PreserveMaterial.QuantityByCanceled"].ToString());
					iQuantityByPreserved =  Convert.ToDecimal(drPreserveMaterial["WH_PreserveMaterial.QuantityByPreserved"].ToString());
				}
			}
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtChild)
		{
			foreach(DataRow dr in dtChild.Rows)
			{
				if(dr.RowState != DataRowState.Deleted)
				{
					return "" ;
				}
			}
			return "NoMaterialSelected" ;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtPreserveMaterial"></param>
		/// <returns></returns>
		public string CheckChildData(DataTable dtPreserveMaterial)
		{
			decimal iQuantityByCanceled = 0.0m ;
			decimal iQuantityByPreserved = 0.0m ;
			CalTotalAmount(dtPreserveMaterial,ref iQuantityByCanceled,ref iQuantityByPreserved);
			if (iQuantityByCanceled > iQuantityByPreserved)
			{
				return "ErrQuantity";
			}
			return "";	
		}
	}
}
