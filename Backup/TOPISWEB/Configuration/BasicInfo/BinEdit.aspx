<%@ Page language="c#" Codebehind="BinEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BinEdit" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BinEdit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
				<TABLE class="TopToolBarLine" id="Table0" cellSpacing="0" cellPadding="0" width="100%"
					border="0">
					<TR>
						<TD width="10"></TD>
						<TD>
							<cc1:ToolBar id="ToolBar1" runat="server"></cc1:ToolBar></TD>
					</TR>
				</TABLE>
				<P>
					<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
						<TR vAlign="top">
							<TD style="HEIGHT: 21px">
								<P>
									<uc1:UCEdit id="VoucherEdit" runat="server"></uc1:UCEdit></P>
							</TD>
						</TR>
						<TR vAlign="top">
							<TD class="StatusLine" vAlign="middle">
								<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label></TD>
						</TR>
					</TABLE>
				</P>
				<P>&nbsp;</P>
		</FONT></FORM></FONT>
	</body>
</HTML>
