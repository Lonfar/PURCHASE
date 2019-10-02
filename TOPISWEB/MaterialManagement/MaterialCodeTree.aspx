<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="MaterialCodeTree.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialManagement.MaterialCodeTree" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MaterialCodeTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout"  Class="Tree_Topline">
		<form id="Form1" method="post" runat="server">
		<table border=1 cellpadding="0" cellspacing="0" width="100%" height="94%" Class="Tree_Area">
				<tr valign="top"><td>
			<iewc:treeview id="tvTables" runat="server" SystemImagesPath="../images/tree/" DefaultStyle="font-size:11px;font-family:Arial;"
				SelectedStyle="font-color:white;background-color:SteelBlue;" HoverStyle="font-color:white;background-color:Red;"
				EnableViewState="false" Target="BasicTables" ExpandedImageUrl="../Images/tree/icon/folderopen.gif"
				ImageUrl="../Images/tree/icon/folder.gif" ExpandLevel="1" SelectExpands="True" AutoSelect="True"
				ShowPlus="True"></iewc:treeview>
				</td></tr>
		</table>
		</form>
	</body>
</HTML>
