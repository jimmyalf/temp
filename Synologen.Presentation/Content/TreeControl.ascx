<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.TreeControl" Codebehind="TreeControl.ascx.cs" %>
<asp:CustomValidator ID="CustomValidation" runat="server" Display="None"  EnableViewState="false"/>
<asp:TreeView ID="treContentTree" 
              runat="server" 
              OnSelectedNodeChanged="treContentTree_SelectedNodeChanged"
              OnAdaptedSelectedNodeChanged="treContentTree_SelectedNodeChanged"
             PathSeparator="#" >
    <RootNodeStyle ImageUrl="~/common/icons/Root-Node.png" />
</asp:TreeView>
