using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSSealConfig ��ժҪ˵����
	/// added by QsQ
	/// </summary>
	public class BUSSealConfig : BUSBase
	{
		public BUSSealConfig()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}		

		/// <summary>
		/// ���ܷ��Ľ��ܴ����ܷ��Ľ��
		/// </summary>
		private const string BUSCHKERR001 = "BUSCHKERR001";

		/// <summary>
		/// �̳л�����߼�����У�鲢�����ܷ��Ľ��ܴ����ܷ��Ľ��
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			// ������Ϣ
			string strErrorMessage = string.Empty;

			foreach ( DataRow dataRow in dt.Rows )
			{
				if ( Convert.ToDecimal( dataRow["TI_SealConfig.SealLevel"] ) < Convert.ToDecimal ( dataRow["TI_SealConfig.UnSealLevel"]) )
				{
					strErrorMessage = BUSCHKERR001;

					break;
				}
			}
			return strErrorMessage;
		}
	}
}
