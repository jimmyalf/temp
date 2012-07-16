<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpotImage.ascx.cs" Inherits="components_Campaign_SpotImage" %>
<div class="SpotImage-ascx">
    <div>
        <asp:Image ID="imgThumb" runat="server" />
        <asp:Button ID="btnClear" runat="server"  Visible="False" Text="Clear" CssClass="spotBtn" OnClick="btnClear_Click" />
	    <asp:Button ID="btnSelect" runat="server"  CausesValidation="false" Text="Select" CssClass="spotBtn" OnClick="btnSelect_Click" />
        </div>

    <div id="dSelectSpot" runat="server" visible="false" class="selectSpotContainer">
        <div class="selectSpotCaption"><h2>Select image</h2></div>
        <div class="selectSpotWrap" >
            
            <div class="selectSpotList">
                <asp:LinkButton ID="btnParent" runat="server" OnClick="btnParent_Click">Parent directory</asp:LinkButton>
                <asp:GridView ID="gvFiles" runat="server" SkinID="Striped" AutoGenerateColumns="false"  DataKeyNames="Id" OnSelectedIndexChanged="gvFiles_SelectedIndexChanged" Width="260">
                    <Columns>
                    <asp:CommandField ShowSelectButton="True" ButtonType="Image"  SelectImageUrl="../../common/icons/arrow_right_9x8.png"/>
                    <asp:TemplateField HeaderText="View" >
                    <ItemTemplate>
                        <img src='<%# DataBinder.Eval(Container.DataItem, "Pic") %>' border="0" height="30" width="30" />
                    </ItemTemplate>
                        <ItemStyle Width="30px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="name" HeaderText="Name" />
                    <asp:BoundField DataField="size" HeaderText="Size" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="selectSpotPreview">
                <asp:Image ID="imgPreview" runat="server" Visible="false" />
            </div>
            <div class="selectSpotButtons">
                <asp:Button ID="btnSet" runat="server"   CausesValidation="false" Text="Set" CssClass="spotBtn" OnClick="btnSet_Click"  />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel" CssClass="spotBtn" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
</div>