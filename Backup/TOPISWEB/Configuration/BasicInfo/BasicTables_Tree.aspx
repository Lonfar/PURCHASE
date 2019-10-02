<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="BasicTables_Tree.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BasicTables_Tree" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BasicTables_Tree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body   Class="Tree_Topline" oncontextmenu="return false">
		<form id="Form1" method="post" runat="server">
		<table border=1 cellpadding="0" cellspacing="0" width="100%" height="94%" Class="Tree_Area">
				<tr valign="top"><td>
			<iewc:TreeView id="trv" runat="server" ExpandedImageUrl="../../Images/Tree/Icon/folderopen.gif"
			DefaultStyle="font-size:11px;font-family:Arial;"
				SelectedStyle="Color:Black;background-color:#FDF4A2;" 
				HoverStyle="Color:White;background-color:#333333;"
				ImageUrl="../../Images/Tree/Icon/Item.gif"></iewc:TreeView>
				</td></tr>
		</table>
		</form>
	</body>
</HTML>
