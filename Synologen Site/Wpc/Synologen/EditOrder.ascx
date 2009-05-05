<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.EditOrder" %>
<asp:PlaceHolder ID="plEditOrder" runat="server" Visible="true">
<div id="synologen-edit-order" class="synologen-control">
<fieldset><legend>Kunduppgifter</legend>
	<label>Ordernummer:</label><span><asp:Literal ID="ltOrderNumber" runat="server" /></span><br />
	<label>Avtal *</label>
	<asp:DropDownList ID="drpContracts" runat="server" DataTextField="cName" DataValueField="cId" AutoPostBack="true" OnSelectedIndexChanged="drpContracts_SelectedIndexChanged"/>
	<asp:RequiredFieldValidator id="reqContracts" InitialValue="0" runat="server" errormessage="Avtal saknas" controltovalidate="drpContracts" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	<br />
	<label>Företag *</label>
	<asp:DropDownList ID="drpCompany" runat="server" DataTextField="cName" DataValueField="cId" AutoPostBack="true" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged" />
	<asp:RequiredFieldValidator id="reqCompany" InitialValue="0" runat="server" errormessage="Företag saknas" controltovalidate="drpCompany" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	<br />
	<label>Kundens<br />Kostnadsställe *</label>
	<asp:TextBox id="txtRST" runat="server" /><span>&nbsp;(NNNNN)</span>
	<asp:RequiredFieldValidator id="reqRST" runat="server" errormessage="Kostnadsställe saknas" controltovalidate="txtRST" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	<asp:RangeValidator ID="vldRST" runat="server" ErrorMessage="RST felaktigt. (Skall vara 5 siffror)" Type="Integer" MinimumValue="0" MaximumValue="99999" ControlToValidate="txtRST" Display="Dynamic" ValidationGroup="vldSubmit">*</asp:RangeValidator>
	<asp:CustomValidator id="valCstRST" runat="server" ControlToValidate="txtRST" OnServerValidate="ValidateRSTLength" ErrorMessage="RST felaktigt. (Skall vara 5 siffror)" Display="Dynamic" ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>
	<%--
	<asp:DropDownList ID="drpRST" runat="server" DataTextField="cName" DataValueField="cId" AutoPostBack="true" OnSelectedIndexChanged="drpRST_SelectedIndexChanged" />
	<asp:RequiredFieldValidator id="reqRST" InitialValue="0" runat="server" errormessage="RST saknas" controltovalidate="drpRST" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	--%>
	<br />
	<label>Kundens Företagsenhet *</label>
	<asp:TextBox id="txtCompanyUnit" runat="server" />
	<asp:RequiredFieldValidator id="reqCompanyUnit" runat="server" errormessage="Enhet saknas" controltovalidate="txtCompanyUnit" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	<br />
	<label>Kundens<br />Förnamn *</label>
	<asp:TextBox id="txtCustomerFirstName" runat="server" />
	<asp:RequiredFieldValidator id="reqCustomerFirstName" runat="server" errormessage="Förnamn saknas" controltovalidate="txtCustomerFirstname" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>
	<br />
	<label>Kundens<br />Efternamn *</label>
	<asp:TextBox id="txtCustomerLastName" runat="server" />	
	<asp:RequiredFieldValidator id="reqCustomerLastName" runat="server" errormessage="Efternamn saknas" controltovalidate="txtCustomerLastName" Display="Dynamic" CssClass="invalid" ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>	
	<br />
	<label>Kundens<br />Personnummer *</label>
	<asp:TextBox id="txtPersonalIDNumber" runat="server" /><span>&nbsp;(ÅÅÅÅMMDD-NNNN)</span>
	<asp:RequiredFieldValidator id="reqPersonalIDNumber" runat="server" ControlToValidate="txtPersonalIDNumber" ErrorMessage="Personnummer saknas" Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>							
	<asp:RegularExpressionValidator ID="regPersonalIDNumber" runat="server" ValidationExpression="^\d{8}-?\d{4}$" ControlToValidate="txtPersonalIDNumber" ErrorMessage="Personnummer med felaktigt format (skall vara &Aring;&Aring;&Aring;&Aring;MMDD-NNNN)" Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:RegularExpressionValidator>
	<asp:CustomValidator ID="vldCustomPersonalIDNumber" runat="server" ControlToValidate="txtPersonalIDNumber" OnServerValidate="PersonalIDNumberValidation" ErrorMessage="Ogiltligt personnummer" Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>								
	<br />	
	<label>Kundens<br />Telefon</label>
	<asp:TextBox id="txtPhone" runat="server" /><br />
	<label>Kundens<br />E-post</label>
	<asp:TextBox id="txtEmail" runat="server" /><br />	
	<label>Skapades av:</label><span><asp:Literal ID="ltSalesPersonName" runat="server" /></span><br />
	<label>Skapades:</label><span><asp:Literal ID="ltCreatedDate" runat="server" /></span><br />
	<label>Uppdaterades:</label><span><asp:Literal ID="ltUpdatedDate" runat="server" /></span><br />
	<label>Utbetald:</label><span><asp:Literal ID="ltMarkedAsPayed" runat="server" /></span><br />
</fieldset>	
	<br />
	<div>
		<div class="rowItem"><label>Artikel</label>
			<asp:DropDownList ID="drpArticle" DataTextField="cName" DataValueField="cId" runat="server" />
			<asp:RequiredFieldValidator id="reqArticle" InitialValue="0" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>
			<asp:RequiredFieldValidator id="reqArticle2" InitialValue="" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>			
		</div>
		<div class="rowItem"><label>Antal</label>
			<asp:DropDownList ID="drpNumberOfItems" runat="server" />
			<asp:RequiredFieldValidator id="reqNumberOfItems" InitialValue="0" runat="server" errormessage="Antal måste väljas" controltovalidate="drpNumberOfItems" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>			
		</div>
		<div class="rowItem"><label>Noteringar</label><asp:TextBox ID="txtNotes" runat="server" /></div>
		<div class="rowControl"><asp:ImageButton ID="btnAddArticle" ImageUrl="~/Wpc/Synologen/Images/Add.png" ToolTip="Lägg till"  AlternateText="Lägg till" runat="server" OnClick="btnAdd_Click" ValidationGroup="vldAdd" /></div>
	</div>
	<br />
	<div id="order-items-cart" class="clearLeft">
		<asp:GridView ID="gvOrderItemsCart" 
			OnRowDeleting="gvOrderItemsCart_Deleting"
			runat="server" 
			DataKeyNames="TemporaryId,Id" 
			SkinID="Striped" 
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
				<%--<asp:BoundField headerText="Totalpris" DataField="DisplayTotalPrice"/>--%>
				<asp:BoundField headerText="Noteringar" DataField="Notes"/>
				<asp:TemplateField ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:ImageButton ID="btnDelete" ImageUrl="~/Wpc/Synologen/Images/Stop.png" CommandName="Delete" ToolTip="Ta bort"  AlternateText="Ta bort" runat="server" />
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
		<%--<asp:Button ID="btnBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />--%>
		<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
		<asp:Button ID="btnSave" runat="server" Text="Spara ändringar" OnClick="btnSave_Click" ValidationGroup="vldSubmit" OnClientClick="return confirm('Är du säker på att du vill spara ordern?');" />
		<%--<asp:Button ID="btnMarkAsPayed" runat="server" Text="Markera som utbetald" OnClick="btnMarkAsPayed_Click" ValidationGroup="vldSubmit" OnClientClick="return confirm('Är du säker på att du vill markera ordern som utbetald?');" Visible="false" />--%>
		<asp:Button ID="btnAbort" runat="server" Text="Makulera order" OnClick="btnAbort_Click" OnClientClick="return confirm('Är du säker på att du vill makulera ordern?');" />
		<asp:Button ID="btnHalt" runat="server" Text="Sätt som vilande" OnClick="btnHalt_Click" OnClientClick="return confirm('Är du säker på att du vill sätta ordern som vilande?');" />
		<asp:Button ID="btnAbortHalt" runat="server" Text="Avbryt vilande" OnClick="btnAbortHalt_Click" OnClientClick="return confirm('Är du säker på att du avbryta vilande order?');" />
	</div>
	<div class="error-summary clearLeft">
		<asp:ValidationSummary id="valSummary" ValidationGroup="vldSubmit" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" />
		<asp:ValidationSummary id="valAdd" ValidationGroup="vldAdd" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" />
		<br />
	 </div>
	 
</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoAccessMessage" runat="server" Visible="false">
<p class="error-message"><strong>!</strong>&nbsp;Du har inte rätt att redigera vald order.</p><br />
<%--<asp:Button ID="btnErrorBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />--%>
<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
</asp:PlaceHolder>