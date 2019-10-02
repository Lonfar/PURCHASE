<%@ Page language="c#" Codebehind="OvertimeLogin.aspx.cs" AutoEventWireup="false" Inherits="Topis.Web.OvertimeLogin" %>
<%@ Register TagPrefix="Topis" Namespace="Topis.Web.Controls" Assembly="Topis.Web" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<Topis:StyleSkin runat="server" ID="SystemLoginStyleSkin" />
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">				
		function lib_bwcheck(){
			this.ver=navigator.appVersion; 
			this.agent=navigator.userAgent
			this.dom=document.getElementById?1:0
			this.win = (navigator.appVersion.indexOf("Win")>0);
    		this.xwin = (navigator.appVersion.indexOf("X11")>0);
			this.ie5=(this.ver.indexOf("MSIE 5")>-1 && this.dom)?1:0;
			this.ie6=(this.ver.indexOf("MSIE 6")>-1 && this.dom)?1:0;
			this.ie4=(document.all && !this.dom)?1:0;
			this.ie=this.ie4||this.ie5||this.ie6
			this.mac=this.agent.indexOf("Mac")>-1
			this.opera5=this.agent.indexOf("Opera 5")>-1
			this.ns6=(this.dom && parseInt(this.ver) >= 5) ?1:0; 
			this.ns4=(document.layers && !this.dom)?1:0;
			this.bw=(this.ie6 || this.ie5 || this.ie4 || this.ns4 || this.ns6 || this.opera5 || this.dom||false);
    		this.width = null;
    		this.height = null;
			return this
		}
		var bw = new lib_bwcheck();
		function updateie(){
			window.alert("You must install Microsoft Internet Explorer 6.0 first !")
		}
		if(!bw.ie6){updateie();}
		</script>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<style type="text/css">
			BODY { MARGIN: 0px }
		</style>
	</HEAD>
	<body onload="document.getElementById('txtUserID').select();document.getElementById('txtUserID').focus()">
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="middle">
						<table width="780" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#000000">
							<tr>
								<td bgcolor="#345574">
									<table id="__01" width="780" border="0" cellpadding="0" cellspacing="0">
										<tr>
											<td height="485" valign="top" background="../images/page/Mainbg.jpg">
												<OBJECT codeBase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
													height="300" width="780" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" VIEWASTEXT>
													<PARAM NAME="_cx" VALUE="20638">
													<PARAM NAME="_cy" VALUE="7938">
													<PARAM NAME="FlashVars" VALUE="">
													<PARAM NAME="Movie" VALUE="../images/page/fla.swf">
													<PARAM NAME="Src" VALUE="../images/page/fla.swf">
													<PARAM NAME="WMode" VALUE="Transparent">
													<PARAM NAME="Play" VALUE="-1">
													<PARAM NAME="Loop" VALUE="-1">
													<PARAM NAME="Quality" VALUE="High">
													<PARAM NAME="SAlign" VALUE="">
													<PARAM NAME="Menu" VALUE="0">
													<PARAM NAME="Base" VALUE="">
													<PARAM NAME="AllowScriptAccess" VALUE="">
													<PARAM NAME="Scale" VALUE="ShowAll">
													<PARAM NAME="DeviceFont" VALUE="0">
													<PARAM NAME="EmbedMovie" VALUE="0">
													<PARAM NAME="BGColor" VALUE="">
													<PARAM NAME="SWRemote" VALUE="">
													<PARAM NAME="MovieData" VALUE="">
													<PARAM NAME="SeamlessTabbing" VALUE="1">
													<PARAM NAME="Profile" VALUE="0">
													<PARAM NAME="ProfileAddress" VALUE="">
													<PARAM NAME="ProfilePort" VALUE="0">
													<PARAM NAME="AllowNetworking" VALUE="all">
													<embed src="../images/fla.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
														type="application/x-shockwave-flash" width="780" height="300"> </embed>
												</OBJECT>
												<table border="0" align="center" style="BORDER-RIGHT:black 0px solid; BORDER-TOP:black 0px solid; FONT-SIZE:12px; BORDER-LEFT:black 0px solid; COLOR:#333333; BORDER-BOTTOM:black 0px solid">
													<tr>
														<td width="100" height="30" align="right">
															<asp:Literal Runat="server" ID="litSelectLanguage" Text='<%#GetString("litSelectLanguage")%>'>
															</asp:Literal>
														</td>
														<td align="left" width="280" style="PADDING-LEFT:5px">
															<asp:DropDownList id="ddlCultureList" runat="server" Width="230px" AutoPostBack="True" TabIndex="1"></asp:DropDownList>
														</td>
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
															&nbsp;
															<asp:ImageButton ImageAlign="AbsMiddle" ImageUrl="../Images/Page/Login.gif" BorderWidth="0" Runat="server"
																id="ImageButton1"></asp:ImageButton>
														</td>
													</tr>
												</table>
												<table align="center" width="450" height="40" border="0">
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
									</table>
								</td>
							</tr>
						</table>
						<asp:Literal id="litScript" Runat="server"></asp:Literal>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
