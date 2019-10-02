<%@ Page Language="c#" Codebehind="POHistory.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.MaterialManagement.POHistory" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCList" Src="../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="Cnwit.Web.UI.WebControls" Assembly="Cnwit.DatePicker" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>POHistory</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
    <link href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
    <link href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
    <link href="../Styles/DatePicker.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../MyScripts/DatePicker.js"></script>

    <script language="javascript" type="text/javascript">
        function openWin(VoucherID,ModuleID,PKValue)   
		{   
			window.open("../UserControls/View.aspx?ID="+VoucherID+"&ModuleID="+ModuleID+"&PKValue="+PKValue,"win","toolbar=no,location=no,directories=no,status=no,scrollbars=yes,menubar=no,resizable=yes,copyhistory=yes,width=800,height=500,top=120,left=100");	
		}   
    </script>

</head>
<body ms_positioning="FlowLayout">
    <form id="Form1" method="post" runat="server">
        <table class="TopToolBarLine" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <font face="宋体"></font>
                </td>
            </tr>
        </table>
        <table class="TopToolBarLine" cellspacing="0" cellpadding="0" width="100%" align="center"
            border="0">
            <tr valign="top">
                <td>
                    <cc1:ToolBar ID="ToolBar1" runat="server">
                    </cc1:ToolBar></td>
            </tr>
        </table>
        <table class="TitleArea" cellspacing="1" cellpadding="1" width="95%" align="center"
            border="0">
            <tr valign="bottom">
                <td align="left" colspan="4">
                    <asp:LinkButton ID="lbHideVoucher" runat="server" Visable="true" ForeColor="#0000FF">cccccccc</asp:LinkButton>
                    <asp:LinkButton ID="lbShowVoucher" runat="server" Visable="true" ForeColor="#0000FF">cccccccccbbb</asp:LinkButton></td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                    <asp:Panel ID="pnlShow" runat="server">
                        <table id="Table2" cellspacing="1" cellpadding="1" width="98%" align="center" border="0">
                            <tr>
                                <td class="FormNormalTitle" align="right" width="20%">
                                    <asp:Label ID="lblOrder" runat="server"></asp:Label></td>
                                <td class="SingleLineTextBox" width="30%" colspan="4">
                                    <asp:TextBox ID="txtOrder" runat="server" Width="250" CssClass="SingleLineTextBox"></asp:TextBox></td>
                                <td class="FormNormalTitle" align="right" width="5%" colspan="2">
                                    <asp:Label ID="lblVendor" runat="server"></asp:Label></td>
                                <td class="SingleLineTextBox" width="30%">
                                    <asp:TextBox ID="txtVendor" runat="server" Width="250" CssClass="SingleLineTextBox"></asp:TextBox></td>
                                <tr>
                                    <td class="FormNormalTitle" align="right" width="20%">
                                        <asp:Literal ID="Literal_From" runat="server"></asp:Literal></td>
                                    <td class="SingleLineTextBox" width="15%">
                                        <cc2:DatePicker ID="DateEditor_From" runat="server"></cc2:DatePicker>
                                    </td>
                                    <td class="FormNormalTitle" align="right" width="5%">
                                        <asp:Literal ID="Literal_To" runat="server"></asp:Literal></td>
                                    <td class="SingleLineTextBox" width="20%" colspan="2">
                                        <cc2:DatePicker ID="DateEditor_To" runat="server"></cc2:DatePicker>
                                    </td>
                                </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr align="center">
                <td class="TitleText1">
                    <asp:Label ID="lbTitle" runat="server" Font-Size="Medium"></asp:Label></td>
            </tr>
        </table>
        <table id="Table3" cellspacing="0" cellpadding="0" width="95%" align="center" border="0">
            <tr align="center">
                <td>
                    <asp:DataGrid ID="DG_VendorContract" runat="server" CssClass="TableGlobalOne" Width="100%"
                        AllowPaging="True" AutoGenerateColumns="False" PageSize="30">
                        <SelectedItemStyle CssClass="TableSelectRow"></SelectedItemStyle>
                        <AlternatingItemStyle CssClass="TableAlterRow"></AlternatingItemStyle>
                        <ItemStyle CssClass="TableRow"></ItemStyle>
                        <Columns>
                            <asp:BoundColumn DataField="NumberIndex">
                                <ItemStyle Height="20px" Width="5%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Height="20px" Width="12%"></ItemStyle>
                                <ItemTemplate>
                                    <a class="BlackLink" href="#" onclick='JavaScript:openWin("vch_PurchaseOrder","ProcurementManagement.ContractDatabase.PurchaseOrder","<%# DataBinder.Eval(Container.DataItem,"POID")%>")'>
                                        <%# DataBinder.Eval(Container.DataItem,"POID")%>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="SignDate" DataFormatString="{0:yyy-MM-dd}">
                                <ItemStyle Height="20px" Width="10%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Height="20px" Width="25%"></ItemStyle>
                                <ItemTemplate>
                                    <a class="BlackLink" href="#" onclick='JavaScript:openWin("vch_PurchaseOrder","ProcurementManagement.ContractDatabase.PurchaseOrder","<%# DataBinder.Eval(Container.DataItem,"POID")%>")'>
                                        <%# DataBinder.Eval(Container.DataItem,"POPurpose")%>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ContractTotalCostNatural" DataFormatString="{0:N}">
                                <ItemStyle Height="20px" Width="10%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="secondRecePerson">
                                <ItemStyle Height="20px" Width="18%"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Height="20px" Width="20%"></ItemStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"VendorName")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Visible="False" Height="25px" HorizontalAlign="Right" ForeColor="Black"
                            BackColor="#F7F7DE" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid></td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
            <tr>
                <td colspan="4">
                    <br/>
                    <webdiyer:AspNetPager ID="pager" runat="server" CssClass="mypager" Width="100%" PageSize="15"
                        PagingButtonType="Image" NavigationToolTipTextFormatString="Turn To Page {0}"
                        PageIndexOutOfRangeErrorString="page index out of range" InvalidPageIndexErrorString="the page index is invalid"
                        TextBeforeInputBox="Turn To " DisabledButtonImageNameExtension="g" CpiButtonImageNameExtension="r"
                        ButtonImageNameExtension="n" ImagePath="../Images/aspnetpager/" ShowCustomInfoSection="left"
                        NumericButtonTextFormatString="[{0}]" SubmitButtonText="Submit" InputBoxStyle="border:0px #0000FF solid;border-bottom:1px #000000 solid;border-right:8px #FFFFFF solid;text-align:center"
                        SubmitButtonStyle="border-width:20px;border:1px solid #666666;height:16px;width:35px"
                        PagingButtonSpacing="4px" ShowInputBox="Always" Height="25px" HorizontalAlign="Right">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
            <tr>
                <td>
                    <br/>
                </td>
            </tr>
            <tr valign="top">
                <td class="StatusLine" valign="middle" colspan="4">
                    <asp:Label ID="lblMSG" runat="server" Width="100%"></asp:Label></td>
            </tr>
        </table>
    </form>
  
</body>
</html>
