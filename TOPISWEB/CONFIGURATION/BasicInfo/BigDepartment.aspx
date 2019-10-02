<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BigDepartment.aspx.cs" Inherits="TopisWeb.Configuration.BasicInfo.BigDepartment" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>AFE</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
</head>
<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" class="TopToolBarLine">
				<TR>
					<td width="10"></td>
					<TD><msp:toolbar id="Toolbar1" runat="server" DESIGNTIMEDRAGDROP="58"></msp:toolbar></TD>
					</TR>
			</TABLE>
			<TABLE cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD>
							<P><uc1:UCList id="VoucherList" runat="server"></uc1:UCList></P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" valign="middle">
							<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
			<P>&nbsp;</P>
		</form>
	</body>
</html>
