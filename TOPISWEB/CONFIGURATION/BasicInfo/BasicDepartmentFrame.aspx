<%@ Page language="c#" Codebehind="BasicDepartmentFrame.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.BasicInfo.BasicDepartmentFrame" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>BasicDepartmentFrame</TITLE>
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
   	
		/*
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
		*/			
		</script>
	</HEAD>
		<frameset cols="160,*" framespacing="5" frameborder="1"  id="BasicTableFrame" onload =HideTree() >
			<frameset rows="24,*" onload="HideTree()"  framespacing="0" frameborder="0">	
				<frame src="BasicDepartmentHead.aspx" width=100% frameborder="0" scrolling=no marginheight=0 marginwidth=0>				
				<frame name="BasicDepartmentTree" src="BasicDepartmentTree.aspx" frameborder="0">
			</frameset>
			<frame name="BasicDepartmentTrees" src="BasicInfoDepartment.aspx" frameborder="0" bordercolor="#53A1CA">
		<noframes>
			This System must run in the explorer support frames
		</noframes>
		
	</frameset>
	
</HTML>
