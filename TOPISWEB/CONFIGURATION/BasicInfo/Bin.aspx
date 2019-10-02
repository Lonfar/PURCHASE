<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Page language="c#" Codebehind="Bin.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.Bin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Bin</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<script language="javascript">
		function refreshTree(binID){
			window.parent.frames("BinTree").location="BinTree.aspx?NewID=" + binID;
		}	
		</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<FONT face="ËÎÌו">
			<FORM id="Form1" method="post" runat="server">
				<TABLE class="TopToolBarLine" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
					border="0">
					<TR>
						<TD width="10"></TD>
						<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
						</TD></TR>
				</TABLE>
				<TABLE id="Table2" width="100%">
					<TR vAlign="top">
						<TD style="HEIGHT: 21px">
							<P><uc1:uclist id="VoucherList" runat="server"></uc1:uclist></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TABLE>
		</FONT></FORM></FONT>
	</body>
</HTML>
