<%@ Page language="c#" Codebehind="ITBDocumentWriteEdit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.TenderOperation.ITBDocumentWriteEdit" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../UserControls/ApproveStateInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Receive</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/DatePicker.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/DatePicker.js"></SCRIPT>
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
				<TBODY>
					<TR vAlign="top">
						<TD>
							<P><FONT face="宋体"></FONT>&nbsp;</P>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD style="HEIGHT: 55px"></FONT><msp:tabcontrol id="TabControl1" runat="server" Width="800px" BackColor="#FF8080" BorderColor="Red">
								<MSP:TABPAGE id="tabPage1">
									<TABLE id="Tblucvoucheredit" cellSpacing="1" cellPadding="1" width="100%" align="center"
										border="0">
										<TR vAlign="top">
											<TD align="center">
												<UC1:UCEDIT id="ucEdit_ITBDocument" runat="server"></UC1:UCEDIT>
											</TD>
										</TR>
										<TR>
											<TD height="25"></TD>
										</TR>
									</TABLE>
								</MSP:TABPAGE>
								<MSP:TABPAGE id="tabPage2">
									<TABLE id="Tblucvoucheredit" cellSpacing="2" cellPadding="2" width="99%" align="center"
										border="0">
										<TR vAlign="bottom" align="center">
											<TD align="center" width="95%" colSpan="2">
												<UC1:CHILDEDITCONTROL id="CEdit_ProposedBidder" runat="server"></UC1:CHILDEDITCONTROL>
											</TD>
										</TR>
									</TABLE>
								</MSP:TABPAGE>
								<MSP:TABPAGE id="tabPage3">
									<TABLE id="tb2" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
										<TR>
											<TD align="center">
												<UC1:ATTACHMENTMANAGER id="AttachmentManager1" runat="server"></UC1:ATTACHMENTMANAGER></TD>
										</TR>
									</TABLE>
								</MSP:TABPAGE>
							</msp:tabcontrol></TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
