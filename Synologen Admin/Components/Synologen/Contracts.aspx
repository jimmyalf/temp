<%@ Page Language="C#" MasterPageFile="~/components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="Contracts.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Contracts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" Runat="Server">
    <div id="dCompMain" class="Components-Synologen-ContractCustomer-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Avtalskunder</h1>
                
                <fieldset>
	                <legend>Filtrera och sök</legend>		
	                <div class="formItem ">
	                    <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" SkinId="Long">Textfilter</asp:Label>
	                    <asp:TextBox runat="server" ID="txtSearch"/>
	                </div>
	                <div class="formItem clearLeft">
						<br />
	                    <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="S&ouml;k"/>
	                </div>	                
                </fieldset>
    
                <div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
                
                <asp:GridView ID="gvContractCustomers" 
                                runat="server" 
                                DataKeyNames="cId" 
                                OnSorting="gvContractCustomers_Sorting" 
                                OnPageIndexChanging="gvContractCustomers_PageIndexChanging" 
                                SkinID="Striped" 
                                OnRowEditing="gvContractCustomers_Editing" 
                                OnRowDeleting="gvContractCustomers_Deleting" 
                                OnRowCommand="gvContractCustomers_RowCommand" 
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
                        <asp:BoundField headerText="Avtalsnummer" DataField="cId" SortExpression="cId" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField headerText="Namn" DataField="cName" SortExpression="cName"/>
                        <asp:BoundField headerText="Epost" DataField="cEmail" SortExpression="cEmail"/>
						<asp:TemplateField headertext="Aktiv" SortExpression="cActive">
							<ItemStyle CssClass="center" />
							<ItemTemplate>
								<asp:Image id="imgActive" runat="server" />
							</ItemTemplate>
						</asp:TemplateField>
                        <asp:ButtonField Text="Visa filer" HeaderText="Filer" CommandName="Files" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"/>
                        <asp:ButtonField Text="Lägg till" HeaderText="Filer" CommandName="AddFiles" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"/>
                        <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn" />
                        <asp:TemplateField headertext="Radera" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center">
	                        <ItemTemplate>
                                <asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" />
	                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
