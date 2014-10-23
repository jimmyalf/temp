<%@ Page Language="C#" %>
<html>
    <head>
        <title><%=Spinit.Wpc.Forum.Components.Globals.GetSiteSettings().SiteName%> Administration</title>
    </head>
    <frameset cols="*,*" border="0" framespacing="0" frameborder="no">
        <frame src="ModerateForum.aspx?ForumID=<%=Request.QueryString["ForumID"] %>" name="ModerateForum" marginwidth="0" marginheight="0" scrolling="auto">
        <frame src="" name="_Preview" marginwidth="0" marginheight="0" scrolling="auto">
    </frameset>
</html>