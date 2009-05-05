<%@ Page Language="C#" MasterPageFile="~/components/Synologen/SynologenMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.AddFiles" Title="Untitled Page" Codebehind="AddFiles.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" Runat="Server">
   <div id="dCompMain" class="Components-Synologen-AddFiles-aspx">
    <div class="fullBox">
    <div class="wrap">
        <h1><%=Heading %></h1>
        
		<fieldset>
			<legend><%=Legend %></legend>
			
			<fieldset><legend>Fil 1</legend>
			<div class="formItem clearLeft">
                <asp:FileUpload ID="uplFile1" runat="server"/>
                <label class="labelLong">Beskrivning</label>
                <asp:TextBox ID="txtDesc1" runat="server"></asp:TextBox>                
            </div>
            <div class="formItem">                     
                <label class="labelLong">Kategori</label>
                <asp:DropDownList ID="drpCategory1" runat="server"></asp:DropDownList>
            </div>
			</fieldset>            
            <fieldset><legend>Fil 2</legend>
            <div class="formItem clearLeft">
                <asp:FileUpload ID="uplFile2" runat="server" />
                <label class="labelLong">Beskrivning</label>
                <asp:TextBox ID="txtDesc2" runat="server"></asp:TextBox>
            </div>
            <div class="formItem">                
                <label class="labelLong">Kategori</label>
                <asp:DropDownList ID="drpCategory2" runat="server"></asp:DropDownList>
            </div>
			</fieldset>            
            <fieldset><legend>Fil 3</legend>
            <div class="formItem clearLeft">
                <asp:FileUpload ID="uplFile3" runat="server" />
                <label class="labelLong">Beskrivning</label>
                <asp:TextBox ID="txtDesc3" runat="server"></asp:TextBox>
            </div>
            <div class="formItem">                     
                <label class="labelLong">Kategori</label>
                <asp:DropDownList ID="drpCategory3" runat="server"></asp:DropDownList>
            </div>
			</fieldset>            
            <fieldset><legend>Fil 4</legend>
            <div class="formItem clearLeft">
                <asp:FileUpload ID="uplFile4" runat="server" />
                <asp:TextBox ID="txtDesc4" runat="server"></asp:TextBox>
            </div>
            <div class="formItem">                     
                <label class="labelLong">Kategori</label>
                <asp:DropDownList ID="drpCategory4" runat="server"></asp:DropDownList>
            </div>
			</fieldset>            
            <fieldset><legend>Fil 5</legend>           
            <div class="formItem clearLeft">
                <asp:FileUpload ID="uplFile5" runat="server" />
                <asp:TextBox ID="txtDesc5" runat="server"></asp:TextBox>
            </div>
            <div class="formItem">                     
                <label class="labelLong">Kategori</label>
                <asp:DropDownList ID="drpCategory5" runat="server"></asp:DropDownList>
            </div>
            </fieldset>
            <div class="formCommands">	
				<input type="button" name="inputBack" class="btnBig" onclick="javascript:window.history.back();" value="Tillbaka" />            				    
			    <asp:button ID="btnAdd" runat="server" CommandName="Add" OnClick="btnSave_Click" Text="Lägg till" SkinId="Big"/>
			</div>
        </fieldset>
        
    </div>
    </div>
    </div>
</asp:Content>

