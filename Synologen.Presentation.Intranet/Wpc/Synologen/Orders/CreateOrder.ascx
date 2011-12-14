<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.CreateOrder" %>

<div id="page" class="step3">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> Emelie Richter</span>
	</header>

    <nav id="tab-navigation">
    	<ul>
    		<li class="completed"><a href="index.html"><span>1</span> Välj Kund</a></li>
    		<li class="selected"><span>2</span> Skapa Beställning</li>
    		<li><span>3</span> Betalningssätt</li>
    		<li><span>4</span> Autogiro Information</li>
    		<li><span>5</span> Bekräfta</li>
    	</ul>
    </nav>
   <div id="tab-container">

   	<fieldset>
   	    <div class="progress">
   			<label>Steg 3 av 6</label>
	 		<div id="progressbar"></div>
   		</div>
    	<p><label>Välj Kategori</label>
            <asp:DropDownList id="ddlPickCategory" runat="server" AutoPostBack="true">
                <asp:ListItem value="endagslinser">
                  Endagslinser
                </asp:ListItem>
                <asp:ListItem value="veckolinser">
                  Veckolinser
                </asp:ListItem>
                <asp:ListItem value="månadslinser">
                  Månadslinser
                </asp:ListItem>
            </asp:DropDownList>
		</p>
    	<p><label>Välj Leverantör</label>
            <asp:DropDownList id="ddlPickSupplier" runat="server">
                <asp:ListItem value="1">
                  Johnsson & Johnsson
                </asp:ListItem>
                <asp:ListItem value="2">
                  En annan leverantör
                </asp:ListItem>
                <asp:ListItem value="3">
                  En annan leverantör
                </asp:ListItem>
            </asp:DropDownList>
    		
		</p>
    	<p><label>Välj typ</label>
            <asp:DropDownList id="ddlPickKind" runat="server">
                <asp:ListItem value="1">
                  Endagslinser F443A
                </asp:ListItem>
                <asp:ListItem value="2">
                  Endagslinser F443B
                </asp:ListItem>
                <asp:ListItem value="3">
                  Endagslinser F443C
                </asp:ListItem>
            </asp:DropDownList>
    	</p>
    	<p><label>Välj Artikel</label>
            <asp:DropDownList id="ddlPickArticle" runat="server">
                <asp:ListItem value="1">
                  Linser 4431
                </asp:ListItem>
                <asp:ListItem value="2">
                  Linser 4432
                </asp:ListItem>
                <asp:ListItem value="3">
                  Linser 4433
                </asp:ListItem>
            </asp:DropDownList>
    	</p>
      </fieldset>
      <fieldset class="left-eye">
      <legend>V</legend>
          	<p><label>Styrka</label>

            <asp:DropDownList id="ddlLeftStrength" runat="server">
                <asp:ListItem value="1">
                  -7,50
                </asp:ListItem>
                <asp:ListItem value="2">
                  8.5
                </asp:ListItem>
                <asp:ListItem value="3">
                  1.0
                </asp:ListItem>
            </asp:DropDownList>
		</p>
    	<p><label>Baskurva</label>

            <asp:DropDownList id="ddlLeftBaskurva" runat="server">
                <asp:ListItem value="1">
                  8,5
                </asp:ListItem>
                <asp:ListItem value="2">
                  9,0
                </asp:ListItem>
                <asp:ListItem value="3">
                  9,5
                </asp:ListItem>
            </asp:DropDownList>

		</p>
    	<p><label>Diameter</label>
            
            <asp:DropDownList id="ddlLeftDiameter" runat="server">
                <asp:ListItem value="1">
                  -14
                </asp:ListItem>
                <asp:ListItem value="2">
                  -15
                </asp:ListItem>
                <asp:ListItem value="3">
                  -16
                </asp:ListItem>
            </asp:DropDownList>
    	</p>
      </fieldset>
      
      <fieldset class="right-eye">
      <legend>H</legend>
          	<p><label>Styrka</label>

            <asp:DropDownList id="ddlRightStrength" runat="server">
                <asp:ListItem value="1">
                  -7,50
                </asp:ListItem>
                <asp:ListItem value="2">
                  8.5
                </asp:ListItem>
                <asp:ListItem value="3">
                  1.0
                </asp:ListItem>
            </asp:DropDownList>
		</p>
    	<p><label>Baskurva</label>

            <asp:DropDownList id="ddlRightBaskurva" runat="server">
                <asp:ListItem value="1">
                  8,5
                </asp:ListItem>
                <asp:ListItem value="2">
                  9,0
                </asp:ListItem>
                <asp:ListItem value="3">
                  9,5
                </asp:ListItem>
            </asp:DropDownList>

		</p>
    	<p><label>Diameter</label>
            
            <asp:DropDownList id="ddlRightDiameter" runat="server">
                <asp:ListItem value="1">
                  -14
                </asp:ListItem>
                <asp:ListItem value="2">
                  -15
                </asp:ListItem>
                <asp:ListItem value="3">
                  -16
                </asp:ListItem>
            </asp:DropDownList>
    	</p>
      </fieldset>
      
      <fieldset>
      <legend>Leverantörsalternativ</legend>
      <asp:RadioButtonList runat="server" ID="SupplierOption" RepeatDirection="Horizontal">
      
           <asp:ListItem 
               Text="Till butik" />
        
           <asp:ListItem
               Text="Till kund" />
               
           <asp:ListItem
               Text="Ingen leverans" />
      </asp:RadioButtonList>
      </fieldset>
      
   <fieldset>
        <div class="next-step">
            <div class="control-actions">
				
                <asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CssClass="cancel-button" />
                <asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" />
		        <asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" />
	        </div>
        </div>
   </fieldset>

  </div>
  </div>