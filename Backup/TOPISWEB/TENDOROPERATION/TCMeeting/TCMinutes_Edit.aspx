<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModuleViewer" Src="../../UserControls/ModuleViewer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Page language="c#" Codebehind="TCMinutes_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.TENDOROPERATION.TCMeeting.TCMinutes_Edit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TCMinutes_Edit</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/Tab.js"></SCRIPT>
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
						<TD style="HEIGHT: 55px"></FONT>
							<msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
								<msp:TabPage ID="tabPage1">
									<TABLE id="Tblucvoucheredit1" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
										<TR vAlign="top">
											<TD align="center">
												<uc1:ucedit runat="server" ID="ucEdit_TCMinutes"></uc1:ucedit>
											</TD>
										</TR>
									</TABLE>
								</msp:TabPage>
								<msp:TabPage id="tabPage2">
									<TABLE id="Tblucvoucheredit2" cellSpacing="1" cellPadding="1" width="100%" align="center"
										border="0">
										<TR vAlign="top">
											<TD>
												<uc1:UCList id="ucList_TCMinutesContent" runat="server"></uc1:UCList></TD>
										</TR>
									</TABLE>
								</msp:TabPage>
								<msp:TabPage id="tabPage3">
									<TABLE id="Tblucvoucheredit3" cellSpacing="1" cellPadding="1" width="100%" align="center"
										border="0">
										<TR vAlign="top">
											<TD>
												<uc1:ChildEditControl id="Child_TCMinutesMember" runat="server"></uc1:ChildEditControl></TD>
										</TR>
										<TR vAlign="top">
											<TD>
												<uc1:ChildEditControl id="Child_TCMinutesAttendee" runat="server"></uc1:ChildEditControl></TD>
										</TR>
										<TR vAlign="top">
											<TD>
												<uc1:ChildEditControl id="Child_TCMinutesParticipant" runat="server"></uc1:ChildEditControl></TD>
										</TR>
									</TABLE>
								</msp:TabPage>
								<msp:TabPage ID="tabPage4">
									<TABLE id="Tblucvoucheredit4" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
										<tr>
											<td align="center">
												<uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager>
											</td>
										</tr>
									</TABLE>
								</msp:TabPage>
							</msp:tabcontrol>
						</TD>
					</TR>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle">
							<asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
	</body>
</HTML>
