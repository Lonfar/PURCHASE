<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="TableFieldSelect.aspx.cs" AutoEventWireup="True" Inherits="UserControls.TableFieldSelect" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TableFieldSelect</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_parent">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<table Class="WindowsBackground" cellSpacing="10" cellPadding="10" width="100%" height="100%"
			border="0">
			<tr>
				<td>
					<table bgcolor="#ffffff" cellSpacing="0" cellPadding="0" width="100%" height="100%" border="1">
						<tr>
							<td>
								<form id="Form1" method="post" runat="server">
									<iewc:treeview id="tvTableField" runat="server" Target="_blank" height="100%" AutoPostBack="true"
										ShowPlus="True" AutoSelect="True" SelectExpands="True" ExpandLevel="2" HoverStyle="font-color:white;background-color:#003399;"
										SelectedStyle="font-color:white;background-color:#003399;" DefaultStyle="font-size:11px;font-family:Arial;"></iewc:treeview>
								</form>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>
