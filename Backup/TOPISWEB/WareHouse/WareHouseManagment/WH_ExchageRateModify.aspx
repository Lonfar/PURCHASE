<%@ Page Language="C#" AutoEventWireup="true" Codebehind="WH_ExchageRateModify.aspx.cs"
    Inherits="TopisWeb.WareHouse.WareHouseManagment.WH_ExchageRateModify" %>

<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="uc1" TagName="UCEditSearch" Src="../../UserControls/UCEditSearch.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UCList" Src="../../UserControls/UCList.ascx" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>InStoreMaterialDetail</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
    <link href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../../MyScripts/Menu.js"></script>

    <script language="JavaScript" src="../../MyScripts/DatePicker.js"></script>

    <script language="JavaScript" src="../../MyScripts/Tab.js"></script>

</head>
<body ms_positioning="FlowLayout">
    <form id="Form1" method="post" runat="server">
        <table class="TopToolBarLine" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td width="10">
                </td>
                <td>
                    <cc1:ToolBar ID="ToolBar1" runat="server">
                    </cc1:ToolBar></td>
            </tr>
        </table>
        <br />
         <br />
        <table  cellspacing="0" cellpadding="0" width="95%" border="0" align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="Label3" runat="server" Text="Exchage Rate Adjustment" Font-Bold="True" Font-Names="Arial" Font-Size="Small"></asp:Label></td>
            </tr>
        </table>
        <br />
         <br />
        <table class="SearchTableArea" cellspacing="0" cellpadding="0" width="95%" border="0" align="center"  >
            <tr>
                
                <td style="height: 22px"><asp:Label ID="Label2" runat="server" Text="PO No.:" Font-Bold="True" Font-Names="Arial"></asp:Label><asp:TextBox ID="txtPOID" runat="server"></asp:TextBox><asp:Button ID="btnSearch" CssClass="graybutton" Text="Search"
                    runat="server" OnClick="btnSearch_Click" CausesValidation="False" />
                    <asp:Button ID="btnView" CssClass="graybutton" Text="View"
                    runat="server" OnClick="btnView_Click" CausesValidation="False"/>
                </td>
                <td style="height: 22px">
                    <asp:Label ID="Label1" runat="server" Text="Exchange Rate:" Font-Bold="True" ForeColor="Red" Font-Names="Arial"></asp:Label><asp:TextBox ID="txtER" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtER"
                        Display="Dynamic" ErrorMessage="Format Error!" ValidationExpression="^(([1-9]\d*(\.\d+)?)|(0\.\d*[1-9]\d*))$"></asp:RegularExpressionValidator></td>
                <td style="height: 22px">
                    <asp:Button ID="btnConfirm" CssClass="graybutton" Text="Confirm"
                    runat="server" OnClick="btnConfirm_Click" />
                </td>
            </tr>
        </table>
         <br />
          <br />
        <table id="Table3" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
            <tr valign="top">
                <td>
                    <uc2:UCList ID="Uclist1" runat="server" NeedSearch="false"></uc2:UCList>
                </td>
            </tr>
            <tr valign="top">
                <td class="StatusLine" valign="middle">
                    <asp:Label ID="lblMSG" runat="server" Width="100%"></asp:Label></td>
            </tr>
        </table>
        
    </form>
</body>
</html>
