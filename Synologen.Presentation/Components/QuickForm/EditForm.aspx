<%@ Page Language="C#" MasterPageFile="~/components/QuickForm/QuickFormMain.master"
    AutoEventWireup="true" Inherits="Spinit.Wpc.QuickForm.Presentation.Components.QuickForm.EditForm"
    Title="Untitled Page" ValidateRequest="false" Codebehind="EditForm.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phQuickForm" runat="Server">
    <div id="dCompMain" class="Components-QuickForm-EditForm-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>QuickForm</h1>
                <fieldset>
                    <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
                    <div class="formItem">
                        <asp:Label ID="lblFrmType" runat="server" AssociatedControlID="drpFrmType" SkinID="Long" />
                        <asp:DropDownList ID="drpFrmType" runat="server" OnSelectedIndexChanged="drpFrmType_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="formItem clearLeft">
                        <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinID="Long" />
                        <asp:TextBox ID="txtName" runat="server" />
                    </div>
                    <div class="formItem clearLeft" id="dFormContent" runat="server">
                        <asp:Label ID="lblContent" runat="server" AssociatedControlID="txtContent" SkinID="Long" />
                        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="800" Height="300"></asp:TextBox>
                    </div>
                </fieldset>
                <div id="dMailParameters" runat="server">
                    <fieldset>
                        <legend>Mail parameters</legend>
                        <div class="formItem clearLeft">
                            <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject" SkinID="Long"></asp:Label>
                            <asp:TextBox ID="txtSubject" runat="server" />
                        </div>
                        <div class="formItem clearLeft">
                            <asp:Label ID="lblMailTo" runat="server" AssociatedControlID="txtMailTo" SkinID="Long"></asp:Label>
                            <asp:TextBox runat="server" ID="txtMailTo" />
                        </div>
                        <div class="formItem clearLeft" id="dMailFrom" runat="server">
                            <asp:Label ID="lblMailFromDefault" runat="server" AssociatedControlID="txtMailFromDefault" SkinID="Long"></asp:Label>
                            <asp:TextBox ID="txtMailFromDefault" runat="server" />
                        </div>
                    </fieldset>
                </div>
                <fieldset>
                    <legend>Submit event</legend>
                    <div class="formItem">
                        <asp:Label ID="lblSubmitType" runat="server" AssociatedControlID="drpSubmitType" SkinID="Long" />
                        <asp:DropDownList ID="drpSubmitType" runat="server" OnSelectedIndexChanged="drpSubmitType_SelectedIndexChanged" AutoPostBack="true" />
                    </div>
                    <div class="formItem clearLeft" id="dReturnUrl" runat="server">
                        <asp:Label ID="lblReturnUrl" runat="server" AssociatedControlID="txtReturnUrl" SkinID="Long"></asp:Label>
                        <asp:TextBox ID="txtReturnUrl" runat="server" />
                    </div>
                    <div class="formItem clearLeft" id="dComitText" runat="server" visible="false">
                        <asp:Label ID="lblComitText" runat="server" AssociatedControlID="txtComitText" SkinID="Long"></asp:Label>
                        <asp:TextBox ID="txtComitText" runat="server" />
                    </div>                    
                    <div class="formItem clearLeft" id="dActivateConfirmationEmail" runat="server" visible="true">
                        <asp:checkbox ID="chkActivateConfirmationEmail" AutoPostBack="true" OnCheckedChanged="chkActivateConfirmationEmail_CheckedChanged" runat="server"></asp:checkbox>
                    </div>                    
                    <div id="dWysiwyg" class="dWysiwyg clearLeft" runat="server" visible="false">
						<uc2:WpcWysiwyg ID="txtBody"  Mode="Basic" runat="server"/>
                    </div>                   
                </fieldset>
                <div class="formCommands">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" SkinID="Big" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

