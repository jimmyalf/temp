<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Orders" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
    <div id="dCompMain" class="Components-Synologen-Orders-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Fakturor</h1>
                <fieldset>
	                <legend>Filtrera och sök</legend>
					<asp:PlaceHolder id="plDisplayingFilteredListing" runat="server" Visible="false">
	                <div class="formItem clearLeft">
	                    <label class="labelLong">Listar fakturor i utbetalning nummer <%=SettlementId%></label>
	                </div> 	                
	                </asp:PlaceHolder>
	                <asp:PlaceHolder id="plNormalListing" runat="server" Visible="true">
	                <div class="formItem clearLeft">
	                    <label class="labelLong">Avtal</label>
	                    <asp:DropDownList runat="server" ID="drpContracts" DataTextField="cName"/>
	                </div>          
	                <div class="formItem">
	                    <label class="labelLong">Datumintervall Från</label>
	                    <dtc:DateTimeCalendar id="dtcStartInterval" runat="server" />

	                </div>
	                <div class="formItem clearLeft">
	                    <label class="labelLong">Fakturastatus</label>
	                    <asp:DropDownList runat="server" ID="drpStatuses" DataTextField="cName"/>
	                </div> 	                
	                <div class="formItem">
	                    <label class="labelLong">Datumintervall Till</label>
	                    <dtc:DateTimeCalendar id="dtcEndInterval" runat="server" />
	                </div> 	      	                	                  	                 	                
	                <div class="formItem clearLeft">
	                    <label class="labelLong">Textfilter</label>
	                    <asp:TextBox runat="server" ID="txtSearch"/>
	                </div> 	                          
	                <div class="formItem clearLeft">
	                    <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="S&ouml;k"  SkinId="Big"/>
	                </div>	
	                </asp:PlaceHolder>                
                </fieldset>
    
                <div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
                <asp:GridView ID="gvOrders" 
                                runat="server" 
                                DataKeyNames="cId" 
                                OnSorting="gvOrders_Sorting" 
                                OnPageIndexChanging="gvOrders_PageIndexChanging" 
                                OnRowEditing="gvOrders_Editing" 
                                OnRowDeleting="gvOrders_Deleting" 
                                AllowSorting="true"
                                SkinID="Striped" >
                    <Columns>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectHeader" runat="server" OnCheckedChanged="chkSelectHeader_CheckedChanged" AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField headerText="Id" DataField="cId" SortExpression="tblSynologenOrder.cId" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField headerText="Faktura" DataField="cInvoiceNumber" SortExpression="tblSynologenOrder.cInvoiceNumber" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField headerText="Avtal" DataField="cContractName" SortExpression="cContractName" />
                        <%--<asp:BoundField headerText="Företag" DataField="cCompanyName" SortExpression="cCompanyName" />--%>
                        <asp:BoundField headerText="Kund" DataField="cCustomerName" SortExpression="cCustomerName" />
                        
                        
                        <%--<asp:BoundField headerText="RST" DataField="cRstName" SortExpression="cRstName" ItemStyle-HorizontalAlign="Center" />--%>

                        <asp:BoundField headerText="Status" DataField="cStatusName" SortExpression="cStatusName" />
                        <asp:TemplateField HeaderText="Säljare">
	                        <ItemTemplate>
                                <span><%# DataBinder.Eval(Container.DataItem, "cSalesPersonShopName")%> (<%# DataBinder.Eval(Container.DataItem, "cSalesPersonName")%>)</span>
	                        </ItemTemplate>
                        </asp:TemplateField>                        
						<asp:BoundField headerText="Registrerad" DataField="cCreatedDate" SortExpression="cCreatedDate" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn" />
                        <asp:TemplateField HeaderText="Hantera" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center"  >
	                        <ItemTemplate>                        
								<asp:HyperLink runat="server" NavigateUrl='<%#GetManageOrdersUrl(Eval("cId"))%>' Text="Hantera" />
	                        </ItemTemplate>
                        </asp:TemplateField> 
                        <%--
                        <asp:TemplateField HeaderText="Radera" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center"  >
	                        <ItemTemplate>
                                <asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" Enabled="false"  />
	                        </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Avbryt" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center"  >
	                        <ItemTemplate>                        
								<asp:Button id="btnAbort" runat="server" OnDataBinding="AddConfirmAbort" commandname="Abort" text="Abryt Order" CssClass="btnSmall" Enabled="false" CommandArgument='<%# Eval("cid") %>'  />                       
	                        </ItemTemplate>
                        </asp:TemplateField> 
                        --%>                                               
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
