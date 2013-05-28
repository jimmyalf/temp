<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateDeviation.ascx.cs"
    Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations.CreateDeviation" %>
<div class="synologen-control">
    <%if (!Model.Success)
      { %>
    <asp:Panel runat="server" ID="pnlCreateDeviationForm">
        <fieldset class="synologen-form">
                <p>
                    <label for="<%=drpTypes.ClientID%>">Typ av avvikelse</label>
                    <asp:DropDownList
                        ID="drpTypes"
                        runat="server"
                        SelectedValue='<%#Model.SelectedType%>'
                        DataSource='<%#Model.Types%>'
                        DataValueField="Id"
                        DataTextField="Name" />
                </p>

                <p>
                    <label for="<%=drpCategories.ClientID%>">Kategori</label>
                    <asp:DropDownList
                        ID="drpCategories"
                        runat="server"
                        AutoPostBack="true"
                        SelectedValue='<%#Model.SelectedCategoryId%>'
                        DataSource='<%#Model.Categories%>'
                        DataValueField="Id"
                        DataTextField="Name" />
                </p>

                <%if (Model.DisplayInternalDeviation)
                  { %>
                <p>
                    <label for="<%=txtInternalDefectDescription.ClientID%>">Beskrivning</label>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtInternalDefectDescription"></asp:TextBox>
                </p>
                <p>
                    <asp:Button runat="server" ID="btnConfirmInternalDeviation" Text="Förhandsgranska" />
                </p>
                <%} %>
                <%if (Model.DisplayExternalDeviation)
                  { %>
                <p>
                    <label for="<%=cblDefects.ClientID%>">Kryssa i fel</label>
                    <asp:CheckBoxList ID="cblDefects"
                        runat="server"
                        DataSource='<%#Model.Defects%>'
                        DataValueField="Id"
                        DataTextField="Name" />
                </p>
                <div id="defect-description">
                    <p>
                        <label for="<%=txtExternalDefectDescription.ClientID%>">Beskrivning</label>
                        <asp:TextBox ID="txtExternalDefectDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </p>
                </div>
                <p>
                    <label for="<%=drpSuppliers.ClientID%>">Leverantör</label>
                    <asp:DropDownList
                        ID="drpSuppliers"
                        runat="server"
                        DataSource='<%#Model.Suppliers%>'
                        DataValueField="Id"
                        DataTextField="Name" />
                </p>
                <p>
                    <asp:Button runat="server" ID="btnConfirmExternalDeviation" Text="Förhandsgranska" />
                </p>
                <%} %>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnlInternalDeviationConfirmation" Visible="false" runat="server">
        <fieldset>
            <legend>Avvikelserapport Intern</legend>
            <p>
                <label for="<%=lblInternalDeviationCategoryName.ClientID%>">Kategori</label>
                <asp:Label ID="lblInternalDeviationCategoryName" runat="server"></asp:Label>
            </p>
            <p>
                <label for="<%=lblInternalDefectDescription.ClientID%>">Beskrivning</label>
                <asp:Label ID="lblInternalDefectDescription" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Button runat="server" ID="btnSubmitInternal" Text="Skicka" />
                <asp:Button runat="server" ID="btnChangeInternal" Text="Ändra" OnClientClick="window.history.back(1); return false;" />

            </p>
        </fieldset>
    </asp:Panel>

    <asp:Panel ID="pnlExternalDeviationConfirmation" Visible="false" runat="server">
        <fieldset>
            <legend>Avvikelserapport Extern</legend>
            <p>
                <label for="<%=lblExternalDeviationCategoryName.ClientID%>">Kategori</label>
                <asp:Label ID="lblExternalDeviationCategoryName" runat="server"></asp:Label>
            </p>
            <p>
                <asp:DataGrid ID="dgExternalDeviationCategoryDefects" runat="server" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:BoundColumn DataField="Name" HeaderText="Fel" />
                    </Columns>
                </asp:DataGrid>
            </p>
            <p>
                <asp:Label ID="lblExternalDefectDescription" runat="server"></asp:Label>
            </p>
            <p>
                <label for="<%=lblSupplier.ClientID%>">Leverantör</label>
                <asp:Label ID="lblSupplier" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Button runat="server" ID="btnSubmitExternal" Text="Skicka" />
                <asp:Button runat="server" ID="btnChangeExternal" Text="Ändra" OnClientClick="window.history.back(1); return false;" />
            </p>
        </fieldset>
    </asp:Panel>

    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%=btnConfirmExternalDeviation.ClientID %>').click(function () {
                if ($('#<%=drpSuppliers.ClientID%> option:selected').val() == 0) {
                    alert("Ange leverantör.");
                    return false;
                }
                return true;
            });

        });
    </script>

    <%
  }
      else
      {
    %>
    <h3>Tack för din avvikelserapport!</h3>
    <%} %>
</div>
