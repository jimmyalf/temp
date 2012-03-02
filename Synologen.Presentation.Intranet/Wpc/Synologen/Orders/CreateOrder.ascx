<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.CreateOrder" %>

<div id="page" class="step3">
	<header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> <%=Model.CustomerName %></span>
	</header>
	<WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">
		<fieldset>
			<div class="progress">
				<label>Steg 3 av 6</label>
				<div id="progressbar"></div>
			</div>
			<p>
				<label>Välj Kategori</label>
				<asp:DropDownList id="ddlPickCategory" DataSource="<% #Model.Categories %>"  SelectedValue="<%#Model.SelectedCategoryId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true" />
				<asp:RequiredFieldValidator 
					InitialValue="0" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Kategori" 
					ControlToValidate="ddlPickCategory" 
					Display="Dynamic" 
					CssClass="error-message"	>&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Välj Typ</label>
				<asp:DropDownList id="ddlPickKind" DataSource="<% #Model.ArticleTypes %>" SelectedValue="<%#Model.SelectedArticleTypeId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true" />
				<asp:RequiredFieldValidator 
					InitialValue="0" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Artikeltyp" 
					ControlToValidate="ddlPickKind" 
					Display="Dynamic" 
					CssClass="error-message" >&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Välj Leverantör</label>
				<asp:DropDownList id="ddlPickSupplier" DataSource="<% #Model.Suppliers %>" SelectedValue="<%#Model.SelectedSupplierId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true" />
				<asp:RequiredFieldValidator 
					InitialValue="0" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Leverantör" 
					ControlToValidate="ddlPickSupplier" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Välj Artikel</label>
				<asp:DropDownList id="ddlPickArticle" DataSource="<% #Model.OrderArticles %>" SelectedValue="<%#Model.SelectedArticleId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true" />
				<asp:RequiredFieldValidator 
					InitialValue="0" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Artikel" 
					ControlToValidate="ddlPickArticle" 
					Display="Dynamic" 
					CssClass="error-message"	>&nbsp;*</asp:RequiredFieldValidator>
			</p>
		</fieldset>

		<fieldset class="right-eye">
			<legend>H</legend>
			<p>
				<label>Styrka</label>
				<asp:TextBox runat="server" ID="txtRightStrength" Text='<%#Model.SelectedPower.Right%>' />
				<asp:RegularExpressionValidator 
					runat="server" 
					ControlToValidate="txtRightStrength" 
					CssClass="error-message"
					ErrorMessage="Fält måste anges med + eller - följt av värdet: Höger styrka"
					ValidationExpression="^[-+]{1}[0-9]*,?[0-9]+$">&nbsp;*</asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Höger styrka" 
					ControlToValidate="txtRightStrength" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Addition</label>
				<asp:TextBox ID="txtRightAddition" runat="server" Text='<%# Model.SelectedAddition.Right%>' Enabled="<%#Model.AdditionOptionsEnabled %>" />
				<asp:RequiredFieldValidator 
					runat="server" 
					ErrorMessage="Obligatoriskt fält: Höger addition" 
					ControlToValidate="txtRightAddition" 
					Display="Dynamic" 
					CssClass="error-message"
					Enabled="<%#Model.AdditionOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p><label>Baskurva</label>
				<asp:DropDownList id="ddlRightBaskurva" DataSource="<% #Model.BaseCurveOptions %>" SelectedValue="<%#Model.SelectedBaseCurve.Right%>" DataTextField="Text" DataValueField="Value" runat="server" />
				<asp:RequiredFieldValidator 
					InitialValue="-9999" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Höger baskurva" 
					ControlToValidate="ddlRightBaskurva" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Diameter</label>
				<asp:DropDownList id="ddlRightDiameter" DataSource="<% #Model.DiameterOptions %>" SelectedValue="<%#Model.SelectedDiameter.Right%>" DataTextField="Text" DataValueField="Value" runat="server" />
				<asp:RequiredFieldValidator 
					InitialValue="-9999" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Höger diameter" 
					ControlToValidate="ddlRightDiameter" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Axel</label>
				<asp:TextBox runat="server" ID="txtRightAxis" Text='<%#Model.SelectedAxis.Right%>' Enabled="<%#Model.AxisOptionsEnabled %>" />
				<asp:RegularExpressionValidator
					runat="server" 
					ControlToValidate="txtRightAxis" 
					CssClass="error-message"
					ErrorMessage="Fält måste anges numeriskt: Höger axel"
					ValidationExpression="^[-+]?[0-9]*,?[0-9]+$"
					Enabled="<%#Model.AxisOptionsEnabled %>">&nbsp;*</asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator 
					runat="server" 
					ErrorMessage="Obligatoriskt fält: Höger axel" 
					ControlToValidate="txtRightAxis" 
					Display="Dynamic" 
					CssClass="error-message"
					Enabled="<%#Model.AxisOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Cylinder</label>
				<asp:TextBox runat="server" ID="txtRightCylinder" Text='<%#Model.SelectedCylinder.Right%>' Enabled="<%#Model.CylinderOptionsEnabled %>" />
				<asp:RegularExpressionValidator 
					runat="server" 
					ControlToValidate="txtRightCylinder" 
					CssClass="error-message"
					ErrorMessage="Fält måste anges med - följt av värde: Höger cylinder"
					ValidationExpression="^[-]{1}[0-9]*,?[0-9]+$"
					Enabled="<%#Model.CylinderOptionsEnabled %>">&nbsp;*</asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Höger cylinder" 
					ControlToValidate="txtRightCylinder" 
					Display="Dynamic" 
					CssClass="error-message"
					Enabled="<%#Model.CylinderOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
			</p>
		</fieldset>

		<fieldset class="left-eye">
			<legend>V</legend>
			<p>
				<label>Styrka</label>
				<asp:TextBox runat="server" ID="txtLeftStrength" Text='<%#Model.SelectedPower.Left%>' />
				<asp:RegularExpressionValidator
					runat="server" 
					ControlToValidate="txtLeftStrength" 
					CssClass="error-message"
					ErrorMessage="Fält måste anges med + eller - följt av värdet: Vänster styrka"
					ValidationExpression="^[-+]{1}[0-9]*,?[0-9]+$">&nbsp;*</asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator 
					runat="server" 
					ErrorMessage="Obligatoriskt fält: Vänster styrka" 
					ControlToValidate="txtLeftStrength" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Addition</label>
				<asp:TextBox runat="server" ID="txtLeftAddition" Text='<%#Model.SelectedAddition.Left%>' Enabled="<%#Model.AdditionOptionsEnabled %>" />
				<asp:RequiredFieldValidator 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Vänster addition" 
					ControlToValidate="txtLeftAddition" 
					Display="Dynamic" 
					CssClass="error-message"
					Enabled="<%#Model.AdditionOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>    
			</p>
			<p>
				<label>Baskurva</label>
				<asp:DropDownList id="ddlLeftBaskurva" DataSource="<% #Model.BaseCurveOptions %>" SelectedValue="<%#Model.SelectedBaseCurve.Left %>" DataTextField="Text" DataValueField="Value" runat="server" />
				<asp:RequiredFieldValidator 
					InitialValue="-9999" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Vänster baskurva" 
					ControlToValidate="ddlLeftBaskurva" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Diameter</label>
				<asp:DropDownList id="ddlLeftDiameter" DataSource="<% #Model.DiameterOptions %>" SelectedValue="<%#Model.SelectedDiameter.Left %>" DataTextField="Text" DataValueField="Value" runat="server" />
				<asp:RequiredFieldValidator 
					InitialValue="-9999" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Vänster diameter" 
					ControlToValidate="ddlLeftDiameter" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Axel</label>
				<asp:TextBox runat="server" ID="txtLeftAxis" Text='<%#Model.SelectedAxis.Left%>' Enabled="<%#Model.AxisOptionsEnabled %>" />
				<asp:RegularExpressionValidator
					runat="server" 
					ControlToValidate="txtLeftAxis" 
					CssClass="error-message"
					ErrorMessage="Fält måste anges numeriskt: Vänster axel"
					ValidationExpression="^[-+]?[0-9]*,?[0-9]+$"
					Enabled="<%#Model.AxisOptionsEnabled %>">&nbsp;*</asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Vänster axel" 
					ControlToValidate="txtLeftAxis" 
					Display="Dynamic" 
					CssClass="error-message"
					Enabled="<%#Model.AxisOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<p>
				<label>Cylinder</label>
				<asp:TextBox runat="server" ID="txtLeftCylinder" Text='<%#Model.SelectedCylinder.Left%>' Enabled="<%#Model.CylinderOptionsEnabled %>" />
				<asp:RegularExpressionValidator
					runat="server" 
					ControlToValidate="txtLeftCylinder" 
					CssClass="error-message"
					ErrorMessage="Fält måste anges med - följt av värde: Vänster cylinder"
					ValidationExpression="^[-]{1}[0-9]*,?[0-9]+$"
					Enabled="<%#Model.CylinderOptionsEnabled %>">&nbsp;*</asp:RegularExpressionValidator>
				<asp:RequiredFieldValidator 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Vänster cylinder" 
					ControlToValidate="txtLeftCylinder" 
					Display="Dynamic" 
					CssClass="error-message"
					Enabled="<%#Model.CylinderOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
			</p>
		</fieldset>

		<fieldset>
			<p>
				<label>Butikens Referens</label>
				<asp:TextBox id="txtReference" Text="<%#Model.Reference %>" runat="server" TextMode="MultiLine" />
			</p>
		</fieldset>
		<fieldset>
			<legend>Leverantörsalternativ</legend>
			<p>
				<asp:DropDownList id="ddlShippingOptions" DataSource="<% #Model.ShippingOptions %>" SelectedValue="<%#Model.SelectedShippingOption %>" DataTextField="Text" DataValueField="Value" runat="server" />
				<asp:RequiredFieldValidator 
					InitialValue="0" 
					Runat="server" 
					ErrorMessage="Obligatoriskt fält: Leverantörsalternativ" 
					ControlToValidate="ddlShippingOptions" 
					Display="Dynamic" 
					CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
			</p>
			<asp:ValidationSummary runat="server" CssClass="error-list"/>
			<div class="next-step">
				<div class="control-actions">
					<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CssClass="cancel-button" CausesValidation="False" />
					<asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" OnClientClick="return confirm('Detta avbryter beställningen, är du säker på att du vill avbryta beställningen?');" />
					<asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" />
				</div>
			</div>
		</fieldset>
	</div>
</div>