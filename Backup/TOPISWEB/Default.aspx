<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb._Default" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<Topis:StyleSkin runat="server" ID="Default" />
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">		
		if (top.location != self.location)
		{
			top.location = self.location;
		}
		
		function Click(){
			alert("Right click is forbidden!");
			window.event.returnValue = false;			
		}

		document.oncontextmenu=Click ;		
		function fnInit(){
			//window.resizeTo(window.screen.availWidth ,window.screen.availHeight);//maxsize the window 			
			//window.moveTo(0,0);			
		}			
		</script>
	</HEAD>
	<frameset rows="97,*" framespacing="0" frameborder="1" border="0" id="mainframeset" onload="fnInit()"
		name="mainframeset">
		<frame name="head" src="SystemLogin/Head.aspx" scrolling="no" noresize bordercolor="aliceblue" frameborder="0">
		<frameset cols="7,*" framespacing="0" frameborder="1" name="bottomframeset" id="bottomframeset"
			onload="fnInit()">
			<frame name="bar" src="SystemLogin/LeftBar.aspx" frameborder="0" id="bar" noresize>
			<frame name="content" src="SystemLogin/ContentFrame.aspx" frameborder="0" id="tree">
		</frameset>
		<noframes>
			This System must run in the explorer support frames
		</noframes>
	</frameset>
</HTML>
