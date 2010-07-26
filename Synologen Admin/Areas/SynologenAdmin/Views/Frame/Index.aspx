<%@ Page MasterPageFile="~/Components/Synologen/SynologenMain.master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.FrameListView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Frame-Index-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>FrameListView</legend>
				<p class="formItem">
					<%= Html.LabelFor(x => x.SearchWord) %>
					<%= Html.EditorFor(x => x.SearchWord) %>
				</p>
				<p class="formCommands">
					<input type="submit" value="Search" class="btnBig" />
				</p>
			</fieldset>
			<% } %>
			<div class="wpcPager">
				<!--
				<input type="submit" value="« First" class="btnSmall"/>
				<input type="submit" value="« Previous" class="btnSmall"/>
				<input type="submit" value="Next »" class="btnSmall"/>
				<input type="submit"  value="Set" class="btnSmall"/>
				<input type="submit" value="Last »" class="btnSmall"/>
				Page Size
				<input type="text" value="<%=Model.List.PageSize %>" style="width: 30px; "/>				
				-->
				<%= Html.ActionLink("« First", "Index", new {search=Model.SearchWord, page = 1}) %>
				&nbsp;|&nbsp;
				<%if (Model.List.HasPreviousPage){%>
					<%=Html.ActionLink("« Previous", "Index", new {search = Model.SearchWord, page = Model.List.Page - 1})%>
				<%}else{%>
					« Previous
				<%} %>
				&nbsp;|&nbsp;Page <%=Model.List.Page %> of <%=Model.List.NumberOfPages %> (<%=Model.List.Total%> items)&nbsp;|&nbsp;
				<%if (Model.List.HasNextPage){%>
					<%= Html.ActionLink("Next »", "Index", new {search=Model.SearchWord, page = Model.List.Page + 1}) %>
				<%}else{%>
					Next »
				<%} %>	
				&nbsp;|&nbsp;			
				<%= Html.ActionLink("Last »", "Index", new {search=Model.SearchWord, page = Model.List.NumberOfPages}) %>
			</div>
			<div>
				<table class="striped" style="border-collapse: collapse; ">
					<tr class="header"><th>Id</th><th>Namn</th></tr>
					<% foreach (var frame in Model.List){ %>
					<tr>
						<td><%=frame.Id %></td>
						<td><%=frame.Name %></td>
					</tr>
					<% } %>
				</table>
			</div>
		</div>
	</div>
</div>
</asp:Content>
