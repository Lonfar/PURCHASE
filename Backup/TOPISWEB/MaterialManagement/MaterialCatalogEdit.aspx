<%@ Page language="c#" Codebehind="MaterialCatalogEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialManagement.MaterialCatalogEdit" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MaterialCatalogEdit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD>
							<P><uc1:ucedit id="ucEdit_Catalog" runat="server"></uc1:ucedit></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
