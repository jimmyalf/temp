<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Intranet.AdminNews" Codebehind="AdminNews.ascx.cs" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc1" %>


<div>
    <asp:LinkButton ID="btnShowHideEdit" runat="server" Visible="False" OnClick="btnShowHideEdit_Click">Edit</asp:LinkButton>
    </div>
    <div>
    <asp:Panel ID="pnNewsAdmin" runat="server" Visible="True">
    
<h3>Add/Edit news</h3>
<asp:GridView ID="gvNews" 
                    runat="server" 
                    DataKeyNames="cId" 
                    SkinID="Striped" 
                    OnRowDeleting="gvNews_Deleting"
                    OnRowEditing="gvNews_Editing" 
                    OnRowDataBound="gvNews_RowDataBound" AutoGenerateColumns="False" Width="500"
                    CssClass="striped_table">
    <RowStyle CssClass="odd" />
<AlternatingRowStyle CssClass="even" />
<HeaderStyle  Font-Bold="true" />   
    <Columns>
        <asp:BoundField headerText="Id" DataField="cId" SortExpression="cId" Visible="false">
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField headerText="Heading" DataField="cHeading" SortExpression="cHeading">
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>
         <asp:TemplateField headertext="Edit">
            <ItemTemplate>
                <asp:ImageButton id="btnEdit" runat="server" commandname="Edit" ToolTip="Edit" ImageUrl="/wpc/News/img/edit.png" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField headertext="Delete">
            <ItemTemplate>
                <asp:ImageButton id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" ToolTip="Delete" ImageUrl="/wpc/News/img/delete.png" />
            </ItemTemplate>
        </asp:TemplateField> 
    </Columns>
</asp:GridView>
        <br />
                <b><asp:Label ID="lblHeading" runat="server" Text="Heading" AssociatedControlID="txtHeading" SkinId="Long"/></b>
                <br />
                <asp:TextBox ID="txtHeading" runat="server"  Width="500" SkinID="Wide"/>
		        <br /><br />
                <b><asp:Label ID="lblSummary" runat="server" Text="Summary" AssociatedControlID="txtSummary" SkinId="Long"/>
                <br />
                <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" Width="500" SkinID="Wide" Height="70px"></asp:TextBox>
		<br /><br />
        <uc1:WpcWysiwyg id="WpcWysiwyg1" mode="basic" runat="server">
            </uc1:WpcWysiwyg></b>
        
       					    
		    <asp:button ID="btnSave" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Save"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />



</asp:Panel>
    
    </div>
