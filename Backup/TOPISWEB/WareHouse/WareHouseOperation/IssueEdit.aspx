<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../../UserControls/ApproveStateInfo.ascx" %>

<%@ Page Language="c#" Codebehind="IssueEdit.aspx.cs" AutoEventWireup="True" EnableEventValidation="false"   Inherits="TopisWeb.WareHouseManagment.IssueEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>IssueEdit</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../../MyScripts/Menu.js"></script>

    <script language="JavaScript" src="../../MyScripts/Tab.js"></script>

    <link href="../../Styles/Default.CSS" type="text/css" rel="stylesheet" />
    <link href="../../Styles/Main.CSS" type="text/css" rel="stylesheet" />
    <link href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
		    
		    function showDialog()
		    {
                var returnValue = window.showModalDialog('../../MaterialPurchase/MaterialList.aspx','','status:No;scroll:Yes;dialogWidth:800px;dialogHeight:600px;edge:raised;unadorned:Yes;resizable:Yes;location:No;'); 
                
                if(returnValue != null && typeof(returnValue) != "undefined" )
                {                    
                    document.getElementById("hidButton").click();
                }                
		    }

    </script>

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
        <table id="Table1" cellspacing="1" cellpadding="1" width="95%" align="center" border="0">
            <tbody>
                <tr valign="top">
                    <td>
                        <p>
                            <font face="ËÎÌו"></font>&nbsp;</p>
                    </td>
                </tr>
                <tr valign="top">
                    <td style="height: 55px">
                        <msp:TabControl ID="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080"
                            Width="800px">
                            <msp:TabPage ID="tabPage1">
                                <table id="Tbl1" cellspacing="1" cellpadding="1" width="100%" border="0" align="center">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnSubmit" runat="server" CausesValidation="False"></asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <uc1:UCEdit runat="server" ID="ucEdit_Issue"></uc1:UCEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager>
                                        </td>
                                    </tr>
                                </table>
                            </msp:TabPage>
                            <msp:TabPage ID="tabPage2">
                                <table id="Tbl4" cellspacing="1" cellpadding="1" width="100%" border="0" align="center">
                                    <tr>
                                        <td align="right">
                                            <asp:Button id="exportMaterialList" type="button"  value="button" visible="false" OnClientClick="showDialog();"
                                                runat="server"  Text = "Add Material"/>
                                            <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CausesValidation="False"></asp:Button>
                                            
                                            <asp:Button ID="btnRefresh" runat="server" CausesValidation="False"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td>
                                            <uc1:ChildEditControl runat="server" ID="Child_IssueMaterial"></uc1:ChildEditControl>
                                        </td>
                                    </tr>
                                </table>
                            </msp:TabPage>
                            <msp:TabPage ID="tabPage3">
                                <table id="Tbl3" cellspacing="1" cellpadding="1" width="100%" align="center" border="0">
                                    <tr valign="top">
                                        <td>
                                            <uc1:ApproveStateInfo ID="ApproveStateInfo1" runat="server"></uc1:ApproveStateInfo>
                                        </td>
                                    </tr>
                                </table>
                            </msp:TabPage>
                        </msp:TabControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="../../Configuration/BasicInfo/AFE.aspx" target="_blank" style="text-decoration: underline; color:blue">
                             AFE</a>&nbsp;&nbsp;&nbsp;<a href="../WareHouseManagment/InStoreMaterialDetail.aspx?moduleID=WareHouse.InStoreMaterialDetail"
                                target="_blank" style="text-decoration: underline; color:blue">Material Inventory Query</a>&nbsp;&nbsp;&nbsp;<a
                                    href="../WareHouseManagment/WH_Material_Receive_Query.aspx?moduleID=WareHouse.WareHouseManagment.MaterialReceivingQuery"
                                    target="_blank" style="text-decoration: underline; color:blue">Material Receiving Query</a>&nbsp;&nbsp;&nbsp;<a
                                        href="Issue.aspx?moduleID=WareHouse.Issue" target="_blank" style="text-decoration: underline; color:blue">Material
                                        Issue Query</a></td>
                </tr>
                <tr valign="top">
                    <td class="StatusLine" valign="middle">
                        <asp:Label ID="lblMSG" runat="server" Width="100%"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
  
    <asp:Literal ID="lblErrorMsg" Visible="False" runat="server"></asp:Literal>
    <input id="hidButton" type="button" value="button" onserverclick="hidButton_ServerClick" runat="server" style="display:none" causesvalidation="false" />
      </form>
</body>
</html>
