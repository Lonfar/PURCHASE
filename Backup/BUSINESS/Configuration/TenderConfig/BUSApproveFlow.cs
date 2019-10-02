using System;
using System.Data;
using Cnwit.Utility;
using DataEntity;
using System.Collections;
using System.Collections.Generic;

namespace Business
{
	/// <summary>
	/// BUSTenderGroup 的摘要说明。
	/// </summary>
	public class BUSApproveFlow:BUSBase
	{
		private DAEApproveFlow dataEntity;

		public BUSApproveFlow()
		{
			dataEntity = new DAEApproveFlow();
		}

		/// <summary>
		/// 验证是否有审批步骤
		/// </summary>
		/// <param name="dtChild"></param>
		/// <returns></returns>
		public string CheckChildRows(DataTable dtChild)
		{
            string errMsg = string.Empty;

            errMsg = CheckEmptyRow(dtChild);

            if (errMsg.Length == 0 )
            {
                DataTable dtCopy = dtChild.Copy();

                string department= "TI_ApproveFlowMember.ApproeDepartmentID";
                string position = "TI_ApproveFlowMember.PositionID";
                
                for (int i = 0; i < dtChild.Rows.Count; i++)
                {
                    if (dtChild.Rows[i].RowState != DataRowState.Deleted)
                    {
                        for (int j = i + 1; j < dtCopy.Rows.Count; j++)
                        {
                            if (dtCopy.Rows[j].RowState != DataRowState.Deleted)
                            {
                                if (dtChild.Rows[i][department].ToString() == dtCopy.Rows[j][department].ToString())
                                {
                                    if (dtChild.Rows[i][position].ToString() == dtCopy.Rows[j][position].ToString())
                                    {
                                        return "ApprovalMemberRepeat";
                                    }
                                }
                            }
                        }
                    }
                }
               
            }
           
            return errMsg;
		}

        private string CheckEmptyRow(DataTable dtChild)
        {
            if (dtChild.Rows.Count == 0)
                return "NoApprovalMember";

            foreach (DataRow dr in dtChild.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    return "";
                }
            }
            return "NoApprovalMember";

        }

		/// <summary>
		/// 校验数据
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessData(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			// 错误信息
			string strErrorMsg = string.Empty;


			if( this.IEntity.TableName == "TI_ApproveFlowMember" )
			{
				// 首先验证流程中是否有没有相关人员的部门职位
				// 只有第一步骤可以不添加部门
				bool IsOk = true;
				foreach ( DataRow dr in dt.Rows )
				{
					if(dr.RowState != DataRowState.Deleted)
					{
						if ( Convert.ToInt32( dr["TI_ApproveFlowMember.ApproeLevel"] ) != 1 && dr["TI_ApproveFlowMember.ApproeDepartmentID"]  == DBNull.Value )
						{
							IsOk = false;
						}
					}
				}

				int iStep = 0 ;

				if ( !dataEntity.ValidateProcess ( dt , ref iStep ) || !IsOk )
				{
					strErrorMsg = "IsNotCompleteProcess";
				}

				// add by wanglijie on 2008-02-01
				// 审批步骤错误则不进行下一步校验
				if ( strErrorMsg == string.Empty )  
				{//end
					if ( IsOk )
					{
						// 不检查第一流程
						strErrorMsg += dataEntity.ValidateData( dt , iStep , 0 );

						if ( strErrorMsg != string.Empty  )
						{
							// 加一个标记表明是第一步校验出错
							strErrorMsg += ":";
						}
					}
				}

				// ============================================================
				if ( strErrorMsg == string.Empty )
				{
					strErrorMsg += base.CheckBusinessData( dt , fieldsList );
				}
			}
			//Modified by qsq 12.11修改重复执行基类验证2次，错误号叠加的bug
			if(strErrorMsg=="")
			{
				strErrorMsg += base.CheckBusinessData( dt , fieldsList );
			}
			return strErrorMsg ;
		}
		/// <summary>
		/// 校验数据
		/// </summary>
		/// <param name="dt"></param>
		/// <param name="fieldsList"></param>
		/// <returns></returns>
		public override string CheckBusinessLogic_calculate(System.Data.DataTable dt, System.Collections.ArrayList fieldsList)
		{
			
			string errMessage="";
			
			if (this.IEntity.TableName == "TI_ApproveFlow")
			{
				if(dt.Rows.Count>0)
				{
					#region 判断金额上下限大小关系
					decimal lowerBudget=0,upperBudget=0;
					lowerBudget = Convert.ToDecimal(dt.Rows[0]["TI_ApproveFlow.LowerBudget"].ToString());
					upperBudget = Convert.ToDecimal(dt.Rows[0]["TI_ApproveFlow.UpperBudget"].ToString());
					if(lowerBudget > upperBudget) 
					{
						errMessage = "ERRBUS_App01";
					}
					else if( lowerBudget == upperBudget )
					{
						errMessage = "ERRBUS_App02";
					}
		
					#endregion 

				}
				else errMessage = "ERRBUS_App00";
			}
			else if(this.IEntity.TableName == "TI_ApproveFlowMember")
			{
				errMessage = base.CheckBusinessLogic_calculate (dt, fieldsList);
			
			}
			
			return errMessage ;

		}
	}
}
