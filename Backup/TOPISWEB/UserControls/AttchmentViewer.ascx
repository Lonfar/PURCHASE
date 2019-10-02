<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AttchmentViewer.ascx.cs" Inherits="UserControls.AttchmentViewer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="ViewTitleText">
			<asp:Literal id="litAttr" runat="server"></asp:Literal></TD>
	</TR>
	<TR>
		<TD>
			<asp:datagrid id="dgrdAttachment" runat="server" AutoGenerateColumns="False" DataKeyField="IDKey"
				Width="100%" EnableViewState="False">
				<Columns>
					<asp:TemplateColumn HeaderText="File Name" HeaderStyle-CssClass="ViewTableHeader">
						<ItemTemplate>
							<a href='<%=Request.ApplicationPath%>/Public/DownloadAttachment.aspx?ModuleID=<%= Server.UrlEncode(ModuleID)%>&AttName=<%# Server.UrlEncode(DataBinder.Eval(Container.DataItem,"AttachName").ToString())%>&FileName=<%# Server.UrlEncode(DataBinder.Eval(Container.DataItem,"IDKey").ToString())%>&AttAddr=<%# Server.UrlEncode(DataBinder.Eval(Container.DataItem,"AttachAddr").ToString())%>'>
								<%#DataBinder.Eval(Container,"DataItem.AttachName")%>
							</a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:BoundColumn DataField="AttachSize" ReadOnly="True" HeaderText="Size" DataFormatString="{0:###,###} bytes"
						HeaderStyle-CssClass="ViewTableHeader"></asp:BoundColumn>
				</Columns>
			</asp:datagrid></TD>
	</TR>
</TABLE>
