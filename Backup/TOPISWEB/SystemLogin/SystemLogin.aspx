<%@ Page language="c#" Codebehind="SystemLogin.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.SystemLogin" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<Topis:StyleSkin runat="server" ID="SystemLoginStyleSkin" />
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">	
		  <!-- 			
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
			function queryString(sParam){
			var sBase = window.location.search
			var re = eval("/" + sParam + "=([^&]*)/")
			if (re.test(sBase)){
			return RegExp.$1
			}
			else{
			return null
				}
			}
			 self.moveTo(0,0)       
			 self.resizeTo(screen.availWidth,screen.availHeight)   
 
  //-->   
		</script>
		<meta http-equiv="Content-Type"  content="text/html; charset=iso-8859-1">
		<style type="text/css">
			BODY { BACKGROUND-IMAGE: url(../images1/page/bodybg.gif); MARGIN: 0px; BACKGROUND-COLOR: #6b9cc5 }
		</style>
	</HEAD>
	<body onload="document.getElementById('txtUserID').select();document.getElementById('txtUserID').focus()" >
		<form id="Form1" method="post" runat="server">
			<table width="100%" height="100%" border="1" align="center" cellpadding="0" cellspacing="1"
				bgcolor="#000000">
				<tr>
					<td bgcolor="#408db8" align="center">
						<table id="__01" width="1024" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td height="689" valign="top" background="../images/page/Mainbg.jpg" style="BORDER-RIGHT:#50b8de 1px solid; BORDER-TOP:#50b8de 1px solid; BORDER-LEFT:#50b8de 1px solid; BORDER-BOTTOM:#50b8de 1px solid">
									<table border="0" align="center">
										<tr>
											<td height="400"></td>
											<td></td>
										</tr>
										<tr>
											<td width="100" height="30" align="right">
												<b>
													<asp:Literal Runat="server" ID="litSelectLanguage" Text='<%#GetString("litSelectLanguage")%>'>
													</asp:Literal></b>
											</td>
											<td align="left" width="280" style="PADDING-LEFT:5px">
												<asp:DropDownList id="ddlCultureList" runat="server" Width="200px" AutoPostBack="True" TabIndex="1" onselectedindexchanged="ddlCultureList_SelectedIndexChanged"></asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td height="30" align="right">
												<b>
													<asp:Literal Runat="server" ID="litUserID" Text='<%#GetString("litUserID")%>'>
													</asp:Literal></b>
											</td>
											<td align="left" style="PADDING-LEFT:5px">
												<asp:TextBox Runat="server" ID="txtUserID" Width="200px" BorderColor="black" BorderStyle="Solid"
													TabIndex="2" ForeColor="blue"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td height="30" align="right">
												<b>
													<asp:Literal Runat="server" ID="litPassword" Text='<%#GetString("litPassword")%>'>
													</asp:Literal></b>
											</td>
											<td align="left" style="PADDING-LEFT:5px"><asp:TextBox Runat="server" ID="txtPasswd" Width="200px" BorderColor="black" TextMode="Password"
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
							<tr>
								<td valign="bottom">
									<asp:Literal Runat="server" ID="litScript"></asp:Literal>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
