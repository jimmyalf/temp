<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.AddEditMemberAd" Codebehind="AddEditMemberAd.ascx.cs" %>

<h5>Lägg till/redigera annons </h5>
<br />
    <b>
    <asp:Label ID="lblHeading" runat="server" associatedcontrolid="txtHeading" skinid="Long">Rubrik</asp:Label>
    </b><br />
    <asp:TextBox ID="txtHeading" runat="server" Width="200"></asp:TextBox>
    <br />
  <br />
  <b><asp:Label ID="lblDesc" runat="server" AssociatedControlID="txtDesc" SkinId="Long">Beskrivning</asp:Label></b>
  <br />
  <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" Width="200" Rows="6"></asp:TextBox>
  <br />
  <asp:CheckBox ID="chkActive" runat="server" Text="Aktiv" Checked="True" />
  <br />   <br />
   
</p>
<div class="formItem clearLeft" >  
            <asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" Visible="False" />
            <asp:Button ID="btnCancel" runat="server" Text="Avbryt" OnClick="btnCancel_Click" Visible="False" />    
            <asp:Button ID="btnAdd" runat="server" Text="Lägg till" OnClick="btnAdd_Click" /></div>
  
    <br />
    <br />
    <asp:GridView ID="gvAds" 
                    runat="server" 
                    DataKeyNames="Id" 
                    SkinID="Striped" 
                    OnRowDeleting="gvAds_Deleting" 
                    OnRowEditing="gvAds_Editing" 
                    OnRowCommand="gvAds_RowCommand" 
                    OnRowDataBound="gvAds_RowDataBound" AutoGenerateColumns="False" Width="100%"
					CssClass="table2">
         <AlternatingRowStyle CssClass="table-row-2" />
        <RowStyle CssClass="table-row-1" />
        <HeaderStyle CssClass="table-head" HorizontalAlign="left"/>           
        <Columns>
            <asp:BoundField headerText="Rubrik" DataField="Heading" SortExpression="Name">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:BoundField headerText="Synlig till" DataField="EndDate" SortExpression="EndDate">
                <ItemStyle HorizontalAlign="left" />
            </asp:BoundField>
            <asp:CheckBoxField ReadOnly="true" headerText="Aktiv" DataField="Active" SortExpression="Active" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton id="btnEdit" runat="server" commandname="Edit" ToolTip="Redigera" ImageUrl="/wpc/Member/img/edit.png" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField >
                <ItemTemplate>
                    <asp:ImageButton id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" ToolTip="Radera" ImageUrl="/wpc/Member/img/delete.png" />
                </ItemTemplate>
            </asp:TemplateField> 
        </Columns>
    </asp:GridView>
