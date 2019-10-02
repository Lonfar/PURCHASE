using System;
using Common;
using Cnwit.Utility;
using System.Data;

namespace Business
{
	/// <summary>
	/// BUSABCCalssScaleEdit 的摘要说明。
	/// </summary>
	public class BUSABCCalssScaleEdit : BUSBase
	{
		DataAcess da = Common.GetProjectDataAcess.GetDataAcess();

		/// <summary>
		/// 判断A,B,C是否合法
		/// </summary>
		/// <param name="dAScale"></param>
		/// <param name="dBScale"></param>
		/// <param name="dCScale"></param>
		/// <returns></returns>
		public bool ABCIsValid(decimal dAScale ,decimal dBScale,ref decimal dCScale)
		{
			if ( dAScale < 0 || dAScale > 100)
			{
				return false;				
			}
			else if ( dBScale < 0 || dBScale > 100)
			{
				return false;
			}
			else if (dAScale + dBScale > 100)
			{
				return false;
			}
			else
			{
				dCScale = 100 - dAScale - dBScale;
				return true;
			}
		}
	}
}
