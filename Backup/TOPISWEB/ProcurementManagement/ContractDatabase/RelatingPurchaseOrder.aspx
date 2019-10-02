<%@ Page language="c#" Codebehind="RelatingPurchaseOrder.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.ProcurementManagement.ContractDatabase.RelatingPurchaseOrder" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RelatingPurchaseOrder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<FONT face="ËÎÌו">
			<FORM id="Form1" method="post" runat="server">
				<TABLE class="TopToolBarLine" id="Table1" cellSpacing="0" cellPadding="0" width="100%"
					border="0">
					<TR>
						<TD width="10"></TD>
						<TD></TD>
						</TD></TR>
				</TABLE>
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
					<TR vAlign="top">
						<TD>
							<P><uc1:uclist id="ucList_RelatingPurchaseOrder" runat="server"></uc1:uclist></P>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		</FONT>
	</body>
</HTML>
