<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Page Language="C#" MasterPageFile="~/commonresources/templates/Master/Forum Template.master" Title="Forum" Inherits="Spinit.Wpc.Content.Presentation.Site.Code.PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
      <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr valign="bottom">
          <td>
            <table width="100%" height="100%" cellspacing="0" cellpadding="0" border="0">
              <tr valign="top">
                <!-- left column -->
                <td>&nbsp; &nbsp; &nbsp;</td>
                <!-- center column -->
                <td id="CenterColumn" Width="95%" runat="server" class="CenterColumn">
                  <br>
<%--                   <Forums:NavigationMenu id="NavigationMenu2" runat="server" /> --%>
                  <Forums:ThreadSplit runat="server" />
                </td>
                <td class="CenterColumn">&nbsp;&nbsp;&nbsp;</td>
                <!-- right margin -->
                <td class="RightColumn">&nbsp;&nbsp;&nbsp;</td>
                <td id="RightColumn" Visible="false" nowrap Width="230" runat="server" class="RightColumn">
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
<asp:Literal ID="ltPageId" Text="283" Visible="false" runat="server"/>
</asp:Content>
