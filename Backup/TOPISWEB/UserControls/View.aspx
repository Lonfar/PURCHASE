<%@ Page language="c#" Codebehind="View.aspx.cs" AutoEventWireup="True" Inherits="UserControls.View" %>
<%@ Register TagPrefix="uc1" TagName="UCView" Src="UCView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>View</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
  </HEAD>
	<body MS_POSITIONING="GridLayout" class="ViewWinBodyBackground" >
		<form id="Form1" method="post" runat="server"><FONT 
face=ËÎÌו></FONT>
		<br>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" align="center" class="ViewWinTableEdge" border="0">
				<TR align="center" valign="top">
					<TD valign="top" >
					<table  cellSpacing="0" cellPadding="10" width="100%" height="100%" border="0">
						<tr><td valign="top" align="center">
							<uc1:ucview id="UCViewControl" runat="server"></uc1:ucview>
							</td></tr>
					</table>
					
					</TD>
				</TR>
			</TABLE>
			<br>
		</form>
	</body>
</HTML>
