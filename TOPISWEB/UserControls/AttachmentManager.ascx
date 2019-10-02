<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AttachmentManager.ascx.cs" Inherits="UserControls.AttachmentManager" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="DateEditor" Src="DateEditor.ascx" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<TOPIS:STYLESKIN id="StyleSkinAttachmentManager" runat="server"></TOPIS:STYLESKIN><asp:panel id="pnlAttachment" Runat="server">
	<TABLE width="100%" align="center" class="TitleArea">
		<TR>
			<TD align="center" class="TitleText1">
				<asp:Literal id="litTitle" Runat="server"></asp:Literal></TD>
		</TR>
	</TABLE>
	<TABLE width="100%" align="center" border="0">
		<TR align="right" width="80%">
			<TD width="10%"></TD>
			<TD noWrap align="left" width="40%">
				<asp:Literal id="litSelectAttachmentFile" Runat="server"></asp:Literal><INPUT oncontextmenu="return false" id="uplTheFile" onkeydown="return false" type="file"
					size="40" name="uplTheFile" runat="server"></TD>
			<TD noWrap align="right" width="10%">
				<asp:Literal id="litVailDay" runat="server" Text="litVailDay"></asp:Literal></TD>
			<TD noWrap align="left" width="10%">
				<uc1:DateEditor id="DateEditor1" runat="server" Width="100px"></uc1:DateEditor></TD>
			<TD noWrap align="left" width="20%">
				<asp:LinkButton id="btnUploadAttachment" Runat="server" Text="upload" CausesValidation="False" cssclass="blueunderline"></asp:LinkButton></TD>
		</TR>
	</TABLE>
	<asp:datagrid id="dgrdAttachment" runat="server" Width="600px" BorderWidth="0px" CellPadding="0"
		HorizontalAlign="Center" DataKeyField="IDKey" AutoGenerateColumns="False">
		<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
		<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
		<ItemStyle CssClass="TableRow"></ItemStyle>
		<HeaderStyle CssClass="AttachTableHeader"></HeaderStyle>
		<Columns>
			<asp:TemplateColumn>
				<ItemStyle HorizontalAlign="Left"></ItemStyle>
				<ItemTemplate>
					<a href='<%=Request.ApplicationPath%>/Public/DownloadAttachment.aspx?ModuleID=<%= Server.UrlEncode(ModuleID)%>&AttName=<%# Server.UrlEncode(DataBinder.Eval(Container.DataItem,"AttachName").ToString())%>&FileName=<%# Server.UrlEncode(DataBinder.Eval(Container.DataItem,"IDKey").ToString())%>&AttAddr=<%# Server.UrlEncode(DataBinder.Eval(Container.DataItem,"AttachAddr").ToString())%>'>
						<%#DataBinder.Eval(Container,"DataItem.AttachName")%>
					</a>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="AttachSize" ReadOnly="True" DataFormatString="{0:###,###} bytes">
				<ItemStyle HorizontalAlign="Left"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn DataField="UploadTime" DataFormatString="{0:d}">
				<ItemStyle HorizontalAlign="Left"></ItemStyle>
			</asp:BoundColumn>
			<asp:BoundColumn Visible="False" DataField="DateOfExpire" DataFormatString="{0:d}">
				<ItemStyle HorizontalAlign="Left"></ItemStyle>
			</asp:BoundColumn>
			<asp:TemplateColumn>
				<ItemTemplate>
					<asp:ImageButton id="imgbtn_Delete" runat="server" CausesValidation="False" CommandName="DELETE"
						ImageUrl="../Images/Grid/delete.gif"></asp:ImageButton>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:datagrid>
	<asp:Label id="lblErrorMsg" Runat="server"></asp:Label>
</asp:panel><asp:panel id="pnlDisable" Runat="server" Visible="False">
	<asp:Literal id="litDisableMsg" Runat="server" Text="You can not use this control now !"></asp:Literal>
</asp:panel>
