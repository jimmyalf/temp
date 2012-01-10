<%@ Control %>
<h2>Komponenter</h2>
<%= Html.Action("Menu",	"WpcContent", new { area = "WpcContent", settings = new { Class = "component-navigation", ShowRootLevel = false, ShowDefaultPage = false, StartAtLevel = 2, StopAtLevel = 2 } }) %>