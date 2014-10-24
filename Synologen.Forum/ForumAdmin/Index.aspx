<%@ Import Namespace="Spinit.Wpc.Base.Business" %>
<%@ Import Namespace="Spinit.Wpc.Utility.Business" %>
<%@ Page Language="C#" MasterPageFile="~/BaseMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Index" Codebehind="Index.aspx.cs" %>
<asp:Content ID="MainComponent" ContentPlaceHolderID="ComponentContent" Runat="Server">
<div id="dCompMain" class="Index-aspx">
	<div class="fullBox">
	<div class="wrap">
	<h1>Home</h1>

	<p>You are logged on as <strong><ASP:LABEL id="lblName" runat="server">Label</ASP:LABEL></strong> and member of
	<strong>
	<asp:Repeater ID="rptGroups" runat="server">
		<ItemTemplate><%# DataBinder.Eval(Container.DataItem, "cName") %></ItemTemplate>
		<SeparatorTemplate>, </SeparatorTemplate>
	</asp:Repeater>
	</strong>
	</p>
	</div>
	</div>

	<div class="columnBoxLeft">
	<div class="wrap">
	<h2>Latest changes</h2>
	<ASP:DATAGRID id="dgrLatestChanges" runat="server" autogeneratecolumns="False" showheader="False" OnItemCreated="dgrLatestChanges_ItemCreated" DataKeyField="ComponentName" SkinID="Striped">
	<COLUMNS>
		<ASP:BOUNDCOLUMN datafield="EditedDate"></ASP:BOUNDCOLUMN>
		<ASP:BOUNDCOLUMN datafield="ComponentName" dataformatstring=" - {0} / "></ASP:BOUNDCOLUMN>
		<ASP:TemplateColumn>
			<ItemTemplate>
				<ASP:HYPERLINK runat="server" id="hplChangedItem">
					<%# cutIfToLong(DataBinder.Eval(Container.DataItem, "ItemName").ToString(),15) %>
				</ASP:HYPERLINK>
			</ItemTemplate>
		</ASP:TemplateColumn>
		<ASP:BOUNDCOLUMN datafield="EditedByName" dataformatstring=" by {0}"></ASP:BOUNDCOLUMN>
	</COLUMNS>
	</ASP:DATAGRID>
	</div>
	</div>

	<div class="columnBoxRight">
	<div class="wrap">
	<h2>In work</h2>
	<!--
	<ASP:DATALIST id="dltInWork" runat="server" onitemcreated="dltInWork_ItemCreated" RepeatLayout="Flow">
	<ITEMTEMPLATE>
	-->
		<h3><ASP:LABEL id="lblInWork" runat="server"><%# ((Spinit.Wpc.Base.Data.ComponentRow)Container.DataItem).Name %></ASP:LABEL></h3>
		<ASP:DATALIST id="dltComponentItemsInWork" runat="server" onitemcreated="dltComponentItemsInWork_ItemCreated" SkinID="Striped">
			<ITEMTEMPLATE>
				<ASP:HYPERLINK runat="server" id="hplItemInWork">
					<%# DataBinder.Eval(Container.DataItem, "ItemName") %>
				</ASP:HYPERLINK>
			</ITEMTEMPLATE>
		</ASP:DATALIST>
	<!--
	</ITEMTEMPLATE>
	</ASP:DATALIST>
	-->
	</div>
	</div>

	<div class="columnBoxRight clearRight">
	<div class="wrap">
	<h2>For approval</h2>
	<!--
	<ASP:DATALIST id="dltForApproval" runat="server" onitemcreated="dltForApproval_ItemCreated" RepeatLayout="Flow">
	<ITEMTEMPLATE>
	-->
		<h3><ASP:LABEL id="lblForApproval" runat="server" Visible="False"><%# ((Spinit.Wpc.Base.Data.ComponentRow)Container.DataItem).Name %></ASP:LABEL></h3>
		<ASP:DATALIST id="dltComponentItemsForApproval" runat="server" OnItemCreated="dltComponentItemsForApproval_ItemCreated" SkinID="Striped">
			<ITEMTEMPLATE>
				<ASP:HYPERLINK runat="server" id="hplItemForApproval"><%# DataBinder.Eval(Container.DataItem, "ItemName") %></ASP:HYPERLINK>
			</ITEMTEMPLATE>
		</ASP:DATALIST>
	<!--
	</ITEMTEMPLATE>
	</ASP:DATALIST>
	-->
	</div>
	</div>

	<div class="columnBoxLeft clearLeft">
	<div class="wrap">
	<h2>Statistics</h2>
	<table summary="Statistics for location" class="detailsMode striped">
	<caption>Statistics</caption>
	<tbody>
		<tr>
			<th scope="row">No of documents:</th>
			<td><%=_nrofdoc%></td>
		</tr>
		<tr>
			<th scope="row">No of PDF:</th>
			<td><%=_nrofpdf%></td>
		</tr>
		<tr>
			<th scope="row">No of Word:</th>
			<td><%=_nrofword%></td>
		</tr>
		<tr>
			<th scope="row">No of images:</th>
			<td><%=_nrofimage%></td>
		</tr>
		<tr>
			<th scope="row">No of users:</th>
			<td><%=_nrofuser%></td>
		</tr>
	</tbody>
	</table>
	</div>
	</div>
	
	<div class="columnBoxLeft clearLeft">
	<div class="wrap">
	<h2>Currently logged on users</h2>
	<ul class="normalList">
	<%
	IDictionaryEnumerator myEnumerator = _activeUserList.GetEnumerator();
	while ( myEnumerator.MoveNext() )
	{
		Response.Write("<li>" + myEnumerator.Value.ToString()+"</li>");					
	
	}
	%>
	</ul>
	</div>
	</div>
</div>
</asp:Content>