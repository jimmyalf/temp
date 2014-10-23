<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<table cellPadding="2" cellspacing="0" width="100%">
    <tr>
        <td align="left">
          <span class="forumName"><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Title")%></span>
          <br>
          <span class="forumThread"><asp:Literal ID="UserName" Runat="server" /></span>
        </td>
    </tr>

    <tr>
        <td>
        &nbsp;
        </td>
    </tr>
</table>
<table border="0" cellPadding="3" cellSpacing="1" class="tableBorder" width="100%">
    <tr>
        <td class="column" colspan="2">
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_About")%>
        </td>
    </tr>
    <tr>
        <td class="f" width="35%">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Joined")%></b>
            <br>
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_JoinedDescription")%>
        </td>
        <td class="fh" width="65%">
            <asp:Literal id="JoinedDate" runat="server" />
        </td>
    </tr>
    
    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_LastLogin")%></b>
            <br>
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_LastLoginDescription")%>
        </td>
        <td class="fh">
            <asp:Literal id="LastLoginDate" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_LastVisit")%></b>
            <br>
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_LastVisitDescription")%>
        </td>
        <td class="fh">
            <asp:Literal id="LastActivityDate" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Timezone")%></b>
            <br>
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_TimezoneDescription")%>
        </td>
        <td class="fh">
            <asp:Literal id="TimeZone" runat="server" /> GMT
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Location")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="Location" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Occupation")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="Occupation" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Interests")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="Interests" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_WebAddress")%></b>
        </td>
        <td class="fh">
            <asp:HyperLink id="WebURL" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_BlogAddress")%></b>
        </td>
        <td class="fh">
            <asp:HyperLink id="BlogURL" runat="server" />
        </td>
    </tr>
</table>

<P>

<table border="0" cellPadding="3" cellSpacing="1" class="tableBorder" width="100%">
    <tr>
        <td class="column" colspan="2">
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_AvatarPostSignature")%>
        </td>
    </tr>
    
    <tr>
        <td class="f" width="35%">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Avatar")%></b>
        </td>
        <td class="fh" width="65%">
            <forums:UserAvatar Visible="False" PadImage="False" Border="1" id="Avatar" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Theme")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="Skin" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Signature")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="Signature" runat="server" />
        </td>
    </tr>

</table>

<P>

<table border="0" cellPadding="3" cellSpacing="1" class="tableBorder" width="100%">
    <tr>
        <td class="column" colspan="2">
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Signature")%>
        </td>
    </tr>
    <tr>
        <td class="f" width="35%">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_Email")%></b>
        </td>
        <td class="fh" width="65%">
            <asp:HyperLink id="Email" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_MsnIm")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="MSNIM" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_AolIm")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="AOLIM" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_YahooIm")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="YahooIM" runat="server" />
        </td>
    </tr>

    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_IcqIm")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="ICQ" runat="server" />
        </td>
    </tr>
</table>

<P>

<table border="0" cellPadding="3" cellSpacing="1" class="tableBorder" width="100%">
    <tr>
        <td class="column" colspan="2">
            <%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_PostStats")%>
        </td>
    </tr>
    <tr>
        <td class="f" width="35%">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_TotalPosts")%></b>
        </td>
        <td class="fh" width="65%">
            <asp:Literal id="TotalPosts" runat="server" />
        </td>
    </tr>
    
    <tr>
        <td class="f">
            <b><%=Spinit.Wpc.Forum.Components.ResourceManager.GetString("ViewUserProfile_PostRank")%></b>
        </td>
        <td class="fh">
            <asp:Literal id="Rank" runat="server" />
        </td>
    </tr>
</table>
