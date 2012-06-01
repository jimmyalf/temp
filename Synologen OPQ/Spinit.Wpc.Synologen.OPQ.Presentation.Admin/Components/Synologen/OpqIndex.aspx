<%@ Page Title="" Language="C#" MasterPageFile="~/Components/Synologen/SynologenOpq.Master"
	AutoEventWireup="true" CodeBehind="OpqIndex.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.OpqIndex" %>

<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc1" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phOpqMainContent" runat="server">
	<div class="Synologen-OpqIndex-aspx fullBox">
		<div class="wrap">
			<asp:PlaceHolder ID="phMenu" runat="server" Visible="false">
				<fieldset>
					<legend>Meny</legend>					
					<div class="formItem clearLeft">
                        <asp:Label ID="lblMenuName" runat="server" AssociatedControlID="txtMenuName" SkinId="Long" Text="Namn:"/>
                        <asp:TextBox ID="txtMenuName" runat="server" SkinID="Wide"/>
					</div>
					<div class="formItem clearLeft">
						<asp:button id="btnSaveMenu" runat="server" onclick="btnSaveMenu_Click" text="Spara" skinid="Big" />
					</div>

				</fieldset>
			</asp:PlaceHolder>
			<asp:PlaceHolder ID="phRoutine" runat="server" Visible="false">
				<fieldset>
					<legend>Rutin</legend>					
					<div class="document-status">
						<asp:Label ID="lblDocumentStatus" runat="server" SkinId="Long" Text="Dokument status: "/>
						<asp:Literal ID="ltDocumentStatus" runat="server" Text="-" Visible="true"/>
					</div>
					<div class="wysiwyg-item clearLeft">
                        <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long" Text="Namn:"/>
                        <asp:TextBox ID="txtName" runat="server" SkinID="Wide"/>
					</div>
					<asp:PlaceHolder ID="phPreview" runat="server">
						<div class="current documentcontent">
							<asp:Literal ID="ltContentPreview" runat="server" />
						</div>
						<div class="wysiwyg-item">
							<asp:button id="btnEdit" runat="server" onclick="btnEdit_Click" text="Redigera" skinid="Big" />
						</div>
					</asp:PlaceHolder>
					<asp:PlaceHolder ID="phWysiwyg" runat="server">
						<uc1:WpcWysiwyg ID="_wysiwyg" runat="server" Mode="Basic" />
						<div class="wysiwyg-item">
							<asp:button id="btnSaveForLater" runat="server" onclick="btnSave_Click" commandname="SaveForLater"
								text="Spara" skinid="Big" />
							<asp:button id="btnSaveAndPublish" runat="server" onclick="btnSave_Click" commandname="SaveAndPublish" text="Spara & Publicera" skinid="Big" />
						</div>
					</asp:PlaceHolder>
				</fieldset>
				<fieldset>
					<legend>Historik</legend>
					<asp:PlaceHolder ID="phViewHistory" runat="server" Visible="false">
					<div class="history documentcontent">
						<asp:Literal ID="ltHistory" runat="server" />
					</div>
					</asp:PlaceHolder>
					<asp:PlaceHolder ID="phNoHistory" runat="server" Visible="false">
						Det finns ingen historik för vald rutin.
					</asp:PlaceHolder>
					<asp:PlaceHolder ID="phHistory" runat="server">					
						<asp:GridView ID="gvHistory" 
							runat="server" 
							OnRowCommand="gvHistory_RowCommand"
							DataKeyNames="Id" 
							SkinID="Striped">
							<Columns>
								<asp:BoundField HeaderText="Datum" ItemStyle-HorizontalAlign="Center" DataField="ChangedDate" />
								<asp:BoundField HeaderText="Senast ändrad av" ItemStyle-HorizontalAlign="Center" DataField="ChangedByName" />
								<asp:TemplateField HeaderText="Visa" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:Button CommandArgument='<%# ((DateTime) Eval("HistoryDate")).Ticks %>' id="btnShow" runat="server" commandname="show" text="Visa" CssClass="btnSmall" HeaderStyle-CssClass="controlColumn" />
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>        
		                </asp:GridView>
					</asp:PlaceHolder>
				</fieldset>
				<fieldset>
					<legend>Dokument</legend>		
					<div class="formItem">
					    <asp:Label ID="lblUploadFileHeader" runat="server" SkinId="Long" Text="Välj fil att ladda upp:"/>
						<asp:FileUpload ID="uplFile" runat="server" />
					</div>
					<div class="formItem">
						<asp:button ID="btnUploadFile" runat="server" OnClick="btnUploadFile_Click" Text="Ladda Upp"  SkinId="Big"/>
						<asp:button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Ångra"  SkinId="Big" Visible="false"/>
					</div>
					<div id="routine-documents">
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
								<asp:TemplateField headertext="Datum"  ItemStyle-HorizontalAlign="Center" >
									<ItemTemplate>
										<asp:Literal ID="ltFileDate" runat="server" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Ändra" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:Button id="btnOverwrite" runat="server" commandname="overwrite" text="Skriv över befintlig" CssClass="btnSmall" HeaderStyle-CssClass="controlColumn" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Upp" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:Button id="btnMoveUp" runat="server" commandname="moveup" text="Flytta upp" CssClass="btnSmall" HeaderStyle-CssClass="controlColumn" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Ner" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:Button id="btnMoveDown" runat="server" commandname="movedown" text="Flytta ner" CssClass="btnSmall" HeaderStyle-CssClass="controlColumn" />
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"  >
									<ItemTemplate>
										<asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" />
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView> 
				</div>
				</fieldset>
            </asp:PlaceHolder>         
		</div>
	</div>
</asp:Content>
