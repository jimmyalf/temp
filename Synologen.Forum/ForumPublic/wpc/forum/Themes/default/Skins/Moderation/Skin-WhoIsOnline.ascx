<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Control Language="C#" %>

<table border="0" cellPadding="0" cellSpacing="0">
    <tr>
        <td><img src="<%= Globals.GetSkinPath() %>/images/forumHeader-Curve-Left.gif"></td>
        <td width="100%" class="h1">Moderators Online</td>
        <td><img src="<%= Globals.GetSkinPath() %>/images/forumHeader-Curve-Right.gif"></td>
    </tr>
</table>
<table class="tableBorder" cellSpacing="1" cellPadding="3" width="100%">
    <tr>
        <td class="f" valign="top">
            <P>
                <span class="normalTextSmaller">
      There are currently:
      <br>
      <b>
                        <asp:Label ID="Moderators" Runat="server" /></b> moderators online.
      <br>
      <br>
      Moderators online<asp:label id="ModeratorList" runat="server"></asp:label>
      </span>
        </td>
    </tr>
</table>
