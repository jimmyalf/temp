<%@ Page Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Index" MasterPageFile="~/components/Synologen/SynologenMain.master" Codebehind="Index.aspx.cs" %>
<%@ Register Src="SynologenSubMenu.ascx" TagName="SubMenu" TagPrefix="syn" %>
<asp:Content runat="server" ContentPlaceHolderID="SubMenuPlaceHolder">
	<syn:SubMenu runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" Runat="Server">
    <div id="dCompMain" class="Components-Synologen-Index-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Medlemmar</h1>
                
				<asp:PlaceHolder id="plFilterByShop" runat="server" Visible="false">        	                	                  	                 	                
				<fieldset><legend>Filter</legend>
					<div class="formItem clearLeft">
						<label >Visar Användare för Butik:</label>&nbsp;<span><%=SelectedShop.Name %></span><br /><br />
						<input type="button" name="inputNew" class="btnBig" onclick="window.location='EditMember.aspx?shopId=<%=SelectedShop.ShopId %>'" value="Skapa ny användare" />
						<input type="button" name="inputBack" class="btnBig" onclick="window.location='Shops.aspx'" value="Tillbaka" />
					</div> 	   	                                                     
				</fieldset>
				</asp:PlaceHolder>	
                
                <asp:PlaceHolder id="plRegularFilter" runat="server" Visible="true">
                <fieldset>
	                <legend>Filtrera medlemmar</legend>		
	                <div class="formItem">
	                    <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpCategories" SkinId="Long">Medlemskategori</asp:Label>
	                    <asp:DropDownList runat="server" ID="drpCategories"/>
	                </div>
	                <div class="formItem ">
	                    <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" SkinId="Long">Textfilter</asp:Label>
	                    <asp:TextBox runat="server" ID="txtSearch"/>
	                </div>
	                <div class="formItem clearLeft">
						<br />
	                    <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text='S&ouml;k'  SkinId="Big"/>
	                </div>	                
                </fieldset>
				</asp:PlaceHolder>
				
                <div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
                
                <asp:GridView ID="gvMembers" 
                                runat="server" 
                                OnRowCreated="gvMembers_RowCreated" 
                                DataKeyNames="cMemberId" 
                                OnSorting="gvMembers_Sorting" 
                                OnPageIndexChanging="gvMembers_PageIndexChanging" 
                                SkinID="Striped" 
                                OnRowEditing="gvMembers_Editing" 
                                OnRowDeleting="gvMembers_Deleting" 
                                OnRowCommand="gvMembers_RowCommand" 
                                OnRowDataBound="gvMembers_RowDataBound" 
                                AllowSorting="true">
                    <Columns>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn" >
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectHeader" runat="server" OnCheckedChanged="chkSelectHeader_CheckedChanged" AutoPostBack="true"  />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField headerText="Id" DataField="cMemberId" SortExpression="cMemberId" ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn"/>
                        <asp:BoundField headerText="Organisation" DataField="cOrgName" SortExpression="cOrgName"/>
                        <asp:BoundField headerText="Medlem" DataField="cFullUserName" SortExpression="cFullUserName"/>
                        <asp:BoundField headerText="Epost" DataField="cEmail" SortExpression="cEmail"/>
						<asp:TemplateField headertext="Aktiv" SortExpression="cActive">
							<ItemStyle CssClass="center" />
							<ItemTemplate>
								<asp:Image id="imgActive" runat="server" />
							</ItemTemplate>
						</asp:TemplateField>
                        <asp:ButtonField HeaderText="Filer" Text="Visa filer" CommandName="Files" ButtonType="Button" ControlStyle-CssClass="btnSmall"   HeaderStyle-CssClass="controlColumn"  ItemStyle-HorizontalAlign="Center"/>
                        <asp:ButtonField HeaderText="Filer" Text="Lägg till" CommandName="AddFiles" ButtonType="Button" ControlStyle-CssClass="btnSmall"   HeaderStyle-CssClass="controlColumn"  ItemStyle-HorizontalAlign="Center"/>
                        <asp:ButtonField HeaderText="Redigera" Text="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall"   HeaderStyle-CssClass="controlColumn"  ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Radera" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center">
	                        <ItemTemplate>
                                <asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall"   HeaderStyle-CssClass="controlColumn" />
	                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
