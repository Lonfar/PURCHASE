<%@ Register TagPrefix="uc1" TagName="CurrencySelector" Src="../../UserControls/DDLRefrence.ascx" %>
<%@ Register TagPrefix="Topis" Namespace="TopisWeb.Controls" Assembly="TopisWeb" %>
<%@ Page language="c#" Codebehind="SystemInit.aspx.cs" AutoEventWireup="false" Inherits="TopisWeb.Initialization.SystemInit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TOPIS:STYLESKIN id="StyleSkin" runat="server"></TOPIS:STYLESKIN>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body id="thebody">
		<TOPIS:PAGEDESCRIPTION id="PageDescription" runat="server" Text=" "></TOPIS:PAGEDESCRIPTION>
		<form id="Form1" method="post" runat="server">
			<br>
			<table class="TableWithBorder" width="100%" align="center">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*">
					<col align="left" width="100">
				</colgroup>
				<tr height="25">
					<td style="FONT-WEIGHT: bold; COLOR: darkblue" colSpan="3" align="left">&nbsp;&nbsp;<asp:literal id=litModifyProjectInfo 
       Text='<%#GetString("litModifyProjectInfo")%>' 
      Runat="server"></asp:literal></td>
				</tr>
				<tr bgcolor="white">
					<td class="RequiredTitle"><asp:literal id=litProjectID 
       Text='<%#GetString("litProjectID")%>' 
      Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtProjectID" Runat="server" Width="100%" MaxLength="32"></asp:textbox></td>
					<td><asp:requiredfieldvalidator id="Requiredfieldvalidator11" runat="server" ControlToValidate="txtProjectID" ErrorMessage='<%#GetString("sRequiredField")%>' ></asp:requiredfieldvalidator></td>
				</tr>
				<tr bgcolor="white">
					<td class="RequiredTitle"><asp:literal 
      id=litProjectName 
      Text='<%#GetString("litProjectName")%>' Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtProjectName" Runat="server" Width="100%"></asp:textbox></td>
					<td><asp:requiredfieldvalidator id="Requiredfieldvalidator12" runat="server" ControlToValidate="txtProjectName"
							ErrorMessage='<%#GetString("sRequiredField")%>' ></asp:requiredfieldvalidator></td>
				</tr>
				<tr bgcolor="white">
					<td class="RequiredTitle"><asp:literal id=litShortName 
       Text='<%#GetString("litShortName")%>' 
      Runat="server"></asp:literal></td>
					<td><asp:textbox id="txtShortName" Runat="server" Width="100%" MaxLength="256"></asp:textbox></td>
					<td><asp:requiredfieldvalidator id="Requiredfieldvalidator13" runat="server" ControlToValidate="txtShortName" ErrorMessage='<%#GetString("sRequiredField")%>' ></asp:requiredfieldvalidator></td>
				</tr>
			</table>
			<br>
			<table class="TableWithBorder" width="100%" align="center">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*">
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
					<td><uc1:currencyselector id="NaturalCurrencySelector" runat="server" ShowEmptyItem="false" Width="100px"></uc1:currencyselector></td>
				</tr>
				<tr bgColor="white">
					<td align="right"><asp:literal id=litStandardCurrency 
       Text='<%#GetString("litStandardCurrency")%>' 
      Runat="server"></asp:literal></td>
					<td>
						<uc1:CurrencySelector id="StandardCurrencySelector" runat="server" Width="100px" ShowEmptyItem="false"></uc1:CurrencySelector></td>
				</tr>
			</table>
			<br>
			<table class="TableWithBorder" width="100%" align="center">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*">
				</colgroup>
				<tr height="25">
					<td style="FONT-WEIGHT: bold; COLOR: darkblue" colSpan="2" align="left">&nbsp;&nbsp;<asp:literal id="Literal1" 
       Text='<%#GetString("litPriceType")%>' 
      Runat="server"></asp:literal></td>
				</tr>
				<tr bgColor="white">
					<td align="right">
					</td>
					<td>
						<asp:RadioButtonList Runat="server" ID="rblPriceType">
							<asp:ListItem Value="0" Selected="True">
								Seperated Price
							</asp:ListItem>
							<asp:ListItem Value="1">
								All PO Price
							</asp:ListItem>
							<asp:ListItem Value="2">
								All Average Price
							</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
			</table>
			<br>
			<table class="TableWithBorder" width="100%" align="center">
				<colgroup>
					<COL align="right" width="150">
					<col align="left" width="*">
				</colgroup>
				<tr height="25">
					<td colspan="2" style="FONT-WEIGHT:bold;COLOR:darkblue" align="left">
						&nbsp;&nbsp;<asp:Literal Runat="server" ID="litMaterialIDRule" Text='<%#GetString("litMaterialIDRule")%>'>
						</asp:Literal>
					</td>
				</tr>
				<tr bgcolor="white">
					<td align="right"><asp:literal id="litMaterialIDLength" Text='<%#GetString("litMaterialIDLength")%>' Runat="server"></asp:literal></td>
					<td>
						<asp:Literal Runat="server" ID="ddlMaterialIDLength"></asp:Literal>
					</td>
				</tr>
				<tr bgcolor="white">
					<td align="right"><asp:literal id="litMaterialIDSegNo" Text='<%#GetString("litMaterialIDSegNo")%>' Runat="server"></asp:literal></td>
					<td><asp:Literal Runat="server" ID="ddlMaterialIDSegNo"></asp:Literal></td>
				</tr>
				<tr bgcolor="white" valign="top">
					<td align="right"><asp:literal id="litSegConfiguration" Text='<%#GetString("litSegConfiguration")%>' Runat="server"></asp:literal></td>
					<td>
						<asp:LinkButton id="btnAddSeg" Runat="server" Text='<%#GetString("btnAddSeg")%>' cssclass="blueunderline" CausesValidation="False" >
						</asp:LinkButton>
						<asp:LinkButton id="btnSubSeg" Runat="server" Text='<%#GetString("btnSubSeg")%>' cssclass="blueunderline" CausesValidation="False" >
						</asp:LinkButton>
						<table runat="server" id="tblSegs" align="left" width="100%" style="BORDER-COLLAPSE: collapse">
							<COLGROUP>
								<COL align="right" width="35">
								<COL align="left" width="155">
								<COL align="left" width="*">
								<COL align="left" width="100">
							</COLGROUP>
							<tr bgcolor="#ffffcc">
								<td width="35"><asp:literal id="litNO" 
       Text='<%#GetString("litNO")%>' 
      Runat="server"></asp:literal></td>
								<td width="155"><asp:literal id="litFormula" 
       Text='<%#GetString("litFormula")%>' 
      Runat="server"></asp:literal></td>
								<td width="*"><asp:literal id="litSegDescription" 
       Text='<%#GetString("litSegDescription")%>' 
      Runat="server"></asp:literal></td>
								<td width="100"></td>
							</tr>
							<tr>
								<td width="35">01</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg1" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription1" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg1" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>'  ControlToValidate="txtSeg1"
										Display="Dynamic">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>02</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg2" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription2" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator2" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg2" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator2" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg2">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>03</td>
								<td><asp:TextBox Runat="server" ID="txtSeg3" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription3" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator3" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg3" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>'  ControlToValidate="txtSeg3"
										Display="Dynamic">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>04</td>
								<td><asp:TextBox Runat="server" ID="txtSeg4" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription4" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator4" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg4" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator4" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg4">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>05</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg5" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription5" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator5" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg5" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator5" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg5">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>06</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg6" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription6" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator6" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg6" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator6" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg6">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>07</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg7" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription7" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator7" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg7" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator7" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg7">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>08</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg8" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription8" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator8" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg8" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator8" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg8">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>09</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg9" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription9" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator9" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg9" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator9" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg9">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
							<tr>
								<td>10</td>
								<td>
									<asp:TextBox Runat="server" ID="txtSeg10" Width="100%" MaxLength="31"></asp:TextBox>
								</td>
								<td><asp:TextBox Runat="server" ID="txtDescription10" Width="100%" MaxLength="100"></asp:TextBox>
								</td>
								<td>
									<asp:RegularExpressionValidator id="Regularexpressionvalidator10" runat="server" ErrorMessage="[ds?]+" ValidationExpression="[ds?]+"
										ControlToValidate="txtSeg10" Display="Dynamic"></asp:RegularExpressionValidator>
									<asp:RequiredFieldValidator Display="Dynamic" id="RequiredFieldValidator10" runat="server" ErrorMessage='<%#GetString("sRequiredField")%>' 
										ControlToValidate="txtSeg10">
									</asp:RequiredFieldValidator>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<div align="center" style="HEIGHT:30px">
				<asp:LinkButton id="btnSave" Runat="server" Text='<%#GetString("btnSave")%>' cssclass="blueunderline">
				</asp:LinkButton>
				<asp:LinkButton id="btnFinishInit" Runat="server" Text='<%#GetString("btnFinishInit")%>'  ForeColor="red">
				</asp:LinkButton>
			</div>
		</form>
		<asp:Literal Runat="server" ID="litScript"></asp:Literal>
	</body>
</HTML>
