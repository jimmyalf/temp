<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOptions.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.PaymentOptions" %>
<div id="page" class="step4">
	<header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> <%#Model.CustomerName %></span>
	</header>
	<WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">
		<fieldset>
			<div class="progress">
				<label>Steg 4 av 6</label>
				<div id="progressbar"></div>
			</div>
			<div>
				<label>Välj konto för betalning</label>
                <p>

                <asp:Repeater runat="server" DataSource="<%# Model.SubscriptionsItems %>" ItemType="Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders.SubscriptionItemListModel">
                    <HeaderTemplate>
                        <table>
                            <thead>
                                <%--<tr>
                                    <th>Konto</th><th>Namn</th><th>Skapad</th><th>Dragningar</th>
                                </tr>--%>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                                <tr>
                                    <asp:PlaceHolder runat="server" Visible="<%#Item.IsFirstInList %>">
                                    <td rowspan="<%#Item.RowSpan%>">
                                        <input type="radio" name="Subscription" value="<%#Item.SubscriptionId%>" <%#Model.RenderChecked(model => model.SelectedOption == Item.SubscriptionId) %>/>
                                        <%#Item.Title %>
                                    </td>
                                    </asp:PlaceHolder>
                                    <%--<asp:PlaceHolder runat="server" Visible="<%#Item.IsDefault %>">
                                    <td colspan="3"></td>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder runat="server" Visible="<%#!Item.IsDefault %>">
                                    <td><%#Item.Name %></td>
                                    <td><%#Item.Created %></td>
                                    <td><%#Item.Withdrawals %></td>
                                    </asp:PlaceHolder>--%>
                                </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                            </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                 </p>
			</div>
			<asp:ValidationSummary runat="server" CssClass="error-list"/>
			<div class="next-step">
				<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CausesValidation="False" />
			    <asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" OnClientClick="return confirm('Detta avbryter beställningen, är du säker på att du vill avbryta beställningen?');" />
		        <asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" CssClass="submit-button" />
			</div>
		</fieldset>
	</div>
</div>