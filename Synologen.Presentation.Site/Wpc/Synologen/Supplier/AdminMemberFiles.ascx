<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.Supplier.AdminMemberFiles" Codebehind="AdminMemberFiles.ascx.cs" %>
<%@ Register TagPrefix="tc" Assembly="pdfthumbnail" Namespace="TallComponents.Web.PDF" %>
    <fieldset>
        <legend>Lägg till fil</legend>
        <div class="formItem clearLeft">
                <asp:Label ID="lblFile1" runat="server" AssociatedControlID="uplFile1" SkinId="Long" Text="Fil"/>
                <asp:FileUpload ID="uplFile1" runat="server" />
                <asp:Label ID="lblDesc1" runat="server" AssociatedControlID="txtDesc1" SkinId="Long" Text="Beskrivning"/>
                <asp:TextBox ID="txtDesc1" runat="server"></asp:TextBox>
                <asp:Label ID="lblCategory1" runat="server" AssociatedControlID="drpCategory1" SkinId="Long" Text="Kategori"/>
                <asp:DropDownList ID="drpCategory1" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            <asp:Button ID="btnAdd" runat="server" Text="Lägg till" OnClick="btnAdd_Click" /></div>
    </fieldset>
    <fieldset>
        <legend>Filtrera</legend>		
        <div class="formItem">
            <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpFileCategories" SkinId="Long" Text="Välj kategori"/>
            <asp:DropDownList runat="server" ID="drpFileCategories" DataTextField="Name" DataValueField="Id"/>&nbsp;
            <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Visa"/>&nbsp;|&nbsp;
            <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Visa alla" />
        </div>
    </fieldset>
<asp:DataList ID="dlFiles" runat="server" OnDeleteCommand="dlFiles_Delete"
                OnItemDataBound="dlFiles_ItemDataBound" RepeatColumns="3">
<ItemTemplate>
<div style="border:#d5ded5 1px solid; padding: 10px; margin-right: 10px; margin-bottom: 10px; text-align:center">
		<asp:HyperLink id="hlThumb" runat="server" Target="_blank"  ></asp:HyperLink>
		<tc:thumbnail id="Thumbnail1" ThumbnailPage="/Wpc/Synologen/Supplier/PdfThumbnail.aspx" index="1" runat="server"></tc:thumbnail>
		<%--path="pdfthumbnail.pdf"--%>
		<br />
		<asp:HyperLink id="hlText" runat="server" Target="_blank" ></asp:HyperLink><br />
        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label><br />
		<asp:Button runat="server" ID="btnDelete" Text="Radera" CommandName="Delete" />
		</div>
		</ItemTemplate>
</asp:DataList>
