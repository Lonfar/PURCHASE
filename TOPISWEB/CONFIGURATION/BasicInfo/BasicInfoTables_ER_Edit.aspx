<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Page language="c#" Codebehind="BasicInfoTables_ER_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BasicInfoTables_ER_Edit" %>
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
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<FORM id="Form1" method="post" runat="server">
			<TABLE Class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD style="HEIGHT: 60px">
							<P><uc1:ucedit id="VoucherEdit" runat="server"></uc1:ucedit></P>
							<asp:label id="lblMSG1" runat="server" Width="100%"></asp:label>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" valign="middle">
							<asp:label id="lblMSG" runat="server" Width="100%"></asp:label>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<P>&nbsp;</P>
		</FORM>
	</body>
</HTML>
