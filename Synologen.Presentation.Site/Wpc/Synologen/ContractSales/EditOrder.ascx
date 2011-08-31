<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ContractSales.EditOrder"  %>
<asp:PlaceHolder ID="plEditOrder" runat="server" Visible="true">
<div id="synologen-edit-order" class="synologen-control">
<fieldset><legend>Kunduppgifter</legend>
	<p>
		<label>Ordernummer:</label>
		<span><asp:Literal ID="ltOrderNumber" runat="server" /></span>
	</p>
	<p>
		<label>Avtal *</label>
		<asp:DropDownList ID="drpContracts" runat="server" DataTextField="cName" DataValueField="cId" AutoPostBack="true" OnSelectedIndexChanged="drpContracts_SelectedIndexChanged"/>
		<asp:RequiredFieldValidator id="reqContracts" InitialValue="0" runat="server" errormessage="Avtal saknas" controltovalidate="drpContracts" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	</p>
	<p>
		<label>F�retag *</label>
		<asp:DropDownList ID="drpCompany" runat="server" DataTextField="cName" DataValueField="cId" AutoPostBack="true" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged" />
		<asp:RequiredFieldValidator id="reqCompany" InitialValue="0" runat="server" errormessage="F�retag saknas" controltovalidate="drpCompany" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	</p>
	<p>
		<label>Kundens<br />Kostnadsst�lle <asp:Literal ID="ltRequiredRST" runat="server" Text='<%#GetControlIsRequiredCharacter("txtRST")%>' /></label>
		<asp:TextBox id="txtRST" runat="server" />
		<asp:CustomValidator id="valRST" runat="server" ControlToValidate="txtRST" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	</p>
	<p>
		<label>Kundens<br />F�retagsenhet <asp:Literal ID="ltRequiredCompanyUnit" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCompanyUnit")%>' /></label>
		<asp:TextBox id="txtCompanyUnit" runat="server" />
		<asp:CustomValidator id="vldCompanyUnit" runat="server" ControlToValidate="txtCompanyUnit" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>	
	</p>
	<p>
		<label>Kundens<br />F�rnamn <asp:Literal ID="ltRequiredCustomerFirstName" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCustomerFirstName")%>' /></label>
		<asp:TextBox id="txtCustomerFirstName" runat="server" />
		<asp:CustomValidator id="vldCustomerFirstName" runat="server" ControlToValidate="txtCustomerFirstName" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	</p>
	<p>
		<label>Kundens<br />Efternamn <asp:Literal ID="ltRequiredCustomerLastName" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCustomerLastName")%>' /></label>
		<asp:TextBox id="txtCustomerLastName" runat="server" />	
		<asp:CustomValidator id="vldCustomerLastName" runat="server" ControlToValidate="txtCustomerLastName" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>	
	</p>
	<p>
		<label>Kundens<br />Personnummer <asp:Literal ID="ltRequiredPersonalIDNumber" runat="server" Text='<%#GetControlIsRequiredCharacter("txtPersonalIDNumber")%>' /></label>
		<asp:TextBox id="txtPersonalIDNumber" runat="server" /><span>&nbsp;(����MMDD-NNNN)</span>
		<asp:CustomValidator id="vldPersonalIDNumber" runat="server" ControlToValidate="txtPersonalIDNumber" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	</p>
	<p>
		<label>Kundens<br />Ordernummer <asp:Literal ID="ltRequiredCustomerOrderNumber" runat="server" Text='<%#GetControlIsRequiredCharacter("txtCustomerOrderNumber")%>' /></label>
		<asp:TextBox id="txtCustomerOrderNumber" runat="server" />
		<asp:CustomValidator id="vldCustomerOrderNumber" runat="server" ControlToValidate="txtCustomerOrderNumber" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	</p>
	<p>
		<label>Kundens<br />Telefon <asp:Literal ID="ltRequiredPhone" runat="server" Text='<%#GetControlIsRequiredCharacter("txtPhone")%>' /></label>
		<asp:TextBox id="txtPhone" runat="server" />
		<asp:CustomValidator id="vldCustomerPhoneNumber" runat="server" ControlToValidate="txtPhone" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>
	</p>
	<p>
		<label>Kundens<br />E-post <asp:Literal ID="ltRequiredEmail" runat="server" Text='<%#GetControlIsRequiredCharacter("txtEmail")%>' /></label>
		<asp:TextBox id="txtEmail" runat="server" />	
		<asp:CustomValidator id="vldEmail" runat="server" ControlToValidate="txtEmail" OnServerValidate="PerformCustomValidation" Display="Dynamic" ValidationGroup="vldSubmit" ValidateEmptyText="true">&nbsp;*</asp:CustomValidator>	
	</p>
	<p><label>Skapades av:</label><span><asp:Literal ID="ltSalesPersonName" runat="server" /></span></p>
	<p><label>Skapades:</label><span><asp:Literal ID="ltCreatedDate" runat="server" /></span></p>
	<p><label>Uppdaterades:</label><span><asp:Literal ID="ltUpdatedDate" runat="server" /></span></p>
	<p><label>Utbetald:</label><span><asp:Literal ID="ltMarkedAsPayed" runat="server" /></span></p>
</fieldset>	
	<br />
	<fieldset id="article-selection">
		<p>
			<label>Artikel</label>
			<asp:DropDownList ID="drpArticle" AutoPostBack="true" OnSelectedIndexChanged="drpArticle_OnSelectedIndexChanged" DataTextField="cName" DataValueField="cId" runat="server" Enabled="false" />
			<asp:RequiredFieldValidator id="reqArticle" InitialValue="0" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="reqArticle2" InitialValue="" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>			
		</p>
		<asp:PlaceHolder ID="plManualPrice" runat="server" Visible="false">
		<p>
			<label>Pris</label>
			<asp:TextBox ID="txtManualPrice" runat="server"/>
			<asp:RequiredFieldValidator id="reqManualParice" runat="server" errormessage="Pris kr�vs" controltovalidate="txtManualPrice" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator id="regexManualPrice" runat="server" ErrorMessage="Pris m�ste anges i numerisk form med utan decimaler." ControlToValidate="txtManualPrice" Display="Dynamic" ValidationGroup="vldAdd" ValidationExpression="^[1-9]{1}[0-9]*$">*</asp:RegularExpressionValidator>
		</p>
		</asp:PlaceHolder>
		<p>
			<label>Antal</label>
			<asp:DropDownList ID="drpNumberOfItems" runat="server" Enabled="false" />
			<asp:RequiredFieldValidator id="reqNumberOfItems" InitialValue="0" runat="server" errormessage="Antal m�ste v�ljas" controltovalidate="drpNumberOfItems" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>			
		</p>
		<p>
			<label>Noteringar</label>
			<asp:TextBox ID="txtNotes" runat="server" />
			<asp:ImageButton ID="btnAddArticle" ImageUrl="~/Wpc/Synologen/Images/Add.png" ToolTip="L�gg till"  AlternateText="L�gg till" runat="server" OnClick="btnAdd_Click" ValidationGroup="vldAdd" />
		</p>
	</fieldset>
	<br />
	<div id="order-items-cart" class="clearLeft">
		<asp:GridView ID="gvOrderItemsCart" 
			OnRowDeleting="gvOrderItemsCart_Deleting"
			runat="server" 
			DataKeyNames="TemporaryId,Id" 
			
			AutoGenerateColumns="false" 
			ShowHeader="true"
			AlternatingRowStyle-CssClass="synologen-table-alternative-row"
			RowStyle-CssClass="synologen-table-row"
			HeaderStyle-CssClass="synologen-table-headerrow"
			CssClass="synologen-table"
			>
			<Columns>
				<asp:BoundField headerText="Artikel" DataField="ArticleDisplayName"/>
				<asp:BoundField headerText="Artikelnr" DataField="ArticleDisplayNumber"/>
				<asp:BoundField headerText="Styckpris" DataField="SinglePrice"/>
				<asp:BoundField headerText="Antal" DataField="NumberOfItems"/>
				<asp:BoundField headerText="Noteringar" DataField="Notes"/>
				<asp:TemplateField ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:ImageButton ID="btnDelete" ImageUrl="~/Wpc/Synologen/Images/Stop.png" CommandName="Delete" ToolTip="Ta bort"  AlternateText="Ta bort" runat="server" />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:CustomValidator ID="vldCheckAnyOrderItems" OnServerValidate="OrderItemsValidation" runat="server" ErrorMessage="Orderrader saknas." Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>
	</div>
	<div class="clearLeft"><label>Totalpris exkl moms: </label><span><asp:Literal id="ltTotalPrice" runat="server" /></span></div>
	<div class="control-actions clearLeft">
		<asp:CustomValidator ID="vldCheckSession" OnServerValidate="SessionValidation" runat="server" ErrorMessage="Du har varit inloggad f&ouml;r l&auml;nge och tappat din session, var v&auml;nlig logga in igen f�r att registrera f&ouml;rs&auml;ljningar." Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>
		<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
		<asp:Button ID="btnSave" runat="server" Text="Spara �ndringar" OnClick="btnSave_Click" ValidationGroup="vldSubmit" OnClientClick="return confirm('�r du s�ker p� att du vill spara ordern?');" />
		<asp:Button ID="btnAbort" runat="server" Text="Makulera order" OnClick="btnAbort_Click" OnClientClick="return confirm('�r du s�ker p� att du vill makulera ordern?');" />
		<asp:Button ID="btnHalt" runat="server" Text="S�tt som vilande" OnClick="btnHalt_Click" OnClientClick="return confirm('�r du s�ker p� att du vill s�tta ordern som vilande?');" />
		<asp:Button ID="btnAbortHalt" runat="server" Text="Avbryt vilande" OnClick="btnAbortHalt_Click" OnClientClick="return confirm('�r du s�ker p� att du avbryta vilande order?');" />
	</div>
	<div class="error-summary clearLeft">
		<asp:ValidationSummary id="valSummary" ValidationGroup="vldSubmit" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" />
		<asp:ValidationSummary id="valAdd" ValidationGroup="vldAdd" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" />
		<br />
	 </div>
	 
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoAccessMessage" runat="server" Visible="false">
<p class="error-message"><strong>!</strong>&nbsp;Du har inte r�tt att redigera vald order.</p><br />
<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
</asp:PlaceHolder>