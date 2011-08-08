<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.PageProperties" Codebehind="PageProperties.ascx.cs" %>
<div class="Content-PageProperties-aspx fullBox">
<div class="wrap">
<h2>Page info</h2>

<table class="striped detailsMode" summary="A detailed list of information related to the selected page">
<caption>Page information</caption>
<tbody>
<tr>
	<th scope="row">Page name</th>
	<td><ASP:LABEL id="lblPageName" runat="server"></ASP:LABEL></td>
</tr>
<tr runat="server" id="tPath">
	<th scope="row"><ASP:LABEL id="lblPathDesc" runat="server">File path</ASP:LABEL></th>
	<td><ASP:LABEL id="lblFileName" runat="server"></ASP:LABEL></td>
</tr>
<tr runat="server" id="tLink">
	<th scope="row"><ASP:LABEL id="lblWebPathDesc" runat="server">Web address</ASP:LABEL></th>
	<td><ASP:HYPERLINK id="lnkWebAddress" runat="server" rel="external"></ASP:HYPERLINK></td>
</tr>
<tr>
	<th scope="row">Created by</th>
	<td><ASP:LABEL id="lblCreatedBy" runat="server"></ASP:LABEL></td>
</tr>
<tr>
	<th scope="row">Created date</th>
	<td><ASP:LABEL id="lblCreatedDate" runat="server"></ASP:LABEL></td>
</tr>
<tr>
	<th scope="row">Last updated by</th>
	<td><ASP:LABEL id="lblUpdatedBy" runat="server"></ASP:LABEL></td>
</tr>
<tr>
	<th scope="row">Last updated date</th>
	<td><ASP:LABEL id="lblUpdatedDate" runat="server"></ASP:LABEL></td>
</tr>
<tr>
	<th scope="row">Last locked by</th>
	<td><ASP:LABEL id="lblLockedBy" runat="server"></ASP:LABEL></td>
</tr>
<tr>
	<th scope="row">Last locked date</th>
	<td><ASP:LABEL id="lblLockedDate" runat="server"></ASP:LABEL></td>
</tr>
<tr>
	<th scope="row">Last approved by</th>
	<td><ASP:LABEL id="lblApprovedBy" runat="server"></ASP:LABEL></td>
</tr>
<tr>
	<th scope="row">Last approved date</th>
	<td><ASP:LABEL id="lblApprovedDate" runat="server"></ASP:LABEL></td>
</tr>
</tbody>
</table>

</div>
</div>