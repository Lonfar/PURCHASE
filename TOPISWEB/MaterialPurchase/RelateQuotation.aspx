<%@ Page language="c#" Codebehind="RelateQuotation.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialPurchase.RelateQuotation" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MaterialRequest</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD>
							<P><uc1:UCList id="ucList_Quotation" runat="server"></uc1:UCList></UC1:UCVOUCHERLIST></P>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
