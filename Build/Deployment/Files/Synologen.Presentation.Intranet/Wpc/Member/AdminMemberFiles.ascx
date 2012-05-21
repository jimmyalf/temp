<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.AdminMemberFiles" Codebehind="AdminMemberFiles.ascx.cs" %>
<div id="contentlev">  
<h3>Lägg till fil</h3>
        <div class="formItem">
                <b><asp:Label ID="lblFile1" runat="server" AssociatedControlID="uplFile1" SkinId="Long"/></b>
                <asp:FileUpload ID="uplFile1" runat="server" /> 
                <b>
                <asp:Label ID="lblDesc1" runat="server" AssociatedControlID="txtDesc1" SkinId="Long"/></b>
                <b><asp:TextBox ID="txtDesc1" runat="server"></asp:TextBox></b><br /><br />

                <b><asp:Label ID="lblCategory1" runat="server" AssociatedControlID="drpCategory1" SkinId="Long"/>
                <asp:DropDownList ID="drpCategory1" runat="server"></asp:DropDownList>
                </b></br>
                <br />
                <br />
                <asp:Button ID="btnAdd" runat="server" Text="Lägg till" OnClick="btnAdd_Click"  CssClass="button"/><br /><br /><hr /></div>

<h3>Filtrera</h3>
        <div class="formItem">
            <b><asp:Label ID="lblShow" runat="server" AssociatedControlID="drpFileCategories" SkinId="Long" /></b>
            <asp:DropDownList runat="server" ID="drpFileCategories"/>&nbsp;
            <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Visa"  CssClass="button"/>&nbsp;|&nbsp;
            <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Visa alla"  CssClass="button"/>
        </div><br />
<br />

    <asp:GridView ID="gvFiles" 
                    runat="server" 
                    DataKeyNames="Id" 
                    SkinID="Striped" 
                    OnRowDeleting="gvFiles_Deleting" 
                    OnRowDataBound="gvFiles_RowDataBound" AutoGenerateColumns="False" Width="100%">
         <HeaderStyle CssClass="table-head"  />
        <RowStyle CssClass="table-row-1" />
        <AlternatingRowStyle CssClass="table-row-2" />
        <Columns >
            <asp:BoundField headerText="Id" DataField="Id" SortExpression="Id">
                <ItemStyle HorizontalAlign="Center"  />
            </asp:BoundField>
            <asp:BoundField headerText="Namn" DataField="Name" SortExpression="Name">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="Beskrivning" DataField="Description" SortExpression="Description">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="Kategori" DataField="Application" SortExpression="Application">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField headertext="Radera">
                <ItemTemplate>
                    <asp:ImageButton id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" ToolTip="Radera" ImageUrl="/wpc/Member/img/delete.gif" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
	</div>
