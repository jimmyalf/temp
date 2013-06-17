<%@ Control %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<!-- ********* Skin-Message.ascx:Start ************* //-->
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<table width="100%">
  <tr>
    <td align="center">
      <table cellspacing="1" cellpadding="3" width="50%" class="tableBorder">
        <tr>
          <td class="column" align="left">
            <asp:label id="MessageTitle" runat="server"/>
          </td>
        </tr>
        <tr>
          <td class="f">
            <table cellpadding="3" cellspacing="0">
              <tr>
                <td>
                  &nbsp;
                </td>
                <td> 
                 <asp:label Cssclass="txt3" id="MessageBody" runat="server"/>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td align="center">
      <br />
      <Forums:JumpDropDownList CssClass="txt4" runat="server"/>
    </td>
  </tr>
</table>
<br />
<br />
<br />
<br />
<!-- ********* Skin-Message.ascx:End ************* //-->