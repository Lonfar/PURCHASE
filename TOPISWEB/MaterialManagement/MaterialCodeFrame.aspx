<%@ Page language="c#" Codebehind="MaterialCodeFrame.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialManagement.MaterialCodeFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MaterialCodeFrame</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
			function HideTree()
			{
				try
				{
    				top.content.tree.HideTree();
    			}
    			catch(x)
    			{
    			}
			}
		</script>
	</HEAD>
	<frameset cols="160,*" framespacing="5" frameborder="1" id="BasicTableFrame" bordercolor="white"
		onload="HideTree()">
		<frameset rows="26,*" onload="HideTree()" framespacing="0" frameborder="0">
			<frame src="MaterialCodeHead.aspx" width="100%" frameborder="0" scrolling="no" marginheight="0"
				marginwidth="0">
			<frame name="MaterialCodeTree" src="MaterialCodeTree.aspx" frameborder="0">
		</frameset>
		<frame name="MaterialCode" src="MaterialCode.aspx" frameborder="0"  bordercolor="#53A1CA">
		<noframes>
			This System must run in the explorer support frames
		</noframes>
	</frameset>
</HTML>
