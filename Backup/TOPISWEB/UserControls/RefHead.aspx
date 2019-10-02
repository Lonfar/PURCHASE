<%@ Page language="c#" Codebehind="RefHead.aspx.cs" AutoEventWireup="True" Inherits="UserControls.clsRefHead" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<script language="javascript">
			function search()
			{
				if(event.keyCode==13)
				{
					if(document.getElementById("skey").value=="")
					{
						window.parent.frames("RefContent").location='RefList.aspx?ModuleID='+document.getElementById("txtModuleID").value+'&TableCode='+document.getElementById("txtTableCode").value+'&MultiSelect='+document.getElementById("txtMultiSelect").value+'&WhereSql='+document.getElementById("txtWhereSql").value+'&ShowClear='+document.getElementById("txtShowClear").value+'&AutoPostBack='+document.getElementById("txtAutoPostBack").value ;
						return;
					}
					window.parent.frames("RefContent").location='RefList.aspx?Filter='+escape(document.getElementById("skey").value+' and type=1')+'&ModuleID='+document.getElementById("txtModuleID").value+'&TableCode='+document.getElementById("txtTableCode").value+'&MultiSelect='+document.getElementById("txtMultiSelect").value+'&WhereSql='+document.getElementById("txtWhereSql").value+'&ShowClear='+document.getElementById("txtShowClear").value+'&AutoPostBack='+document.getElementById("txtAutoPostBack").value ;
				}						
			}	
			function search2()
			{
				if(document.getElementById("skey").value=="")
				{
					window.parent.frames("RefContent").location='RefList.aspx?ModuleID='+document.getElementById("txtModuleID").value+'&TableCode='+document.getElementById("txtTableCode").value+'&MultiSelect='+document.getElementById("txtMultiSelect").value+'&WhereSql='+document.getElementById("txtWhereSql").value+'&ShowClear='+document.getElementById("txtShowClear").value+'&AutoPostBack='+document.getElementById("txtAutoPostBack").value ;
					return;
				}
				window.parent.frames("RefContent").location='RefList.aspx?Filter='+escape(document.getElementById("skey").value+' and type=1')+'&ModuleID='+document.getElementById("txtModuleID").value+'&TableCode='+document.getElementById("txtTableCode").value+'&MultiSelect='+document.getElementById("txtMultiSelect").value+'&WhereSql='+document.getElementById("txtWhereSql").value+'&ShowClear='+document.getElementById("txtShowClear").value+'&AutoPostBack='+document.getElementById("txtAutoPostBack").value;
			}			
		</script>
	</HEAD>
	<body id="thebody">
		<table align="center" width="100%" Class="RefHeadSearch">
			<tr>
				<td align="right">
					<input type="text" id="txtModuleID" name="txtModuleID" style="WIDTH: 0px" runat="server"> 
					<input type="text" id="txtTableCode" name="txtTableCode" style="WIDTH: 0px" runat="server">
					<input type="text" id="txtMultiSelect" name="txtMultiSelect" style="WIDTH: 0px" runat="server">
					<input type="text" id="txtWhereSql" name="txtWhereSql" style="WIDTH: 0px" runat="server"> 
					<input type="text" id="txtFilter" name="txtFilter" style="WIDTH: 0px" runat="server">
					<input type="text" id="txtAutoPostBack" name="txtAutoPostBack" style="WIDTH: 0px" runat="server">
                    <input type="text" id="txtShowClear" name="txtShowClear" style="WIDTH: 0px" runat="server">
					<asp:Literal Runat="server" ID="litSearch" ></asp:Literal>
					<input type="text" size="40"  maxlength="50" id="skey" name="skey" OnKeyDown="search()">
					<img src="../Images/Page/find.gif" alt="Search" title="Search" border="0" onclick="search2()"
						align="absMiddle" style="CURSOR:hand">
				</td>
				<td width=20></td>
			</tr>
		</table>
	</body>
</HTML>

