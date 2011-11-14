<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateTransaction.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions.CreateTransaction" %>
<%@ Register Src="~/Wpc/Synologen/ValidationButton.ascx" TagPrefix="WpcSynologen" TagName="ValidationButton" %>
<fieldset class="synologen-form">
	<legend>Ny transaktion</legend>
	<% if (Model.DisplayChooseReason) { %>
	<div class="control-actions">
		<asp:Button ID="btnWithdrawal" OnClick="ReasonToWithdrawal" ValidationGroup="vgCreateTransaction" runat="server" Text="Uttag" />
		<asp:Button ID="btnCorrection"  OnClick="ReasonToCorrection" ValidationGroup="vgCreateTransaction" runat="server" Text="Korrigering" />
	</div>
	<% } %>
	<% if (Model.DisplaySaveCorrection) { %>
	<p>
		<label for="<%=drpTransactionType.ClientID%>">Typ</label>
		<asp:DropDownList ID="drpTransactionType"  runat="server" DataSource='<%#Model.TypeList%>' DataTextField="Text" DataValueField="Value" SelectedValue='<%#Model.SelectedTransactionType%>' />
		<asp:RequiredFieldValidator ID="reqdrpTransactionType" InitialValue="0" ValidationGroup="vgCreateTransaction" runat="server" ErrorMessage="Typ måste väljas" ControlToValidate="drpTransactionType" Display="Dynamic">*</asp:RequiredFieldValidator>
	</p>
	<% } %>
	<% if (Model.DisplaySaveCorrection || Model.DisplaySaveWithdrawal) { %>	
	<p>
		<label for="<%=txtAmount.ClientID%>">Belopp</label>
		<asp:TextBox ID="txtAmount" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtAmount" ValidationGroup="vgCreateTransaction" runat="server" ErrorMessage="Belopp måste anges" ControlToValidate="txtAmount" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RangeValidator ID="rngtxtAmount" ValidationGroup="vgCreateTransaction" runat="server" ErrorMessage="Belopp måste anges som ett positivt tal med kommatecken som decimalavgränsare" ControlToValidate="txtAmount" Display="Dynamic" MinimumValue="0" MaximumValue='99999,99' Type="Double" >*</asp:RangeValidator>
	</p>
	<% } %>
	
	<% if (Model.DisplaySaveWithdrawal) { %>
	<p>
		<label for="<%=drpArticle.ClientID%>">Artikel</label>
		<asp:DropDownList ID="drpArticle" runat="server" DataSource='<%#Model.Articles%>' DataTextField="Text" DataValueField="Value" SelectedValue='<%#Model.SelectedArticleValue%>' />
		<asp:RequiredFieldValidator ID="reqdrpArticle" InitialValue="0" ValidationGroup="vgCreateTransaction" runat="server" ErrorMessage="Artikel är obligatorisk vid uttag" ControlToValidate="drpArticle" Display="Dynamic" Enabled='<%#Model.DisplaySaveWithdrawal%>'>*</asp:RequiredFieldValidator>
	</p>
	<% } %>
	
	<% if (Model.DisplaySaveWithdrawal || Model.DisplaySaveCorrection) { %>
	<div class="control-actions">
		<WpcSynologen:ValidationButton runat="server" OnValidateSuccess="Save" OnEvent="Update_Form" />
		<asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Avbryt" />
	</div>
	
	<asp:ValidationSummary ID="vldSummary" ValidationGroup="vgCreateTransaction" runat="server" />
	<% } %>

</fieldset>