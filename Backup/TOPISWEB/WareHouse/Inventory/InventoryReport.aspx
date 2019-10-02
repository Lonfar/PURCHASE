<%@ Page language="c#" Codebehind="InventoryReport.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.WareHouse.Inventory.InventoryReport" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>InventoryReport</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE Class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<table id="Table2" cellSpacing="3" cellPadding="3" width="95%" align="center" border="0">
				<TR vAlign="bottom">
					<TD align="left" colSpan="4">
						<asp:LinkButton ID="lbHideVoucher" Runat="server" CssClass="blueunderline" Visable="true" onclick="lbHideVoucher_Click"></asp:LinkButton>
						<asp:LinkButton ID="lbShowVoucher" Runat="server" CssClass="blueunderline" Visable="true" onclick="lbShowVoucher_Click"></asp:LinkButton></TD>
				</TR>
				<tr vAlign="top">
					<td vAlign="bottom" align="left" colspan="3">
						<asp:Panel id="pnlShow" runat="server">
<asp:label id="lbSearch" runat="server">Label</asp:label>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:textbox id="txtFilter" runat="server"></asp:textbox></asp:Panel></td>
				</tr>
				<tr>
					<td class="TitleText1" align="center" colSpan="3"><asp:label id="lbTitle" runat="server">Label</asp:label></td>
				</tr>
				<tr>
					<td align="center" colSpan="3"><asp:datagrid id="dgDownload" runat="server" Width="100%" PageSize="8" CssClass="TableGlobalOne"
							AutoGenerateColumns="False" AllowSorting="True">
							<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
							<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
							<ItemStyle CssClass="TableRow"></ItemStyle>
							<HeaderStyle CssClass="TableHeader"></HeaderStyle>
							<Columns>
								<asp:BoundColumn DataField="CheckStockNO" SortExpression="CheckStockNO" HeaderText="CheckStockNO">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AttachName" SortExpression="AttachName" HeaderText="AttachName">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="AttachSize" SortExpression="AttachSize" HeaderText="AttachSize">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="UploadTime" SortExpression="UploadTime" HeaderText="UploadTime">
									<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:BoundColumn>
								<asp:TemplateColumn SortExpression="DownloadFiles" HeaderText="DownloadFiles">
									<ItemStyle HorizontalAlign="Center" Height="20px" VerticalAlign="Middle"></ItemStyle>
									<HeaderTemplate>
										<FONT face="宋体">
											<%#GetString("DownloadFiles")%>
										</FONT>
									</HeaderTemplate>
									<ItemTemplate>
										<A href='<%=Request.ApplicationPath%>/Public/DownloadAttachment.aspx?ModuleID=Topis.WareHouse.Inventory.CheckStock&FileName=<%#  Server.UrlEncode(DataBinder.Eval(Container.DataItem,"IDKey").ToString())%>&AttName=<%# Server.UrlEncode(DataBinder.Eval(Container.DataItem,"AttachName").ToString())%>&AttAddr=<%#  Server.UrlEncode(DataBinder.Eval(Container.DataItem,"AttachAddr").ToString())%>'>
											<%#GetString("DownloadFiles")%>
										</A>
									</ItemTemplate>
									<EditItemTemplate>
										<FONT face="宋体"></FONT>
									</EditItemTemplate>
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td align="right" colspan="3"><WEBDIYER:ASPNETPAGER id="pager" runat="server" CssClass="mypager" PageSize="15" Width="100%" ShowInputBox="Always"
							PagingButtonSpacing="4px" SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px" InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
							SubmitButtonText="Submit" NumericButtonTextFormatString="[{0}]" ShowCustomInfoSection="left" ImagePath="../../Images/aspnetpager/"
							ButtonImageNameExtension="n" CpiButtonImageNameExtension="r" DisabledButtonImageNameExtension="g" TextBeforeInputBox="Turn To "
							InvalidPageIndexErrorString="the page index is invalid" PageIndexOutOfRangeErrorString="page index out of range" NavigationToolTipTextFormatString="Turn To Page {0}"
							PagingButtonType="Image" Height="25px" HorizontalAlign="Right"></WEBDIYER:ASPNETPAGER></td>
				</tr>
				<TR vAlign="top">
					<TD class="StatusLine" valign="middle" colspan="3">
						<asp:label id="lblMSG" runat="server" Width="100%"></asp:label>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
