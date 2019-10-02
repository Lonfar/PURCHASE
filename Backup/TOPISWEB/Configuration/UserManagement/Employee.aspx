<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Page language="c#" Codebehind="Employee.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.UserManagement.Employee" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:STYLESKIN id="StyleSkin" runat="server"></TOPIS:STYLESKIN>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">	
		<TOPIS:PAGEDESCRIPTION id="PageDescription" runat="server"></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="1" cellpadding="1">
				<tr bgcolor="#C8C8D8" width = 100%>
					<td valign="top" align="left" width = "100%">						
						<msp:toolbar id="ToolBar1" runat="server">											
						</msp:toolbar>
					</td>
				</tr>
			</table>			
			<uc1:UCList id="VoucherList" runat="server"></uc1:UCList></form>
	</body>
</HTML>
