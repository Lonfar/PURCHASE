<%@ Register TagPrefix="uc1" TagName="AttachmentManager" Src="../UserControls/AttachmentManager.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ChildEditControl" Src="../UserControls/ChildEditControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UCEdit" Src="../UserControls/UCEdit.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="msp" Namespace="Cnwit" Assembly="Cnwit.TabControl" %>
<%@ Register TagPrefix="uc1" TagName="RefEditor" Src="../UserControls/RefEditor.ascx" %>
<%@ Register TagPrefix="uc5" TagName="RefButton" Src="../UserControls/RefButton.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ApproveStateInfo" Src="../UserControls/ApproveStateInfo.ascx" %>
<%@ Page language="c#" Codebehind="MaterialRequest_Edit.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.MaterialPurchase.MaterialRequest_Edit" %>
<%@ Register TagPrefix="uc1" TagName="ModuleViewer" Src="../UserControls/ModuleViewer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>MaterialRequest_Edit</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/TabControl/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../MyScripts/Menu.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../MyScripts/Tab.js"></SCRIPT>
		
		<script type="text/javascript" language="javascript">
		    
		    function showDialog()
		    {
                var returnValue = window.showModalDialog('../MaterialPurchase/ShowMaterialList.aspx','','status:No;scroll:Yes;dialogWidth:650px;dialogHeight:600px;edge:raised;unadorned:Yes;resizable:Yes;location:No;'); 
                
                if(returnValue != null && typeof(returnValue) != "undefined" )
                {                    
                    document.getElementById("hidButton").click();
                }                
		    }

		</script>	
		
</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="95%" align="center" border="0">
				<TR vAlign="top">
					<TD>
						<P>&nbsp;</P>
					</TD>
				</TR>
				<TR vAlign="top">
					<TD style="HEIGHT: 55px"><msp:tabcontrol id="TabControl1" runat="server" BorderColor="Red" BackColor="#FF8080" Width="800px">
							<msp:TabPage ID="tabPage1">
								<TABLE id="Tblucvoucheredit1" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<tr>
										<td align="right">
										    <asp:Button ID="btnDirectApproved" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnMisDepApproved" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnApproved" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnSubmit" Runat="server" CausesValidation="False"></asp:Button>
											<asp:Button ID="btnCancel" Runat="server" CausesValidation="False"></asp:Button>
											<uc5:RefButton id="RefButton1" runat="server"></uc5:RefButton>
										</td>
									</tr>
									<TR vAlign="top">
										<TD align="center">
											<uc1:ucedit runat="server" ID="ucEdit_MaterialRequest"></uc1:ucedit>
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage2">
								<TABLE id="Tblucvoucheredit2" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<tr>
										<td align="right"> 
										   <input id="exportMaterialList" type="button" value="button" onclick="showDialog();" runat="server" />
                                           <asp:Button ID="btnInputExcel" Runat="server" CausesValidation="False"></asp:Button>
                                           <a href="../Template/MR_Material_Template.xls" target="_blank">Material List Templete</a>
										</td>
									</tr>
									<TR vAlign="top">
										<TD>
											<uc1:ChildEditControl id="ChildEdit_MaterialList" runat="server"></uc1:ChildEditControl>											
										</TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage3">
								<TABLE id="tblViewer" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
									<TR vAlign="top">
										<TD>
											<uc1:ModuleViewer id="ModuleViewer1" runat="server"></uc1:ModuleViewer></TD>
									</TR>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage4">
								<TABLE id="Tblucvoucheredit4" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<tr>
										<td valign="top">
											<uc1:AttachmentManager runat="server" ID="AttachmentManager1"></uc1:AttachmentManager></td>
									</tr>
								</TABLE>
							</msp:TabPage>
							<msp:TabPage ID="tabPage5">
								<TABLE id="Tblucvoucheredit5" cellSpacing="1" cellPadding="1" width="100%" border="0" align="center">
									<TR vAlign="top">
										<TD>
											<uc1:ApproveStateInfo id="ApproveStateInfo1" runat="server"></uc1:ApproveStateInfo></TD>
									</TR>
								</TABLE>
							</msp:TabPage>
						</msp:tabcontrol></TD>
				</TR>
				<TR vAlign="top">
					<TD class="StatusLine" vAlign="middle"><asp:label id="lblMSG" runat="server" Width="100%"></asp:label><asp:label id="lbltemp" runat="server" Visible="False"></asp:label></TD>
				</TR>
			</TABLE>
			<asp:literal id="lbError" runat="server"></asp:literal>
             <input id="hidButton" type="button" value="button" onserverclick="hidButton_ServerClick" runat="server" style="display:none" causesvalidation="false" />
             </form>
	</body>
</HTML>
