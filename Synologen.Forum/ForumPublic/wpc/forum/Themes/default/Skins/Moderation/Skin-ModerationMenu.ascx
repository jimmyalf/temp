<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Control Language="C#" %>
<P>
<table Class="moderationTable" width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td class="txt4Bold" align="left">
            <asp:Button id="Approve" runat="server" />
            <asp:Button id="ApproveReply" runat="server" />
            <asp:Button id="ApproveEdit" runat="server" />
        </td>
        <td class="txt4Bold" align="right">
            <asp:HyperLink id="Move" runat="server"></asp:HyperLink>
            <asp:HyperLink id="DeletePost" runat="server"></asp:HyperLink>
            <asp:HyperLink id="MergeSplit" runat="server"></asp:HyperLink>
        </td>
    </tr>
</table>

