<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.CreateOrder" %>
<div id="synologen-create-order" class="synologen-control">
<fieldset><legend>Kunduppgifter</legend>
	<label>Avtal *</label>
	<asp:DropDownList ID="drpContracts" runat="server" AutoPostBack="true" DataTextField="cName" DataValueField="cId" OnSelectedIndexChanged="drpContracts_SelectedIndexChanged"/>
	<asp:RequiredFieldValidator id="reqContracts" InitialValue="0" runat="server" errormessage="Avtal saknas" controltovalidate="drpContracts" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	<br />
	<label>Företag *</label>
	<asp:DropDownList ID="drpCompany" runat="server" AutoPostBack="true" DataTextField="cName" DataValueField="cId" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged" Enabled="false"/>
	<asp:RequiredFieldValidator id="reqCompany" InitialValue="0" runat="server" errormessage="Företag saknas" controltovalidate="drpCompany" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	<br />
	<label>Kundens<br />Kostnadsställe <asp:Literal ID="ltRequiredRST" runat="server" Text='<%#GetControlIsRequiredCharacter("txtRST")%>' /></label>
	<asp:TextBox id="txtRST" runat="server" />
	<asp:CustomValidator id="valRST" runat="server" ControlToValidate="txtRST" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	<br />
	<label>Kundens<br />Företagsenhet <asp:Literal ID="ltRequiredCompanyUnit" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCompanyUnit")%>' /></label>
	<asp:TextBox id="txtCompanyUnit" runat="server" />
	<asp:CustomValidator id="vldCompanyUnit" runat="server" ControlToValidate="txtCompanyUnit" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>	
	<br />
	<label>Kundens<br />Förnamn <asp:Literal ID="ltRequiredCustomerFirstName" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCustomerFirstName")%>' /></label>
	<asp:TextBox id="txtCustomerFirstName" runat="server" />
	<asp:CustomValidator id="vldCustomerFirstName" runat="server" ControlToValidate="txtCustomerFirstName" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	<br />
	<label>Kundens<br />Efternamn <asp:Literal ID="ltRequiredCustomerLastName" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCustomerLastName")%>' /></label>
	<asp:TextBox id="txtCustomerLastName" runat="server" />	
	<asp:CustomValidator id="vldCustomerLastName" runat="server" ControlToValidate="txtCustomerLastName" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>	
	<br />
	<label>Kundens<br />Personnummer <asp:Literal ID="ltRequiredPersonalIDNumber" runat="server" Text='<%#GetControlIsRequiredCharacter("txtPersonalIDNumber")%>' /></label>
	<asp:TextBox id="txtPersonalIDNumber" runat="server" /><span>&nbsp;(ÅÅÅÅMMDD-NNNN)</span>
	<asp:CustomValidator id="vldPersonalIDNumber" runat="server" ControlToValidate="txtPersonalIDNumber" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	<br />
	<label>Kundens<br />Ordernummer <asp:Literal ID="ltRequiredCustomerOrderNumber" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCustomerOrderNumber")%>' /></label>
	<asp:TextBox id="txtCustomerOrderNumber" runat="server" />
	<asp:CustomValidator id="vldCustomerOrderNumber" runat="server" ControlToValidate="txtCustomerOrderNumber" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	<br />	
	<label>Kundens<br />Telefon <asp:Literal ID="ltRequiredPhone" runat="server" Text='<%#GetControlIsRequiredCharacter("txtPhone")%>' /></label>
	<asp:TextBox id="txtPhone" runat="server" />
	<asp:CustomValidator id="vldCustomerPhoneNumber" runat="server" ControlToValidate="txtPhone" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	<br />
	<label>Kundens<br />E-post <asp:Literal ID="ltRequiredEmail" runat="server" Text='<%#GetControlIsRequiredCharacter("txtEmail")%>' /></label>
	<asp:TextBox id="txtEmail" runat="server" />
	<asp:CustomValidator id="vldEmail" runat="server" ControlToValidate="txtEmail" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>	
</fieldset>	
	<br />
	<fieldset id="article-selection">
<%--		<asp:ScriptManager ID="ScriptManager1" runat="server"/>	
		<asp:UpdatePanel ID="updArticleControl" runat="server" UpdateMode="Conditional">
		<ContentTemplate>--%>
		<label>Artikel</label>
			<asp:DropDownList ID="drpArticle" AutoPostBack="true" OnSelectedIndexChanged="drpArticle_OnSelectedIndexChanged" DataTextField="cName" DataValueField="cId" runat="server" Enabled="false" />
			<asp:RequiredFieldValidator id="reqArticle" InitialValue="0" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="reqArticle2" InitialValue="" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>			
		<br />
		<asp:PlaceHolder ID="plManualPrice" runat="server" Visible="false">
		<label>Pris</label>
			<asp:TextBox ID="txtManualPrice" runat="server"/>
			<asp:RequiredFieldValidator id="reqManualParice" runat="server" errormessage="Pris krävs" controltovalidate="txtManualPrice" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator id="regexManualPrice" runat="server" ErrorMessage="Pris måste anges i numerisk form utan decimaler." ControlToValidate="txtManualPrice" Display="Dynamic" ValidationGroup="vldAdd" ValidationExpression="^[1-9]{1}[0-9]*$">*</asp:RegularExpressionValidator>
		<br />
		</asp:PlaceHolder>
<%--		</ContentTemplate>
		<Triggers >
			<asp:AsyncPostBackTrigger ControlID ="drpArticle" EventName ="SelectedIndexChanged" />
		</Triggers>		
		</asp:UpdatePanel>--%>
		<label>Antal</label>
			<asp:DropDownList ID="drpNumberOfItems" runat="server" Enabled="false" />
			<asp:RequiredFieldValidator id="reqNumberOfItems" InitialValue="0" runat="server" errormessage="Antal måste väljas" controltovalidate="drpNumberOfItems" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>
		<br />
		<label>Noteringar</label>
			<asp:TextBox ID="txtNotes" runat="server" />
			<asp:ImageButton ID="btnAddArticle" ImageUrl="~/Wpc/Synologen/Images/Add.png" ToolTip="Lägg till"  AlternateText="Lägg till" runat="server" OnClick="btnAdd_Click" ValidationGroup="vldAdd" />
		<br />
	</fieldset>
	<br />
	<div id="order-items-cart" class="clearLeft">
		<asp:GridView ID="gvOrderItemsCart" 
			OnRowDeleting="gvOrderItemsCart_Deleting"
			runat="server" 
			DataKeyNames="TemporaryId" 
			SkinID="Striped" 
			AutoGenerateColumns="false" 
			ShowHeader="true"
			AlternatingRowStyle-CssClass="synologen-table-alternative-row"
			RowStyle-CssClass="synologen-table-row"
			HeaderStyle-CssClass="synologen-table-headerrow"
			CssClass="synologen-table">
			<Columns>
				<asp:BoundField headerText="Artikel" DataField="ArticleDisplayName"/>
				<asp:BoundField headerText="Artikelnr" DataField="ArticleDisplayNumber"/>
				<asp:BoundField headerText="Styckpris" DataField="SinglePrice"/>
				<asp:BoundField headerText="Antal" DataField="NumberOfItems"/>
				<%--<asp:BoundField headerText="Totalpris" DataField="DisplayTotalPrice"/>--%>
				<asp:BoundField headerText="Noteringar" DataField="Notes"/>
				<asp:TemplateField ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
					<asp:ImageButton ID="ImageButton1" ImageUrl="~/Wpc/Synologen/Images/Stop.png" CommandName="Delete" ToolTip="Ta bort"  AlternateText="Ta bort" runat="server" />
						<%--<asp:Button id="btnDelete" runat="server" commandname="Delete" text="Ta bort" CssClass="btnSmall" />--%>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:CustomValidator ID="vldCheckAnyOrderItems" OnServerValidate="OrderItemsValidation" runat="server" ErrorMessage="Orderrader saknas." Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>
	</div>		
	<div class="clearLeft"><label>Totalpris exkl moms: </label><span><asp:Literal id="ltTotalPrice" runat="server" /></span></div>

	<div class="control-actions clearLeft">
		<asp:CustomValidator ID="vldCheckSession" OnServerValidate="SessionValidation" runat="server" ErrorMessage="Du har varit inloggad f&ouml;r l&auml;nge och tappat din session, var v&auml;nlig logga in igen för att registrera f&ouml;rs&auml;ljningar." Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>
		<asp:Button ID="btnSave" runat="server" Text="Registrera order" Enabled="false" OnClick="btnSave_Click" ValidationGroup="vldSubmit" OnClientClick="return confirm('Är du säker på att du vill skicka ordern?');"  CausesValidation="true" />
	</div>
	<div class="error-summary clearLeft">
		<asp:ValidationSummary id="valSummary" ValidationGroup="vldSubmit" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" />
		<asp:ValidationSummary id="valAdd" ValidationGroup="vldAdd" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" />
		<br />
	 </div>
	 
</div>
