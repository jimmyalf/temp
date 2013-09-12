<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Control Language="C#" %>
<table cellSpacing="0" border="0">
  <tr>
    <td class="txt4"><Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb1" /></td>
  </tr>
</table>
<p>
<center>
<table Class="tableBorder" width="75%" CellPadding="3" Cellspacing="1">
  <tr>
    <td class="column" align="left" height="25">
      &nbsp; <% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadJoin_Title")%>
    </td>
  </tr>
  <tr>
    <td class="f">
      <table cellSpacing="1" cellPadding="3">
        <tr>
          <td vAlign="top" nowrap align="left"><span class="txt3"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadJoin_Description")%></span></td>
        </tr>
        <tr>
          <td align="left" colspan="2">
            <table>
              <tr> 
                <td align="left" colspan="2">
                  <span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadJoin_Select")%></span>
                </td>
              </tr>

              <tr> 
                <td align="right">
                  <span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadJoin_ParentID")%></span>
                </td>
                <td align="left">
                  <asp:TextBox columns="20" CssClass="txt3" runat="server" id="ParentThreadID" /> <asp:Button id="ValidateParentThread" runat="server" />
                </td>
              </tr>

              <tr> 
                <td align="left" colspan="2" class="txt3">
                  <asp:Checkbox runat="server" id="ParentThreadIsValid" />
                </td>
              </tr>

              <tr> 
                <td align="left" colspan="2">
                  &nbsp;
                </td>
              </tr>

              <tr>
                <td align="left" colspan="2">
                  <asp:HyperLink Target="_blank" CssClass="txt3Bold" runat="server" id="ChildThread" /> <span class="txt3"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("ThreadJoin_JoinRelationship")%></span> <asp:HyperLink Target="_blank" CssClass="txt3Bold" runat="server" id="ParentThread" />
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
