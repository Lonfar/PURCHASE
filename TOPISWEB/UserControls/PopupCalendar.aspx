<%@ Page language="c#" Codebehind="PopupCalendar.aspx.cs" AutoEventWireup="True" Inherits="UserControls.PopupCalendar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>litDatePicker</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<base target="_parent">
		<meta http-equiv="Pragma" content="no-cache">
		<style>TD { FONT-SIZE: 12px; FONT-FAMILY: Arial }
	TABLE.calendar { BORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #ffffff 1px solid; BORDER-LEFT: #ffffff 1px solid; BORDER-BOTTOM: #ffffff 1px solid }
	TD.calday { BORDER-LEFT-COLOR: #ffffff; BORDER-BOTTOM-COLOR: #425d8c; CURSOR: hand; BORDER-TOP-STYLE: solid; BORDER-TOP-COLOR: #ffffff; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; BACKGROUND-COLOR: #e7e7e7; TEXT-ALIGN: center; BORDER-RIGHT-COLOR: #425d8c; BORDER-BOTTOM-STYLE: solid }
	TD.calweekday { BORDER-LEFT-COLOR: #ffffff; BORDER-BOTTOM-COLOR: #425d8c; CURSOR: hand; COLOR: #ff0000; BORDER-TOP-STYLE: solid; BORDER-TOP-COLOR: #ffffff; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; BACKGROUND-COLOR: #e7e7e7; TEXT-ALIGN: center; BORDER-RIGHT-COLOR: #425d8c; BORDER-BOTTOM-STYLE: solid }
	TD.selcalday { BORDER-RIGHT: #425d8c 1px solid; BORDER-TOP: #425d8c 1px solid; BORDER-LEFT: #425d8c 1px solid; COLOR: blue; BORDER-BOTTOM: #425d8c 1px solid; BACKGROUND-COLOR: white; TEXT-ALIGN: center }
	TD.dayheader { BORDER-RIGHT: #425d8c 1px solid; BORDER-TOP: #425d8c 1px solid; BORDER-LEFT: #425d8c 1px solid; COLOR: #ffffff; BORDER-BOTTOM: #425d8c 1px solid; BACKGROUND-COLOR: #425d8c }
	TD.today { BORDER-RIGHT: red 1px solid; BORDER-TOP: red 1px solid; BORDER-LEFT: red 1px solid; COLOR: red; BORDER-BOTTOM: red 1px solid; BACKGROUND-COLOR: white; TEXT-ALIGN: center }
	TD.caltitle { COLOR: red; TEXT-ALIGN: center }
	TD.calotherday { COLOR: gray }
	.DropDownList { FONT-SIZE: 11px; WIDTH: 60px; FONT-FAMILY: Arial }
		</style>
	</HEAD>
	<body style="MARGIN: 0px">
		<form id="Form1" method="post" runat="server">
			<table height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
				<tr>
					<td vAlign="middle" align="center"><font style="FONT-SIZE: 12px; COLOR: blue; FONT-FAMILY: arial">
							<asp:Literal Runat="server" ID="litDatePicker" Text="litDatePicker">
							</asp:Literal>
						</font>
						<asp:dropdownlist id="ddlYear" CssClass="DropDownList" Runat="server" AutoPostBack="True" onselectedindexchanged="ddlYear_SelectedIndexChanged"></asp:dropdownlist>
						<asp:DropDownList Runat="server" ID="ddlMonth" CssClass="DropDownList" AutoPostBack="True" onselectedindexchanged="ddlMonth_SelectedIndexChanged">
							<asp:ListItem>01</asp:ListItem>
							<asp:ListItem>02</asp:ListItem>
							<asp:ListItem>03</asp:ListItem>
							<asp:ListItem>04</asp:ListItem>
							<asp:ListItem>05</asp:ListItem>
							<asp:ListItem>06</asp:ListItem>
							<asp:ListItem>07</asp:ListItem>
							<asp:ListItem>08</asp:ListItem>
							<asp:ListItem>09</asp:ListItem>
							<asp:ListItem>10</asp:ListItem>
							<asp:ListItem>11</asp:ListItem>
							<asp:ListItem>12</asp:ListItem>
						</asp:DropDownList>
						<asp:ImageButton Runat="server" ID="btnToday" AlternateText="Today" ImageUrl="../Images/Control/RefreshCal.jpg" ></asp:ImageButton>
						<asp:Calendar id="Calendar1" runat="server" CssClass="calendar" DayNameFormat="Short" CellSpacing="2"
							ShowGridLines="True" NextPrevFormat="FullMonth" ShowDayHeader="True" Width="250px" onselectionchanged="Calendar1_SelectionChanged">
							<DayStyle CssClass="calday"></DayStyle>
							<DayHeaderStyle CssClass="dayheader"></DayHeaderStyle>
							<OtherMonthDayStyle CssClass="calotherday"></OtherMonthDayStyle>
							<SelectedDayStyle CssClass="selcalday"></SelectedDayStyle>
							<TitleStyle CssClass="caltitle"></TitleStyle>
							<WeekendDayStyle CssClass="calweekday"></WeekendDayStyle>
							<TodayDayStyle CssClass="today"></TodayDayStyle>
						</asp:Calendar>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
