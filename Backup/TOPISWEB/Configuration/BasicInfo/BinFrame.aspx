<%@ Page language="c#" Codebehind="BinFrame.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BinFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BinFrame</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<script language="javascript">
		 function HideTree()
		{
			try{
    			top.content.tree.HideTree();
    			}catch(x){
    			}
		}
		</script>
</HEAD>
	<frameset cols="160,*" framespacing="5" frameborder="1" name="BinFrame" id="BinFrame"	bordercolor="#97acce" onload="HideTree()">	
		<frameset rows="24,*"   onload="HideTree()" framespacing="0" frameborder="0">	
			<frame src="BinHead.aspx" width=100% frameborder="0" scrolling=no marginheight=0 marginwidth=0>	
			<frame name="BinTree" src="BinTree.aspx" frameborder="0" id="tree" bordercolor="white">		
							
		</frameset>
		<frame name="MainFrame" src="Bin.aspx" frameborder="0"  bordercolor="#53A1CA">
		<noframes>
			This System must run in the explorer support frames
		</noframes>
	</frameset>
</HTML>