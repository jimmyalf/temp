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
<div class="interval-parameter frame">
	<div class="frame-order-item left">
		<label for="drpPupillaryDistanceLeft">Pupilldistans V</label>
		<select id="drpPupillaryDistanceLeft">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj PD --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
	<div class="frame-order-item right">
		<label for="drpPupillaryDistanceRight">Pupilldistans H</label>
		<select id="drpPupillaryDistanceRight">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj PD --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
</div>

<div class="interval-parameter sphere">
	<div class="frame-order-item left">
		<label for="drpSphereLeft">Sfär V</label>
		<select id="drpSphereLeft">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Sfär --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
	<div class="frame-order-item right">
		<label for="drpSphereRight">Sfär H</label>
		<select id="drpSphereRight">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Sfär --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
</div>

<div class="interval-parameter cylinder">
	<div class="frame-order-item left">
		<label for="drpCylinderLeft">Cylinder V</label>
		<select id="drpCylinderLeft">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Cylinder --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
	<div class="frame-order-item right">
		<label for="drpCylinderRight">Cylinder H</label>
		<select id="drpCylinderRight">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Cylinder --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
</div>

<div class="interval-parameter axis">
	<div class="frame-order-item left">
		<label for="txtAxisLeft">Axel V</label>
		<input type="txtAxisLeft" maxlength="3" />
	</div>
	<div class="frame-order-item right">
		<label for="txtAxisRight">Axel H</label>
		<input type="txtAxisRight" maxlength="3"  />
	</div>
</div>

<div class="interval-parameter addition">
	<div class="frame-order-item left">
		<label for="drpAdditionLeft">Addition V</label>
		<select id="drpAdditionLeft">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Addition --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
	<div class="frame-order-item right">
		<label for="drpAdditionRight">Addition H</label>
		<select id="drpAdditionRight">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Addition --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
</div>

<div class="interval-parameter height">
	<div class="frame-order-item left">
		<label for="drpHeightLeft">Höjd V</label>
		<select id="drpHeightLeft">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Höjd --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
	<div class="frame-order-item right">
		<label for="drpAdditionRight">Höjd H</label>
		<select id="drpAdditionRight">
			<option value="<%#Model.NotSelectedIntervalValue%>">-- Välj Höjd --</option>
			<option value="20">20</option>
			<option value="21">21</option>
			<option value="22">..</option>
		</select>
	</div>
</div>

<div class="form-controls">
	<asp:ValidationSummary ID="vldSummary" ValidationGroup="vldSubmit" runat="server" />
	<br />
	<asp:Button ID="btnSave" runat="server" Text="Spara" ValidationGroup="vldSubmit" CausesValidation="true"/>
</div>