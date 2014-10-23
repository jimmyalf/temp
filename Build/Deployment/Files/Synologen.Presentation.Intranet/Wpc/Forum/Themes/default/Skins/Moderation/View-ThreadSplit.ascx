<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Control Language="C#" %>
<table cellSpacing="0" border="0">
  <tr>
    <td class="txt4"><forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb1" /></td>
  </tr>
</table>
<p>
<center>
<table Class="tableBorder" width="75%" CellPadding="3" Cellspacing="1">
  <tr>
    <td class="column" align="left" height="25">
      &nbsp; <% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadSplit_Title")%>
    </td>
  </tr>
  <tr>
    <td class="f">
      <table cellSpacing="1" cellPadding="3">
        <tr>
          <td vAlign="top" nowrap align="left"><span class="txt3"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadSplit_Description")%></span></td>
        </tr>
        <tr>
          <td align="left" colspan="2">
            <table>
              <tr> 
                <td align="right">
                  <span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("MovePost_Subject")%></span>
                </td>
                <td align="left">
                  <asp:TextBox columns="83" CssClass="txt3" runat="server" id="Subject" />
                </td>
              </tr>
              <tr> 
                <td valign="top" align="right">
                  <span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("MovePost_HasReplies")%></span>
                </td>
                <td align="left">
                  <asp:Label CssClass="txt3" runat="server" id="HasReplies" />
                </td>
              </tr>
              <tr> 
                <td valign="top" align="right">
                  <span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("MovePost_PostedBy")%></span>
                </td>
                <td align="left">
                  <asp:Label CssClass="txt3" runat="server" id="PostedBy" />
                </td>
              </tr>
              <tr> 
                <td valign="top" align="right">
                  <span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("MovePost_Body")%></span>
                </td>
                <td align="left">
                  <asp:Label CssClass="txt3" runat="server" id="Body" />
                </td>
              </tr>
              <tr> 
                <td valign="top" align="right">
                  <span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("MovePost_MoveTo")%></span>
                </td>
                <td align="left">
                  <forums:ForumListBox CssClass="txt4" id="MoveTo" Height="200" runat="server" />
                </td>
              </tr>
            </table>
          </td>
        </tr>
        <tr> 
          <td colspan="2" valign="top" align="left">
            <span class="txt3"><asp:CheckBox Checked="true" id="SendUserEmail" runat="server" /></span>
          </td>
        </tr>
        <tr>
          <td vAlign="top" colspan="2" nowrap align="right"><asp:HyperLink CssClass="linkSmallBold" id="CancelMove" runat="server" /> &nbsp; <asp:LinkButton CssClass="linkSmallBold" id="MovePost" runat="server" /></td>
        </tr>
      </table>
    </td>
  </tr>
</table>
</center>
