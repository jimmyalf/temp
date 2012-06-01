<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqFileGridView.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.Controls.OpqFileGridView" %>

<h3><asp:Literal ID="headerText" runat="server"/></h3>
<asp:GridView ID="gvFiles" runat="server" OnRowCreated="gvFiles_RowCreated" DataKeyNames="Id" SkinID="Striped">
	<Columns>
		<asp:TemplateField headertext="Namn"  ItemStyle-HorizontalAlign="Center" >
			<ItemTemplate>
				<asp:Literal ID="ltFile" runat="server" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField headertext="Datum"  ItemStyle-HorizontalAlign="Center" >
			<ItemTemplate>
				<asp:Literal ID="ltFileDate" runat="server" />
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>
