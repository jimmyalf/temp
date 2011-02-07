<%@ Page Language="C#" MasterPageFile="~/BaseMain.master" AutoEventWireup="true" CodeBehind="ChangeLocation.aspx.cs" Inherits="Spinit.Wpc.Base.Presentation.ChangeLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ComponentContent" runat="server">
<div id="dCompMain" class="ChangeLocation-aspx">
	<div class="fullBox">
	<div class="wrap">
	<h1>Change location</h1>
	<asp:Repeater ID="rptLocations" runat="server">
		<HeaderTemplate><ul id="locations"></HeaderTemplate>
			<ItemTemplate><li class="selected-<%# DataBinder.Eval(Container.DataItem, "Selected") %>">
				<a href="<%= Spinit.Wpc.Base.Business.Globals.BaseUrl %>ChangeLocation.aspx?location=<%# DataBinder.Eval(Container.DataItem, "Location.Id") %>&amp;language=<%# DataBinder.Eval(Container.DataItem, "Language.Id") %>" title="<%# DataBinder.Eval(Container.DataItem, "Name") %>">
					<%# DataBinder.Eval(Container.DataItem, "Name") %>
				</a>
			</li></ItemTemplate>
		<FooterTemplate></ul></FooterTemplate>
	</asp:Repeater>
	</div>
	</div>
</div>
</asp:Content>