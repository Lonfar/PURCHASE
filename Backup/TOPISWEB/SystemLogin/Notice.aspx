<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Page language="c#" Codebehind="Notice.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Notice" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>BasicInfoSex</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href=../Styles/main.css type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
</HEAD>
	<body  MS_POSITIONING="FlowLayout">
		<form id="Form2" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" class="TopToolBarLine">
				<TR>
					<td width="10"></td>
					<TD><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></TD></TD></TR>
					
			</TABLE>
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
					<TBODY>
						<TR vAlign="top">
							<TD>
								<uc1:UCList id="ucList_Notice" runat="server"></uc1:UCList>
							</TD>
						</TR>
						<TR vAlign="top">
							<TD class="StatusLine" valign="middle">
								<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
							</TD>
						</TR>
					</TBODY>
				</TABLE>
			<P><FONT face=宋体></FONT>&nbsp;</P>
		</form>
	</body>
</HTML>
