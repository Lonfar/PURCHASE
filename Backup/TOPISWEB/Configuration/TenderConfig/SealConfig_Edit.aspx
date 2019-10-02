<%@ Page language="c#" Codebehind="SealConfig_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.TenderConfig.SealConfig_Edit" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BasicInfoSex_Edit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD style="HEIGHT: 20px" colspan=2><uc1:ucedit id="VoucherEdit" runat="server"></uc1:ucedit></TD>
					</TR>
					<tr><td colspan=2 height="20"></td></tr>
					<tr>
						<td width="15%" align="right" >
							<asp:label id="lbChoose" runat="server" CssClass="FormNormalTitle"></asp:label>
						</td>
						<td width="85%">
							<asp:textbox id="txtChoose" runat="server" CssClass="SingleLineTextBox" Width="270px"></asp:textbox>
						</td>
					</tr>
					<tr>
						<td width="15%" align="right">
							<asp:label id="lblCurrencyNote" runat="server" CssClass="FormNormalTitle"></asp:label>
							</td>
						<td width="85%">
							<asp:textbox id="txtCurrency" runat="server" CssClass="SingleLineTextBox" ></asp:textbox></td>
					</tr>
					<tr><td colspan=2 height="20"></td></tr>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle" colSpan=2>
							<p><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></p>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<P><FONT face="ËÎÌו"></FONT>&nbsp;</P>
		</FORM>
	</body>
</HTML>
