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
    	<p><label>Välj Kategori</label>
            <asp:DropDownList id="ddlPickCategory" DataSource="<% #Model.Categories %>"  SelectedValue="<%#Model.SelectedCategoryId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator1" 
		        InitialValue="0" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Kategori" 
		        ControlToValidate="ddlPickCategory" 
		        Display="Dynamic" 
		        CssClass="error-message"	>&nbsp;*</asp:RequiredFieldValidator>
		</p>
        <p><label>Välj Typ</label>
            <asp:DropDownList id="ddlPickKind" DataSource="<% #Model.ArticleTypes %>" SelectedValue="<%#Model.SelectedArticleTypeId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator2" 
		        InitialValue="0" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Artikeltyp" 
		        ControlToValidate="ddlPickKind" 
		        Display="Dynamic" 
		        CssClass="error-message" >&nbsp;*</asp:RequiredFieldValidator>
    	</p>
    	<p><label>Välj Leverantör</label>
            <asp:DropDownList id="ddlPickSupplier" DataSource="<% #Model.Suppliers %>" SelectedValue="<%#Model.SelectedSupplierId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator3" 
		        InitialValue="0" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Leverantör" 
		        ControlToValidate="ddlPickSupplier" 
		        Display="Dynamic" 
		        CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
		</p>
    	<p><label>Välj Artikel</label>
            <asp:DropDownList id="ddlPickArticle" DataSource="<% #Model.OrderArticles %>" SelectedValue="<%#Model.SelectedArticleId%>" DataTextField="Text" DataValueField="Value" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator4" 
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
          	<p><label>Styrka</label>
            <asp:DropDownList id="ddlRightStrength" DataSource="<% #Model.PowerOptions %>" Enabled="<%#Model.PowerOptionsEnabled %>" SelectedValue="<%#Model.SelectedRightPower%>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator5" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Höger styrka" 
		        ControlToValidate="ddlRightStrength" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.PowerOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
		</p>
          	<p><label>Addition</label>
            <asp:DropDownList id="ddlRightAddition" DataSource="<% #Model.AdditionOptions %>" Enabled="<%#Model.AdditionOptionsEnabled %>" SelectedValue="<%#Model.SelectedRightAddition%>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator15" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Höger addition" 
		        ControlToValidate="ddlRightAddition" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.AdditionOptionsEnabled %>"	>&nbsp;*</asp:RequiredFieldValidator>
            
		</p>

    	<p><label>Baskurva</label>
            <asp:DropDownList id="ddlRightBaskurva" DataSource="<% #Model.BaseCurveOptions %>" Enabled="<%#Model.BaseCurveOptionsEnabled %>" SelectedValue="<%#Model.SelectedRightBaseCurve%>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator6" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Höger baskurva" 
		        ControlToValidate="ddlRightBaskurva" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.BaseCurveOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
		</p>
    	<p><label>Diameter</label>
            <asp:DropDownList id="ddlRightDiameter" DataSource="<% #Model.DiameterOptions %>" Enabled="<%#Model.DiameterOptionsEnabled %>" SelectedValue="<%#Model.SelectedRightDiameter%>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator7" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Höger diameter" 
		        ControlToValidate="ddlRightDiameter" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.DiameterOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
    	</p>
        <p><label>Axel</label>
            <asp:DropDownList id="ddlRightAxis" DataSource="<% #Model.AxisOptions %>" Enabled="<%#Model.AxisOptionsEnabled %>" SelectedValue="<%#Model.SelectedRightAxis%>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator8" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Höger axel" 
		        ControlToValidate="ddlRightAxis" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.AxisOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
    	</p>
        <p><label>Cylinder</label>
            <asp:DropDownList id="ddlRightCylinder" DataSource="<% #Model.CylinderOptions %>" Enabled="<%#Model.CylinderOptionsEnabled %>" SelectedValue="<%#Model.SelectedRightCylinder%>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator9" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Höger cylinder" 
		        ControlToValidate="ddlRightCylinder" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.CylinderOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
    	</p>
      </fieldset>
      <fieldset class="left-eye">
      <legend>V</legend>
          	<p><label>Styrka</label>
            <asp:DropDownList id="ddlLeftStrength" Enabled="<%#Model.PowerOptionsEnabled %>" DataTextField="Text" DataValueField="Value" SelectedValue="<%#Model.SelectedLeftPower %>" DataSource="<% #Model.PowerOptions %>" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator10" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Vänster styrka" 
		        ControlToValidate="ddlLeftStrength" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.PowerOptionsEnabled %>"	>&nbsp;*</asp:RequiredFieldValidator>
		</p>
        
        <p><label>Addition</label>
            <asp:DropDownList id="ddlLeftAddition" DataSource="<% #Model.AdditionOptions %>" Enabled="<%#Model.AdditionOptionsEnabled %>" SelectedValue="<%#Model.SelectedLeftAddition%>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
      
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator16" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Vänster addition" 
		        ControlToValidate="ddlLeftAddition" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.AdditionOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
              
		</p>
        
    	<p><label>Baskurva</label>
            <asp:DropDownList id="ddlLeftBaskurva" DataSource="<% #Model.BaseCurveOptions %>" Enabled="<%#Model.BaseCurveOptionsEnabled %>" SelectedValue="<%#Model.SelectedLeftBaseCurve %>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator11" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Vänster baskurva" 
		        ControlToValidate="ddlLeftBaskurva" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.BaseCurveOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
		</p>
    	<p><label>Diameter</label>
            <asp:DropDownList id="ddlLeftDiameter" DataSource="<% #Model.DiameterOptions %>" Enabled="<%#Model.DiameterOptionsEnabled %>" SelectedValue="<%#Model.SelectedLeftDiameter %>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator12" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Vänster diameter" 
		        ControlToValidate="ddlLeftDiameter" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.DiameterOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
    	</p>
        <p><label>Axel</label>
            <asp:DropDownList id="ddlLeftAxis" DataSource="<% #Model.AxisOptions %>" Enabled="<%#Model.AxisOptionsEnabled %>" SelectedValue="<%#Model.SelectedLeftAxis %>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator13" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Vänster axel" 
		        ControlToValidate="ddlLeftAxis" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.AxisOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
    	</p>
        <p><label>Cylinder</label>
            <asp:DropDownList id="ddlLeftCylinder" DataSource="<% #Model.CylinderOptions %>" Enabled="<%#Model.CylinderOptionsEnabled %>" SelectedValue="<%#Model.SelectedLeftCylinder %>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator14" 
		        InitialValue="-9999" 
		        Runat="server" 
		        ErrorMessage="Obligatoriskt fält: Vänster cylinder" 
		        ControlToValidate="ddlLeftCylinder" 
		        Display="Dynamic" 
		        CssClass="error-message"
                Enabled="<%#Model.CylinderOptionsEnabled %>">&nbsp;*</asp:RequiredFieldValidator>
    	</p>
      </fieldset>
           
      <fieldset>
      <legend>Leverantörsalternativ</legend>

        <p>
            <asp:DropDownList id="ddlShippingOptions" DataSource="<% #Model.ShippingOptions %>" SelectedValue="<%#Model.SelectedShippingOption %>" DataTextField="Text" DataValueField="Value" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator 
		        ID="RequiredFieldValidator17" 
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