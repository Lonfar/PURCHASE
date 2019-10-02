<%@ Page language="c#" Codebehind="CheckStockEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.Inventory.CheckStockEdit" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEditSearch" Src="../../UserControls/UCEditSearch.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CheckStockEdit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE Class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TR vAlign="top">
					<TD>
						<msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
							<msp:TabPage ID="tabPage1">
								<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD align="center">
											<uc1:ucedit id="VoucherEdit" runat="server"></uc1:ucedit>
										</TD>
									</TR>
									<tr>
										<td height="30"></td>
									</tr>
									<TR vAlign="top">
										<TD align="center">
											<uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage2">
								<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD align="right">
											<asp:Button ID="btnExport" Runat="server"></asp:Button>
										</TD>
									</TR>
									<TR vAlign="top">
										<TD align="center">
											<uc2:uclist id="ucInventoryDetails" runat="server"></uc2:uclist>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
						</msp:tabcontrol>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD class="StatusLine" valign="middle">
						<asp:label id="lblMSG" runat="server" Width="100%"></asp:label>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
