using System;
using System.Data;

namespace Business
{
	/// <summary>
	/// Ĭ������С���߼���
	/// </summary>
	public class BUSDefaultTCGroup : BUSBase
	{
		public BUSDefaultTCGroup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�

			
			//
		}

		/// <summary>
		/// У���Ƿ���ίԱ���Ա
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
		/// У������
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessData(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			return base.CheckBusinessData( dt , fieldsList );
		}

		/// <summary>
		/// У�����
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_calculate(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			return base.CheckBusinessLogic_calculate (dt, fieldsList);
		}

		/// <summary>
		/// У��ҵ�����
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_rule(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			return base.CheckBusinessLogic_rule (dt, fieldsList);
		}


 
	}
}
