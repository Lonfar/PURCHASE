<%@ Page language="c#" Codebehind="ReLogin.aspx.cs" AutoEventWireup="false" Inherits="Topis.Web.ReLogin" %>
<%@ Register TagPrefix="Topis" Namespace="Topis.Web.Controls" Assembly="Topis.Web" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<Topis:StyleSkin runat="server" ID="SystemLoginStyleSkin" />
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body onload="document.getElementById('txtUserID').select();document.getElementById('txtUserID').focus()">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td height="101" colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2" valign="middle"><table width="100%" border="0" cellpadding="0" cellspacing="0" background="">
							<tr>
							</tr>
							<tr>
								<td height="390" valign="top">
									<table border="0" align="center" bgcolor="#94aece" style="BORDER-RIGHT:black 0px solid; BORDER-TOP:black 0px solid; FONT-SIZE:12px; BORDER-LEFT:black 0px solid; COLOR:#ffffff; BORDER-BOTTOM:black 0px solid">
										<tr>
											<td width="100" height="30" align="right">
												<asp:Literal Runat="server" ID="litSelectLanguage" Text='<%#GetString("litSelectLanguage")%>'>
												</asp:Literal>
											</td>
											<td align="left" width="280" style="PADDING-LEFT:5px">
												<asp:DropDownList id="ddlCultureList" runat="server" Width="230px" AutoPostBack="True" TabIndex="1"></asp:DropDownList></td>
										</tr>
										<tr>
											<td height="30" align="right">
												<asp:Literal Runat="server" ID="litUserID" Text='<%#GetString("litUserID")%>'>
												</asp:Literal>
											</td>
											<td align="left" style="PADDING-LEFT:5px">
												<asp:TextBox Runat="server" ID="txtUserID" Width="230px" BorderColor="black" BorderStyle="Solid"
													TabIndex="2" ForeColor="blue"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td height="30" align="right"><asp:Literal Runat="server" ID="litPassword" Text='<%#GetString("litPassword")%>'>
												</asp:Literal>
											</td>
											<td align="left" style="PADDING-LEFT:5px"><asp:TextBox Runat="server" ID="txtPasswd" Width="230px" BorderColor="black" TextMode="Password"
													BorderStyle="Solid" TabIndex="3" ForeColor="blue"></asp:TextBox>
												&nbsp;&nbsp;
												<asp:ImageButton ImageAlign="AbsMiddle" ImageUrl="../Images/Page/Login.gif" BorderWidth="0" Runat="server"
													id="ImageButton1"></asp:ImageButton></td>
										</tr>
									</table>
									<table align="center" width="450" height="40">
										<tr>
											<td valign="middle" align="center">
												<asp:Label Runat="server" ID="lblErrorMessage" ForeColor="red"></asp:Label>
												<asp:RequiredFieldValidator id="RequiredFieldUserID" runat="server" Display="None" ControlToValidate="txtUserID" ErrorMessage='<%#GetString("UserIDRequired")%>'>
												</asp:RequiredFieldValidator>
												<asp:RequiredFieldValidator id="RequiredFieldPassword" runat="server" Display="None" ControlToValidate="txtPasswd" ErrorMessage='<%#GetString("PasswdRequired")%>'>
												</asp:RequiredFieldValidator>
												<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td width="550" height="101"><asp:Literal Runat="server" ID="litScript"></asp:Literal></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
