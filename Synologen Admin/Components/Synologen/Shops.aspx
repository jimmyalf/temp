<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="Shops.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Shops" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
    <div id="dCompMain" class="Components-Synologen-Shops-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Butiker</h1>
                
                <fieldset>
	                <legend>Filtrera och sök</legend>
	                <div class="formItem clearLeft">
	                    <label class="labelLong">Kategorier</label>
	                    <asp:DropDownList runat="server" ID="drpCategories" DataTextField="/>
	                </div>
	                <div class="formItem ">
	                    <label class="labelLong">Avtalskunder</label>
	                    <asp:DropDownList runat="server" ID="drpContractCustomers"/>
	                </div>	                	                
	                <div class="formItem clearLeft">
	                    <label class="labelLong">Textfilter</label>
	                    <asp:TextBox runat="server" ID="txtSearch"/>	                
	                </div>	 	                
	                <div class="formItem">
	                    <label class="labelLong">Utrustning</label>
	                    <asp:DropDownList runat="server" ID="drpEquipment"/>
	                </div>
	                <div class="formItem clearLeft">
						<br />
	                    <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="S&ouml;k"/>
	                </div>	                
                </fieldset>
    
                <div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
                
                <asp:GridView ID="gvShops" 
                                runat="server" 
                                DataKeyNames="cId" 
                                OnSorting="gvShops_Sorting" 
                                OnPageIndexChanging="gvShops_PageIndexChanging" 
                                SkinID="Striped" 
                                OnRowEditing="gvShops_Editing" 
                                OnRowDeleting="gvShops_Deleting" 
                                AllowSorting="true">
                    <Columns>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectHeader" runat="server" OnCheckedChanged="chkSelectHeader_CheckedChanged" AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField headerText="Id" DataField="cId" SortExpression="cId" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField headerText="Butik" DataField="cShopName" SortExpression="cShopName"/>
                        <asp:BoundField headerText="Butiknummer" DataField="cShopNumber" SortExpression="cShopNumber"/>
                        <asp:BoundField headerText="Stad" DataField="cCity" SortExpression="cCity"/>
                        <asp:BoundField headerText="Kategori" DataField="cCategoryName" SortExpression="cCategoryName"/>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn" HeaderText="Personal" >
							    <ItemTemplate>
                                    <a href="Index.aspx?shopId=<%# DataBinder.Eval(Container.DataItem, "cId")%>">Personal</a>
                                </ItemTemplate>
                        </asp:TemplateField>
						<asp:TemplateField headertext="Aktiv" SortExpression="cActive" HeaderStyle-CssClass="controlColumn">
							<ItemStyle CssClass="center" />
							<ItemTemplate>
								<asp:Image id="imgActive" runat="server" />
							</ItemTemplate>
						</asp:TemplateField>
                        <asp:ButtonField  HeaderText="Redigera" Text="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn" />
                        <asp:TemplateField headertext="Radera" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn">
	                        <ItemTemplate>
                                <asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" Enabled="false" />
	                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
