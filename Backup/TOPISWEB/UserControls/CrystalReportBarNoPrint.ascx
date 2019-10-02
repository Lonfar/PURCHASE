<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CrystalReportBarNoPrint.ascx.cs" Inherits="UserControls.CrystalReportBar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table align="center" class="TableWithBorder" style="MARGIN-BOTTOM: 5px" cellPadding="2"
	width="100%" border="0">
	<tr bgcolor="white" height="25">
		<td nowrap>
			<asp:LinkButton Runat="server" id="btnFirst" Text='First' CssClass="blueunderline"></asp:LinkButton>
			<asp:LinkButton Runat="server" id="btnPrev" Text='Previous' CssClass="blueunderline"></asp:LinkButton>
			&nbsp;
			<asp:Label Runat="server" ID="lblCurrentPage" ForeColor="red" Font-Bold="True"></asp:Label>
			&nbsp;
			<asp:LinkButton Runat="server" id="btnNext" Text='Next' CssClass="blueunderline"></asp:LinkButton>
			<asp:LinkButton Runat="server" id="btnLast" Text='Last' CssClass="blueunderline"></asp:LinkButton>
			&nbsp;&nbsp;
			<asp:TextBox Runat="server" ID="txtPageNO" Width="30px" MaxLength="10"></asp:TextBox>
			<asp:LinkButton Runat="server" id="btnToN" Text='Turn-To' CssClass="blueunderline"></asp:LinkButton>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Literal Runat="server" ID="litZoom" Text='<%#GetPublicString("string","litZoom")%>' Visible="False" >
			</asp:Literal>
			<asp:DropDownList Runat="server" ID="ddlZoomFactor" Width="60px" AutoPostBack="True">
				<asp:ListItem Value="25">25%</asp:ListItem>
				<asp:ListItem Value="40">40%</asp:ListItem>
				<asp:ListItem Value="50">50%</asp:ListItem>
				<asp:ListItem Value="60">60%</asp:ListItem>
				<asp:ListItem Value="70">70%</asp:ListItem>
				<asp:ListItem Value="80">80%</asp:ListItem>
				<asp:ListItem Value="90">90%</asp:ListItem>
				<asp:ListItem Value="100" Selected="True">100%</asp:ListItem>
				<asp:ListItem Value="125">125%</asp:ListItem>
				<asp:ListItem Value="150">150%</asp:ListItem>
				<asp:ListItem Value="200">200%</asp:ListItem>
				<asp:ListItem Value="300">300%</asp:ListItem>
				<asp:ListItem Value="400">400%</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
</table>
