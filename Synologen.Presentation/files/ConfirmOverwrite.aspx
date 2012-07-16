<%@ Page Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.ConfirmOverwrite" Codebehind="ConfirmOverwrite.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="/common/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server">
      <strong>Confirm save: </strong>
      <table width="100%" border="0" cellpadding="1" cellspacing="0" bgcolor="#e3e3e3">
  <tr>
    <td><table width="100%" border="0" cellpadding="5" cellspacing="0" bgcolor="#FFFFFF">
      <tr>
        <td valign="top"><b>Overwrite file?:</b></td>
      </tr>
      <tr>
        <td><table width="100%">
            <tr>
              <td width="100%" class="tableheader">Name</td>
            </tr>
            <asp:Repeater ID="rptConnections" runat="server" DataMember="tblConnections">
              <ItemTemplate>
                <tr>
                  <td width="100%" bgcolor="#F4F4F4"><%# DataBinder.Eval(Container.DataItem, "cName") %></td>
                </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                <tr>
                  <td width="100%" bgcolor="#FFFFFF"><%# DataBinder.Eval(Container.DataItem, "cName") %></td>
                </tr>
              </AlternatingItemTemplate>
            </asp:Repeater>
        </table></td>
      </tr>
      <tr>
        <td align="center"><asp:Button ID="btnDelete" runat="server" Text="Overwrite" OnClick="btnDelete_Click" />    
          &nbsp;
          <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></td>
      </tr>
    </table></td>
  </tr>
</table>
    </form>
</body>
</html>
