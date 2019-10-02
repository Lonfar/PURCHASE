<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>

<%@ Page Language="c#" Codebehind="Tree.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Tree" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <Topis:StyleSkin runat="server" ID="Tree" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Styles/main.CSS" type="text/css" rel="stylesheet">

    <script language="javascript">
		var strColumns_Current = "160,*";
		var hide=0;
		function ShowTree()
		{	
			if(hide==1)
			{
				parent.document.getElementById("contentframe").cols = strColumns_Current;	
				parent.document.getElementById("contentframe").frameSpacing="4";
				top.frames("head").document.getElementById("btnHideTree").style.display = "inline";
				top.frames("head").document.getElementById("btnShowTree").style.display = "none";
				hide=0;
			}
			
		}
		function HideTree(){			
			if(hide==0)
			{
				strColumns_Current = parent.document.getElementById("contentframe").cols;		
				parent.document.getElementById("contentframe").cols = "0,*";
				parent.document.getElementById("contentframe").frameSpacing="0";
				top.frames("head").document.getElementById("btnHideTree").style.display = "none";
				top.frames("head").document.getElementById("btnShowTree").style.display = "inline";
				hide=1;
			}
		}	
		function ShowOrHideTree(){
			if(hide==1)
			{
				parent.document.getElementById("contentframe").cols = strColumns_Current;	
				parent.document.getElementById("contentframe").frameSpacing="4";
				top.frames("head").document.getElementById("btnHideTree").style.display = "inline";
				top.frames("head").document.getElementById("btnShowTree").style.display = "none";
				hide=0;
			}else if(hide==0)
			{
				strColumns_Current = parent.document.getElementById("contentframe").cols;		
				parent.document.getElementById("contentframe").cols = "0,*";
				parent.document.getElementById("contentframe").frameSpacing="0";
				top.frames("head").document.getElementById("btnHideTree").style.display = "none";
				top.frames("head").document.getElementById("btnShowTree").style.display = "inline";
				hide=1;
			}
		}	
		function mouseover(item)
		{
			item.style.borderColor = "#003399";
			item.style.background = "#CCCCFF";
		}
		function mouseout(item)
		{
			item.style.borderColor = "#CCCCCC";
			item.style.background = "#CCCCCC";
		}
		function refreshtree()
		{
			window.location.reload (true);//浠庢湇鍔″櫒涓嬭浇鏂伴〉闈?		
		}
		
		function mouseover1()
		{
			window.status = "hide the menu";
			document.getElementById("imgHideToc").src = "../Images/Page/hidetoc2.gif";
		}

		function mouseout1()
		{
			window.status = "";
			document.getElementById("imgHideToc").src = "../Images/Page/hidetoc1.gif"
		}

    </script>

    <style type="text/css"> 
        <!-- 
            .skin 
            { 
                cursor:default; 
                font:menutext; 
                position:absolute; 
                text-align:left; 
                font-family: Arial, Helvetica, sans-serif; 
                font-size: 10pt; 
                width:120px; 
                background-color:menu; 
                border:1 solid buttonface; 
                visibility:hidden; 
                border:2 outset buttonhighlight; 
            } 
            .menuitems 
            { 
                padding-left:15px; 
                padding-right:10px; 
            } 
        --> 
    
		BODY { OVERFLOW: auto }
	</style>
</head>
<body style="margin: 0px" class="Tree_Topline" onclick="hideMenu()" oncontextmenu="return false" >
    <form id="Form1" method="post" runat="server" style="margin: 0px">
        <table border="0" cellpadding="0" cellspacing="0" height="20" width="100%" id="MenuBar"
            class="Tree_Topline">
            <tr height="20">
                <td style="color: LightSteelBlue; padding-left: 3px; font-weight: bold" nowrap>
                    <b>
                        <asp:Literal runat="server" ID="litTopis"></asp:Literal></b>
                </td>
                <td align="right" width="20">
                    <img id="imgHideToc" style="cursor: hand" onclick="HideTree();" onmouseover="mouseover1();"
                        onmouseout="mouseout1();" src="../Images/Page/hidetoc1.gif" title="�?" height="20"
                        width="15">
                </td>
            </tr>
        </table>
        <table border="1" cellpadding="0" cellspacing="0" width="100%" height="90%" class="Tree_Area">
            <tr valign="top">
                <td>
                    <iewc:TreeView ID="tvModule" runat="server" SystemImagesPath="../images/tree/" DefaultStyle="font-size:11px;font-family:Arial;"
                        SelectedStyle="Color:Black;background-color:#FDF4A2;" HoverStyle="Color:White;background-color:#333333;"
                        EnableViewState="false" Target="main" ExpandedImageUrl="../Images/tree/icon/folderopen.gif"
                        ImageUrl="../Images/tree/icon/folder.gif" ExpandLevel="1" SelectExpands="True"
                        AutoSelect="True" ShowPlus="True">
                    </iewc:TreeView>
                </td>
            </tr>
        </table>
        <div id="popupMenu" class="skin" onmouseover="highlighItem()" onmouseout="lowlightItem()"
            onclick="clickItem()">
            <div class="menuitems" func="open">
                Open In Window</div>
                <hr />
            
        </div>
    </form>

    <script language="javascript" type="text/javascript"> 
        var menuskin = "skin"; 
        var node = null; 

        function hideMenu() 
        { 
            popupMenu.style.visibility = "hidden"; 
        } 

        function highlighItem() 
        { 
            if (event.srcElement.className == "menuitems") 
            { 
                event.srcElement.style.backgroundColor = "highlight"; 
                event.srcElement.style.color = "white"; 
            } 
        } 

        function lowlightItem() 
        { 
            if (event.srcElement.className == "menuitems") 
            { 
                event.srcElement.style.backgroundColor = ""; 
                event.srcElement.style.color = "black"; 
                window.status = ""; 
            } 
        } 

        function clickItem() 
        { 
            if (event.srcElement.className == "menuitems") 
            { 
                if (event.srcElement.getAttribute("func") == "open" && node != null)
                {
                    var url = node.getAttribute("NavigateUrl");
                    if(url.indexOf(".aspx") > 0)
                    {
                        window.open(node.getAttribute("NavigateUrl"));
                    }
                    //window.alert(node.getTarget());
                }
            } 
        } 

        function tvModule.oncontextmenu() 
        { 
            var nodeindex = event.treeNodeIndex; 
            if (typeof(nodeindex) == "undefined") 
            { 
                node = null; 
                return; 
            } 

            node = tvModule.getTreeNode(nodeindex); 

            var rightedge = document.body.clientWidth-event.clientX; 
            var bottomedge = document.body.clientHeight-event.clientY; 
            if (rightedge <popupMenu.offsetWidth) 
            { 
                popupMenu.style.left = document.body.scrollLeft + event.clientX - popupMenu.offsetWidth + 20; 
            } 
            else 
            { 
                popupMenu.style.left = document.body.scrollLeft + event.clientX; 
            } 
            if (bottomedge <popupMenu.offsetHeight) 
            { 
                popupMenu.style.top = document.body.scrollTop + event.clientY - popupMenu.offsetHeight; 
            } 
            else 
            { 
                popupMenu.style.top = document.body.scrollTop + event.clientY; 
            } 
            popupMenu.style.visibility = "visible"; 
            return false; 
        } 

    </script>

</body>
</html>
