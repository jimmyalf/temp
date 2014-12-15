<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.AddEditMemberAd" Codebehind="AddEditMemberAd.ascx.cs" %>

<h5>L�gg till/redigera annons </h5>
        <div class="formItem clearLeft">
                <b><asp:Label ID="lblName" runat="server" AssociatedControlID="txtName">Namn</asp:Label></b>
                <asp:TextBox ID="txtName" runat="server" Width="200"></asp:TextBox>
         </div>
         <div class="formItem clearLeft">
                <b><asp:Label ID="lblTelephone" runat="server" AssociatedControlID="txtTelephone">Telefon</asp:Label></b>
                <asp:TextBox ID="txtTelephone" runat="server" Width="200"></asp:TextBox>
         </div>
         <div class="formItem clearLeft">
                <b><asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail">E-post</asp:Label></b>
                <asp:TextBox ID="txtEmail" runat="server" Width="200"></asp:TextBox>
         </div>
    <b><asp:Label ID="lblHeading" runat="server" associatedcontrolid="txtHeading" skinid="Long">Rubrik</asp:Label>
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
            <asp:Button ID="btnAdd" runat="server" Text="L�gg till" OnClick="btnAdd_Click" /></div>
  
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
            <asp:BoundField headerText="Id" DataField="Id" SortExpression="Id">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="Namn" DataField="Name" SortExpression="Name">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="Telefon" DataField="Telephone" SortExpression="Telephone">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="E-post" DataField="Email" SortExpression="Email">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
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
                    <asp:ImageButton id="btnEdit" runat="server" commandname="Edit" ToolTip="Redigera" AlternateText="Redigera" ImageUrl="/wpc/Member/img/edit.png" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField >
                <ItemTemplate>
                    <asp:ImageButton id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" AlternateText="Radera"  commandname="Delete" ToolTip="Radera" ImageUrl="/wpc/Member/img/delete.png" />
                </ItemTemplate>
            </asp:TemplateField> 
        </Columns>
    </asp:GridView>
