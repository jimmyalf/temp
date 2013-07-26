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

                <asp:Repeater runat="server" DataSource="<%# Model.Subscriptions %>" ItemType="Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders.SubscriptionListItemModel">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>
                                    <% if (Model.SelectedOption == 0) { %>
                                    <input type="radio" name="Subscription" value="0" id="Subscription-0" checked="checked"> Nytt abonnemang
                                    <% } else{ %>
                                    <input type="radio" name="Subscription" value="0" id="Subscription-0"> Nytt abonnemang
                                    <% } %>
                                </td>
                                <td>
                                    <table>
                                        <thead>
                                            <tr><th>Namn</th><th>Skapad</th><th>Dragningar</th></tr>
                                        </thead>
                                    </table>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <input type="radio" name="Subscription" id="Subscription-<%#Item.Id %>" value="<%# Item.Id %>"  <%#Item.CheckedStatement %>/><%# Item.Title %>
                            </td>
                            <td>
                                <asp:Repeater runat="server" DataSource="<%#Item.SubscriptionItems %>" ItemType="Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders.SubscriptionItemListItemModel">
                                    <HeaderTemplate>
                                        <table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Item.Name %></td>
                                            <td><%#Item.Created %></td>
                                            <td><%#Item.Withdrawals%></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <%--
				<asp:RadioButtonList runat="server" ID="rblAccounts" SelectedValue="<%# Model.SelectedOption %>" DataSource="<%# Model.Subscriptions %>" RepeatLayout="UnorderedList" DataTextField="Text" DataValueField="Value" TextAlign="Right" CssClass="radio-list" />
				<asp:RequiredFieldValidator runat="server" ErrorMessage="Ett konto måste anges" ControlToValidate="rblAccounts" Display="Dynamic" CssClass="error-message">&nbsp;*</asp:RequiredFieldValidator>
                --%>
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