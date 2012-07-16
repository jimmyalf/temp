<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.Add_Component.CourseList" Codebehind="CourseList.ascx.cs" %>
<div class="Component-Courses-AddComponent-CourseList-ascx fullBox">
<div class="wrap">
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
		<div class="formItem">
		    <asp:Label ID="lblCourseCat" runat="server" AssociatedControlID="drpCourseCat" SkinId="Long"/>
		    <asp:DropDownList ID="drpCourseCat" runat="server"/>
		</div>
		<div class="formItem">
		    <asp:Label ID="lblMax" runat="server" AssociatedControlID="txtMax" SkinId="Long"/>
            <asp:TextBox ID="txtMax" runat="server"></asp:TextBox>
		</div>
		
	</fieldset>
</div>
</div>