<%@ Page language="c#" Codebehind="Borrow.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.WareHouseOperation.Borrow" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Borrow</title>
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
		<FONT face="ËÎÌו">
			<FORM id="Form1" method="post" runat="server">
				<TABLE class="TopToolBarLine" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
					border="0">
					<TR>
						<TD width="10"></TD>
						<TD>
							<msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></TD></TD></TR>
				</TABLE>
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
					<TR vAlign="top">
						<TD>
							<P>
								<uc1:UCList id="ucList_Borrow" runat="server"></uc1:UCList></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle">
							<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label></TD>
					</TR>
				</TABLE>
				<P><FONT face="ËÎÌו"></FONT>&nbsp;</P>
			</FORM>
		</FONT>
	</body>
</HTML>
