<%@ Page language="c#" Codebehind="BasicTables_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BasicTables_Edit" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BasicTables_Edit</title>
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
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="TopToolBarLine">
						<TR>
							<td width="10"></td>
							<TD><cc1:ToolBar id="ToolBar1" runat="server"></cc1:ToolBar></TD>
							</TD></TR>
					</TABLE>
					<TABLE id="Table2" width="100%">
						<TBODY>
							<TR vAlign="top">
								<TD style="HEIGHT: 21px">
									<P>
										<uc1:UCEdit id="VoucherEdit" runat="server"></uc1:UCEdit></P>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD class="StatusLine" valign="middle">
									<asp:Label ID="lblMSG1" Runat="server"></asp:Label>
									<asp:Label id="lblMSG" runat="server" Width="90%"></asp:Label>
								</TD>
							</TR>
						</TBODY>
					</TABLE>
					<P></P>
						</form>
	</body>
</HTML>
