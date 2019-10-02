<%@ Page language="c#" Codebehind="RefFrame.aspx.cs" AutoEventWireup="True" Inherits="UserControls.clsRefFrame" %>
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
	<frameset rows="46,*" frameborder="1" framespacing="1" >
		<frame name="RefHead" src="RefHead.aspx?ModuleID=<%=Server.UrlEncode(Request.QueryString["ModuleID"])%>&TableCode=<%=Server.UrlEncode(Request.QueryString["TableCode"])%>&MultiSelect=<%=Request.QueryString["MultiSelect"]%>&Filter=<%=Server.UrlEncode(Request.QueryString["Filter"])%>&WhereSql=<%=Server.UrlEncode(Request.QueryString["WhereSql"])%>&OrderSql=<%=Server.UrlEncode(Request.QueryString["OrderSql"])%>" scrolling="no" noresize bordercolor="#999999"
			frameborder="0">
		<frameset cols="160,*" framespacing="4" frameborder="0" id = "RefMain">
			<frame name="RefTree" src="RefTree.aspx?ModuleID=<%=Server.UrlEncode(Request.QueryString["ModuleID"])%>&TableCode=<%=Server.UrlEncode(Request.QueryString["TableCode"])%>&MultiSelect=<%=Request.QueryString["MultiSelect"]%>&Filter=<%=Server.UrlEncode(Request.QueryString["Filter"])%>&WhereSql=<%=Server.UrlEncode(Request.QueryString["WhereSql"])%>&OrderSql=<%=Server.UrlEncode(Request.QueryString["OrderSql"])%>" frameborder="0" id = "RefTree">
			<frame name="RefContent" frameborder="0" bordercolor="#999999" src="RefList.aspx?ModuleID=<%=Server.UrlEncode(Request.QueryString["ModuleID"])%>&TableCode=<%=Server.UrlEncode(Request.QueryString["TableCode"])%>&MultiSelect=<%=Request.QueryString["MultiSelect"]%>&Filter=<%=Server.UrlEncode(Request.QueryString["Filter"])%>&WhereSql=<%=Server.UrlEncode(Request.QueryString["WhereSql"])%>&OrderSql=<%=Server.UrlEncode(Request.QueryString["OrderSql"])%>" id = "RefContent">
		</frameset>
		<noframes>
			This System must run in the explorer support frames
		</noframes>
	</frameset>
</HTML>
