<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemCodeQuery.aspx.cs" Inherits="TopisWeb.MaterialManagement.ItemCodeQuery" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MaterialHistory</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/DatePicker.css" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/DatePicker.js"></SCRIPT>
		<SCRIPT language="javascript">
  
        function openWin(VoucherID,ModuleID,PKValue)   
		{   
			window.open("../UserControls/View.aspx?ID="+VoucherID+"&ModuleID="+ModuleID+"&PKValue="+PKValue,"win","toolbar=no,location=no,directories=no,status=no,scrollbars=yes,menubar=no,resizable=yes,copyhistory=yes,width=800,height=500,top=120,left=100");	
		}   

        document.onkeyup = function search()
        {
            
            if(window.event.keyCode == 13)
            {
                document.getElementById("btnSearch").click();
            }
        }
        
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			
			<TABLE class="TopToolBarLine" id="Table2" cellSpacing="0" cellPadding="0" width="100%" 
				border="0">
				<TBODY>
					<TR vAlign="top">
						<TD><cc1:toolbar id="ToolBar1" runat="server"  ></cc1:toolbar></TD>
					</TR>
				</TBODY>
			</TABLE>
			<TABLE class="TitleArea" id="Table0" cellSpacing="1" cellPadding="1" width="95%" align="center"
				border="0">
				<TBODY>
					<TR vAlign="bottom">
						<TD align="left" colSpan="4"><asp:linkbutton id="lbHideVoucher" Runat="server" ForeColor="#0000FF" Visable="true">cccccccc</asp:linkbutton><asp:linkbutton id="lbShowVoucher" Runat="server" ForeColor="#0000FF" Visable="true">cccccccccbbb</asp:linkbutton></TD>
					</TR>
					<tr>
						<td colspan="4">&nbsp;
							<asp:Panel id="pnlShow" runat="server">
								<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
									<TR vAlign="top">
										<TD align="right" colSpan="1">
											<asp:literal id="Literal_From" runat="server"></asp:literal></TD>
										<TD   align="center" colSpan="1">
											<asp:Textbox id="txtItemCode" runat="server" CssClass="SingleLineTextBox" ></asp:Textbox></TD>
										<TD style="FONT-SIZE: 11px; WIDTH: 10%; COLOR: dimgray; FONT-FAMILY: Arial" vAlign="bottom"
											align="center" colSpan="1">
											<asp:literal id="Literal_To" runat="server"></asp:literal></TD>
										<TD  align="left" colSpan="1">
											<asp:Textbox id="txtCataLog" runat="server" CssClass="SingleLineTextBox" ></asp:Textbox></TD>
										<TD ></TD>
									</TR>
									<TR>
										<TD align="right" width="30%" colSpan="1">
											<asp:label id="lbName" runat="server" CssClass="FormNormalTitle"></asp:label></TD>
										<TD align="left" width="50%" colSpan="3">
											<asp:textbox id="txtDes" runat="server" CssClass="SingleLineTextBox" Width="100%"></asp:textbox></TD>
										<TD style="width: 1342177.27%"></TD>
									</TR>
									
								</TABLE>
							</asp:Panel></td>
					</tr>
					<TR align="center">
						<TD class="TitleText1"><asp:label id="lbTitle" runat="server" Font-Size="Medium"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
			<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
				<TBODY>
					<tr align="center">
						<td>
							<asp:datagrid id="DG_VendorContract" runat="server" CssClass="TableGlobalOne" Width="100%" AllowPaging="True"
								AutoGenerateColumns="False" PageSize="30">
								<SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
								<AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
								<ItemStyle CssClass="TableRow"></ItemStyle>
								<Columns>
									<asp:TemplateColumn >
										<ItemTemplate>
											<asp:Label id="lbNum" runat="server"  Width="4%" >
												<%#DataBinder.Eval(Container.DataItem,"NumberIndex")%>
											</asp:Label>
										</ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="ItemCode">
										<ItemStyle Height="20px" Width="20%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Description">
										<ItemStyle Height="20px" Width="46%"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
									</asp:BoundColumn>
									<asp:BoundColumn DataField="Category">
										<ItemStyle Height="20px" Width="15%" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
									</asp:BoundColumn>
									<asp:BoundColumn DataField="UOM">
										<ItemStyle Height="20px" Width="15%" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
									</asp:BoundColumn>
									
								</Columns>
								<PagerStyle Visible="False" Height="25px" HorizontalAlign="Right" ForeColor="Black" BackColor="#F7F7DE"
									Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
					</tr>
				</TBODY>
			</TABLE>
			<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<tr>
						<td colSpan="4"><br>
							<WEBDIYER:ASPNETPAGER id="pager" runat="server" CssClass="mypager" Width="100%" PageSize="15" PagingButtonType="Image"
								NavigationToolTipTextFormatString="Turn To Page {0}" PageIndexOutOfRangeErrorString="page index out of range"
								InvalidPageIndexErrorString="the page index is invalid" TextBeforeInputBox="Turn To " DisabledButtonImageNameExtension="g"
								CpiButtonImageNameExtension="r" ButtonImageNameExtension="n" ImagePath="../Images/aspnetpager/" ShowCustomInfoSection="left"
								NumericButtonTextFormatString="[{0}]" SubmitButtonText="Submit" InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
								SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px" PagingButtonSpacing="4px"
								ShowInputBox="Always" Height="25px" HorizontalAlign="Right"></WEBDIYER:ASPNETPAGER></td>
					</tr>
				</TBODY>
			</TABLE>
			<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TBODY>
					<tr>
						<td colSpan="4"><br>
						</td>
					</tr>
					<TR vAlign="top">
						<TD class="StatusLine" vAlign="middle" colSpan="4"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label></TD>
					</TR>
				</TBODY>
			</TABLE>
			<input type="button" id="btnSearch" runat="server" onserverclick="btnsubmit_Click" style="display:none" />
		</form>
		
	</body>
</HTML>
