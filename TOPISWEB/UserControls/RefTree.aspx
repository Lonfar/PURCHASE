<%@ Page language="c#" Codebehind="RefTree.aspx.cs" AutoEventWireup="True" Inherits="UserControls.RefTree" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body id="thebody">
		<form id="Form1" method="post" runat="server">
			<iewc:treeview id="tvRef" runat="server" SystemImagesPath="../images/tree/" DefaultStyle="font-size:11px;font-family:Arial;"
				SelectedStyle="font-color:white;background-color:#003399;" HoverStyle="font-color:white;background-color:#003399;"
				ExpandedImageUrl="../Images/tree/icon/folderopen.jpg" ImageUrl="../Images/tree/icon/folder.gif"
				ExpandLevel="1" SelectExpands="True" AutoSelect="True" ShowPlus="True" AutoPostBack="False"
				height="100%" Target="RefContent"></iewc:treeview>
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal></FORM>
	</body>
</HTML>
