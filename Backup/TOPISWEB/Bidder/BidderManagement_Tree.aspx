<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="BidderManagement_Tree.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Bidder.BidderManagement_Tree" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BidderManagement_Tree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" Class="Tree_Topline">
		<form id="Form1" method="post" runat="server">
		<table border=1 cellpadding="0" cellspacing="0" width="100%" height="94%" Class="Tree_Area">
				<tr valign="top"><td>
			<iewc:TreeView id="TreeView1" runat="server" ExpandedImageUrl="..\IMAGES\Tree\Icon\folderopen.gif"
				SelectedStyle="font-color:white;background-color:SteelBlue;" HoverStyle="font-color:white;background-color:Red;"
				ImageUrl="..\IMAGES\Tree\Icon\Item.gif"></iewc:TreeView>
				</td></tr>
			</table>
		</form>
	</body>
</HTML>
