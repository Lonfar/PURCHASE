<%@ Page language="c#" Codebehind="Head.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Head" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
	<Topis:StyleSkin runat="server" ID="StyleSkin1" />
	<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" Content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<script language="javascript">
		function ReloadAll(){			
			
			window.parent.frames("tree").location.reload();
			window.parent.frames("main").location=window.parent.frames("main").location;
		}
		function ShowHelp(){
				window.alert("Show help of :"+window.parent.frames("main").location);					
		}
		
		function RefreshTree(){
				window.parent.frames("tree").location.reload();		
		}
		function AlertLogout(msg){			
			if(window.confirm (msg)){
				top.location.href="Logout.aspx";
			}		
		}
		//**************************************************//
		//修改：马长阳
		//日期：2008-11-12
		//备注：客户端定时刷新
		//
		var t = 0;
		function setTime()
		{
		    var t = document.getElementById("hidValue").value;
		    setTimeout('getSubmit()',1000);
		}
		
		function getSubmit()
		{
		    
		    t = parseInt(t) + 1;
            setTimeout("getSubmit()",1000)
            if( t == 180 )
            {
		        document.getElementById("btnSub").click();
		    }
		}
		//**************************************************//
		</script>
	<style>
		#btnSetLanguage { WIDTH: 40px }
		</style>
	<script language="JavaScript" type="text/JavaScript">
<!--
function MM_reloadPage(init) {  //reloads the window if Nav4 resized
  if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
    document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
  else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
}
MM_reloadPage(true);
//-->
    </script>
	</HEAD>
<body style="MARGIN: 0px;" bgcolor="white" oncontextmenu="return false" id="thebody" onload="setTime()">
		
		<form id="Form1" method="post" runat="server" style="margin:0px" >
			<table width="100%" height="120" border="0" cellpadding="0" cellspacing="0" background="../Images/Page/top2.jpg">
              <tr>
                <td valign="top">
                
                <table align="right" width="50%" cellpadding="2" cellspacing="0" border="0"
													height="92px" style="font-size:12px; color:#FFFFFF ">
                  
                 <tr height="22px">
                    <td align="right" nowrap valign="middle" style="COLOR:white">                      
                      <a href="javascript:top.content.tree.HideTree();" name="btnHideTree" style="display:inline;text-DECORATION:none;border:0px solid #333333;background-color:transport;padding:1px;color:#DDDDDD"
							onmouseover="this.style.borderWidth='1px';this.style.backgroundColor='#7894AD';this.style.color='#FFFFFF'"
							onmouseout="this.style.borderWidth='0';this.style.backgroundColor='';this.style.color='#DDDDDD'" title='<%#GetString("litHideTree")%>'><img src="../Images/Page/hidemenu.gif" border="0" align="absmiddle" alt='<%#GetString("litHideTree")%>' title='<%#GetString("litHideTree")%>'/><%#GetString("litHideTree")%></a> 
					<a href="javascript:top.content.tree.ShowTree();" name="btnShowTree" style="display:none;text-DECORATION:none;border:0px solid #333333;background-color:transport;padding:1px;color:#DDDDDD"
							onmouseover="this.style.borderWidth='1px';this.style.backgroundColor='#7894AD';this.style.color='#FFFFFF'"
							onmouseout="this.style.borderWidth='0';this.style.backgroundColor='';this.style.color='#DDDDDD'" title='<%#GetString("litShowTree")%>'><img src="../Images/Page/showmenu.gif" border="0" align="absmiddle" alt='<%#GetString("litShowTree")%>' title='<%#GetString("litShowTree")%>'/><%#GetString("litShowTree")%></a> 
					<a href="javascript:ShowHelp()" style="CURSOR:help;text-DECORATION:none;display:none;border:0px solid #333333;background-color:transport;padding:1px;color:#DDDDDD"
							onmouseover="this.style.borderWidth='1px';this.style.backgroundColor='#7894AD';this.style.color='#FFFFFF'"
							onmouseout="this.style.borderWidth='0';this.style.backgroundColor='';this.style.color='#DDDDDD'" title='<%#GetString("litHelp")%>'><img src="../Images/Page/help.gif" border="0" align="absmiddle" alt='<%#GetString("litHelp")%>' title='<%#GetString("litHelp")%>' /><%#GetString("litHelp")%></a> 
					<a href='javascript:AlertLogout("<%#GetString("sLogoutAlert")%>")' style="text-DECORATION:none;border:0px solid #333333;background-color:transport;padding:1px;color:#DDDDDD"
							onmouseover="this.style.borderWidth='1px';this.style.backgroundColor='#7894AD';this.style.color='#FFFFFF'"
							onmouseout="this.style.borderWidth='0';this.style.backgroundColor='';this.style.color='#DDDDDD'" title='<%#GetString("litLogout")%>'><img src="../Images/Page/logout.gif" border="0" align="absmiddle" alt='<%#GetString("litLogout")%>' title='<%#GetString("litLogout")%>' /><%#GetString("litLogout")%> </a>&nbsp;&nbsp; </td>
                  </tr>
                  <tr height="70px" valign="bottom">
                    <td style="COLOR:WhiteSmoke" nowrap align="right"> <img src="../Images/Page/User.gif" border="0" align="absmiddle"> <b> <%=UserID%> </b>| <%=UserName%> | <%=System.Threading.Thread.CurrentThread.CurrentCulture.NativeName%> </td>
                  </tr>
                </table>
                  
                </table>
                </td>
              </tr>
            </table>
            <input type="submit" id="btnSub" style="display:none" />
            <input type="hidden" id="hidValue" value="0" />
	</form>
	</body>
</HTML>
