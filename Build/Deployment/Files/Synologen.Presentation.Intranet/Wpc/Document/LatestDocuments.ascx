<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestDocuments.ascx.cs" Inherits="Spinit.Wpc.Document.Presentation.Site.LatestDocuments" %>
<asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False"
    DataKeyNames="Id">
    <Columns>
    <asp:HyperLinkField HeaderText="Namn" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="ViewDocument.aspx?documentId={0}" Target="_blank" DataTextField="Name" />
    <asp:BoundField HeaderText="Beskrivning" DataField="Description" />
    <asp:BoundField HeaderText="Datum" DataField="CreatedDate" DataFormatString="{0:yyyy-MM-dd}" />
    </Columns>
    </asp:GridView>