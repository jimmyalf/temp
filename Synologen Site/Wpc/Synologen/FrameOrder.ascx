<%@ Control Language="C#" CodeBehind="FrameOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrder" %>
<h1><%#Model.Message %></h1>
<p>
	<label for="<%=drpFrames.ClientID%>">Bågar:</label>
	<asp:DropDownList 
		ID="drpFrames" 
		Runat="server" 
		DataSource='<%#Model.FramesList%>' 
		SelectedValue='<%#Model.SelectedFrameId%>'
		AutoPostBack="true"
		DataValueField="Id"
		DataTextField="Name" />
	<asp:RequiredFieldValidator 
		ID="reqFrames" 
		InitialValue="0" 
		Runat="server" 
		ErrorMessage="<%#Model.FrameRequiredErrorMessage %>" 
		ControlToValidate="drpFrames" 
		Display="Dynamic" 
		CssClass="invalid" 
		ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
</p>
<p>
	<label for="<%=drpIndex.ClientID%>">Index:</label>
	<asp:DropDownList 
		ID="drpIndex" 
		Runat="server" 
		DataSource='<%#Model.IndexList%>' 
		DataValueField="Value" 
		DataTextField="Name" />
	<asp:RequiredFieldValidator 
		ID="reqIndex" 
		InitialValue="<%#Model.NotSelectedIntervalValue%>" 
		Runat="server" 
		ErrorMessage="<%#Model.IndexRequiredErrorMessage %>" 
		ControlToValidate="drpIndex" 
		Display="Dynamic" 
		CssClass="invalid" 
		ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
</p>
<p>
	<label for="<%=drpSphere.ClientID%>">Sfär:</label>
	<asp:DropDownList 
		ID="drpSphere" 
		runat="server" 
		DataSource='<%#Model.SphereList%>' 
		DataValueField="Value" 
		DataTextField="Name" />
	<asp:RequiredFieldValidator 
		ID="reqSphere" 
		InitialValue="<%#Model.NotSelectedIntervalValue%>" 
		Runat="server" 
		ErrorMessage="<%#Model.SphereRequiredErrorMessage %>" 
		ControlToValidate="drpSphere" 
		Display="Dynamic" 
		CssClass="invalid" 
		ValidationGroup="vldSubmit" >&nbsp;*</asp:RequiredFieldValidator>
</p>
<asp:ValidationSummary ID="vldSummary" ValidationGroup="vldSubmit" runat="server" />
<br />
<asp:Button ID="btnSave" runat="server" Text="Spara" ValidationGroup="vldSubmit" CausesValidation="true"/>