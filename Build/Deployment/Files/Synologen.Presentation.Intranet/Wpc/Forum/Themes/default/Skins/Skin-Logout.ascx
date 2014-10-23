<%@ Control Language="C#" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<!-- ********* Skin-Logout.ascx:Start ************* //-->	
<center>
  <table class="tableBorder" cellspacing="1" cellpadding="3" width="300">
    <tr>
      <td class="column" align="left" colspan="2">
        <% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Logout_Title") %>
      </td>
    </tr>
    <tr>
      <td class="f" align="center" valign="top" colspan="2">
        &nbsp;
        <br />
        <% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Logout_Status") %>
        <p />
        &nbsp;
        <a href="<%=Globals.ApplicationPath%>"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("Logout_ReturnHome") %></a>
      </td>
    </tr>
  </table>
</center>
<!-- ********* Skin-Logout.ascx:End ************* //-->	

