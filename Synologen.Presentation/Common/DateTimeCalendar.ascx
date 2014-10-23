<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Common.WebControls.DateTimeCalendar" %>
<div class="DateTimeCalendar-ascx">
<div>
	<asp:Literal ID="ltDate" runat="server">No date!</asp:Literal>
	<asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Visible="False" Text="Clear" CssClass="calendarBtn" />
	<asp:Button ID="btnNewCancel" runat="server" OnClick="btnNewCancel_Click" CausesValidation="false" Text="New date" CssClass="calendarBtn" />
	<asp:Button ID="btnSetDate" runat="server" OnClick="btnSetDate_Click" CausesValidation="true" Text="Set" Visible="False" CssClass="calendarBtn" />
</div>

<div id="dCalendarContainer" runat="server" visible="false" class="calendarContainer">
<div class="calendarWrap">
<div class="calendarItem">
<asp:Calendar ID="dtiDateCalendar" runat="server" DayNameFormat="Shortest" CssClass="calendarControl">
    <SelectedDayStyle CssClass="SelectedDayStyle" />
    <TodayDayStyle CssClass="TodayDayStyle" />
    <SelectorStyle CssClass="SelectorStyle" />
    <WeekendDayStyle CssClass="WeekendDayStyle" />
    <OtherMonthDayStyle CssClass="OtherMonthDayStyle" />
    <NextPrevStyle CssClass="NextPrevStyle" />
    <DayHeaderStyle CssClass="DayHeaderStyle" />
    <TitleStyle CssClass="TitleStyle" />
    <DayStyle CssClass="DayStyle" />
</asp:Calendar>
</div>

<div class="calendarItem">
	<asp:Label ID="lblTime" runat="server" AssociatedControlID="txtTime" SkinID="Long">Time (HH:MM)<asp:RegularExpressionValidator ID="vldxTimeFormat" runat="server" ControlToValidate="txtTime" Display="Dynamic" ErrorMessage="Wrong time format!" ValidationExpression="[0-2]{1}[0-9]{1}[: /.][0-5]{1}[0-9]{1}" Width="120px">(Wrong time format!)</asp:RegularExpressionValidator></asp:Label>
	<asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
</div>
</div>
</div>
</div>