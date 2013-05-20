<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<WpcLoggedInUser>" %>
<div id="userinfo">
Firstname = <%=Model.FirstName%><br />
Lastname =<%=Model.LastName%><br />
Email = <%=Model.Email%><br />
Username = <%=Model.UserName%><br />
UserId = <%=Model.UserId%>
</div>

