<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AttManager.ascx.cs" Inherits="UserControls.AttManager" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
			<asp:Panel Runat="server" ID="pnlAttachment">
				<TABLE width="98%" align="center" border="0">
					<TR>
						<TD align=left><INPUT id="uplTheFile" type="file" size="60" name="uplTheFile" runat="server">
							<asp:LinkButton id="btnUploadAttachment" Runat="server" Text='<%#Topis.Web.Globalization.MyResouceManager.GetString("UserControl.AttachmentManager","btnUploadAttachment")%>' cssclass="blueunderline" CausesValidation="False">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
				<asp:datagrid id="dgrdAttachment" runat="server" AutoGenerateColumns="False" DataKeyField="AttachmentID"
					HorizontalAlign="Center" CellPadding="2" AllowPaging="False" BorderColor="gray" CssClass="dgrdGlobal"
					Width="98%">
					<SelectedItemStyle CssClass="dgrdSelectedItem"></SelectedItemStyle>
					<AlternatingItemStyle CssClass="dgrdAlterItem"></AlternatingItemStyle>
					<ItemStyle CssClass="dgrdItem"></ItemStyle>
					<HeaderStyle CssClass="dgrdHeader" BackColor="#97acce" ForeColor="#000000"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="File Name" ItemStyle-HorizontalAlign="Left">
							<ItemTemplate>
								<a href='<%=Request.ApplicationPath%>/Public/DownloadAttachment.aspx?ModuleID=<%=ModuleID%>&AttName=<%#DataBinder.Eval(Container,"DataItem.AttName")%>&FileName=<%#DataBinder.Eval(Container,"DataItem.AttachmentID")%><%#DataBinder.Eval(Container,"DataItem.AttFileEx")%>'>
									<%#DataBinder.Eval(Container,"DataItem.AttName")%>
									 </a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="AttSize" ReadOnly="True" HeaderText="Size" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:###,###} bytes"></asp:BoundColumn>
						<asp:ButtonColumn ButtonType="LinkButton" Text="Delete" CommandName="Delete" ItemStyle-Width="40px"></asp:ButtonColumn>
					</Columns>
				</asp:datagrid>
				<asp:Label id="lblErrorMsg" Runat="server"></asp:Label>
			</asp:Panel>
			<asp:Panel Runat="server" ID="pnlDisable" Visible="False">
				<asp:Literal Runat="server" ID="litDisableMsg" Text="You can not use this control now !"></asp:Literal>
			</asp:Panel>