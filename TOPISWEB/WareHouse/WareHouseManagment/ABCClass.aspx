<%@ Page language="c#" Codebehind="ABCClass.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.WareHouseManagment.ABCClass" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ABCCalss</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../../MyScripts/DatePicker.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<table id="Table2" cellSpacing="3" cellPadding="3" width="95%" align="center" border="0">
				<TR vAlign="bottom">
					<TD align="left" colSpan="4">
						<asp:linkbutton id="lbHideVoucher" Runat="server" CssClass="blueunderline" Visable="true" onclick="lbHideVoucher_Click"></asp:linkbutton>
						<asp:linkbutton id="lbShowVoucher" Runat="server" CssClass="blueunderline" Visable="true" onclick="lbShowVoucher_Click"></asp:linkbutton>
					</TD>
				</TR>
				<tr vAlign="top">
					<td vAlign="bottom" align="left" colSpan="3"><asp:panel id="pnlShow" runat="server">
							<TABLE cellSpacing="1" cellPadding="1" align="center" border="0">
								<TR>
									<TD class="FormNormalTitle" style="WIDTH: 10%" align="right">
										<asp:Label id="lblWHID" runat="server"></asp:Label></TD>
									<TD>
										<uc1:RefEditor id="Ref_WHID" runat="server"></uc1:RefEditor></TD>
									<TD>&nbsp;</TD>
									<TD class="FormNormalTitle" style="WIDTH: 10%" align="right">
										<asp:Label id="lblABCClass" runat="server"></asp:Label></TD>
									<TD>
										<asp:TextBox class="SingleLineTextBox" id="txtABCClass" runat="server"></asp:TextBox></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
				<tr>
					<td class="TitleText1" align="center" colSpan="3"><asp:label id="lbTitle" runat="server">Label</asp:label></td>
				</tr>
				<tr>
					<td align="center" colSpan="3">
						<asp:datagrid id="dgDownload" runat="server" Width="100%" CssClass="TableGlobalOne" AutoGenerateColumns="False"
							AllowSorting="True" PageSize="12" AllowPaging="True">
							<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
							<ItemStyle CssClass="TableRow"></ItemStyle>
							<Columns>
								<asp:BoundColumn DataField="WHName" SortExpression="WHName" HeaderText="WHName" HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="ItemCode" SortExpression="ItemCode" HeaderText="ItemCode" HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="MaterialName" SortExpression="MaterialName" HeaderText="MaterialName"
									HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="BINID" SortExpression="BINID" HeaderText="BINID" HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="QuantityInBin" SortExpression="QuantityInBin" HeaderText="QuantityInBin"
									HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UOMName" SortExpression="UOMName" HeaderText="UOMName" HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UnitPricePOStandard" SortExpression="UnitPricePOStandard" HeaderText="UnitPricePOStandard"
									DataFormatString="{0:N}" HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UnitPricePONatural" SortExpression="UnitPricePONatural" HeaderText="UnitPricePONatural"
									DataFormatString="{0:N}" HeaderStyle-CssClass="TableHeaderTD">
									<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<TR vAlign="top">
					<TD class="StatusLine" valign="middle">
						<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
