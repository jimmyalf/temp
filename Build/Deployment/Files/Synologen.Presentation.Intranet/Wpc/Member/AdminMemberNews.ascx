<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.AdminMemberNews" Codebehind="AdminMemberNews.ascx.cs" %>
<div>
<h5>Lägg/ redigera nyheter</h5>
        <br />
                <b><asp:Label ID="lblHeading" runat="server" Text="Rubrik" AssociatedControlID="txtHeading" SkinId="Long"/></b>
                <br />
                <asp:TextBox ID="txtHeading" runat="server" TextMode="MultiLine" Width="300" SkinID="Wide"/>
		        <br /><br />
                <b><asp:Label ID="lblSummary" runat="server" Text="Sammanfattning" AssociatedControlID="txtSummary" SkinId="Long"/>
                <br />
                <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Width="300" SkinID="Wide"></asp:TextBox>
		<br /><br />
        <WPC:WpcWysiwyg ID="txtBody" runat="server" Width="500" ShowComponent="false" ShowInternalLink="false" /></b>
        
       					    
		    <asp:button ID="btnSave" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Spara nyhet" SkinId="Big"/>
            <asp:Button ID="btnCancel" runat="server" Text="Avbryt" OnClick="btnCancel_Click" />



<asp:GridView ID="gvNews" 
                    runat="server" 
                    DataKeyNames="cId" 
                    SkinID="Striped" 
                    OnRowDeleting="gvNews_Deleting"
                    OnRowEditing="gvNews_Editing" 
                    OnRowDataBound="gvNews_RowDataBound" AutoGenerateColumns="False" Width="500"
                    CssClass="table2">
         <AlternatingRowStyle CssClass="table-row-2" />
        <RowStyle CssClass="table-row-1" />
        <HeaderStyle CssClass="table-head" HorizontalAlign="left"/>    
        <Columns>
            <asp:BoundField headerText="Id" DataField="cId" SortExpression="cId">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField headerText="Namn" DataField="cHeading" SortExpression="cHeading">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
             <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton id="btnEdit" runat="server" commandname="Edit" ToolTip="Redigera" ImageUrl="/wpc/Member/img/edit.png" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField headertext="radera">
                <ItemTemplate>
                    <asp:ImageButton id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" ToolTip="Radera" ImageUrl="/wpc/Member/img/delete.png" />
                </ItemTemplate>
            </asp:TemplateField> 
        </Columns>
    </asp:GridView>
    </div>