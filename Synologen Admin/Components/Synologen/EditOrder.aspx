<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="EditOrder.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.EditOrder" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
    <div id="dCompMain" class="Components-Synologen-EditOrder-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Order</h1>
                <fieldset><legend>Orderdetaljer</legend>
                <div class="formItem clearLeft">
					<div class="formItem">
						<label class="labelShort">Id: </label><span><asp:Literal id="ltOrderNumber" runat="server" /></span><br /><br />
						<label class="labelShort">SPCS Fakturanummer: </label><span><asp:Literal id="ltSPCSOrderNumber" runat="server" /></span><br /><br />
						<label class="labelShort">SPCS värde inkl moms: </label><span><asp:Literal id="ltSPCSValueIncludingVAT" runat="server" /></span><br /><br />
						<label class="labelShort">SPCS värde exkl moms: </label><span><asp:Literal id="ltSPCSValueExcludingVAT" runat="server" /></span><br /><br />					
						<label class="labelShort">Avtal: </label><span><asp:Literal ID="ltContractName" runat="server" /></span><br /><br />					
					</div>
					<div class="formItem">
						<label class="labelLong">Företag</label>
						<asp:DropDownList ID="drpCompanies" runat="server" DataValueField="cId" DataTextField="cName" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged" AutoPostBack="true"  />
					</div>
					<div class="formItem">
						<label class="labelLong">Kostnadsställe</label>
						<asp:TextBox id="txtRST" runat="server" />
						<asp:RangeValidator ID="vldRST" runat="server" ErrorMessage="RST felaktigt. (Skall vara 5 siffror)" Type="Integer" MinimumValue="10000" MaximumValue="99999" ControlToValidate="txtRST" Display="Dynamic" ValidationGroup="Error">*</asp:RangeValidator>
						<%--<asp:DropDownList ID="drpRSTs" runat="server" DataValueField="cId" DataTextField="cName" />--%>
					</div>	
					<div class="formItem">
						<label class="labelLong">Kundens Ordernummer</label>
						<asp:TextBox id="txtCustomerOrderNumber" runat="server" />
					</div>						
					<div class="formItem">
						<label class="labelLong">Orderstatus</label>
						<asp:DropDownList ID="drpStatus" runat="server" DataValueField="cId" DataTextField="cName"  />
					</div>															
					<div class="formItem clearLeft">
						<label class="labelLong">Kund Enhet</label>
						<asp:TextBox id="txtUnit" runat="server" />
					</div>					
					<div class="formItem clearLeft">
						<label class="labelLong">Kund förnamn</label>
						<asp:TextBox id="txtCustomerFirstName" runat="server" />
					</div>	
					<div class="formItem clearLeft">
						<label class="labelLong">Kund efternamn</label>
						<asp:TextBox id="txtCustomerLastName" runat="server" />
					</div>		
					<div class="formItem clearLeft">
						<label class="labelLong">Kund personnummer</label>
						<asp:TextBox id="txtPersonalIDNumber" runat="server" />
						<asp:RequiredFieldValidator id="reqPersonalIDNumber" runat="server" ControlToValidate="txtPersonalIDNumber" ErrorMessage="Personnummer saknas" Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:RequiredFieldValidator>							
						<asp:RegularExpressionValidator ID="regPersonalIDNumber" runat="server" ValidationExpression="^\d{8}-?\d{4}$" ControlToValidate="txtPersonalIDNumber" ErrorMessage="Personnummer med felaktigt format (skall vara &Aring;&Aring;&Aring;&Aring;MMDD-NNNN)" Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:RegularExpressionValidator>
						<asp:CustomValidator ID="vldCustomPersonalIDNumber" runat="server" ControlToValidate="txtPersonalIDNumber" OnServerValidate="PersonalIDNumberValidation" ErrorMessage="Ogiltligt personnummer" Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>														
					</div>		
					<div class="formItem clearLeft">
						<label class="labelLong">Kund telefon</label>
						<asp:TextBox id="txtPhone" runat="server" />
					</div>	
					<div class="formItem clearLeft">
						<label class="labelLong">Kund e-post</label>
						<asp:TextBox id="txtEmail" runat="server" />
					</div>
					<div class="formItem">
						<label class="labelShort">Butik: </label><span><asp:Literal id="ltShopName" runat="server" /></span><br /><br />
						<label class="labelShort">Registrerad av: </label><span><asp:Literal id="ltmemberName" runat="server" /></span><br /><br />
						<label class="labelShort">Försäljning registrerad: </label><span><asp:Literal id="ltSaleDate" runat="server" /></span><br /><br />
						<label class="labelShort">Försäljning uppdaterad: </label><span><asp:Literal id="ltUpdateDate" runat="server" /></span><br /><br />
						<label class="labelShort">Markerad som utbetald: </label><span><asp:Literal id="ltMarkedAsPayed" runat="server" /></span>
					</div>					
				</div>					
		        <div class="formItem rstControl">
					<div>
						<div class="formItem clearLeft"><label class="labelLong">Artikel</label>
							<asp:DropDownList ID="drpArticle" DataTextField="cName" DataValueField="cId" runat="server" />
							<asp:RequiredFieldValidator id="reqArticle" InitialValue="0" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="reqArticle2" InitialValue="" runat="server" errormessage="Artikel saknas" controltovalidate="drpArticle" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>			
						</div>
						<div class="formItem clearLeft"><label class="labelLong">Antal</label>
							<asp:DropDownList ID="drpNumberOfItems" runat="server" />
							<asp:RequiredFieldValidator id="reqNumberOfItems" InitialValue="0" runat="server" errormessage="Antal måste väljas" controltovalidate="drpNumberOfItems" Display="Dynamic" ValidationGroup="vldAdd">*</asp:RequiredFieldValidator>			
						</div>
						<div class="formItem clearLeft"><label class="labelLong">Noteringar</label><asp:TextBox ID="txtNotes" runat="server" />&nbsp;<asp:Button ID="btnAddArticle"  runat="server" OnClick="btnAdd_Click" ValidationGroup="vldAdd" Text="Lägg till" /></div>
					</div>  		        
					<label class="labelLong">Orderrader</label>
					<asp:GridView ID="gvOrderItems" OnRowDeleting="gvOrderItems_Deleting"	runat="server" DataKeyNames="Id,TemporaryId" SkinID="Striped" >
						<Columns>
							<asp:BoundField headerText="Artikel" DataField="ArticleDisplayName"/>
							<asp:BoundField headerText="Artikelnummer" DataField="ArticleDisplayNumber"/>
							<asp:BoundField headerText="Antal" DataField="NumberOfItems"/>
							<asp:BoundField headerText="Styckpris" DataField="SinglePrice"/>
							<asp:BoundField headerText="SPCS-Konto" DataField="SPCSAccountNumber"/>
							<asp:TemplateField headertext="Momsfri" SortExpression="cVATFree"  HeaderStyle-CssClass="controlColumn" >
								<ItemStyle CssClass="center" />
								<ItemTemplate>
									<asp:Image id="imgVATFree" runat="server" />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField headerText="Noteringar" DataField="Notes"/>
							<asp:ButtonField headertext="Radera" commandname="Delete"  Text="Radera"   HeaderStyle-CssClass="controlColumn" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center"/>
						</Columns>
					</asp:GridView>
					<asp:CustomValidator ID="vldCheckAnyOrderItems" OnServerValidate="OrderItemsValidation" runat="server" ErrorMessage="Orderrader saknas." Display="dynamic" CssClass="invalid"  ValidationGroup="vldSubmit">&nbsp;*</asp:CustomValidator>
					<br /><br />
					<div class="formItem clearLeft">
						<label class="labelLong">Försäljningshistorik</label>
						<asp:Repeater ID="rptOrderHistory" runat="server">
						<HeaderTemplate><ul id="order-history"></HeaderTemplate>
						<ItemTemplate><li><%# DataBinder.Eval(Container.DataItem, "cCreatedDate","{0:yyyy-MM-dd HH:mm}") %>:&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "cText") %></li></ItemTemplate>
						<FooterTemplate></ul></FooterTemplate>
						</asp:Repeater>
					</div>							
		        </div>        
		        <div class="error-summary clearLeft">																						
					<asp:ValidationSummary ID="vldSummary" runat="server" HeaderText="F&ouml;ljande f&auml;lt &auml;r obligatoriska eller felaktigt ifyllda :" ShowSummary="true" DisplayMode="List" ValidationGroup="vldSubmit" />
		        </div> 		        
				</fieldset>          	
				<fieldset>													
					<div class="formCommands">
						<input type="button" name="inputBack" class="btnBig" onclick="javascript:window.history.back();" value="Tillbaka" />					
					    <asp:button ID="btnSave" runat="server" CommandName="Save" OnClick="btnSave_Click" Text="Spara" SkinId="Big" CausesValidation="true" ValidationGroup="vldSubmit"/>
					    <asp:Button ID="btnAbort" runat="server" Text="Avbryt order" OnClick="btnAbort_Click" SkinId="Big" />
					    <asp:Button ID="btnHalt" runat="server" Text="Sätt som vilande" OnClick="btnHalt_Click" SkinId="Big" />
						<asp:Button ID="btnAbortHalt" runat="server" Text="Avbryt vilande" OnClick="btnAbortHalt_Click" SkinId="Big" />
					</div>
					
					
				</fieldset>
			</div>
        </div>
    </div>
</asp:Content>
