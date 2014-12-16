<%@ Control Language="C#" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Controls" %>
<a name="Top" />
<!-- ********* View-UserProfile.ascx:Start ************* //-->	
<Forums:NavigationMenu DisplayTitle="false" id="Navigationmenu" runat="server" />
<table align="center" width="95%" cellspacing="12" cellpadding="0" border="0">
<!-- View-UserProfile.Header.Start -->
	<tr>
		<td>
			<table width="100%" cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td valign="top" width="*" style="padding-right: 12px;">
						<table width="100%" cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<table class="tableBorder" align="center" width="85%" cellspacing="1" cellpadding="3">
										<tr>
											<td width="100%" class="column"><%= ResourceManager.GetString("Faq_Title")%></td>
										</tr>										
										<tr>
											<td class="fh">
												<table width="100%" cellspacing="0" border="0" cellpadding="0">
													<tr>    
														<td align="left" valign="middle">
															<table width="100%" cellpadding="4" cellspacing="0">
																<tr>
																	<td align="center">
																		<span class="forumName"><%= ResourceManager.GetString("Faq_Description")%></span>
																	</td>
																</tr>
															</table>
														</td>
														<td width="1"><img height="85" width="1" src="<%# Globals.GetSkinPath() + "/images/spacer.gif"%>"></td>
													</tr>
												</table>    
											</td>    
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>  					
				</tr>
			</table>	
		</td>
	</tr>
<!-- View-UserProfile.Header.End -->	
<!-- View-UserProfile.Body.Start -->	
	<tr>
    <tr>
	<tr>
		<td>
			<table cellpadding="0" cellspacing="0" width="100%" border="0">
				<tr>
					<td align="left" valign="bottom" colspan="2">
						<div style="padding-bottom: 6px;">
						<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td class="txt4Bold">
									&nbsp;<Forums:BreadCrumb ShowHome="true" runat="server" />
								</td>
								<td align="right" valign="top" class="txt4" nowrap>
									<Forums:SearchRedirect SkinFileName="Skin-SearchForum.ascx"  />
								</td>
							</tr>
						</table>		
						</div>				
					</td>
				</tr>
			</table>
		</td>
	</tr>
  
  <tr>
    <td colspan="2">

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Frågor och svar
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <b>Registrering & inloggning</b>
      <br>
      <a href="#1">Varför måste jag registrera mig?</a>
      <br>
      <a href="#2">Hur registrerar jag mig?</a>
      <br>
      <a href="#3">Jag har ett användarnamn och lösenord, hur loggar jag in?</a>
      <br>
      <a href="#4">Jag är redan inloggad, varför blir jag automatiskt utloggad?</a>
      <br>
      <a href="#5">Jag har glömt mitt användarnamn och/eller lösenord.</a>
      <br>
      <a href="#6"> Vad gör jag om jag registrerat mig, men ändå inte kan logga in?</a>
      <br> 
      <a href="#7">Jag har loggat in förut, men nu kan jag inte logga in längre.</a>

      <P>

      <b>Användarprofil & inställningar </b>
      <br>
      <a href="#8">Vad är en användarprofil?</a>
      <br> 
      <a href="#9">Varför ska jag ställa in min tidszon?</a> 
      <br> 
      <a href="#10">Hur kan jag lägga till en automatisk signatur till mina meddelanden? 
      <br> 
      <a href="#11">Vad är en Avatar?</a> 
      <br> 
      <a href="#12">Hur ställer jag in min Avatar? 
      <br> 
      <a href="#13">Hur kan jag ändra vilket språk forumen visas i?</a> 
      <br> 
      <a href="#14">Hur ställer jag in datumformatet?</a>  
      <br> 
      <a href="#15">Hur kan jag slå av e-postnotifieringar?</a> 
      <br> 
      <a href="#16">Vad betyder de andra ikonerna/avatarerna som visas bredvid varje användare?</a> 
      <br> 
      <a href="#17">Varför måste jag logga in för att skriva meddelanden, se medlemslista, eller skicka e-post till andra medlemmar?</a> 

      <P>

      <b>Integritet & säkerhet</b>
      <br>
      <a href="#18">Hur ändrar jag mitt lösenord?</a> 
      <br>
      <a href="#19">Hur ändrar jag mitt användarnamn?</a> 
      <br>
      <a href="#20">Hur ändrar jag min e-postadress?</a>  
      <br>
      <a href="#21">Vilka profil-inställningar är obligatoriska?</a>  
      <br>
      <a href="#22">Vad gör jag om jag inte vill att mitt namn ska visas i medlemslistan?</a>  

      <P>
      <b>Navigering</b>
      <br>
      <a href="#23">Vad är en forumgrupp?</a> 
      <br>
      <a href="#24">Vad är ett forum?</a> 
      <br>
      <a href="#25">Vad är en konversation?</a> 
      <br>
      <a href="#58">Vad betyder ikonerna bredvid varje konversation?</a> 
      <br>
      <a href="#26">När jag tittar i ett forum ser jag inga konversationer/meddelanden.</a> 
      <br>
      <a href="#27">Jag postade just ett meddelande, varför kan jag inte se det?</a> 
      <br>
      <a href="#28">Vad betyder de olika ikonerna bredvid varje konversation?</a> 
      <br>
      <a href="#29">Vad är ett tillkännagivande?</a> 
      <br>
      <a href="#30">Vad är en klistrad konversation?</a> 
      <br>
      <a href="#31">Vad är en låst konversation?</a> 
      <br>
      <a href="#32">Kan jag sortera konversationer när jag tittar i ett forum?</a> 
      <br>
      <a href="#33">Vad betyder ‘XML’-ikonen längst ned i varje forum?</a> 
      <br>
      <a href="#34">Vad betyder den röda/gröna ikonen som visas bredvid användarnamnet i ett meddelande?</a> 
      <br>
      <a href="#35">Jag kommer inte åt ett forum som jag vet existerar.</a> 

      <P>
      <b>Posta meddelanden</b>
      <br>
      <a href="#36">Kan jag använda HTML-kod i meddelanden?</a> 
      <br>
      <a href="#37">Vad betyder BBCode?</a> 
      <br>
      <a href="#38">Kan jag bifoga filer till mina meddelanden?</a> 
      <br>
      <a href="#39">Vad är uttryckssymboler?</a> 
      <br>
      <a href="#40">Hur postar jag ett nytt meddelande till ett forum?</a> 
      <br>
      <a href="#41">Hur svarar jag på ett befintligt meddelande?</a> 
      <br>
      <a href="#42">Hur ändrar jag på ett av mina meddelanden?</a> 
      <br>
      <a href="#43">Hur raderar jag ett av mina meddelanden?</a> 
      <br>
      <a href="#44">Mitt meddelande innehåller ord som ersatts med ***?</a> 
      <br>
      <a href="#45">Hur lägger jag till en signatur till mina meddelanden?</a> 
      <br>
      <a href="#46">Hur lägger jag till en avatar till mina meddelanden?</a> 

      <P>
      <b>Användarroller & rättigheter</b>
      <br>
      <a href="#47">Vad är rättigheter?</a> 
      <br>
      <a href="#48">Vad är en administratör?</a> 
      <br>
      <a href="#49">Vad är en moderator?</a> 
      <br>
      <a href="#50">Vad är en forumroll?</a> 
      <br>
      <a href="#51">Hur ansluter jag mig till en forumroll eller grupp?</a> 

      <P>
      <b>Privata meddelanden</b>

      <P>
      <b>Sökning</b>

      <P>
      <b>Om Community Server :: Forums</b>
      <br>
      <a href="#52">Vad är Community Server :: Forums?</a> 
      <br>
      <a href="#53">Vilka använder Community Server :: Forums?</a> 
      <br>
      <a href="#54">Vilka har byggt Community Server :: Forums?</a> 
      <br>
      <a href="#55">Vart kan jag ladda ned Community Server :: Forums?</a> 
      <br>
      <a href="#56">Hur ser licensen ut för att använda forumen?</a> 
      <br>
      <a href="#57">Vad gör jag men nya funktioner jag skapat eller buggar som jag fixat?</a> 

    </td>
  </tr>
</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Registrering & inloggning
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="1"></a><b>Varför måste jag registrera mig?</b>
      <br>
      Beroende på hur administratören har konfigurerat forumen kan det hända att du måste ha ett inloggningskonto för att kunna posta, 
      och i vissa fall för att kunna läsa andra medlemmars meddelanden. De flesta konfigurationer tillåter att man kan läsa meddelanden 
      utan att behöva registrera sig. Forumen tillåter också anonym postning av meddelanden, och administratören kan konfigurera vissa 
      forum att tillåta anonym/gäst postning. För att dra fördel av alla funktioner som forumen erbjuder, som t ex ställa in din avatar 
      , skicka e-post till medlemmar, privata meddelanden, komma åt privata forum, och en massa andra behöver du ett inloggningskonto. 
      Det tar bara några få sekunder att registrera sig, och det är rekommenderat att du gör det.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="2"></a><b>Hur registrerar jag mig?</b>
      <br>
      För att skapa ett inloggningskonto behöver du gå till registreringssidan och fylla i formuläret för att skapa ett nytt konto. 
      Här får du specificera uppgifter som t ex användarnamn och e-postadress - beroende på hur forumen är konfigurerade kanske du 
      också behöver ange ett lösenord som du sen ska använda vid inloggning. Om du inte får en fråga att ange lösenord, kommer ett 
      lösenord att skickas till dig via e-post efter att du registrerat dig.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="3"></a><b>Jag har ett användarnamn och lösenord, hur loggar jag in?</b>
      <br>
      Efter det att du registrerat dig borde du ha ett användarnamn och lösenord. Då kan du besöka inloggningssidan
      och fylla i ditt användarnamn och lösenord för att logga in på forumen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="4"></a><b>Jag är redan inloggad, varför blir jag automatiskt utloggad?</b>
      <Br>
      Om du loggar in utan att kryssa i 'Kom ihåg mig', kommer du att automatiskt loggas ut efter en viss tids inaktivitet. Hur lång tid, 
      bestäms av hur administratören konfigurerat webbplatsen, vanligtvis 20 minuter. Om du vill att forumen alltid ska logga in dig
      automatiskt, kryssa i 'Kom ihåg mig' på inloggningsformuläret.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="5"></a><b>Jag har glömt mitt användarnamn och/eller lösenord.</b>
      <Br>
      Om du glömt bort ditt användarnamn och/eller lösenord kan du besöka 'Bortglömt lösenord'-sidan, och få både ditt användarnamn
      och ett nytt lösenord skickat till dig via e-post genom att ange den e-postadress som du registrerade dig med. Du kommer att få 
      ett nytt lösenord skickat till dig eftersom forumen lagrar lösenorden krypterat och det finns inget sätt för oss att hämta det o-krypterade 
      lösenordet. När du fått ditt användarnamn och nya lösenord kan du logga in och byta till ett annat lösenord.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="6"></a><b>Vad gör jag om jag registrerat mig, men ändå inte kan logga in?</b>
      <br>
      Om du registrerat dig men ändå inte kan logga in, kontrollera att du har ett giltigt användarnamn och lösenord. Om användarnamn 
      och lösenord är giltiga, men du kan fortfarande inte logga in kanske du behöver vänta på att kontot ska aktiveras. Om det är fallet 
      är det bästa att kontakta en administratör eller moderator av forumen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="7"></a><b>Jag har loggat in förut, men nu kan jag inte logga in längre.</b>
      <br>
      Kontrollera först att det användarnamn och lösenord du anger är korrekta. Om du fortfarande inte kan logga in kanske kontot 
      har tillfälligt blockerats eller raderats på grund av inaktivitet. Kontakta en administratör eller moderator.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td class="f">
    </td>
  </tr>
</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Användarprofil & inställningar
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="8"></a><b>Vad är en användarprofil?</b>
      <br>
      En användarprofil innehåller information om ditt inloggningskonto och styr hur du ser information i forumen. Detta inkluderar
      detaljer om konversationer som du bidragit till, personlig information som du vill dela som exempelvis din webbadress eller 
      webblogg. Profilen kontrollerar också hur du ser forumen som t ex: teman, tidszon, och många andra inställningar.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="9"></a><b>Varför ska jag ställa in min tidszon?</b>
      <br>
      När forumen vet i vilken tidszon du befinner dig i kommer de att visa alla datum och tider relativt din tidszon.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="10"></a><b>Hur kan jag lägga till en automatisk signatur till mina meddelanden?</b>
      <br>
      En signatur är ett meddelande som läggs till sist i varje meddelande som du postar i forumen. Du kan ändra din signatur 
      i kontrollpanelen under 'Ändra användarprofil' fliken. 
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="11"></a><b>Vad är en Avatar?</b>
      <br>
      En avatar är en funktion i forumen som låter dig visa en liten bild i varje meddelande du postar. Avatarer kan vara aktiverade 
      eller avaktiverade av forumens administratör.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="12"></a><b>Hur ställer jag in min Avatar?</b>
      <br>
      Om avatarer är aktiverat av administratören så ser du en avatarsektion när du tittar på din profil. Därifrån kan du ladda upp en 
      avatar eller specificera en länk till din avatar. Du måste också kryssa i att aktivera din avatar för att den ska visas i dina 
      meddelanden.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="13"></a><b>Hur kan jag ändra vilket språk forumen visas i?</b>
      <br>
      Forumen är designade för att tillåta flera olika språk. För närvarande finns engelska, svenska och ett antal andra språk, men 
      ytterligare språkpaket kan installeras och ge stöd för fler språk. Besök http://forums.asp.net för extra språkpaket. I 
      din profil kan du se vilka språk som är tillgängliga.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="14"></a><b>Hur ställer jag in datumformatet?</b>
      <br>
      Datumformatet som används för att visa datuminformation i forumen kan konfigureras i din profil.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="15"></a><b>Hur kan jag slå av e-postnotifieringar?</b>
      <br>
      E-postnotifiering är en funktion som skickar e-post till dig när t ex nya meddelanden lägg till konversationer 
      som du prenumerera på. Du kan globalt slå av dina prenumerationer i din profil.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="16"></a><b>Vad betyder de andra ikonerna/avatarerna som visas bredvid varje användare?</b>
      <br>
      Det finns många olika typer av ikoner som kan visas bredvid användarnamn i forumen. Vanliga exempel är administratör, moderator 
      och de som postat flest meddelanden. Andra bilder kan visas beroende på roller som användaren tillhör.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="17"></a><b>Varför måste jag logga in för att skriva meddelanden, se medlemslista, eller skicka e-post till andra medlemmar?</b>
      <br>
      Beroende på hur administratören har konfigurerat forumen kanske du måste logga in innan du kan se/använda dessa areor av forumen. 
      Detta är i första hand för att skydda integriteten av användare som har delat sin information, eller för att skydda mot oönskad e-post (spam).
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Integritet & säkerhet
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="18"></a><b>Hur ändrar jag mitt lösenord?</b>
      <br>
      När du loggat in kan du ändra ditt lösenord från din profilsida.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="19"></a><b>Hur ändrar jag mitt användarnamn?</b>
      <br>
      Om inte administratören har konfigurerat forumen för att tillåta att du kan ändra dit användarnamn, så kan du inte ändra det. 
      I annat fall kan du ändra användarnamnet från din profilsida.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="20"></a><b>Hur ändrar jag min e-postadress?</b>
      <br>
      Efter du loggat in kan du ändra din privata e-postadress från din profilsida.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="21"></a><b>Vilka profilinställningar är obligatoriska?</b>
      <br>
      Den enda profilinställning som är obligatorisk är din privata e-postadress. Det är den e-postadress 
      som används när du prenumererar på konversationer eller när du behöver få ett nytt lösenord via e-post. 
      Den privata e-postadress du anger delas inte med någon annan, eller visas publikt. Om du vill dela din e-postadress 
      publikt kan du använda fältet för publik e-postadress. Resten av profilinställningarna är valfria.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="22"></a><b>Vad gör jag om jag inte vill att mitt namn ska visas i medlemslistan?</b>
      <br>
      Du kan göra den inställningen i din profil och ditt namn kommer inte att visas i någon medlemslista, inkluderat listan på 
      vilka som är online.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Navigering
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="23"></a><b>Vad är en forumgrupp?</b>
      <br>
      En forumgrupp är en gruppering av relaterade forum. En forumgrupp innehåller 1 eller flera forum.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="24"></a><b>Vad är ett forum?</b>
      <br>
      Ett forum är en gruppering relaterad till 'trådar' av konversationer. Ett forum innehåller 0 eller fler konversationer och 
      0 eller fler underforum.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="25"></a><b>Vad är en konversation?</b>
      <br>
      En konversation är en gruppering av relaterade meddelanden i en diskussion. En konversation innehåller 1 eller fler 
      meddelanden. Det första meddelandet skapar konversationen, och följande svar ökar inkrementellt detaljer i konversationen 
      som exempelvis 'antal svar' och 'senaste meddelande'.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="58"></a><b>Vad betyder ikonerna bredvid varje konversation?</b>
      <br>
      <table cellpadding="2" class="tableBorder" cellspacing="1">
	<tr>
		<td align="left" valign="top" class="column" colspan="2">
			Förklaring av konversationsikoner
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic_notread.gif"%>'>
		</td>
		<td width="100%" class="fh3">Konversationer med meddelanden som du inte läst.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic.gif"%>'>
		</td>
		<td width="100%" class="fh3">Konversationer med meddelanden du läst.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-popular_notread.gif"%>'>
		</td>
		<td width="100%" class="fh3">Populära konversationer med meddelanden du inte läst. En konversation 
		blir 'populär' efter den visats ett visst antal gånger eller innehåller ett visst antal svar (definierat av administratören).
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-popular.gif"%>'>
		</td>
		<td width="100%" class="fh3">Populära konversationer med meddelanden du läst. En konversation 
		blir 'populär' efter den visats ett visst antal gånger eller innehåller ett visst antal svar (definierat av administratören).
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-announce_notread.gif"%>'>
		</td>
		<td class="fh3">Tillkännagivanden du inte läst.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-announce.gif"%>'>
		</td>
		<td class="fh3">Tillkännagivanden du läst.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned_notread.gif"%>'>
		</td>
		<td class="fh3">En klistrad konversation med meddelande du inte läst. Klistrade meddelanden 
		visas överst i konversationslistan även efter nya konversationer skapats i forumet, de är klistrade 
		där under en viss tid.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned.gif"%>'>
		</td>
		<td class="fh3">En klistrad konversation med meddelande du läst. Klistrade meddelanden 
		visas överst i konversationslistan även efter nya konversationer skapats i forumet, de är klistrade 
		där under en viss tid.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned&popular_notread.gif"%>'>
		</td>
		<td class="fh3">En klistrad populär konversation med meddelanden du inte läst. En klistrad
		 konversation med tillräckligt många visningar eller svar för att bli populär.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned&popular.gif"%>'>
		</td>
		<td class="fh3">En klistrad populär konversation med meddelanden du läst. En klistrad
		 konversation med tillräckligt många visningar eller svar för att bli populär.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-locked_notread.gif"%>'>
		</td>
		<td class="fh3">En låst konversation med meddelanden du inte läst. Låsta konversationer
			tillåter inga svar.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-locked.gif"%>'>
		</td>
		<td class="fh3">En låst konversation med meddelanden du läst. Låsta konversationer
			tillåter inga svar.
		</td>
	</tr>
      </table>
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="26"></a><b>När jag tittar i ett forum ser jag inga konversationer/meddelanden.</b>
      <br>
      Ett forum kanske inte visar några konversationer om det inte finns några konversationer eller om det är filtrerat, och 
      inga konversationer matchar filtret. Ett exempel på filter är filtrering för att visa enbart konversationer som är nyare 
      än ett visst datum, exempelvis nyare än 2 veckor.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="27"></a><b>Jag postade just ett meddelande, varför kan jag inte se det?</b>
      <br>
      Ett forum kan vara modererat eller inte beroende på hur forumet har konfigurerats. Efter du postat ett meddelande 
      i ett modererat forum kan du få ett meddelande som säger att meddelandet väntar på moderering. När någon av moderatorerna 
      godkänt ditt meddelande kommer ditt meddelande att bli synligt i forumet. Moderatorn kan välja att flytta, ändra, eller radera 
      ditt meddelande för att försäkra att meddelandet håller sig till ämnet i aktuellt forum.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="28"></a><b>Vad betyder de olika ikonerna bredvid varje konversation?</b>
      <br>
      Ikonerna som visas bredvid varje konversation indikerar olika status. Du kan hålla muspekaren över ikonerna för att se 
      vad de olika statustyperna betyder.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="29"></a><b>Vad är ett tillkännagivande?</b>
      <br>
      Ett tillkännagivande är en speciell typ av meddelande som alltid visas längst upp i ett forum för den tid som det 
      är konfigurerat. Syftet med ett tillkännagivande är att öka synligheten för vissa ämnen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="30"></a><b>Vad är en klistrad konversation?</b>
      <br>
      En klistrad konversation är en speciell konversation som gör att meddelandet stannar på toppen i forumets konversationslista 
      för en specificerad tid. En klistrad konversation är liknande ett tillkännagivande, där dock tillkännagivandet visas separat 
      från de andra konversationerna och tillåter vanligtvis inga svar.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>


  <tr>
    <td class="fh2">
      <a name="31"></a><b>Vad är en låst konversation?</b>
      <br>
      En låst konversation är en speciell konversation som inte tillåter några svar. När en användare eller en administratör/moderator
      låser en konversation tillåts inga fler inlägg i den konversationen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="32"></a><b>Kan jag sortera konversationer när jag tittar i ett forum?</b>
      <br>
      Ja, du kan sortera konversationer som visas i ett forum efter författare, antal svar, visningar, och senaste meddelande. 
      Sorteringsordning är som standard att visa nyaste konversationen överst. För att sortera klicka bara på en kolumntitel, t ex 
      senaste meddelande.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="33"></a><b>Vad betyder ‘XML’-ikonen längst ned i varje forum?</b>
      <br>
      'XML'-ikonen är länkad till en RSS-kanal för forumet. RSS används för att låta andra program prenumerera på forumets meddelanden.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="34"></a><b>Vad betyder den röda/gröna ikonen som visas bredvid användarnamnet i ett meddelande?</b>
      <br>
      Denna ikon indikerar om användaren är online. En grön ikon visar att användaren nyligen varit aktiv (vanligtvis de 
      senaste 15 minuterna). En röd ikon visar att användaren inte varit aktiv den senaste tiden. Du kan hålla muspekaren 
      över denna ikon för att se detaljer om användarens senaste aktivitet.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="35"></a><b>Jag kommer inte åt ett forum som jag vet existerar.</b>
      <br>
      Om du försöker komma åt ett forum som du besökt förut, men nu får felmeddelande att forumet inte existerar finns det två 
      tänkbara orsaker. Den ena är att forumet du försöker komma åt är privat och du är inte inloggad. Den andra är att forumet 
      tagits bort.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Posta meddelanden
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="36"></a><b>Kan jag använda HTML-kod i meddelanden?</b>
      <br>
      Ja och nej. Du kan inte skriva HTML-kod direkt i editorn. Om du använder Internet Explorer kommer 
      standard editorn att vara 'Rich Text Editor' som automatiskt kommer att formatera dina meddelanden 
      genom att använda HTML-kod. Om du använder en annan internet-läsare kommer en standard HTML textbox att 
      användas och du kan använda s.k. BBCode för att formatera ditt meddelande.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="37"></a><b>Vad betyder BBCode?</b>
      <br>
      BBCode är en speciell syntax för att formatera textmeddelanden.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="38"></a><b>Kan jag bifoga filer till mina meddelanden?</b>
      <br>
      Ja, men det kräver att en moderator eller administratör har ställt in rättigheter för detta i aktuellt forum.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="39"></a><b>Vad är uttryckssymboler?</b>
      <br>
      Uttryckssymboler är speciella bilder som kan läggas till i meddelandet för att visa känslor och uttryck i meddelandet. 
      Vanliga exempel på det är användandet av s.k. 'smilies' i ett meddelande. Forumen kommer med en fördefinierad mängd 
      uttryckssymboler, men administratören kan lägga till extra uttryckssymboler.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="40"></a><b>Hur postar jag ett nytt meddelande till ett forum?</b>
      <br>
      Du kan posta ett meddelande till ett forum på många olika sätt beroende på hur administratören har konfigurerat 
      webbplatsen. När du är inne i ett forum borde du se en knapp där det står Ny konversation. 
      Om du klickar på den så hamnar du i ett formulär för att posta ett meddelande eller ett som ber dig logga in först. 
      Beroende på hur administratören har konfigurerat forumen kan det hända att du också kan posta meddelanden i vissa forum 
      utan att logga in (anonymt). Om du inte ser knappen Ny konversation även efter du loggat in, så beror det förmodligen på 
      att du inte har rättigheter att posta meddelanden där. Däremot kan du ha rättigheter att läsa meddelanden.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="41"></a><b>Hur svarar jag på ett befintligt meddelande?</b>
      <br>
      
      Du kan svara på ett befintligt meddelande genom att använda knapparna Svara och Citera som visas i meddelandet.
      Om du inte kan se knapparna Svara och Citera när du läser ett meddelande kan det bero på att du saknar rättigheter att svara eller
      citera meddelandet. 
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="42"></a><b>Hur ändrar jag på ett av mina meddelanden?</b>
      <br>
      Om administratören eller moderatorn har konfigurerat forumet eller din roll för att tillåta ändringar av meddelanden så ser du en 
      knapp Redigera bredvid meddelandet du skapat. Klickar du på denna knapp tillåts du redigera meddelandet.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="43"></a><b>Hur raderar jag ett av mina meddelanden?</b>
      <br>
      Om administratören eller moderatorn har konfigurerat forumet eller din roll för att tillåta borttagning av meddelanden så ser du en 
      knapp Ta bort bredvid meddelandet du skapat. Om ditt meddelande har ett eller flera svar kan du inte ta bort meddelandet.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="44"></a><b>Mitt meddelande innehåller ord som ersatts med ***?</b>
      <br>
      Administratören kan ha specificerat ordfiltrering for meddelanden. När ordfiltrering är aktiverat klassas vissa ord som stötande och ersätts då med ‘*’ tecken.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="45"></a><b>Hur lägger jag till en signatur till mina meddelanden?</b>
      <br>
      Se Hur kan jag lägga till en automatisk signatur till mina meddelanden? under Ändra användarprofil i Kontrollpanelen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="46"></a><b>Hur lägger jag till en avatar till mina meddelanden?</b>
      <br>
      Se Vad är en Avatar? och Hur ställer jag in min Avatar? under Ändra användarprofil i Kontrollpanelen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Användarroller & rättigheter
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="47"></a><b>Vad är rättigheter?</b>
      <br>
      Rättigheter kontrollerar vad du tillåts göra när du utforskar forumen. När du använder standardvyer för forumen så visas rättigheterna in det nedre högra hörnet av sidan (sidor där rättigheter används). Dina rättigheter styr vad du ser i forumen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="48"></a><b>Vad är en administratör?</b>
      <br>
      Administratören har högsta rättigheterna inom forumen. Som standard så har en administratör fulla rättigheter för alla aktiviteter inom forumen så som moderering, godkänna användare, skapa nya forum och så vidare. Administratörer tillhör gruppen Forumadministratörer.  
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="49"></a><b>Vad är en moderator?</b>
      <br>
      En moderator har näst högsta nivå av rättigheter inom forumen. Som standard kan en moderator utföra ett antal uppgifter inom forumen t.ex. godkänna meddelanden, flytta meddelanden, ta bort meddelanden, redigera meddelanden och utesluta användare. Om du har problem med ett specifikt forum är det bäst att vända sig till en moderator. Moderatorer tillhör olika grupper som administratören styr.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="50"></a><b>Vad är en forumroll?</b>
      <br>
      En roll i ett forum är en användare med speciella rättigheter. I tillägg till generella rättigheter så kan en forumgrupp också användas för att visa en bild för användare i gruppen. Roller gör jobbet med att administrera och moderera forumen lättare eftersom användare kan tilldelas olika roller och rättigheter för de olika forumen.  
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="51"></a><b>Hur ansluter jag mig till en forumroll eller grupp?</b>
      <br>
      Användare tilldelas en forumroll av administratören eller moderatorn. Om det finns en speciell forumroll du vill ansluta dig till så skickar du ett privat meddelande eller ett e-postmeddelande till en av dess användare för mer information. 
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Privata meddelanden
    </td>
  <tr>
</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Sökning
    </td>
  <tr>
</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Om Community Server :: Forums
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="52"></a><b>Vad är Community Server :: Forums?</b>
      <br>
      Community Server :: Forums är ett skalbart, flexibelt och gratis webbaserat diskussionsforum som du kan använda för att lägga till funktionalitet för diskussionsforum till din webbplats. 
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="53"></a><b>Vilka använder Community Server :: Forums?</b>
      <br>
		Detta diskussionsforum används av många publika och privata organisationer så som Microsoft. Både webbplatsen för ASP.NET och Xbox använder systemet för sina diskussionsforum. Dessa två sajter har tusentals användare dagligen och körs över flera servrar så om du har funderingar på hur skalbart forumet är kan du titta på dessa webbplatser. Du kan använda Google för att hitta andra webbplatser som använder Community Server :: Forums. 
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="54"></a><b>Vilka har byggt Community Server :: Forums?</b>
      <br>
      Community Server :: Forums är ett shared source-projekt som är utvecklat av en olika utvecklare och tillhandahålls via Microsofts ASP.NET-grupp. Lär dig mer om forumet på http://forums.asp.net.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="55"></a><b>Vart kan jag ladda ned Community Server :: Forums?</b>
      <br>
		Besök http://forums.asp.net/builds/ för att hämta senaste version av Community Server :: Forums. Läs filen builds.txt för att få mer information om tillgängliga versioner.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="56"></a><b>Hur ser licensen ut för att använda forumen?</b>
      <br>
      
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

  <tr>
    <td colspan="2" class="flatViewSpacing">
    </td>
  </tr>

  <tr>
    <td class="fh2">
      <a name="57"></a><b>Vad gör jag men nya funktioner jag skapat eller buggar som jag fixat?</b>
      <br>
      
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>


<P>

    </td>
  </tr>
</table>











 



