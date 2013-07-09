<%@ Control Language="C#" CodeBehind="ViewDeviation.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.ViewDeviation" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Core.Domain.Model.Deviations" %>
<div class="synologen-control">
	<div class="toolbar">
	    <input type="button" onclick="window.print()" value="Skriv ut" />
    </div>
    <h1>Visa Avvikelse</h1>
    
    
    <fieldset class="synologen-form">
        <legend>Avvikelse <%= Model.Id %></legend>
        <dl>
	        <dt>Typ:</dt><dd> <%= Model.Type %></dd>
	        <%
	            if (Model.Type == DeviationType.Internal)
	            {
	        %>
	        <dt>Rubrik:</dt><dd> <%= Model.Title %></dd>
	        <%
	            }
	        %>
	        <% if (!string.IsNullOrEmpty(Model.DefectDescription))
	           {
	        %>
	        <dt>Övrigt:</dt><dd> <%= Model.DefectDescription %></dd>
	        <% }
	
	        %>
	        <%
	            if (Model.Type == DeviationType.External)
	            {
	        %>
	        <dt>Leverantör:</dt><dd> <%= Model.Supplier.Name %></dd>
	        <dt>Kategori:</dt><dd> <%= Model.Category.Name %></dd>
			
			<%
            }
			%>
        </dl>
			<%
	            if (Model.Type == DeviationType.External)
	            {
	        %>
        <p>
            <strong>Fel:</strong>
        </p>
        <div class="synologen-deviation-list">
            <asp:Repeater ID="rptDefects" runat="server" DataSource='<%#Model.Defects%>'>
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <th width="10%">ID
                            </th>
                            <th width="90%">Namn
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="center-cell">
                            <%# Eval("Id")%>
                        </td>
                        <td>
                            <%# Eval("Name")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="synologen-table-alternative-row">
                        <td class="center-cell">
                            <%# Eval("Id")%>
                        </td>
                        <td>
                            <%# Eval("Name")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    <%
            }
    %>
    <br />
    <fieldset>
        <legend>Ändra status</legend>
        <asp:DropDownList
            ID="drpStatus"
            runat="server"
            SelectedValue='<%#Model.SelectedStatus%>'
            DataSource='<%#Model.Statuses%>'
            DataValueField="Id"
            DataTextField="Name" />
        <p>
            <asp:Button ID="btnSaveStatus" runat="server" Text="Spara" />
        </p>
    </fieldset>
    <br />
    <fieldset>
        <legend>Skriv kommentar</legend>
        <asp:TextBox runat="server" ID="txtComment" TextMode="MultiLine" Columns="20" Rows="2"></asp:TextBox>
        <p>
            <asp:Button ID="btnSaveComment" runat="server" Text="Spara" />
        </p>
        <p>
            <asp:Repeater ID="rptComments" runat="server" DataSource='<%#Model.Comments%>'>
                <HeaderTemplate>
                    <table width="100%">
                        <tr>
                            <th width="75%">Kommentar
                            </th>
                            <th width="25%">Datum
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# ((string)Eval("Description")).Replace("\n", "<br/>") %>
                        </td>
                        <td>
                            <%# Eval("CreatedDate") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="synologen-table-alternative-row">
                        <td>
                            <%# ((string)Eval("Description")).Replace("\n", "<br/>") %>
                        </td>
                        <td>
                            <%# Eval("CreatedDate") %>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </p>
    </fieldset>
    <a href="javascript:history.back(-1);">Tillbaka</a>
</div>
<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script>
    $(document).ready(function () {
        $('#<%=btnSaveStatus.ClientID %>').click(function () {
            if ($('#<%=drpStatus.ClientID%> option:selected').val() == 0) {
                alert("Ange status.");
                return false;
            }
            return true;
        });

    });
</script>
