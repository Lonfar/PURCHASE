<%@ Page language="c#" Codebehind="RefFrameNoTree.aspx.cs" AutoEventWireup="True" Inherits="UserControls.clsRefFrameNoTree" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<base target="_parent">
		<meta http-equiv="Pragma" content="no-cache">
		<script language="javascript">
		function HideTree()
		{
    		top.content.tree.HideTree();
		}
		</script>
	</HEAD>
	<frameset rows="46,*" frameborder="1" framespacing="1">		 
		<frame name="RefHead" src="RefHead.aspx?ModuleID=<%=Server.UrlEncode(Request.QueryString["ModuleID"])%>&TableCode=<%=Server.UrlEncode(Request.QueryString["TableCode"])%>&MultiSelect=<%=Request.QueryString["MultiSelect"]%>&Filter=<%=Server.UrlEncode(Request.QueryString["Filter"])%>&WhereSql=<%=Server.UrlEncode(Request.QueryString["WhereSql"])%>&ShowClear=<%=Server.UrlEncode(Request.QueryString["ShowClear"])%>&AutoPostBack=<%=Server.UrlEncode(Request.QueryString["AutoPostBack"])%>" scrolling="no" noresize bordercolor="#999999"
			frameborder="0">				
		<frame name="RefContent" frameborder="0" bordercolor="#999999" src="RefList.aspx?ModuleID=<%=Server.UrlEncode(Request.QueryString["ModuleID"])%>&TableCode=<%=Server.UrlEncode(Request.QueryString["TableCode"])%>&MultiSelect=<%=Request.QueryString["MultiSelect"]%>&Filter=<%=Server.UrlEncode(Request.QueryString["Filter"])%>&WhereSql=<%=Server.UrlEncode(Request.QueryString["WhereSql"])%>&ShowClear=<%=Server.UrlEncode(Request.QueryString["ShowClear"])%>&AutoPostBack=<%=Server.UrlEncode(Request.QueryString["AutoPostBack"])%>" id="RefContent">		
		<noframes>
			This System must run in the explorer support frames
		</noframes>
	</frameset>
</HTML>