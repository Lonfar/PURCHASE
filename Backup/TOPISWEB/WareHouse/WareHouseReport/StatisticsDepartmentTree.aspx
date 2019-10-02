<%@ Page language="c#" Codebehind="StatisticsDepartmentTree.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.WareHouseReport.StatisticsDepartmentTree" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register Src="../../UserControls/WebTreeView.ascx" TagName="WebTreeView" TagPrefix="uc1" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<HTML>
	<HEAD>
		<title>StatisticsDepartmentTree</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" Class="Tree_Topline">
		<form id="Form1" method="post" runat="server">
			<table border=1 cellpadding="0" cellspacing="0" width="100%" height="94%" Class="Tree_Area">
				<tr valign="top"><td>
                    <uc1:WebTreeView ID="WebTreeView1" runat="server" />
						
					</td></tr>
			</table>
		</form>
	</body>
</HTML>
