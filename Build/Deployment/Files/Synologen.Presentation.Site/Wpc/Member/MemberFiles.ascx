<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberFiles" Codebehind="MemberFiles.ascx.cs" %>
    <asp:GridView ID="gvFiles" 
                    runat="server"                
                    DataKeyNames="Id" 
                    SkinID="Striped"                    
                    AllowSorting="false" 
                    AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField headerText="Namn" DataField="Name" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="Beskrivning" DataField="Description" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="Kategori" DataField="Application" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Visa" ItemStyle-Width="30px">
                <ItemTemplate>
                <a href='<%# DataBinder.Eval(Container.DataItem, "Link") %>'><img src='<%# DataBinder.Eval(Container.DataItem, "Pic") %>' border="0" /></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
