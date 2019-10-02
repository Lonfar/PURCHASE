<%@ Page language="c#" Codebehind="PONoBidFlow_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialPurchase.PONoBidFlow_Edit" %>
<%@ Register TagPrefix="uc1" TagName="RefButton" Src="../UserControls/RefButton.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../UserControls/ApproveStateInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModuleViewer" Src="../UserControls/ModuleViewer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PONoBidFlow_Edit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../MyScripts/Tab.js"></SCRIPT>
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
				<TR vAlign="top">
					<TD>
						<P><FONT face="ËÎÌו"></FONT>&nbsp;</P>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 55px"><msp:tabcontrol id="TabControl1" runat="server" Width="800px" BackColor="#FF8080" BorderColor="Red">
							<msp:TabPage ID="tabPage1">
								<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
									<tr>
										<td align="left">
										</td>
										<td align="right"></td>
										<td align="right"></td>
										<td align="right"></td>
										<td align="right">
										    <uc1:RefButton id="RefButton1" runat="server"></uc1:RefButton>
											<asp:Button ID="btnApproved" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnSubmit" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnCancel" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnSign" Runat="server" CausesValidation="False"></asp:Button>
										</td>
									</tr>
									<TR vAlign="top">
										<TD style="HEIGHT: 20px" colSpan="5">
											<uc1:ucedit id="ucEdit_PONoBidFlow" runat="server"></uc1:ucedit>
										</TD>
									</TR>
									<TR vAlign="top">
										<TD style="HEIGHT: 20px" colSpan="5">
										</TD>
									</TR>									
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage2">
								<table id="Tbl11" cellspacing="1" cellpadding="1" width="100%" border="0" align="center">
								    <tr>
										<td align="right">
										    <asp:Button ID="btnCalclate" Runat="server"  CausesValidation="False"></asp:Button>
										</td>
								    </tr>
									<tr valign="top">
										<TD align="center" colspan="2">
											<uc1:ChildEditControl id="ChildEdit_MaterialList" runat="server"></uc1:ChildEditControl>
										</TD>
									</tr>
								</table>
							</msp:TabPage>
							<msp:TabPage ID="tabPage3">
								<table cellSpacing="1" cellPadding="1" width="100%" border="0" align="center" class="TitleArea">
									<TR>
										<TD class="TitleText1">
											<uc1:ApproveStateInfo id="ApproveStateInfo1" runat="server"></uc1:ApproveStateInfo>
										</TD>
									</TR>
								</table>
							</msp:TabPage>
							<msp:TabPage ID="tabPage4">
								<TABLE id="tblViewer" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
										<TR vAlign="top">
											<TD>
											<uc1:ModuleViewer id="ModuleViewer1" runat="server"></uc1:ModuleViewer></TD>
										</TR>
									</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage5">
								<TABLE id="Tbl6" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
								<tr>
										<td align="right">
											<asp:Button ID="btnSaveAttachment" Runat="server" CausesValidation="False"></asp:Button>
										</td>
									</tr>
									<TR vAlign="top">
										<TD>
											<uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
						</msp:tabcontrol></TD>
				</TR>
				<TR vAlign="top">
					<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label><asp:Literal id="lbError" runat="server"></asp:Literal></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
