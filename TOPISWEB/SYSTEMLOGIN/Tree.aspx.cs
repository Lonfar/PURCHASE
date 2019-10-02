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
	/// Tree 的摘要说明。
	/// </summary>
	public partial class Tree : PageBase
	{
		/// <summary>
		/// 系统目录树控件
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
		/// <param name="parentModuleID">要增加子节点的节点</param>
		/// <param name="tnParent"></param>
		/// <returns></returns>
		private void GetSubModulesAndBuildTree(string parentModuleID,TreeNode tnParent)
		{	
			//获取模块列表
			if(tblModule==null)
			{
				GetTreeModuleTable();
			}

			//DataTable tbl=tblModule.Clone();
			bool find = false;

			if(parentModuleID == null)
			{
				//如果父模块编号为空，为根节点
				foreach(DataRow dr in tblModule.Rows)
				{
					//取出对应的节点（父节点为空）
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
							//获得模块的本地化名称
							tnsub.Text=GetPublicString("Modules",dr["ModuleID"].ToString()) ;
							//如果没有对应的本地化名称，则使用数据库中默认的模块名
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
						//找到了最少一个模块
						find=true;
						if(!isLeafModule)
						{//如果不为叶节点，则处理该节点的子节点
							tvModule.Nodes.Add (tnsub);
							GetSubModulesAndBuildTree(moduleID,tnsub);
							if(tnsub.Nodes.Count==0)
							{//如果没有任何子节点，则删除此节点
								tvModule.Nodes.Remove(tnsub) ;
							}
						}
						else
						{
							if((bool)dr["IsPublicModule"])
							{//如果为公共模块，则显示不同的图标
								tnsub.ImageUrl="../Images/Tree/Icon/PinkItem.gif";
								tvModule.Nodes.Add (tnsub);

							}
							else if(!this.HasEntranceAuthority(dr))
							{
								//如果没有入口权限，则灰掉
								tnsub.ImageUrl="Images/Tree/Icon/Item_gray.gif";
								tnsub.NavigateUrl="";
								tnsub.DefaultStyle.Add("color","gray") ;
								//改动：不添加此模块
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
			{//处理同上
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
								//如果没有任何子节点，则删除此节点
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

		#region Web 窗体设计器生成的代码
		/// <summary>
		/// 自动生成
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
