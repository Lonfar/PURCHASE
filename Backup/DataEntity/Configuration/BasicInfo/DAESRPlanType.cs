using System;
using Cnwit.Utility;
using Common;
using System.Data;


namespace DataEntity
{
	/// <summary>
	/// DAESRPlanType ��ժҪ˵����
	/// </summary>
	public class DAESRPlanType:DAEBase
	{
		string strSql = string.Empty;
		Cnwit.Utility.DataAcess _da = Common.GetProjectDataAcess.GetDataAcess();
		public DAESRPlanType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}



		/// <param name="table">���ݱ�</param>
		/// <param name="parentid">��id</param>
		/// ͨ�����ϲ�������¼�Ŀ¼��id Create by wxc at 2006/12/21
		public string GetId(string table,string parentid)
		{
			DataSet dbset=new DataSet();
			string sql="select * from "+table+" where ParentID='"+parentid+"'";
			dbset=_da.GetDataSet(sql);
			string s="";
			if (dbset.Tables[0].Rows.Count>0)
			{
				//�Ѿ������Ӽ�
				string strid=dbset.Tables[0].Rows[0]["IDKey"].ToString();
				int max=0;
				max=int.Parse(strid) ;//int.Parse(strid.Substring(1,strid.Length-1));
				for (int i=0;i<dbset.Tables[0].Rows.Count;i++)
				{
					strid=dbset.Tables[0].Rows[i]["IDKey"].ToString();
					if (int.Parse(strid.ToString())>=max)
					{
						max=int.Parse(strid.Trim());
					}
				}
				max+=1;
				if (max.ToString().Length<strid.Length)
				{
					for (int i=0;i<strid.Length-max.ToString().Length;i++)
					{
						s=s+"0";
					}
				}
				s=s+max.ToString();
			}
			else
			{
				//��û���Ӽ�
				if (parentid=="0")
				{
					//��Ŀ¼
					s="00";
				}
				else
				{
					//��Ŀ¼
					sql="select * from "+table+" where IDKey='"+parentid+"'";
					dbset=_da.GetDataSet(sql);
					if (dbset.Tables[0].Rows.Count>0)
					{
						for (int i=0;i<int.Parse(dbset.Tables[0].Rows[0]["Number"].ToString());i++)
						{
							s=s+"0";
						}
						s=parentid+s+"1";
					}
				}
			}
			return s;

		}

		public int GetRank(string table,string parentid)
		{
			//���Ӽ��ļ���
			DataSet dbset=new DataSet();
			string sql="select * from "+table+" where IDkey='"+parentid+"'";
			dbset=_da.GetDataSet(sql);
			int Number=0;
			if (dbset.Tables[0].Rows.Count>0)
			{
				Number=int.Parse(dbset.Tables[0].Rows[0]["Number"].ToString())+1;
			}
			return Number;

		}

		/// <summary>
		///  wether one node has children
		/// </summary>
		/// <param name="idKey">Bi_department's pk</param>
		/// <returns></returns>
		public bool HashChildren(string strConID)
		{
			string sql="";
			sql="select count(*) from BT_SRPlanType where ParentID  = '"+strConID+"'";
			System.Data.DataTable dt=_da.GetDataTable(sql);
			if ((int)dt.Rows[0][0]>0)
				return true;
			else
				return false;
		}




	}
}
