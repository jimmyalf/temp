<%@ Page Title="" Language="C#" MasterPageFile="~/Components/Synologen/SynologenOpq.Master"
	AutoEventWireup="true" CodeBehind="OpqIndex.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.OpqIndex" %>

<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg"
	TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phOpqMainContent" runat="server">
	<div class="Synologen-OpqIndex-aspx fullBox">
		<div class="wrap">
				<fieldset>
					<legend>Central rutin</legend>					
					<div class="formItem clearLeft">
                        <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long" Text="Namn:"/>
                        <asp:TextBox ID="txtName" runat="server" SkinID="Wide"/>
					</div>
					<asp:PlaceHolder ID="phRoutine" runat="server">
					<uc1:WpcWysiwyg ID="_wysiwyg" runat="server" Mode="Basic" />
					</asp:PlaceHolder>
					<div class="formCommands">
						<asp:button id="btnSaveForLater" runat="server" onclick="btnSave_Click" commandname="SaveForLater"
							text="Spara" skinid="Big" />
						<asp:button id="btnSaveAndPublish" runat="server" onclick="btnSave_Click" commandname="SaveAndPublish" text="Spara & Publicera" skinid="Big" />
					</div>
				</fieldset>
				<fieldset>
		        <legend>Centrala dokument</legend>		
		        <div class="formItem">
		        <asp:FileUpload ID="uplFile" runat="server" />
		            <asp:Label ID="lblFileName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
		            <asp:TextBox runat="server" ID="txtFileName"/>
		        </div>
		        <div class="formCommands">
		            <asp:button ID="btnUploadFile" runat="server" OnClick="btnUploadFile_Click" Text="Spara"  SkinId="Big"/>
		        </div>
	        </fieldset>
        
			<br />
			<asp:GridView id="test" runat="server"></asp:GridView>
            <asp:GridView ID="gvFiles" 
                runat="server" 
                OnRowCreated="gvFiles_RowCreated" 
                DataKeyNames="Id" 
                SkinID="Striped" 
                OnRowEditing="gvFiles_Editing" 
                OnRowDeleting="gvFiles_Deleting" 
                OnRowCommand="gvFiles_RowCommand">
                <Columns>
		            <asp:TemplateField headertext="Namn"  ItemStyle-HorizontalAlign="Center" >
			            <ItemTemplate>
							<asp:Literal ID="ltFile" runat="server" />
			            </ItemTemplate>
		            </asp:TemplateField>
		            <asp:TemplateField headertext="Namn"  ItemStyle-HorizontalAlign="Center" >
			            <ItemTemplate>
							<asp:Literal ID="ltFileDate" runat="server" />
			            </ItemTemplate>
		            </asp:TemplateField>
                    <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center"/>
		            <asp:TemplateField headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"  >
			            <ItemTemplate>
                            <asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" />
			            </ItemTemplate>
		            </asp:TemplateField>
                </Columns>
            </asp:GridView>          

		</div>
	</div>
</asp:Content>
