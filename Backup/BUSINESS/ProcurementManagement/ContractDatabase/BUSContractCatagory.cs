using System;
using DataEntity;

namespace Business
{
	/// <summary>
	/// 合同类别的业务逻辑类 Added by Liujun at 11.17
	/// </summary>
	public class BUSContractCatagory : BUSBase
	{
		/// <summary>
		/// 数据实体层
		/// </summary>
		private DataEntity.DAEContractCatagory dataEntity;

		public BUSContractCatagory()
		{
			dataEntity = new DAEContractCatagory();
		}

		/// <summary>
		/// 在同一级别下是否又相同的描述
		/// </summary>
		/// <param name="strContractDescription">合同类型描述</param>
		/// <param name="strConID">主键</param>
		/// <param name="strParentID">父ID</param>
		/// <returns>true:合同描述重复,false:合同描述未重复</returns>
		public bool IsRepeatDescription ( string strContractDescription , string strConID , string strParentID )
		{
			int iCount = dataEntity.ContractNum( strContractDescription , strConID , strParentID );

			if ( iCount > 0 )
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
