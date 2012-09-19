<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier.AdminMemberFiles" Codebehind="AdminMemberFiles.ascx.cs" %>
<%@ Register TagPrefix="tc" Assembly="pdfthumbnail" Namespace="TallComponents.Web.PDF" %>
<div id="synologen-edit-shop" class="synologen-control">
    <fieldset class="add-file">
        <legend>Lägg till fil</legend>
		<p>
			<label for="<%=uplFile1.ClientID %>">Fil</label>
			<asp:FileUpload ID="uplFile1" runat="server" />
		</p>
		<p>
			<label for="<%=txtDesc1.ClientID %>">Beskrivning</label>
			<asp:TextBox ID="txtDesc1" runat="server"></asp:TextBox>
		</p>
		<p>
			<label for="<%=drpCategory1.ClientID %>">Kategori</label>
			<asp:DropDownList ID="drpCategory1" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
		</p>
		<asp:ValidationSummary runat="server" ValidationGroup="AddFile"/>
		<p class="control-actions">
			<asp:Button ID="btnAdd" runat="server" Text="Lägg till" OnClick="btnAdd_Click" ValidationGroup="AddFile" CausesValidation="True"/>
		</p>
    </fieldset>
    <fieldset class="filter-files">
        <legend>Filtrera</legend>		
        <div class="formItem">
            <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpFileCategories" SkinId="Long" Text="Välj kategori"/>
            <asp:DropDownList runat="server" ID="drpFileCategories" DataTextField="Name" DataValueField="Id"/>&nbsp;
            <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Visa" CausesValidation="False"/>&nbsp;|&nbsp;
            <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Visa alla" CausesValidation="False" />
        </div>
    </fieldset>
</div>
<asp:DataList ID="dlFiles" runat="server" OnDeleteCommand="dlFiles_Delete"
                OnItemDataBound="dlFiles_ItemDataBound" RepeatColumns="3">
<ItemTemplate>
<div style="border:#d5ded5 1px solid; padding: 10px; margin-right: 10px; margin-bottom: 10px; text-align:center">
		<asp:HyperLink id="hlThumb" runat="server" Target="_blank"  ></asp:HyperLink>
		<tc:thumbnail id="Thumbnail1" ThumbnailPage="/Wpc/Synologen/Supplier/PdfThumbnail.aspx" index="1" runat="server"></tc:thumbnail>
		<br />
		<asp:HyperLink id="hlText" runat="server" Target="_blank" ></asp:HyperLink><br />
        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label><br />
		<asp:Button runat="server" ID="btnDelete" Text="Radera" CommandName="Delete" />
		</div>
		</ItemTemplate>
</asp:DataList>
