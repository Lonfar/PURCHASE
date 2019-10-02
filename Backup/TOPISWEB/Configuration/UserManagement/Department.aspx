<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Page language="c#" Codebehind="Department.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.Department" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="Department" runat="server"></TOPIS:StyleSkin>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="FlowLayout" id="thebody">
		<TOPIS:PAGEDESCRIPTION id="DepartmentPageDescription" runat="server" Text=" "></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<asp:Panel Runat="server" ID="pnlQuery" Visible="False">
				<TABLE class="TableWithBorder" style="MARGIN-BOTTOM: 3px" cellPadding="3" width="100%"
					align="center" border="0">
					<TR>
						<TD align="center">
							<asp:Literal id=litSearch Text='<%#GetString("litSearch")%>' Runat="server">
							</asp:Literal>
							<asp:TextBox id="txtSearchCondition" Runat="server" Width="150px" MaxLength="50"></asp:TextBox>
							<asp:LinkButton id=btnSearch Text='<%#GetString("btnSearch")%>' Runat="server" cssclass="blueunderline" onclick="btnSearch_Click">
							</asp:LinkButton>
							<asp:LinkButton id=btnToAddNew Text='<%#GetString("btnToAddNew")%>' Runat="server" cssclass="blueunderline" onclick="btnToAddNew_Click">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
				<asp:datagrid id="dgrdList" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyField="DepartmentID"
					HorizontalAlign="Center" CellPadding="2" AllowPaging="False" PageSize="20" BorderColor="gray"
					CssClass="dgrdGlobal" onselectedindexchanged="dgrdList_SelectedIndexChanged">
					<SelectedItemStyle CssClass="dgrdSelectedItem"></SelectedItemStyle>
					<AlternatingItemStyle CssClass="dgrdAlterItem"></AlternatingItemStyle>
					<ItemStyle CssClass="dgrdItem"></ItemStyle>
					<HeaderStyle CssClass="dgrdHeader"></HeaderStyle>
					<Columns>
						<asp:HyperLinkColumn DataNavigateUrlField="DepartmentID" DataTextField="DepartmentID" HeaderText="DepartmentID"
							ItemStyle-HorizontalAlign="Left" Target="_blank" DataNavigateUrlFormatString="DepartmentInfo.aspx?DepartmentID={0}"></asp:HyperLinkColumn>
						<asp:BoundColumn DataField="DepartmentName" ReadOnly="True" HeaderText="¡°DepartmentName" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
						<asp:BoundColumn DataField="Principal" ReadOnly="True" HeaderText="Principal"></asp:BoundColumn>
						<asp:BoundColumn DataField="Tel" ReadOnly="True" HeaderText="Tel"></asp:BoundColumn>
						<asp:ButtonColumn ButtonType="LinkButton" Text="Edit" CommandName="Select" ItemStyle-Width="40px"></asp:ButtonColumn>
						<asp:ButtonColumn ButtonType="LinkButton" Text="Delete" CommandName="Delete" ItemStyle-Width="40px"></asp:ButtonColumn>
					</Columns>
					<PagerStyle Mode="NumericPages" CssClass="dgrdPager" HorizontalAlign="Right"></PagerStyle>
				</asp:datagrid>
				<TABLE width="100%" align="center">
					<TR>
						<TD>
							<webdiyer:AspNetPager id="pager" runat="server" Width="100%" HorizontalAlign="Right" PageSize="15" CssClass="mypager"
								PagingButtonSpacing="2px" Height="25px" SubmitButtonStyle="border:1px solid #000066;height:20px;width:30px"
								InputBoxStyle="border:1px #0000FF solid;text-align:center" SubmitButtonText="OK" NumericButtonTextFormatString="[{0}]"
								ShowCustomInfoSection="left" ImagePath="../Images/aspnetpager" ButtonImageNameExtension="n" CpiButtonImageNameExtension="r"
								DisabledButtonImageNameExtension="g" TextBeforeInputBox="Turn To Page" InvalidPageIndexErrorString="the page index is invalid"
								NavigationToolTipTextFormatString="Turn to page {0}" PagingButtonType="Image"></webdiyer:AspNetPager></TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:Panel Runat="server" ID="pnlDetails" Visible="false">
				<TABLE class="TableBlueBorderWhiteBg" width="100%" align="center">
					<COLGROUP>
						<COL align="right" width="100">
						<COL align="left" width="*">
						<COL align="left" width="100">
					</COLGROUP>
					<TR>
						<TD class="TableTitle" colSpan="3">
							<asp:Literal id=litModifyDepartmentInfo Text='<%#GetString("litInfoDetails")%>' Runat="server">
							</asp:Literal></TD>
					</TR>
					<TR>
						<TD class="Seperator" colSpan="3"></TD>
					</TR>
					<TR>
						<TD class="RequiredTitle" width="100">
							<asp:literal id=litDepartrmentID Text='<%#GetString("litDepartmentID")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtDepartmentID" Runat="server" Width="100%" MaxLength="50"></asp:textbox></TD>
						<TD width="100"><FONT class="Blue">(50)</FONT>
							<asp:RequiredFieldValidator id=Requiredfieldvalidator4 runat="server" Display="Dynamic" ErrorMessage='<%#GetString("sRequiredField")%>' ControlToValidate="txtDepartmentID">
							</asp:RequiredFieldValidator>
							<asp:RegularExpressionValidator id=Regularexpressionvalidator1 Runat="server" Display="Dynamic" ErrorMessage='<%#GetString("sExistsInvalidIDChar")%>' ControlToValidate="txtDepartmentID" ValidationExpression="[^<\s]*">
							</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD class="RequiredTitle">
							<asp:literal id=litDepartmentName Text='<%#GetString("litDepartmentName")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtDepartmentName" Runat="server" Width="100%" MaxLength="255"></asp:textbox></TD>
						<TD><FONT class="Blue">(255)</FONT>
							<asp:RequiredFieldValidator id=RequiredFieldValidator1 runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' ControlToValidate="txtDepartmentName">
							</asp:RequiredFieldValidator>
							<asp:RegularExpressionValidator id=Regularexpressionvalidator3 Runat="server" Display="Dynamic" ErrorMessage='<%#GetString("sExistsInvalidChar")%>' ControlToValidate="txtDepartmentName" ValidationExpression="[^<]*">
							</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD vAlign="top">
							<asp:literal id=litDepartmentDescription Text='<%#GetString("litDepartmentDescription")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtDepartmentDescription" Runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></TD>
						<TD><FONT class="Blue">(0)</FONT></TD>
					</TR>
					<TR>
						<TD>
							<asp:literal id=litPrincipal Text='<%#GetString("litPrincipal")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtPrincipal" Runat="server" Width="100%" MaxLength="64"></asp:textbox></TD>
						<TD><FONT class="Blue">(64)</FONT>
							<asp:RegularExpressionValidator id=Regularexpressionvalidator4 Runat="server" Display="Dynamic" ErrorMessage='<%#GetString("sExistsInvalidChar")%>' ControlToValidate="txtPrincipal" ValidationExpression="[^<]*">
							</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD vAlign="top">
							<asp:literal id=litAddress Text='<%#GetString("litAddress")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtAddress" Runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></TD>
						<TD><FONT class="Blue">(255)</FONT>
						</TD>
					</TR>
					<TR>
						<TD>
							<asp:literal id=litFax Text='<%#GetString("litFax")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtFax" Runat="server" Width="100%" MaxLength="64"></asp:textbox></TD>
						<TD><FONT class="Blue">(64)</FONT>
							<asp:RegularExpressionValidator id=Regularexpressionvalidator5 Runat="server" Display="Dynamic" ErrorMessage='<%#GetString("sExistsInvalidChar")%>' ControlToValidate="txtFax" ValidationExpression="[^<]*">
							</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD>
							<asp:literal id=litTel Text='<%#GetString("litTel")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtTel" Runat="server" Width="100%" MaxLength="64"></asp:textbox></TD>
						<TD><FONT class="Blue">(64)</FONT>
							<asp:RegularExpressionValidator id=Regularexpressionvalidator6 Runat="server" Display="Dynamic" ErrorMessage='<%#GetString("sExistsInvalidChar")%>' ControlToValidate="txtTel" ValidationExpression="[^<]*">
							</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD>
							<asp:literal id=litEmail Text='<%#GetString("litEmail")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:textbox id="txtEmail" Runat="server" Width="100%" MaxLength="64"></asp:textbox></TD>
						<TD><FONT class="Blue">(64)</FONT>
							<asp:RegularExpressionValidator id="Regularexpressionvalidator2" runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail"
								ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD>
							<asp:literal id=litContact Text='<%#GetString("litContact")%>' Runat="server">
							</asp:literal></TD>
						<TD>
							<asp:TextBox id="txtContact" Runat="server" Width="100%" MaxLength="64"></asp:TextBox></TD>
						<TD><FONT class="Blue">(64)</FONT>
							<asp:RegularExpressionValidator id=Regularexpressionvalidator7 Runat="server" Display="Dynamic" ErrorMessage='<%#GetString("sExistsInvalidChar")%>' ControlToValidate="txtContact" ValidationExpression="[^<]*">
							</asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD class="TableTail" colSpan="3">
							<asp:LinkButton id=btnAdd Text='<%#GetString("btnAdd")%>' Runat="server" cssclass="blueunderline" onclick="btnAdd_Click">
							</asp:LinkButton>
							<asp:LinkButton id=btnSave Text='<%#GetString("btnSave")%>' Runat="server" cssclass="blueunderline" onclick="btnSave_Click">
							</asp:LinkButton>
							<asp:LinkButton id=btnReturnToList Text='<%#GetString("btnReturnToList")%>' Runat="server" cssclass="blueunderline" CausesValidation="False" onclick="btnReturnToList_Click">
							</asp:LinkButton></TD>
					</TR>
				</TABLE>
			</asp:Panel>
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal>
	</body>
</HTML>
