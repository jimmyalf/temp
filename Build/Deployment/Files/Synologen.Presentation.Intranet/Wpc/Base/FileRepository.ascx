<%@ Control Language="C#" AutoEventWireup="true" Codebehind="FileRepository.ascx.cs"
	Inherits="FileRepository.FileRepository" %>
<%@ Register Src="UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<uc1:UploadControl id="UploadControl1" runat="server" Visible="false" />
<asp:ValidationSummary ID="vSummary" CssClass="Error" runat="server" ValidationGroup="AddUpdateFile" />
<div id="file-explorer" class="clear-fix">
	<asp:TreeView ID="treeMyFiles" runat="server" 
		OnSelectedNodeChanged="treeMyFiles_SelectedNodeChanged"
		OnAdaptedSelectedNodeChanged="treeMyFiles_SelectedNodeChanged" 
		PathSeparator="#">
		<RootNodeStyle ImageUrl="~/Wpc/Base/Img/Folder-Open.png" />
		<ParentNodeStyle ImageUrl="~/Wpc/Base/Img/Folder-Closed.png" />
		<LeafNodeStyle ImageUrl="~/Wpc/Base/Img/Folder-Closed.png" />
		<SelectedNodeStyle ImageUrl="~/Wpc/Base/Img/Folder-Open.png" />
	</asp:TreeView>
<div id="file-view">
	<ul id="file-menu">
		<li id="upload"><asp:LinkButton ID="lbUpload" runat="server" OnCommand="ToolBarAction_Click" CommandName="Upload">Ladda upp filer</asp:LinkButton></li>
		<li id="cut"><asp:LinkButton ID="lbCut" runat="server" OnCommand="ToolBarAction_Click" CommandName="Cut">Klipp ut</asp:LinkButton></li>
		<li id="copy"><asp:LinkButton ID="lbCopy" runat="server" OnCommand="ToolBarAction_Click" CommandName="Copy">Kopiera</asp:LinkButton></li>
		<li id="paste"><asp:LinkButton ID="lbPaste" runat="server" OnCommand="ToolBarAction_Click" CommandName="Paste">Klistra in</asp:LinkButton></li>
		<li id="rename"><asp:LinkButton ID="lbRename" runat="server" OnCommand="ToolBarAction_Click" CommandName="Rename">Byt namn</asp:LinkButton></li>
		<li id="remove"><asp:LinkButton ID="lbRemove" runat="server" OnClientClick="return confirm('Alla förbockade filer och mappar kommer att raderas!')" OnCommand="ToolBarAction_Click" CommandName="Remove">Ta bort</asp:LinkButton></li>
		<li id="newfolder"><asp:LinkButton ID="lbNewFolder" runat="server" OnCommand="ToolBarAction_Click" CommandName="Newfolder">Ny mapp</asp:LinkButton></li>
	</ul>
	<asp:PlaceHolder ID="phNewfolder" runat="server" Visible="false">
		<div id="add-update-folder-file">
			<asp:Literal ID="ltFileText" runat="server" Text="Namn: " />
			<asp:TextBox ValidationGroup="AddUpdateFile" ID="txtNewFile" runat="server"/>
			<asp:RegularExpressionValidator ValidationExpression='^[^ \\/:*?""<>|]+([ ]+[^ \\/:*?""<>|]+)*$' ID="RegularExpressionValidator1" runat="server" ErrorMessage='Namnet får inte börja eller sluta med mellanslag eller innehålla tecknena \ / : * ? " < > |' ControlToValidate="txtNewFile" ValidationGroup="AddUpdateFile" Display="Dynamic" Text="*" />
			<asp:RequiredFieldValidator ID="rfvNewName" ControlToValidate="txtNewFile" runat="server" ErrorMessage="Du måste ange ett namn" ValidationGroup="AddUpdateFile" Display="Dynamic" Text="*"/>
			<asp:Literal ID="ltFileExtension" runat="server" />&nbsp;<asp:Button ID="btnSave" runat="server" Text="spara" OnCommand="ToolBarAction_Click" CommandName="AddUpdate" ValidationGroup="AddUpdateFile" />
		</div>
	</asp:PlaceHolder>
	<asp:PlaceHolder ID="phComponentFileConnections" runat="server" Visible="false">
	<div id="component-file-connections">
		<asp:Literal ID="ltConnectionsText" runat="server" Text="Följande fil(er) används på ett eller flera ställen och kan ej tas bort:" />
		<asp:Repeater runat="server" ID="rptConnectedFiles" OnItemDataBound="rptConnectedFiles_ItemDataBound">
			<HeaderTemplate>
				<ul>
			</HeaderTemplate>
			<ItemTemplate>
				<li>
					<asp:Literal ID="ltFileName" runat="server" Text="" />
					<asp:Repeater runat="server" ID="rptComponentFileConnections">
					<HeaderTemplate>
						<ul>
					</HeaderTemplate>
					<ItemTemplate>
						<li>			
							<a href="<%# DataBinder.Eval(Container.DataItem, "ItemUrl")%>">
								<%# DataBinder.Eval(Container.DataItem, "ComponentName")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "ItemName")%>
							</a>
						</li>
					</ItemTemplate>
					<FooterTemplate>
						</ul>
					</FooterTemplate>
					</asp:Repeater>
				</li>
			</ItemTemplate>
			<FooterTemplate>
				</ul>
			</FooterTemplate>
		</asp:Repeater>
	</div>
	</asp:PlaceHolder>
	<asp:Repeater runat="server" ID="rptThumbs" OnItemDataBound="rptThumbs_ItemDataBound">
		<HeaderTemplate>
			<ul id="thumbs" class="clear-fix">
		</HeaderTemplate>
		<ItemTemplate>
			<li>			
			<asp:Literal ID="ltPath" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Path")%>' />			
			<asp:Image ID="imgItem" runat="server"/>
			<span class="filename"><%# DataBinder.Eval(Container.DataItem, "Name")%></span>
			<asp:CheckBox ID="fileSelection" Checked="false" Runat="server" Enabled="true"/>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul></FooterTemplate>
	</asp:Repeater>
</div>
</div>

