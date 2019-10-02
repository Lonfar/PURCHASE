using System;
using System.Data ;

namespace Business
{
	/// <summary>
	/// BUSTransferDEP2DEP 的摘要说明。
	/// </summary>
	public class BUSTransferDEP2DEP :BUSBase
	{
		/// <summary>
		/// 
		/// </summary>
		public BUSTransferDEP2DEP()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dtTransferDEP2DEPMaterial"></param>
		/// <returns></returns>
		public bool CheckQuantity(DataTable dtTransferDEP2DEPMaterial)
		{
			bool bCheck = true;
			if (dtTransferDEP2DEPMaterial.Rows.Count > 0)
			{
				foreach(DataRow drTransferDEP2DEPMaterial in dtTransferDEP2DEPMaterial.Rows)
				{
					if (drTransferDEP2DEPMaterial.RowState != DataRowState.Deleted)
					{
						Decimal decFactIssuedQuantity = Decimal.Parse(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.FactIssuedQuantity"].ToString()); 
						Decimal decIssueQuantity = Decimal.Parse(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.IssueQuantity"].ToString()) ; 
						Decimal decDepreciationRate = Decimal.Parse(drTransferDEP2DEPMaterial["WH_TransferDEP2DEPMaterial.depreciationRate"].ToString()) ; 
						if (decFactIssuedQuantity > decIssueQuantity || decDepreciationRate -1 >0)
						{
							bCheck = false;
							break;
						}
					}
				}
			}
			return bCheck ;

		}
	}
}
