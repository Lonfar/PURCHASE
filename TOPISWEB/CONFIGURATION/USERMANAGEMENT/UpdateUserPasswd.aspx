<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Page language="c#" Codebehind="UpdateUserPasswd.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.UpdateUserPasswd" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="StyleSkinCurrency" runat="server"></TOPIS:StyleSkin>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../styles/Tabcontrol/Default.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">
		<TOPIS:PAGEDESCRIPTION id="PageDescriptionCurrency" runat="server"></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
		<br /><br />
	    <TABLE class="TabControlBackgroundColor" cellPadding="0"  cellspacing="0" width="95%" height="50" align="center" border="0">
				<TR>
					<TD>
					
			<TABLE class="TitleArea" cellPadding="0" width="95%" height="50" align="center" border="0">
				<TR>
					<TD class="TitleText1">
						<asp:Literal Runat="server" ID="litChangePassword" Text='<%#GetString("litChangePassword")%>'>
						</asp:Literal>
					</TD>
				</TR>
			</TABLE>
			<TABLE width="95%" align="center" border="0" class="TableBlueBorderWhiteBg" style="MARGIN-BOTTOM:3px"
				cellpadding="2">
				<colgroup>
					<COL align="right" width="200">
					<col width="*" align="left">
					<col width="150" align="left">
				</colgroup>
				<tr>
					<td>
						<asp:literal id="litDepartment" Text='<%#GetString("litDepartment")%>' Runat="server">
						</asp:literal>
					</td>
					<td>
						<asp:DropDownList id="ddlDepartmentList" runat="server" Width="100%" AutoPostBack="True" onselectedindexchanged="ddlDepartmentList_SelectedIndexChanged"></asp:DropDownList></td>
					<td><FONT face="ËÎÌו"></FONT></td>
				</tr>
				<tr>
					<td>
						<asp:literal id=litUserID Text='<%#GetString("litUserID")%>' Runat="server">
						</asp:literal></td>
					<td>
						<asp:DropDownList id="ddlUserList" runat="server" Width="100%"></asp:DropDownList>
					</td>
					<td>
					</td>
				</tr>
				<tr>
					<td class="RequiredTitle">
						<asp:literal id="litPassword" Text='<%#GetString("litPassword")%>' Runat="server">
						</asp:literal>
					</td>
					<td><asp:textbox id="txtPassword" Runat="server" Width="100%" TextMode="Password"></asp:textbox></td>
					<td>
						<asp:RequiredFieldValidator id="RequiredFieldPassword" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>'  Display="Dynamic"
							ControlToValidate="txtPassword">
						</asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="6-20 bit" ControlToValidate="txtPassword"
							ValidationExpression="\w{6,20}" Display="Dynamic"></asp:RegularExpressionValidator>
					</td>
				</tr>
				<tr>
					<td class="RequiredTitle">
						<asp:literal id="litPassword2" Text='<%#GetString("litPassword2")%>' Runat="server">
						</asp:literal>
					</td>
					<td><asp:textbox id="txtPassword2" Runat="server" Width="100%" TextMode="Password"></asp:textbox></td>
					<td>
						<asp:RequiredFieldValidator id="RequiredFieldPass2" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>'  Display="Dynamic"
							ControlToValidate="txtPassword2">
						</asp:RequiredFieldValidator>
						<asp:CompareValidator id="CompareValidator1" runat="server" ErrorMessage="Wrong" Display="Dynamic" ControlToValidate="txtPassword2"
							ControlToCompare="txtPassword"></asp:CompareValidator>
					</td>
				</tr>
				<tr>
					<TD class="TableTail" colSpan="3" align="center" height="50">
						<asp:LinkButton Runat="server" ID="btnChangePassword" Text='<%#GetString("btnChangePassword")%>' cssclass="graybutton" onclick="btnChangePassword_Click">
						</asp:LinkButton>
					</TD>
				</tr>
			</TABLE>
			
			        </TD></TR>
		</TABLE>
	    <br />
			
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" border="0" align="center">
				<TBODY>
					<TR vAlign="top">
						<TD class="StatusLine" valign="middle">
							<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal>
	</body>
</HTML>
