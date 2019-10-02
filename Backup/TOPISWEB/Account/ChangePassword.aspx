<%@ Page language="c#" Codebehind="ChangePassword.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Account.ChangePassword" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body id="thebody" MS_POSITIONING="FlowLayout">
		<TOPIS:PAGEDESCRIPTION id="PageDescriptionPass" runat="server"></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<br>
			<TABLE class="TitleArea" cellPadding="0" width="95%" align="center" border="0">
				<TR>
					<TD class="TitleText1"><asp:literal 
      id=litChangePassword 
      Text='<%#GetString("litChangePassword")%>' 
  Runat="server"></asp:literal></TD>
				</TR>
			</TABLE>
			<table class="TableBlueBorderWhiteBg" style="MARGIN-TOP: 5px" width="80%" align="center" border="0">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*">
					<col align="left" width="100">
				</colgroup>
				<tr>
					<td class="Seperator" colSpan="3"></td>
				</tr>
				<tr>
					<td class="RequiredTitle"><asp:literal id=litUserID 
       Text='<%#GetString("litUserID")%>'  Runat="server"></asp:literal></td>
					<td>:&nbsp;<asp:textbox id="txtUserID" Runat="server" Width="90%" CssClass="ReadOnlyTextBox" ReadOnly="True"></asp:textbox></td>
					<td></td>
				</tr>
				<tr>
					<td class="RequiredTitle"><asp:literal 
      id=litOldPassword 
      Text='<%#GetString("litOldPassword")%>' Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtOldPassword" Runat="server" Width="100%" TextMode="Password"></asp:textbox></td>
					<td><asp:requiredfieldvalidator id=Requiredfieldvalidator1 runat="server" ControlToValidate="txtOldPassword" Display="Dynamic" ErrorMessage='<%#GetString("sOldPasswordRequired")%>'>
							<%#GetString("sRequiredField")%>
						</asp:requiredfieldvalidator></td>
				</tr>
				<tr>
					<td class="RequiredTitle"><asp:literal 
      id=litNewPassword 
      Text='<%#GetString("litNewPassword")%>' Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtPassword" Runat="server" Width="100%" TextMode="Password"></asp:textbox></td>
					<td><asp:requiredfieldvalidator id=RequiredFieldPassword runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage='<%#GetString("sNewPasswordRequired")%>'>
							<%#GetString("sRequiredField")%>
						</asp:requiredfieldvalidator><asp:regularexpressionvalidator id=RegularExpressionValidator1 runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage='<%#GetString("sNewPasswordLengthWrong")%>' ValidationExpression="\w{6,20}">
							<%#GetString("sNewPasswordLengthText")%>
						</asp:regularexpressionvalidator></td>
				</tr>
				<tr>
					<td class="RequiredTitle"><asp:literal id=litVerify 
       Text='<%#GetString("litVerify")%>' 
      Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtPassword2" Runat="server" Width="100%" TextMode="Password"></asp:textbox></td>
					<td><asp:requiredfieldvalidator id=RequiredFieldPass2 runat="server" ControlToValidate="txtPassword2" Display="Dynamic" ErrorMessage='<%#GetString("sVerifyPasswordRequired")%>'>
							<%#GetString("sRequiredField")%>
						</asp:requiredfieldvalidator><asp:comparevalidator id=CompareValidator1 runat="server" ControlToValidate="txtPassword2" Display="Dynamic" ErrorMessage='<%#GetString("sVerifyPasswordWrong")%>' ControlToCompare="txtPassword">
							<%#GetString("sVerifyPasswordWrongText")%>
						</asp:comparevalidator></td>
				</tr>
				<tr>
					<td class="TableTail" align="center" colSpan="3" height="30"><asp:validationsummary id="ValidationSummary1" Runat="server" DisplayMode="List" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
						<asp:LinkButton Runat="server" ID="btnChangePassword" Text='<%#GetString("btnChangePassword")%>' cssclass="graybutton">
						</asp:LinkButton></td>
				</tr>
			</table>
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal>
	</body>
</HTML>
