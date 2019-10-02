<%@ Register TagPrefix="cc1" Namespace="MSPlus.Web.UI.WebControls" Assembly="MSPlus.ToolBarAndMenu" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="aspnetpager" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="Role.aspx.cs" AutoEventWireup="True" Inherits="TopisWeb.Configuration.Role" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:STYLESKIN id="StyleSkin" runat="server"></TOPIS:STYLESKIN>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../Styles/Default.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/Main.CSS" type="text/css" rel="stylesheet">
		<LINK href="../../Styles/menuStyle2003.CSS" type="text/css" rel="stylesheet">
		<SCRIPT language="JavaScript" src="../../MyScripts/Menu.js"></SCRIPT>
		<script language="javascript">
<!--

function tree_oncheck()
{
    var node = trs.getTreeNode(event.treeNodeIndex);
    var Pchecked = node.getAttribute("checked");
    setcheck(node, Pchecked);
    TopisWeb.Configuration.Role.selectChildren(node);
    trs.queueEvent('oncheck', node.getNodeIndex());
}

function setcheck(node, Pc)
{
    var ChildNode = new Array();
    ChildNode = node.getChildren();
    if (parseInt(ChildNode.length) != 0)
    {
        for (var i = 0; i < ChildNode.length; i++)
        {
            var cNode = ChildNode[i];
            if (cNode.getAttribute("checked") != Pc)
            {
                if (parseInt(cNode.getChildren().length) != 0)
                    setcheck(cNode, Pc);
                cNode.setAttribute("checked", Pc);
                trs.queueEvent('oncheck', cNode.getNodeIndex());
            }
        }
    }
}

//-->
		</script>
	</HEAD>
	<body id="thebody" MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE class="TopToolBarLine" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td width="10"></td>
					<TD><cc1:toolbar id="ToolBar1" runat="server"></cc1:toolbar></TD>
				</TR>
			</TABLE>
			<asp:panel id="pnlDetails" Runat="server">
				<TABLE class="TableBlueBorderWhiteBg" width="100%" align="center" border="0">
					<COLGROUP>
						<COL align="right" width="100">
						<COL align="left" width="*">
						<COL align="left" width="100">
					</COLGROUP>
					<TR>
						<TD class="TitleText1" align="center" colSpan="3">
							<asp:Literal id="litModifyInfo" Runat="server"></asp:Literal></TD>
					</TR>
					<TR>
						<TD class="Seperator" colSpan="3"><FONT face="ËÎÌו"></FONT></TD>
					</TR>
					<TR>
						<TD>
							<asp:literal id="litRoleID" Runat="server" Visible="False"></asp:literal></TD>
						<TD>
							<asp:textbox id="txtRoleID" Runat="server" Visible="False" Width="100%" CssClass="ReadOnlyTextBox"
								ReadOnly="True"></asp:textbox></TD>
						<TD></TD>
					</TR>
					<TR>
						<TD class="FormRequiredTitle" width="100">
							<asp:literal id="litRoleName" Runat="server"></asp:literal></TD>
						<TD>
							<asp:textbox id="txtRoleName" Runat="server" Width="100%" MaxLength="100"></asp:textbox></TD>
						<TD width="100"><FONT class="Blue">(100)</FONT>
							<asp:RequiredFieldValidator id="RequiredRoleName" runat="server" ControlToValidate="txtRoleName" Display="Dynamic"></asp:RequiredFieldValidator>
							<asp:RegularExpressionValidator id="Regularexpressionvalidator1" Runat="server" ControlToValidate="txtRoleName"
								Display="Dynamic" ValidationExpression="[^<]*"></asp:RegularExpressionValidator></TD>
					</TR>
					<TR>
						<TD vAlign="top">
							<asp:literal id="litRoleDescription" Runat="server"></asp:literal></TD>
						<TD>
							<asp:textbox id="txtRoleDescription" Runat="server" Width="100%" Rows="3" TextMode="MultiLine"></asp:textbox></TD>
						<TD><FONT class="Blue">(500)</FONT>
							<asp:RegularExpressionValidator id="Regularexpressionvalidator2" Runat="server" ControlToValidate="txtRoleDescription"
								Display="Dynamic" ValidationExpression="[^<]*"></asp:RegularExpressionValidator></TD>
					</TR>
					<TR height="400">
						<TD vAlign="top">
							<asp:literal id="litAssignAuthority" Runat="server"></asp:literal></TD>
						<TD vAlign="top" align="left">
							<DIV style="BORDER-RIGHT: black 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: black 1px solid; OVERFLOW-Y: scroll; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: black 1px solid; WIDTH: 100%; PADDING-TOP: 5px; BORDER-BOTTOM: black 1px solid; HEIGHT: 100%">
								<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
									<TR>
										<TD vAlign="top">
											<iewc:TreeView id="trs" runat="server"></iewc:TreeView></TD>
										<TD>
											<asp:Table id="tblAuthority" Runat="server" Width="100%" BorderColor="Gray" CellPadding="3"
												GridLines="Both" CellSpacing="0" BorderStyle="Solid"></asp:Table></TD>
									</TR>
								</TABLE>
							</DIV>
						</TD>
						<TD><FONT face="ËÎÌו"></FONT></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<br>
			<TABLE id="Table2" width="95%" align="center" border="0">
				<TBODY>
					<TR vAlign="top">
						<TD class="StatusLine" valign="middle">
							<asp:Label id="lblMSG" runat="server" Width="100%"></asp:Label>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		<asp:literal id="litScript" Runat="server"></asp:literal>
	</body>
</HTML>
