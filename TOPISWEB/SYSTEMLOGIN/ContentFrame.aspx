<%@ Page language="c#" Codebehind="ContentFrame.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.ContentFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>ContentFrame</TITLE>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<frameset cols="160,*" framespacing="5" frameborder="1" name="contentframe" id="contentframe"
		 bordercolor="#53A1CA">
		<frame name="tree" src="Tree.aspx" frameborder="0" id="tree">
		<frame name="main" src="Desktop.aspx" frameborder="0"  >
		<noframes>
			This System must run in the explorer support frames
		</noframes>
	</frameset>
</HTML>
