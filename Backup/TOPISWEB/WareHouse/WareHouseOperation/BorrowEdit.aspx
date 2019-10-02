<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../../UserControls/ApproveStateInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Page language="c#" Codebehind="BorrowEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.WareHouseOperation.BorrowEdit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>BorrowMaterialEdit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="FlowLayout">
			<FORM id="Form2" method="post" runat="server">
				<TABLE class="TopToolBarLine" id="Table2" cellSpacing="0" cellPadding="0" width="100%"
					border="0">
					<TR>
						<TD width="10"></TD>
						<TD>
							<cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
					<TBODY>
						<TR vAlign="top">
							<TD>
								<P><FONT face="ËÎÌו"></FONT>&nbsp;</P>
							</TD>
						</TR>
						<TR vAlign="top">
							<TD style="HEIGHT: 55px">
		</FONT>
		<msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
			<msp:TabPage id="tabPage1">
				<TABLE id="Tbl1" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
					<TR>
						<TD align="right">
							<asp:Button id="btnSubmit" CausesValidation="False" Runat="server"></asp:Button>
							<asp:Button id="btnCancel" CausesValidation="False" Runat="server"></asp:Button></TD>
					</TR>
					<TR vAlign="top">
						<TD>
							<uc1:ucedit id="ucEdit_Borrow" runat="server"></uc1:ucedit></TD>
					</TR>
					<tr><td height="30"></td></tr>
					<TR>
						<TD align="center">
							<uc1:AttachmentManager id="AttachmentManager1" runat="server"></uc1:AttachmentManager></TD>
					</TR>
				</TABLE>
			</msp:TabPage>
			<msp:TabPage id="tabPage2">
				<TABLE id="Tbl3" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
				    <tr>
						<td align="right">
							<asp:Button ID="btnRefresh" Runat="server" CausesValidation="False"></asp:Button>
						</td>
					</tr>
					<TR vAlign="top">
						<TD>
							<uc1:ChildEditControl id="child_BorrowMaterial" runat="server"></uc1:ChildEditControl></TD>
					</TR>
				</TABLE>
			</msp:TabPage>
			<msp:TabPage id="tabPage3">
				<TABLE id="Tbl3" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
					<TR vAlign="top">
						<TD>
							<uc1:ApproveStateInfo id="ApproveStateInfo1" runat="server"></uc1:ApproveStateInfo></TD>
					</TR>
				</TABLE>
			</msp:TabPage>
		</msp:tabcontrol></TD></TR>
		<TR vAlign="top">
			<TD class="StatusLine" vAlign="middle">
				<asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
		</TR></TBODY></TABLE></FORM></FONT>
	</body>
</HTML>
