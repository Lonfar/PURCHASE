<%@ Page language="c#" Codebehind="Project.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Configuration.Project" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:StyleSkin id="ProjectStyleSkin" runat="server"></TOPIS:StyleSkin>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta http-equiv="Pragma" content="no-cache">
	</HEAD>
	<BODY id="thebody">
		<TOPIS:PAGEDESCRIPTION id="ProjectPageDescription" runat="server" Text=" "></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<br>
			<table class="TableWithBorder" width="100%" align="center" border="1" bordercolor="gray"
				cellpadding="3">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*" style="COLOR:darkblue">
				</colgroup>
				<tr height="25">
					<td style="FONT-WEIGHT: bold; COLOR: darkblue" colSpan="2" align="left">&nbsp;&nbsp;<asp:literal id="Literal1" 
       Text='<%#GetString("litProjectInfo")%>' 
      Runat="server"></asp:literal></td>
				</tr>
				<tr bgcolor="white">
					<td><asp:literal id="Literal2" 
       Text='<%#GetString("litProjectID")%>' 
      Runat="server"></asp:literal></td>
					<td>
						<asp:Label Runat="server" ID="lblProjectID"></asp:Label>
					</td>
				</tr>
				<tr bgcolor="white">
					<td><asp:literal 
      id="Literal3" 
      Text='<%#GetString("litProjectName")%>' Runat="server"></asp:literal></td>
					<td><asp:Label Runat="server" ID="lblProjectName"></asp:Label></td>
				</tr>
				<tr bgcolor="white">
					<td><asp:literal id="Literal4" 
       Text='<%#GetString("litShortName")%>' 
      Runat="server"></asp:literal></td>
					<td><asp:Label Runat="server" ID="lblShortName"></asp:Label></td>
				</tr>
			</table>
			<br>
			<table class="TableWithBorder" width="100%" align="center" border="1" bordercolor="gray"
				cellpadding="3">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*" style="COLOR:darkblue">
				</colgroup>
				<tr height="25">
					<td style="FONT-WEIGHT: bold; COLOR: darkblue" colSpan="2" align="left">&nbsp;&nbsp;<asp:literal id=litSysCurrency 
       Text='<%#GetString("litSysCurrency")%>' 
      Runat="server"></asp:literal></td>
				</tr>
				<tr bgColor="white">
					<td align="right"><asp:literal 
      id=litNaturalCurrency 
      Text='<%#GetString("litNaturalCurrency")%>' 
    Runat="server"></asp:literal></td>
					<td><asp:Label Runat="server" ID="lblNaturalCurrency"></asp:Label></td>
				</tr>
				<tr bgColor="white">
					<td align="right"><asp:literal id=litStandardCurrency 
       Text='<%#GetString("litStandardCurrency")%>' 
      Runat="server"></asp:literal></td>
					<td>
						<asp:Label Runat="server" ID="lblStandardCurrency"></asp:Label></td>
				</tr>
			</table>
			<br>
			<table class="TableWithBorder" width="100%" align="center" border="1" bordercolor="gray"
				cellpadding="3">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*" style="COLOR:darkblue">
				</colgroup>
				<tr height="25">
					<td colspan="2" style="FONT-WEIGHT:bold;COLOR:darkblue" align="left">
						&nbsp;&nbsp;<asp:Literal Runat="server" ID="litMaterialIDRule" Text='<%#GetString("litMaterialIDRule")%>'>
						</asp:Literal>
					</td>
				</tr>
				<tr bgcolor="white">
					<td align="right"><asp:literal id="litMaterialIDLength" 
       Text='<%#GetString("litMaterialIDLength")%>' 
      Runat="server"></asp:literal></td>
					<td>
						<asp:Label Runat="server" ID="lblMaterialIDLength"></asp:Label>
					</td>
				</tr>
				<tr bgcolor="white">
					<td align="right"><asp:literal id="litMaterialIDSegNo" 
       Text='<%#GetString("litMaterialIDSegNo")%>' 
      Runat="server"></asp:literal></td>
					<td><asp:Label Runat="server" ID="lblMaterialIDSegNumber"></asp:Label></td>
				</tr>
				<tr bgcolor="white" valign="top">
					<td align="right"><asp:literal id="litSegConfiguration" 
       Text='<%#GetString("litSegConfiguration")%>'
      Runat="server"></asp:literal></td>
					<td>
						<asp:LinkButton id="btnAddSeg" Runat="server" Text='<%#GetString("btnAddSeg")%>' cssclass="blueunderline" CausesValidation="False" >
						</asp:LinkButton>
						<asp:LinkButton id="btnSubSeg" Runat="server" Text='<%#GetString("btnSubSeg")%>' cssclass="blueunderline" CausesValidation="False" >
						</asp:LinkButton>
						<table runat="server" id="tblSegs" align="left" width="100%" style="BORDER-COLLAPSE: collapse">
							<tr bgcolor="#ffffcc">
								<td width="35"><asp:literal id="litNO" 
       Text='<%#GetString("litNO")%>' 
      Runat="server"></asp:literal></td>
								<td width="*"><asp:literal id="litLength" 
       Text='<%#GetString("litLength")%>' 
      Runat="server"></asp:literal></td>
								<td width="155"><asp:literal id="litFormula" 
       Text='<%#GetString("litFormula")%>' 
      Runat="server"></asp:literal></td>
								<td width="*"><asp:literal id="litFullFormula" 
       Text='<%#GetString("litFullFormula")%>' 
      Runat="server"></asp:literal></td>
								<td width="*"><asp:literal id="litSegDescription" 
       Text='<%#GetString("litSegDescription")%>' 
      Runat="server"></asp:literal></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div align="center" style="HEIGHT:30px">
			</div>
			<table width="100%" align="center" class="TableBlueBorderWhiteBg">
				<colgroup>
					<COL align="right" width="100">
					<col width="*" align="left">
					<col width="100" align="left">
				</colgroup>
				<tr>
					<td colspan="3" class="TableTitle">
						<asp:Literal Runat="server" ID="litModifyProjectInfo" Text='<%#GetString("litModifyProjectInfo")%>'>
						</asp:Literal>
					</td>
				</tr>
				<tr>
					<td class="Seperator" colspan="3"></td>
				</tr>				
				<tr>
					<td class="RequiredTitle" valign="top"><asp:literal id=litAddress 
      Text='<%#GetString("litAddress")%>' Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtAddress" Runat="server" Width="100%" TextMode="MultiLine" Rows="3"></asp:textbox></td>
					<td><font class="Blue">(512)</font>
						<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>'  ControlToValidate="txtAddress">
						</asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td><asp:literal id=litFax 
      Text='<%#GetString("litFax")%>' Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtFax" Runat="server" Width="100%" MaxLength="64"></asp:textbox></td>
					<td><font class="Blue">(64)</font></td>
				</tr>
				<tr>
					<td><asp:literal id=litTel 
      Text='<%#GetString("litTel")%>' Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtTel" Runat="server" Width="100%" MaxLength="64"></asp:textbox></td>
					<td><font class="Blue">(64)</font></td>
				</tr>
				<tr>
					<td><asp:literal id=litEmail 
      Text='<%#GetString("litEmail")%>' Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtEmail" Runat="server" Width="100%" MaxLength="64"></asp:textbox></td>
					<td><font class="Blue">(64)</font>
						<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid Email"
							ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
				</tr>
				<tr>
					<td><asp:literal id=litContact 
      Text='<%#GetString("litContact")%>' Runat="server"></asp:literal></td>
					<td><asp:TextBox Runat="server" ID="txtContact" Width="100%" MaxLength="64"></asp:TextBox></td>
					<td><font class="Blue">(64)</font></td>
				</tr>
				<tr>
					<td><asp:literal id="litBank"   Text='<%#GetString("litBank")%>' Runat="server"></asp:literal></td>
					<td><asp:TextBox Runat="server" ID="txtBank" Width="100%" MaxLength="100"></asp:TextBox></td>
					<td><font class="Blue">(100)</font></td>
				</tr>
				<tr>
					<td><asp:literal id="litAccountNo"   Text='<%#GetString("litAccountNo")%>' Runat="server"></asp:literal></td>
					<td><asp:TextBox Runat="server" ID="txtAccountNo" Width="100%" MaxLength="100"></asp:TextBox></td>
					<td><font class="Blue">(100)</font></td>
				</tr>
				<tr>
					<td><asp:literal id="litTaxNo"   Text='<%#GetString("litTaxNo")%>' Runat="server"></asp:literal></td>
					<td><asp:TextBox Runat="server" ID="txtTaxNo" Width="100%" MaxLength="100"></asp:TextBox></td>
					<td><font class="Blue">(100)</font></td>
				</tr>
				<tr>
					<td><asp:literal id="litAttorney"   Text='<%#GetString("litAttorney")%>' Runat="server"></asp:literal></td>
					<td><asp:TextBox Runat="server" ID="txtAttorney" Width="100%" MaxLength="50"></asp:TextBox></td>
					<td><font class="Blue">(50)</font></td>
				</tr>
				<tr>
					<td><asp:literal id="litDeputy"   Text='<%#GetString("litDeputy")%>' Runat="server"></asp:literal></td>
					<td><asp:TextBox Runat="server" ID="txtDeputy" Width="100%" MaxLength="50"></asp:TextBox></td>
					<td><font class="Blue">(50)</font></td>
				</tr>
				<tr>
					<td valign="top">
						<asp:literal id="litProjectLogo" 
      Text='<%#GetString("litProjectLogo")%>' Runat="server">
						</asp:literal>
					</td>
					<td>
						<div style="BORDER-RIGHT:black 1px solid; PADDING-RIGHT:5px; BORDER-TOP:black 1px solid; PADDING-LEFT:5px; OVERFLOW-X:auto; PADDING-BOTTOM:5px; BORDER-LEFT:black 1px solid; WIDTH:100%; PADDING-TOP:5px; BORDER-BOTTOM:black 1px solid">
							<asp:literal id="litUploadLogo" 
      Text='<%#GetString("litUploadLogo")%>' Runat="server">
							</asp:literal>
							<br>
							<INPUT id="uplTheFile" type="file" size="60" name="uplTheFile" runat="server">
							<asp:LinkButton id="btnUploadImage" Runat="server" Text='<%#GetString("btnUploadImage")%>' cssclass="blueunderline" CausesValidation="False" Visible="False" >
							</asp:LinkButton>
							<br>
							<br>
							<asp:HyperLink Runat="server" ID="RefreshLink">
								<asp:literal id="litProjectLogoPreview" Text='<%#GetString("litProjectLogoPreview")%>' Runat="server">
								</asp:literal>
							</asp:HyperLink>
							<br>
							<asp:Image Runat="server" ID="imgLogo" BorderWidth="0" AlternateText="Project Logo"></asp:Image>
						</div>
					</td>
					<td></td>
				</tr>
				<tr>
					<td colspan="3" class="TableTail">
						<asp:LinkButton Runat="server" ID="btnSave" Text='<%#GetString("btnSave")%>' cssclass="blueunderline">
						</asp:LinkButton>
					</td>
				</tr>
			</table>
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal>
	</BODY>
</HTML>
