<%@ Page language="c#" Codebehind="DocumentModelFrame.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.DocumentManagement.DocumentModelFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>DocumentModelFrame</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
		 function HideTree()
		{
			try{
    			top.content.tree.HideTree();
    			}catch(x){
    			}
		}
		</script>
  </head>
	<frameset cols="160,*" framespacing="5" frameborder="1" id="BasicTableFrame" bordercolor="#97acce" onload="HideTree()">	
		<frameset rows="24,*" onload="HideTree()" framespacing="0" frameborder="0">
			<frame src="DocumentModelHead.aspx" scrolling=no marginheight=0 marginwidth=0>
			<frame id="tree" name="DocumentModelTree" src="DocumentModelTree.aspx" frameborder="0" bordercolor="white">
		</frameset>
		<frame name="MainFrame" src="DocumentModel.aspx" frameborder="0"  bordercolor="#53A1CA">
		<noframes>
			This System must run in the explorer support frames
		</noframes>
	</frameset>	
</html>
