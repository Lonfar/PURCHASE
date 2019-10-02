using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSRejectEdit ��ժҪ˵����
	/// </summary>
	public class BUSRejectEdit : BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSRejectEdit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public string CheckRejectMaterial ( DataTable dtRejectMaterial )
		{
			foreach(DataRow drRejectMaterial in dtRejectMaterial.Rows)
			{
				if (drRejectMaterial.RowState != DataRowState.Deleted)
				{
					decimal iTransQuan = Decimal.Parse(drRejectMaterial["WH_RejectMaterial.QuantityReject"].ToString()); 
					decimal iTransQuanOld = Decimal.Parse(drRejectMaterial["WH_RejectMaterial.QuantityInBin"].ToString()) ; 
					if (iTransQuan > iTransQuanOld)
					{	
						return "Error01" ;
					}
				}
			}

			return string.Empty ;
		}

		/// <summary>
		/// У�������߼�
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public string CheckBusinessData(System.Data.DataTable dt)
		{
			// ������Ϣ
			string sErrorMsg = string.Empty;
			// У���ӱ��Ƿ�������
			sErrorMsg = CheckChildRows(dt);
			if ( sErrorMsg.Trim().Length == 0 )
			{
				// У��ҵ������           
				sErrorMsg = CheckRejectMaterial ( dt );
				if ( sErrorMsg.Trim().Length > 0 )
				{ 
					return sErrorMsg;
				}
			}
			return sErrorMsg;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dt">Edit��</param>
		/// <returns>sErrMsg</returns>
		private string CheckChildRows(DataTable dtChild)
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
	}
}
