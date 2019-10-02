<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="ChildEditControl.ascx" %>
<%@ Page language="c#" Codebehind="ChildEditControlView.aspx.cs" AutoEventWireup="True" Inherits="UserControls.ChildEditControlView" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ChildEditControlView</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" class="ViewWinBodyBackground">
		<form id="Form1" method="post" runat="server">
			<table id="table1" cellpadding="1" cellspacing="1" width="95%" border="0">
				<tr valign="top" align="center">
					<td align="center">
						<table cellSpacing="0" cellPadding="10" width="100%" height="100%" border="0">
							<tr>
								<td align="center" valign="top">
									<uc1:ChildEditControl ID="ChildControl" Runat="server"></uc1:ChildEditControl>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
