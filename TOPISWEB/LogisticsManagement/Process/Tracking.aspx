<%@ Page language="c#" Codebehind="Tracking.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.LogisticsManagement.Process.Tracking" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SDT Tracking</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
	<table id="ToolBarTable" cellSpacing="0" cellPadding="0" width="100%" border="0" class="TopToolBarLine" >
	    <tr>
		    <td width="10"></td>
		    <td><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></td>
	    </tr>
	</table>	
	<table id="UCListTable" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
		<tr vAlign="top">
			<td>
				<P><uc1:UCList id="VoucherList" runat="server"></uc1:UCList></P>
			</td>
		</tr>
		<tr vAlign="top">
			<td class="StatusLine" valign="middle">
				<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
			</td>
		</tr>
	</table>
		</form>
	</body>
</HTML>