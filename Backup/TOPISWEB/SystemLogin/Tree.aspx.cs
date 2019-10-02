/*
------------------------------------------------------------------------------------------------------------------------
-- Create by: Dou Zhi-Cheng
-- Version List
--  Version 1.0 2005.7.1
------------------------------------------------------------------------------------------------------------------------
-- Update List
--    2005.6.7 Dou Zhi-Cheng alt the TreeNodeSrc, add new fill tree method
--    2005.7.5 Dou Zhi-cheng optimize using
*/

using System;
using System.Data;
using System.Web.UI;
using Microsoft.Web.UI.WebControls;
using Business.SystemConfig;
using Business.TopisSystem;

namespace TopisWeb
{
	/// <summary>
	/// Tree ��ժҪ˵����
	/// </summary>
	public partial class Tree : PageBase
	{
		/// <summary>
		/// ϵͳĿ¼���ؼ�
		/// </summary>
		protected TreeView tvModule;

		/// <summary>
		/// DataTable to store the module list
		/// </summary>
		DataTable tblModule=null;
	
		private void GetModuleTable()
		{
			CDAModule dam=new CDAModule ();
			tblModule=dam.SelectModulesForTree ();
		}

		/// <summary>
		/// load module list from database
		/// </summary>
		private void GetTreeModuleTable()
		{
			CDAModule dam=new CDAModule ();
			tblModule=dam.SelectTreeModules ();
		}

		/// <summary>
		/// get the sub modules DataTable in the module and fill the tree
		/// </summary>
		/// <param name="parentModuleID">Ҫ�����ӽڵ�Ľڵ�</param>
		/// <param name="tnParent"></param>
		/// <returns></returns>
		private void GetSubModulesAndBuildTree(string parentModuleID,TreeNode tnParent)
		{	
			//��ȡģ���б�
			if(tblModule==null)
			{
				GetTreeModuleTable();
			}

			//DataTable tbl=tblModule.Clone();
			bool find = false;

			if(parentModuleID == null)
			{
				//�����ģ����Ϊ�գ�Ϊ���ڵ�
				foreach(DataRow dr in tblModule.Rows)
				{
					//ȡ����Ӧ�Ľڵ㣨���ڵ�Ϊ�գ�
					if(dr["ParentModuleID"]==DBNull.Value )
					{	
						//tbl.Rows.Add (dr.ItemArray );
						TreeNode tnsub = new TreeNode ( );
						string moduleID=(string)dr["ModuleID"];
						bool isLeafModule=(bool)dr["IsLeafModule"];
						tnsub.NodeData=moduleID;
						//tnsub.Text=dr["ModuleName"].ToString ();
						try
						{
							//���ģ��ı��ػ�����
							tnsub.Text=GetPublicString("Modules",dr["ModuleID"].ToString()) ;
							//���û�ж�Ӧ�ı��ػ����ƣ���ʹ�����ݿ���Ĭ�ϵ�ģ����
							if(tnsub.Text==String.Empty )
							{
								tnsub.Text=dr["ModuleName"].ToString ();
							}
						}
						catch
						{
							tnsub.Text=dr["ModuleName"].ToString ();
						}
						if(dr["ModuleUrl"].ToString ().ToLower() != "desktop.aspx" && dr["ModuleUrl"].ToString () != "")
						{
							tnsub.NavigateUrl="../" + dr["ModuleUrl"].ToString ();	
                            //tnsub.NavigateUrl = "loadingpage.aspx?ModuleUrl=../" + Server.UrlEncode(dr["ModuleUrl"].ToString());

						}
						else
						{
							 tnsub.NavigateUrl=dr["ModuleUrl"].ToString ();
                            //tnsub.NavigateUrl = "loadingpage.aspx?ModuleUrl=" + Server.UrlEncode(dr["ModuleUrl"].ToString());

						}
						tnsub.Expanded = true;
						//tvModule.Nodes.Add (tnsub);
						
						//delete the node from the datatable,reduce the search cost
						//tblModule.Rows.Remove(dr);
						//�ҵ�������һ��ģ��
						find=true;
						if(!isLeafModule)
						{//�����ΪҶ�ڵ㣬����ýڵ���ӽڵ�
							tvModule.Nodes.Add (tnsub);
							GetSubModulesAndBuildTree(moduleID,tnsub);
							if(tnsub.Nodes.Count==0)
							{//���û���κ��ӽڵ㣬��ɾ���˽ڵ�
								tvModule.Nodes.Remove(tnsub) ;
							}
						}
						else
						{
							if((bool)dr["IsPublicModule"])
							{//���Ϊ����ģ�飬����ʾ��ͬ��ͼ��
								tnsub.ImageUrl="../Images/Tree/Icon/PinkItem.gif";
								tvModule.Nodes.Add (tnsub);

							}
							else if(!this.HasEntranceAuthority(dr))
							{
								//���û�����Ȩ�ޣ���ҵ�
								tnsub.ImageUrl="Images/Tree/Icon/Item_gray.gif";
								tnsub.NavigateUrl="";
								tnsub.DefaultStyle.Add("color","gray") ;
								//�Ķ�������Ӵ�ģ��
								//tvModule.Nodes.Add (tnsub);
								
							}
							else
							{
								tnsub.ImageUrl="../Images/Tree/Icon/Item.gif";
								tvModule.Nodes.Add (tnsub);
							}
						}
					}
					else if(find)
					{
						break;
					}
				}
			}
			else
			{//����ͬ��
				foreach(DataRow dr in tblModule.Rows)
				{
					if(dr["ParentModuleID"].ToString ().ToLower() == parentModuleID.ToLower())
					{
						//tbl.Rows.Add (dr.ItemArray );
						TreeNode tnsub = new TreeNode ( );
						string moduleID=(string)dr["ModuleID"];
						bool isLeafModule=(bool)dr["IsLeafModule"];
						tnsub.NodeData=moduleID;
						//tnsub.Text=dr["ModuleName"].ToString ();
						try
						{
							tnsub.Text=GetPublicString("Modules",dr["ModuleID"].ToString()) ;
							if(tnsub.Text==String.Empty )
							{
								tnsub.Text=dr["ModuleName"].ToString ();
							}

						}
						catch
						{
							tnsub.Text=dr["ModuleName"].ToString ();
						}
						if(dr["ModuleUrl"].ToString ().ToLower() != "desktop.aspx" && dr["ModuleUrl"].ToString () != "")
						{
							//tnsub.NavigateUrl="../" + dr["ModuleUrl"].ToString ();	
                            tnsub.NavigateUrl = "loadingpage.aspx?ModuleUrl=../" + Server.UrlEncode(dr["ModuleUrl"].ToString());
						}
						else
						{
							tnsub.NavigateUrl=dr["ModuleUrl"].ToString ();
                            //tnsub.NavigateUrl = "loadingpage.aspx?ModuleUrl=" + Server.UrlEncode(dr["ModuleUrl"].ToString());
						}
						//tnParent.Nodes.Add (tnsub);
						

						//delete the node from the datatable,reduce the search cost
						//tblModule.Rows.Remove(dr);
						find=true;
						if(!isLeafModule)
						{
							tnParent.Nodes.Add (tnsub);
							GetSubModulesAndBuildTree(moduleID,tnsub);
							if(tnsub.Nodes.Count==0)
							{
								//���û���κ��ӽڵ㣬��ɾ���˽ڵ�
								tnParent.Nodes.Remove(tnsub) ;
							}
						}
						else
						{
							if(moduleID=="Topis.Desktop")
							{
								tnsub.ImageUrl="../Images/Tree/Icon/desktop.gif";
								tnParent.Nodes.Add (tnsub);
							}
							else
							{
								if(dr["EntranceAuthority"]==DBNull.Value)
								{
									tnsub.ImageUrl="../Images/Tree/Icon/PinkItem.gif";
									tnParent.Nodes.Add (tnsub);
								}
									
								else if(!this.HasEntranceAuthority(dr))
								{
									tnsub.ImageUrl="Images/Tree/Icon/Item_gray.gif";
									tnsub.NavigateUrl="";
									tnsub.DefaultStyle.Add("color","gray") ;
									//tnParent.Nodes.Add (tnsub);
								}
								else
								{
									tnsub.ImageUrl="../Images/Tree/Icon/Item.gif";
									tnParent.Nodes.Add (tnsub);
								}
							}

						}
					}
					else if(find)
					{
						break;
					}
				}
			}
			//return tbl;			
		}
		
		/// <summary>
		/// function to test whether current user has the view authority to module
		/// </summary>
		/// <param name="pmoduleID"></param>
		/// <returns></returns>
//		private bool HasEntranceAuthority(string pmoduleID,string auth)
//		{
//			//if the currentuser doesnot exist,return false
//			if(CurrentUser==null)
//			{
//				return false;
//			}
//			DataTable tblauth=null;
//			if(Session["AllAuthoritiesOfUser"]==null)
//			{
//				DataAccess.SystemConfig.CDAUserRoleAuthority dau=new CDAUserRoleAuthority() ;
//
//				tblauth= dau.SelectAuthoritiesTableByUserID(CurrentUser.UserID) ;
//				tblauth.PrimaryKey=new DataColumn[]{tblauth.Columns["AuthorityID"]};
//
//				Session["AllAuthoritiesOfUser"]=tblauth;
//			}
//			else
//			{
//				tblauth=(DataTable)Session["AllAuthoritiesOfUser"];
//			}
//			//			//get from the database
//			//			CDAUserRoleAuthority daua=new CDAUserRoleAuthority ();
//			//			string authorityID = "Topis."+_moduleID+"."+authorityType;
//			//			return daua.CheckUserAuthority(CurrentUser.UserID,authorityID);			
//			if(tblauth.Rows.Find(pmoduleID+"."+auth) !=null)
//			{
//				return true;
//			}
//			else
//			{
//				return false;
//			}
//		}
		protected void Page_Load(object sender, EventArgs e)
		{
//			ModuleID="Default";			
			base.PageName = "SystemLogin.Tree" ;
			if(!Page.IsPostBack )
			{				

//				litTopis.Text = Topis.Web.Globalization.GlobalizationConfiguration.GetTreeModuleName("Topis") ;
				litTopis.Text="TOPIS";

				GetSubModulesAndBuildTree(null,null);
				/*
				//Delete the code on 2005.6.7 by douzhicheng
				try
				{
					
					if(System.IO.File.Exists(Server.MapPath("MenuTree."+System.Threading.Thread.CurrentThread.CurrentCulture.Name+".xml")))
					{

						this.tvDoc.TreeNodeSrc="MenuTree."+System.Threading.Thread.CurrentThread.CurrentCulture.Name+".xml";
					}
					else
					{						
						this.tvDoc.TreeNodeSrc="MenuTree.xml";
						
					}
					this.tvDoc.DataBind ();
				}
				catch(Exception exp)
				{
					Response.Write ("Unable to get the menu tree!" +exp.Message );
				}
				*/
			}
		}

		#region Web ������������ɵĴ���
		/// <summary>
		/// �Զ�����
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
