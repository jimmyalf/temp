<%@ Control Language="C#" CodeBehind="FrameOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrder" %>
<style type="text/css">

.interval-parameter { float:left; }
.frame-order-item { clear:both; margin: 10px 10px 10px 0px; }
.frame-order-item label{ display:block; clear:both; }
.frame-order-item input{ width: 4em; }
.form-controls { clear: both; }

</style>


<div class="frame-order-item frame">
	<label for="<%=drpFrames.ClientID%>">Bågar</label>
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
</div>

<div class="frame-order-item glasstype">
	<label for="<%=drpGlassTypes.ClientID%>">Bågar</label>
	<asp:DropDownList 
		ID="drpGlassTypes" 
		Runat="server" 
		DataSource='<%#Model.GlassTypesList%>' 
		SelectedValue='<%#Model.SelectedGlassTypeId%>'
		AutoPostBack="true"
		DataValueField="Id"
		DataTextField="Name" />
	<asp:RequiredFieldValidator 
		ID="RequiredFieldValidator1" 
		InitialValue="0" 
		Runat="server" 
		ErrorMessage="<%#Model.GlassTypeRequiredErrorMessage %>" 
		ControlToValidate="drpFrames" 
		Display="Dynamic" 
		CssClass="invalid" 
		ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
</div>
<hr />
<div class="interval-parameter pupillarydistance">
	<div class="frame-order-item left">
		<label for="<%=drpPupillaryDistanceLeft.ClientID%>">Pupilldistans V</label>
		<asp:DropDownList 
			ID="drpPupillaryDistanceLeft" 
			Runat="server" 
			DataSource='<%#Model.PupillaryDistance.List%>' 
			SelectedValue='<%#Model.PupillaryDistance.Selection.Left%>'
			DataValueField="Value"
			DataTextField="Name" />
		<asp:RequiredFieldValidator 
			ID="reqPupillaryDistanceLeft" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.PupillaryDistanceRequiredErrorMessage %>" 
			ControlToValidate="drpPupillaryDistanceLeft" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
	</div>
	<div class="frame-order-item right">
		<label for="<%=drpPupillaryDistanceRight.ClientID%>">Pupilldistans H</label>
		<asp:DropDownList 
			ID="drpPupillaryDistanceRight" 
			Runat="server" 
			DataSource='<%#Model.PupillaryDistance.List%>' 
			SelectedValue='<%#Model.PupillaryDistance.Selection.Right%>'
			DataValueField="Value"
			DataTextField="Name" />
		<asp:RequiredFieldValidator 
			ID="reqPupillaryDistanceRight" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.PupillaryDistanceRequiredErrorMessage %>" 
			ControlToValidate="drpPupillaryDistanceRight" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
	</div>
</div>

<div class="interval-parameter sphere">
	<div class="frame-order-item left">
		<label for="<%=drpSphereLeft.ClientID%>">Sfär V</label>
		<asp:DropDownList 
			ID="drpSphereLeft" 
			Runat="server" 
			DataSource='<%#Model.Sphere.List%>' 
			SelectedValue='<%#Model.Sphere.Selection.Left%>'
			DataValueField="Value"
			DataTextField="Name" />
		<asp:RequiredFieldValidator 
			ID="reqSphereLeft" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.SphereRequiredErrorMessage %>" 
			ControlToValidate="drpSphereLeft" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
	</div>
	<div class="frame-order-item right">
		<label for="<%=drpSphereRight.ClientID%>">Sfär H</label>
		<asp:DropDownList 
			ID="drpSphereRight" 
			Runat="server" 
			DataSource='<%#Model.Sphere.List%>' 
			SelectedValue='<%#Model.Sphere.Selection.Right%>'
			DataValueField="Value"
			DataTextField="Name" />
		<asp:RequiredFieldValidator 
			ID="reqSphereRight" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>" 
			Runat="server" 
			ErrorMessage="<%#Model.SphereRequiredErrorMessage %>" 
			ControlToValidate="drpSphereRight" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
	</div>
</div>

<div class="interval-parameter cylinder">
	<div class="frame-order-item left">
		<label for="<%=drpCylinderLeft.ClientID%>">Cylinder V</label>
		<asp:DropDownList 
			ID="drpCylinderLeft" 
			Runat="server" 
			DataSource='<%#Model.Cylinder.List%>' 
			SelectedValue='<%#Model.Cylinder.Selection.Left%>'
			DataValueField="Value"
			DataTextField="Name" />
		<asp:RequiredFieldValidator 
			ID="reqCylinderLeft" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.CylinderRequiredErrorMessage %>" 
			ControlToValidate="drpCylinderLeft" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
	</div>
	<div class="frame-order-item right">
		<label for="<%=drpCylinderRight.ClientID%>">Cylinder H</label>
		<asp:DropDownList 
			ID="drpCylinderRight" 
			Runat="server" 
			DataSource='<%#Model.Cylinder.List%>' 
			SelectedValue='<%#Model.Cylinder.Selection.Right%>'
			DataValueField="Value"
			DataTextField="Name" />
		<asp:RequiredFieldValidator 
			ID="reqCylinderRight" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.CylinderRequiredErrorMessage %>" 
			ControlToValidate="drpCylinderRight" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"	>&nbsp;*</asp:RequiredFieldValidator>
	</div>
</div>

<div class="interval-parameter axis">
	<div class="frame-order-item left">
		<label for="<%=txtAxisLeft.ClientID%>">Axel V</label>
		<asp:TextBox ID="txtAxisLeft" runat="server" maxlength="3"  />
		<asp:RangeValidator 
			ID="rngAxisLeft" 
			runat="server" 
			Display="Dynamic" 
			CssClass="invalid" 
			MinimumValue="0"
			MaximumValue="180"
			ControlToValidate="txtAxisLeft"
			ErrorMessage='<%#Model.AxisRangeMessage %>'
			Type="Integer"
			ValidationGroup="vldSubmit">&nbsp;*</asp:RangeValidator>
		<asp:RequiredFieldValidator 
			ID="reqAxisLeft" 
			runat="server"
			Display="Dynamic" 
			CssClass="invalid"
			ControlToValidate="txtAxisLeft"
			ValidationGroup="vldSubmit"
			ErrorMessage='<%#Model.AxisRequiredMessage %>'>&nbsp;*</asp:RequiredFieldValidator>
	</div>
	<div class="frame-order-item right">
		<label for="<%=txtAxisRight.ClientID%>">Axel H</label>
		<asp:TextBox ID="txtAxisRight" runat="server" maxlength="3" />
		<asp:RangeValidator 
			ID="rngAxisRight" 
			runat="server" 
			Display="Dynamic" 
			CssClass="invalid" 
			MinimumValue="0"
			MaximumValue="180"
			ControlToValidate="txtAxisRight"
			ErrorMessage='<%#Model.AxisRangeMessage %>'
			Type="Integer"
			ValidationGroup="vldSubmit">&nbsp;*</asp:RangeValidator>
		<asp:RequiredFieldValidator 
			ID="reqAxisRight" 
			runat="server"
			Display="Dynamic" 
			CssClass="invalid"
			ControlToValidate="txtAxisRight"
			ValidationGroup="vldSubmit"
			ErrorMessage='<%#Model.AxisRequiredMessage %>'>&nbsp;*</asp:RequiredFieldValidator>		
	</div>
</div>

<div class="interval-parameter addition">
	<div class="frame-order-item left">
		<label for="<%=drpAdditionLeft.ClientID%>">Addition V</label>
		<asp:DropDownList 
			ID="drpAdditionLeft" 
			Runat="server" 
			DataSource='<%#Model.Addition.List%>' 
			SelectedValue='<%#Model.Addition.Selection.Left%>'
			DataValueField="Value"
			DataTextField="Name"
			Enabled='<%#Model.AdditionParametersEnabled %>' />
		<asp:RequiredFieldValidator 
			ID="reqAdditionLeft" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.AdditionRequiredErrorMessage %>" 
			ControlToValidate="drpAdditionLeft" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"
			Enabled='<%#Model.AdditionParametersEnabled %>' >&nbsp;*</asp:RequiredFieldValidator>
	</div>
	<div class="frame-order-item right">
		<label for="<%=drpAdditionRight.ClientID%>">Addition H</label>
		<asp:DropDownList 
			ID="drpAdditionRight" 
			Runat="server" 
			DataSource='<%#Model.Addition.List%>' 
			SelectedValue='<%#Model.Addition.Selection.Right%>'
			DataValueField="Value"
			DataTextField="Name"
			Enabled='<%#Model.AdditionParametersEnabled %>' />
		<asp:RequiredFieldValidator 
			ID="reqAdditionRight" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.AdditionRequiredErrorMessage %>" 
			ControlToValidate="drpAdditionRight" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"
			Enabled='<%#Model.AdditionParametersEnabled %>' >&nbsp;*</asp:RequiredFieldValidator>
	</div>
</div>

<div class="interval-parameter height">
	<div class="frame-order-item left">
		<label for="<%=drpHeightLeft.ClientID%>">Höjd V</label>
		<asp:DropDownList 
			ID="drpHeightLeft" 
			Runat="server" 
			DataSource='<%#Model.Height.List%>' 
			SelectedValue='<%#Model.Height.Selection.Left%>'
			DataValueField="Value"
			DataTextField="Name"
			Enabled='<%#Model.HeightParametersEnabled %>' />
		<asp:RequiredFieldValidator 
			ID="reqHeightLeft" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.HeightRequiredMessage %>" 
			ControlToValidate="drpHeightLeft" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"
			Enabled='<%#Model.HeightParametersEnabled %>' >&nbsp;*</asp:RequiredFieldValidator>
	</div>
	<div class="frame-order-item right">
		<label for="<%=drpHeightRight.ClientID%>">Höjd H</label>
		<asp:DropDownList 
			ID="drpHeightRight" 
			Runat="server" 
			DataSource='<%#Model.Height.List%>' 
			SelectedValue='<%#Model.Height.Selection.Right%>'
			DataValueField="Value"
			DataTextField="Name"
			Enabled='<%#Model.HeightParametersEnabled %>' />
		<asp:RequiredFieldValidator 
			ID="reqHeightRight" 
			InitialValue="<%#Model.NotSelectedIntervalValue %>"
			Runat="server" 
			ErrorMessage="<%#Model.HeightRequiredMessage %>" 
			ControlToValidate="drpHeightRight" 
			Display="Dynamic" 
			CssClass="invalid" 
			ValidationGroup="vldSubmit"
			Enabled='<%#Model.HeightParametersEnabled %>' >&nbsp;*</asp:RequiredFieldValidator>
	</div>
</div>

<div class="form-controls">
	<asp:ValidationSummary ID="vldSummary" ValidationGroup="vldSubmit" runat="server" />
	<br />
	<asp:Button ID="btnSave" runat="server" Text="Spara" ValidationGroup="vldSubmit" CausesValidation="true"/>
</div>