<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Document.ascx.cs" Inherits="Spinit.Wpc.Document.Presentation.Site.Document" %>
<asp:Panel ID="pnlTest" runat="server">
<table width="100%">
<tr><td>
<div>
<asp:TreeView ID="tvwDocumentNode" runat="server" OnSelectedNodeChanged="tvwDocumentNode_SelectedNodeChanged" OnAdaptedSelectedNodeChanged="tvwDocumentNode_SelectedNodeChanged" />
</div>
</td><td>
<div>
    <asp:GridView ID="gvDocuments" runat="server" AutoGenerateColumns="False"
    DataKeyNames="cId">
    <Columns>
    <asp:HyperLinkField HeaderText="Namn" DataNavigateUrlFields="cId" DataNavigateUrlFormatString="ViewDocument.aspx?documentId={0}" Target="_blank" DataTextField="cName" />
    <asp:BoundField HeaderText="Beskrivning" DataField="cDescription" />
    <asp:BoundField HeaderText="Datum" DataField="cCreatedDate" DataFormatString="{0:yyyy-MM-dd}" />
    </Columns>
    </asp:GridView>
</div>
</td></tr>
</table>
</asp:Panel>