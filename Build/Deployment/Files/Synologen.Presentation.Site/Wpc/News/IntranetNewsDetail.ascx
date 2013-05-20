<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetNewsDetail.ascx.cs" Inherits="Spinit.Wpc.News.Presentation.Intranet.wpc.News.IntranetNewsDetail" %>
<%@ Register Src="../Common/WebControls/FileManagerControl.ascx" TagName="FileManagerControl"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc1" %>
<asp:PlaceHolder ID="phEntry" runat="server">
<div id="entry">

    <asp:HyperLink ID="hlAuthorPhoto" runat="server" ToolTip="Bild av f&#246;rfattaren" ><asp:Image ID="imgAuthorPhoto" runat="server" CssClass="author-photo"/></asp:HyperLink>
    <h3><a href="#" title="Läs hela inlägget"><asp:Literal
            ID="ltHeading" runat="server"></asp:Literal></a></h3>
    
    <asp:PlaceHolder ID="phWysiwyg" runat="server" Visible="False">
            <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="False" Text="Error"></asp:Label>
                      
            
            <fieldset class="publish-to">
                                    <legend><asp:Label ID="lblGroups" runat="server" Text="Publicera f&#246;r"></asp:Label></legend>
                
                <asp:CheckBoxList ID="chklGroups" runat="server" DataTextField="Name" DataValueField="Id"  RepeatDirection="Horizontal" RepeatLayout="Flow">
                </asp:CheckBoxList>
            </fieldset>
            <asp:PlaceHolder ID="phUpload" runat="server" Visible="False">
            <div id="browse-for-attachments">     
            <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" CssClass="attach-file" Text="Bifoga..." />
            <uc2:FileManagerControl ID="FileManagerControl1" runat="server" Visible="False" AllowUpload="True"
				HeadingLevel="3"
				RootDirectoryName="Mina filer"
				ShowInternalNavigation="True"
				UniqueClientID="Test-File-Manager" />
					</div>
			</asp:PlaceHolder>
            <p class="edit-entry-heading">
                <asp:Label ID="lblHeading" runat="server" Text="Rubrik" AssociatedControlID="txtHeading"></asp:Label>
                <asp:TextBox ID="txtHeading" CssClass="entry-heading" runat="server"></asp:TextBox><asp:RequiredFieldValidator
					ID="vlrHeading" runat="server" ErrorMessage="Rubrik saknas" ControlToValidate="txtHeading" ></asp:RequiredFieldValidator>
            </p>
            <uc1:WpcWysiwyg id="WpcWysiwyg1" mode="basic" runat="server">
            </uc1:WpcWysiwyg>
            <p class="preview-save">
            <asp:Button ID="btnPreview" runat="server" Text="F&#246;rhandsgranska" OnClick="btnPreview_Click" />
            <asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" /></p>
            
    </asp:PlaceHolder>
		&nbsp;
		<asp:Literal ID="ltBody" runat="server"></asp:Literal>
	
   <div class="meta clear-fix">
        <asp:PlaceHolder ID="phAttachments" runat="server">
        <dl class="attachments collapsible">
        <dt>Bilagor (<asp:Label ID="lblNoOfAttachments" runat="server" Text="0"></asp:Label>)</dt>
        <dd>
            <asp:Repeater ID="rptAttachments" runat="server" OnItemCommand="rptAttachments_ItemCommand" OnItemDataBound="rptAttachments_ItemDataBound">
            <HeaderTemplate><ul></HeaderTemplate>
            <ItemTemplate>
				<li>
					<a href="<%=Spinit.Wpc.Base.Business.Globals.CommonFileUrl%><%# DataBinder.Eval(Container.DataItem, "Name") %>"><%# ShortFilename(DataBinder.Eval(Container.DataItem, "Name").ToString()) %></a>
					<asp:ImageButton  ID="btnDeleteAttacment" ToolTip="Ta bort bifogad fil" CommandName="Delete"  runat="server" ImageUrl="/wpc/News/img/delete.gif" AlternateText="Ta bort bifogad fil" />
				</li>
			</ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
        </dd>
        </dl>
        </asp:PlaceHolder>
        <span class="name-date">
        <asp:HyperLink ID="hlAuthor" runat="server"></asp:HyperLink>
        - <asp:Label ID="lblDateTime" runat="server"></asp:Label></span>
        <asp:PlaceHolder ID="phEditRemove" runat="server">
        <p class="edit-remove">
        <asp:Button ID="btnEdit" runat="server" Text="Redigera inl&#228;gget" Visible="False" OnClick="btnEdit_Click" />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Ta bort inl&#228;gget" Visible="False" />
        </p>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phPreview" Visible="False" runat="server">
		<p class="preview-save">
			<asp:Button ID="btnPreviewCancel" runat="server" Text="Avbryt f&#246;rhandsgranskning" OnClick="btnPreviewCancel_Click" />
			<asp:Button ID="btnPreviewSave" runat="server" Text="Spara" OnClick="btnSave_Click" />
		</p>
	</asp:PlaceHolder>
	</div>
    
    
</div>
<asp:PlaceHolder ID="phReadByList" Visible="False" runat="server">
<div id="has-read-this-entry">
	<h2>Personer som har läst anslaget</h2>		
	<asp:Repeater ID="rptReadByList" runat="server">
	<HeaderTemplate><ul></HeaderTemplate>
	<ItemTemplate><li><span class="meta"><a href='<%=AuthorPageUrl %>?userId=<%# DataBinder.Eval(Container.DataItem, "cId") %>'><%# DataBinder.Eval(Container.DataItem, "cFirstName") %> <%# DataBinder.Eval(Container.DataItem, "cLastName") %></a> - <%# ((DateTime) DataBinder.Eval(Container.DataItem, "cReadDate")).ToString("dd MMM yyyy")%>, kl. <%# ((DateTime) DataBinder.Eval(Container.DataItem, "cReadDate")).ToString("HH:mm")%> </span></li></ItemTemplate>
	<FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>    
</div>
</asp:PlaceHolder>
</asp:PlaceHolder>
<asp:PlaceHolder ID="phNoEntry" Visible="false" runat="server">
<p>Nyheten/anslaget finns inte.</p>
</asp:PlaceHolder>

