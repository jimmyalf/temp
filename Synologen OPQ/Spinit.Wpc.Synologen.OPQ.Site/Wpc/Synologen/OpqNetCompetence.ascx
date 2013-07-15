<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpqNetCompetence.ascx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Site.Wpc.Synologen.OpqNetCompetence" %>
<opq:UserMessageManager ID="userMessageManager" ControlId="Opq-UserMessage-SubPage" runat="server" Visible="false" />

<asp:DropDownList ID="drpUsers" runat="server"/>
<asp:Button ID="btnNavigateToNetCompetence" OnClick="BtnNavigateToNetCompetenceClick" runat="server"/>