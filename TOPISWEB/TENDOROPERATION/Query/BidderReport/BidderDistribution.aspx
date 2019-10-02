<%@ Page language="c#" Codebehind="BidderDistribution.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.TENDOROPERATION.Query.BidderReport.BidderDistribution" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>BidderDistribution</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<TOPIS:PAGEDESCRIPTION id="PageDescriptionPass" runat="server"></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td align="center"  class="TitleText1">
						<asp:Label id="lbContinent" runat="server">Label</asp:Label>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:DataGrid id="dgContinent" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="TableGlobalOne"
							Width="60%">
							<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
							<ItemStyle CssClass="TableRow"></ItemStyle>
							<HeaderStyle CssClass="TableHeader" HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="Continent"></asp:BoundColumn>
								<asp:BoundColumn DataField="Num"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td height="50">
					</td>
				</tr>
			</table>
			<table width="100%">
				<tr>
					<td align="center" class="TitleText1">
						<asp:Label id="lbCountry" runat="server">Label</asp:Label>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:DataGrid id="dgCountry" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="TableGlobalOne"
							Width="60%">
							<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
							<ItemStyle CssClass="TableRow"></ItemStyle>
							<HeaderStyle CssClass="TableHeader" HorizontalAlign="Center"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="CountryName"></asp:BoundColumn>
								<asp:BoundColumn DataField="Num"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
