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
      Fr�gor och svar
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <b>Registrering & inloggning</b>
      <br>
      <a href="#1">Varf�r m�ste jag registrera mig?</a>
      <br>
      <a href="#2">Hur registrerar jag mig?</a>
      <br>
      <a href="#3">Jag har ett anv�ndarnamn och l�senord, hur loggar jag in?</a>
      <br>
      <a href="#4">Jag �r redan inloggad, varf�r blir jag automatiskt utloggad?</a>
      <br>
      <a href="#5">Jag har gl�mt mitt anv�ndarnamn och/eller l�senord.</a>
      <br>
      <a href="#6"> Vad g�r jag om jag registrerat mig, men �nd� inte kan logga in?</a>
      <br> 
      <a href="#7">Jag har loggat in f�rut, men nu kan jag inte logga in l�ngre.</a>

      <P>

      <b>Anv�ndarprofil & inst�llningar </b>
      <br>
      <a href="#8">Vad �r en anv�ndarprofil?</a>
      <br> 
      <a href="#9">Varf�r ska jag st�lla in min tidszon?</a> 
      <br> 
      <a href="#10">Hur kan jag l�gga till en automatisk signatur till mina meddelanden? 
      <br> 
      <a href="#11">Vad �r en Avatar?</a> 
      <br> 
      <a href="#12">Hur st�ller jag in min Avatar? 
      <br> 
      <a href="#13">Hur kan jag �ndra vilket spr�k forumen visas i?</a> 
      <br> 
      <a href="#14">Hur st�ller jag in datumformatet?</a>  
      <br> 
      <a href="#15">Hur kan jag sl� av e-postnotifieringar?</a> 
      <br> 
      <a href="#16">Vad betyder de andra ikonerna/avatarerna som visas bredvid varje anv�ndare?</a> 
      <br> 
      <a href="#17">Varf�r m�ste jag logga in f�r att skriva meddelanden, se medlemslista, eller skicka e-post till andra medlemmar?</a> 

      <P>

      <b>Integritet & s�kerhet</b>
      <br>
      <a href="#18">Hur �ndrar jag mitt l�senord?</a> 
      <br>
      <a href="#19">Hur �ndrar jag mitt anv�ndarnamn?</a> 
      <br>
      <a href="#20">Hur �ndrar jag min e-postadress?</a>  
      <br>
      <a href="#21">Vilka profil-inst�llningar �r obligatoriska?</a>  
      <br>
      <a href="#22">Vad g�r jag om jag inte vill att mitt namn ska visas i medlemslistan?</a>  

      <P>
      <b>Navigering</b>
      <br>
      <a href="#23">Vad �r en forumgrupp?</a> 
      <br>
      <a href="#24">Vad �r ett forum?</a> 
      <br>
      <a href="#25">Vad �r en konversation?</a> 
      <br>
      <a href="#58">Vad betyder ikonerna bredvid varje konversation?</a> 
      <br>
      <a href="#26">N�r jag tittar i ett forum ser jag inga konversationer/meddelanden.</a> 
      <br>
      <a href="#27">Jag postade just ett meddelande, varf�r kan jag inte se det?</a> 
      <br>
      <a href="#28">Vad betyder de olika ikonerna bredvid varje konversation?</a> 
      <br>
      <a href="#29">Vad �r ett tillk�nnagivande?</a> 
      <br>
      <a href="#30">Vad �r en klistrad konversation?</a> 
      <br>
      <a href="#31">Vad �r en l�st konversation?</a> 
      <br>
      <a href="#32">Kan jag sortera konversationer n�r jag tittar i ett forum?</a> 
      <br>
      <a href="#33">Vad betyder �XML�-ikonen l�ngst ned i varje forum?</a> 
      <br>
      <a href="#34">Vad betyder den r�da/gr�na ikonen som visas bredvid anv�ndarnamnet i ett meddelande?</a> 
      <br>
      <a href="#35">Jag kommer inte �t ett forum som jag vet existerar.</a> 

      <P>
      <b>Posta meddelanden</b>
      <br>
      <a href="#36">Kan jag anv�nda HTML-kod i meddelanden?</a> 
      <br>
      <a href="#37">Vad betyder BBCode?</a> 
      <br>
      <a href="#38">Kan jag bifoga filer till mina meddelanden?</a> 
      <br>
      <a href="#39">Vad �r uttryckssymboler?</a> 
      <br>
      <a href="#40">Hur postar jag ett nytt meddelande till ett forum?</a> 
      <br>
      <a href="#41">Hur svarar jag p� ett befintligt meddelande?</a> 
      <br>
      <a href="#42">Hur �ndrar jag p� ett av mina meddelanden?</a> 
      <br>
      <a href="#43">Hur raderar jag ett av mina meddelanden?</a> 
      <br>
      <a href="#44">Mitt meddelande inneh�ller ord som ersatts med ***?</a> 
      <br>
      <a href="#45">Hur l�gger jag till en signatur till mina meddelanden?</a> 
      <br>
      <a href="#46">Hur l�gger jag till en avatar till mina meddelanden?</a> 

      <P>
      <b>Anv�ndarroller & r�ttigheter</b>
      <br>
      <a href="#47">Vad �r r�ttigheter?</a> 
      <br>
      <a href="#48">Vad �r en administrat�r?</a> 
      <br>
      <a href="#49">Vad �r en moderator?</a> 
      <br>
      <a href="#50">Vad �r en forumroll?</a> 
      <br>
      <a href="#51">Hur ansluter jag mig till en forumroll eller grupp?</a> 

      <P>
      <b>Privata meddelanden</b>

      <P>
      <b>S�kning</b>

      <P>
      <b>Om Community Server :: Forums</b>
      <br>
      <a href="#52">Vad �r Community Server :: Forums?</a> 
      <br>
      <a href="#53">Vilka anv�nder Community Server :: Forums?</a> 
      <br>
      <a href="#54">Vilka har byggt Community Server :: Forums?</a> 
      <br>
      <a href="#55">Vart kan jag ladda ned Community Server :: Forums?</a> 
      <br>
      <a href="#56">Hur ser licensen ut f�r att anv�nda forumen?</a> 
      <br>
      <a href="#57">Vad g�r jag men nya funktioner jag skapat eller buggar som jag fixat?</a> 

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
      <a name="1"></a><b>Varf�r m�ste jag registrera mig?</b>
      <br>
      Beroende p� hur administrat�ren har konfigurerat forumen kan det h�nda att du m�ste ha ett inloggningskonto f�r att kunna posta, 
      och i vissa fall f�r att kunna l�sa andra medlemmars meddelanden. De flesta konfigurationer till�ter att man kan l�sa meddelanden 
      utan att beh�va registrera sig. Forumen till�ter ocks� anonym postning av meddelanden, och administrat�ren kan konfigurera vissa 
      forum att till�ta anonym/g�st postning. F�r att dra f�rdel av alla funktioner som forumen erbjuder, som t ex st�lla in din avatar 
      , skicka e-post till medlemmar, privata meddelanden, komma �t privata forum, och en massa andra beh�ver du ett inloggningskonto. 
      Det tar bara n�gra f� sekunder att registrera sig, och det �r rekommenderat att du g�r det.
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
      F�r att skapa ett inloggningskonto beh�ver du g� till registreringssidan och fylla i formul�ret f�r att skapa ett nytt konto. 
      H�r f�r du specificera uppgifter som t ex anv�ndarnamn och e-postadress - beroende p� hur forumen �r konfigurerade kanske du 
      ocks� beh�ver ange ett l�senord som du sen ska anv�nda vid inloggning. Om du inte f�r en fr�ga att ange l�senord, kommer ett 
      l�senord att skickas till dig via e-post efter att du registrerat dig.
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
      <a name="3"></a><b>Jag har ett anv�ndarnamn och l�senord, hur loggar jag in?</b>
      <br>
      Efter det att du registrerat dig borde du ha ett anv�ndarnamn och l�senord. D� kan du bes�ka inloggningssidan
      och fylla i ditt anv�ndarnamn och l�senord f�r att logga in p� forumen.
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
      <a name="4"></a><b>Jag �r redan inloggad, varf�r blir jag automatiskt utloggad?</b>
      <Br>
      Om du loggar in utan att kryssa i 'Kom ih�g mig', kommer du att automatiskt loggas ut efter en viss tids inaktivitet. Hur l�ng tid, 
      best�ms av hur administrat�ren konfigurerat webbplatsen, vanligtvis 20 minuter. Om du vill att forumen alltid ska logga in dig
      automatiskt, kryssa i 'Kom ih�g mig' p� inloggningsformul�ret.
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
      <a name="5"></a><b>Jag har gl�mt mitt anv�ndarnamn och/eller l�senord.</b>
      <Br>
      Om du gl�mt bort ditt anv�ndarnamn och/eller l�senord kan du bes�ka 'Bortgl�mt l�senord'-sidan, och f� b�de ditt anv�ndarnamn
      och ett nytt l�senord skickat till dig via e-post genom att ange den e-postadress som du registrerade dig med. Du kommer att f� 
      ett nytt l�senord skickat till dig eftersom forumen lagrar l�senorden krypterat och det finns inget s�tt f�r oss att h�mta det o-krypterade 
      l�senordet. N�r du f�tt ditt anv�ndarnamn och nya l�senord kan du logga in och byta till ett annat l�senord.
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
      <a name="6"></a><b>Vad g�r jag om jag registrerat mig, men �nd� inte kan logga in?</b>
      <br>
      Om du registrerat dig men �nd� inte kan logga in, kontrollera att du har ett giltigt anv�ndarnamn och l�senord. Om anv�ndarnamn 
      och l�senord �r giltiga, men du kan fortfarande inte logga in kanske du beh�ver v�nta p� att kontot ska aktiveras. Om det �r fallet 
      �r det b�sta att kontakta en administrat�r eller moderator av forumen.
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
      <a name="7"></a><b>Jag har loggat in f�rut, men nu kan jag inte logga in l�ngre.</b>
      <br>
      Kontrollera f�rst att det anv�ndarnamn och l�senord du anger �r korrekta. Om du fortfarande inte kan logga in kanske kontot 
      har tillf�lligt blockerats eller raderats p� grund av inaktivitet. Kontakta en administrat�r eller moderator.
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
      Anv�ndarprofil & inst�llningar
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="8"></a><b>Vad �r en anv�ndarprofil?</b>
      <br>
      En anv�ndarprofil inneh�ller information om ditt inloggningskonto och styr hur du ser information i forumen. Detta inkluderar
      detaljer om konversationer som du bidragit till, personlig information som du vill dela som exempelvis din webbadress eller 
      webblogg. Profilen kontrollerar ocks� hur du ser forumen som t ex: teman, tidszon, och m�nga andra inst�llningar.
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
      <a name="9"></a><b>Varf�r ska jag st�lla in min tidszon?</b>
      <br>
      N�r forumen vet i vilken tidszon du befinner dig i kommer de att visa alla datum och tider relativt din tidszon.
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
      <a name="10"></a><b>Hur kan jag l�gga till en automatisk signatur till mina meddelanden?</b>
      <br>
      En signatur �r ett meddelande som l�ggs till sist i varje meddelande som du postar i forumen. Du kan �ndra din signatur 
      i kontrollpanelen under '�ndra anv�ndarprofil' fliken. 
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
      <a name="11"></a><b>Vad �r en Avatar?</b>
      <br>
      En avatar �r en funktion i forumen som l�ter dig visa en liten bild i varje meddelande du postar. Avatarer kan vara aktiverade 
      eller avaktiverade av forumens administrat�r.
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
      <a name="12"></a><b>Hur st�ller jag in min Avatar?</b>
      <br>
      Om avatarer �r aktiverat av administrat�ren s� ser du en avatarsektion n�r du tittar p� din profil. D�rifr�n kan du ladda upp en 
      avatar eller specificera en l�nk till din avatar. Du m�ste ocks� kryssa i att aktivera din avatar f�r att den ska visas i dina 
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
      <a name="13"></a><b>Hur kan jag �ndra vilket spr�k forumen visas i?</b>
      <br>
      Forumen �r designade f�r att till�ta flera olika spr�k. F�r n�rvarande finns engelska, svenska och ett antal andra spr�k, men 
      ytterligare spr�kpaket kan installeras och ge st�d f�r fler spr�k. Bes�k http://forums.asp.net f�r extra spr�kpaket. I 
      din profil kan du se vilka spr�k som �r tillg�ngliga.
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
      <a name="14"></a><b>Hur st�ller jag in datumformatet?</b>
      <br>
      Datumformatet som anv�nds f�r att visa datuminformation i forumen kan konfigureras i din profil.
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
      <a name="15"></a><b>Hur kan jag sl� av e-postnotifieringar?</b>
      <br>
      E-postnotifiering �r en funktion som skickar e-post till dig n�r t ex nya meddelanden l�gg till konversationer 
      som du prenumerera p�. Du kan globalt sl� av dina prenumerationer i din profil.
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
      <a name="16"></a><b>Vad betyder de andra ikonerna/avatarerna som visas bredvid varje anv�ndare?</b>
      <br>
      Det finns m�nga olika typer av ikoner som kan visas bredvid anv�ndarnamn i forumen. Vanliga exempel �r administrat�r, moderator 
      och de som postat flest meddelanden. Andra bilder kan visas beroende p� roller som anv�ndaren tillh�r.
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
      <a name="17"></a><b>Varf�r m�ste jag logga in f�r att skriva meddelanden, se medlemslista, eller skicka e-post till andra medlemmar?</b>
      <br>
      Beroende p� hur administrat�ren har konfigurerat forumen kanske du m�ste logga in innan du kan se/anv�nda dessa areor av forumen. 
      Detta �r i f�rsta hand f�r att skydda integriteten av anv�ndare som har delat sin information, eller f�r att skydda mot o�nskad e-post (spam).
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>

<p>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Integritet & s�kerhet
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="18"></a><b>Hur �ndrar jag mitt l�senord?</b>
      <br>
      N�r du loggat in kan du �ndra ditt l�senord fr�n din profilsida.
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
      <a name="19"></a><b>Hur �ndrar jag mitt anv�ndarnamn?</b>
      <br>
      Om inte administrat�ren har konfigurerat forumen f�r att till�ta att du kan �ndra dit anv�ndarnamn, s� kan du inte �ndra det. 
      I annat fall kan du �ndra anv�ndarnamnet fr�n din profilsida.
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
      <a name="20"></a><b>Hur �ndrar jag min e-postadress?</b>
      <br>
      Efter du loggat in kan du �ndra din privata e-postadress fr�n din profilsida.
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
      <a name="21"></a><b>Vilka profilinst�llningar �r obligatoriska?</b>
      <br>
      Den enda profilinst�llning som �r obligatorisk �r din privata e-postadress. Det �r den e-postadress 
      som anv�nds n�r du prenumererar p� konversationer eller n�r du beh�ver f� ett nytt l�senord via e-post. 
      Den privata e-postadress du anger delas inte med n�gon annan, eller visas publikt. Om du vill dela din e-postadress 
      publikt kan du anv�nda f�ltet f�r publik e-postadress. Resten av profilinst�llningarna �r valfria.
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
      <a name="22"></a><b>Vad g�r jag om jag inte vill att mitt namn ska visas i medlemslistan?</b>
      <br>
      Du kan g�ra den inst�llningen i din profil och ditt namn kommer inte att visas i n�gon medlemslista, inkluderat listan p� 
      vilka som �r online.
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
      <a name="23"></a><b>Vad �r en forumgrupp?</b>
      <br>
      En forumgrupp �r en gruppering av relaterade forum. En forumgrupp inneh�ller 1 eller flera forum.
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
      <a name="24"></a><b>Vad �r ett forum?</b>
      <br>
      Ett forum �r en gruppering relaterad till 'tr�dar' av konversationer. Ett forum inneh�ller 0 eller fler konversationer och 
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
      <a name="25"></a><b>Vad �r en konversation?</b>
      <br>
      En konversation �r en gruppering av relaterade meddelanden i en diskussion. En konversation inneh�ller 1 eller fler 
      meddelanden. Det f�rsta meddelandet skapar konversationen, och f�ljande svar �kar inkrementellt detaljer i konversationen 
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
			F�rklaring av konversationsikoner
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic_notread.gif"%>'>
		</td>
		<td width="100%" class="fh3">Konversationer med meddelanden som du inte l�st.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic.gif"%>'>
		</td>
		<td width="100%" class="fh3">Konversationer med meddelanden du l�st.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-popular_notread.gif"%>'>
		</td>
		<td width="100%" class="fh3">Popul�ra konversationer med meddelanden du inte l�st. En konversation 
		blir 'popul�r' efter den visats ett visst antal g�nger eller inneh�ller ett visst antal svar (definierat av administrat�ren).
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-popular.gif"%>'>
		</td>
		<td width="100%" class="fh3">Popul�ra konversationer med meddelanden du l�st. En konversation 
		blir 'popul�r' efter den visats ett visst antal g�nger eller inneh�ller ett visst antal svar (definierat av administrat�ren).
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-announce_notread.gif"%>'>
		</td>
		<td class="fh3">Tillk�nnagivanden du inte l�st.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-announce.gif"%>'>
		</td>
		<td class="fh3">Tillk�nnagivanden du l�st.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned_notread.gif"%>'>
		</td>
		<td class="fh3">En klistrad konversation med meddelande du inte l�st. Klistrade meddelanden 
		visas �verst i konversationslistan �ven efter nya konversationer skapats i forumet, de �r klistrade 
		d�r under en viss tid.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned.gif"%>'>
		</td>
		<td class="fh3">En klistrad konversation med meddelande du l�st. Klistrade meddelanden 
		visas �verst i konversationslistan �ven efter nya konversationer skapats i forumet, de �r klistrade 
		d�r under en viss tid.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned&popular_notread.gif"%>'>
		</td>
		<td class="fh3">En klistrad popul�r konversation med meddelanden du inte l�st. En klistrad
		 konversation med tillr�ckligt m�nga visningar eller svar f�r att bli popul�r.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-pinned&popular.gif"%>'>
		</td>
		<td class="fh3">En klistrad popul�r konversation med meddelanden du l�st. En klistrad
		 konversation med tillr�ckligt m�nga visningar eller svar f�r att bli popul�r.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-locked_notread.gif"%>'>
		</td>
		<td class="fh3">En l�st konversation med meddelanden du inte l�st. L�sta konversationer
			till�ter inga svar.
		</td>
	</tr>
	<tr>
		<td class="fh3">
			<img src='<%=Globals.GetSkinPath() + "/images/topic-locked.gif"%>'>
		</td>
		<td class="fh3">En l�st konversation med meddelanden du l�st. L�sta konversationer
			till�ter inga svar.
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
      <a name="26"></a><b>N�r jag tittar i ett forum ser jag inga konversationer/meddelanden.</b>
      <br>
      Ett forum kanske inte visar n�gra konversationer om det inte finns n�gra konversationer eller om det �r filtrerat, och 
      inga konversationer matchar filtret. Ett exempel p� filter �r filtrering f�r att visa enbart konversationer som �r nyare 
      �n ett visst datum, exempelvis nyare �n 2 veckor.
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
      <a name="27"></a><b>Jag postade just ett meddelande, varf�r kan jag inte se det?</b>
      <br>
      Ett forum kan vara modererat eller inte beroende p� hur forumet har konfigurerats. Efter du postat ett meddelande 
      i ett modererat forum kan du f� ett meddelande som s�ger att meddelandet v�ntar p� moderering. N�r n�gon av moderatorerna 
      godk�nt ditt meddelande kommer ditt meddelande att bli synligt i forumet. Moderatorn kan v�lja att flytta, �ndra, eller radera 
      ditt meddelande f�r att f�rs�kra att meddelandet h�ller sig till �mnet i aktuellt forum.
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
      Ikonerna som visas bredvid varje konversation indikerar olika status. Du kan h�lla muspekaren �ver ikonerna f�r att se 
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
      <a name="29"></a><b>Vad �r ett tillk�nnagivande?</b>
      <br>
      Ett tillk�nnagivande �r en speciell typ av meddelande som alltid visas l�ngst upp i ett forum f�r den tid som det 
      �r konfigurerat. Syftet med ett tillk�nnagivande �r att �ka synligheten f�r vissa �mnen.
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
      <a name="30"></a><b>Vad �r en klistrad konversation?</b>
      <br>
      En klistrad konversation �r en speciell konversation som g�r att meddelandet stannar p� toppen i forumets konversationslista 
      f�r en specificerad tid. En klistrad konversation �r liknande ett tillk�nnagivande, d�r dock tillk�nnagivandet visas separat 
      fr�n de andra konversationerna och till�ter vanligtvis inga svar.
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
      <a name="31"></a><b>Vad �r en l�st konversation?</b>
      <br>
      En l�st konversation �r en speciell konversation som inte till�ter n�gra svar. N�r en anv�ndare eller en administrat�r/moderator
      l�ser en konversation till�ts inga fler inl�gg i den konversationen.
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
      <a name="32"></a><b>Kan jag sortera konversationer n�r jag tittar i ett forum?</b>
      <br>
      Ja, du kan sortera konversationer som visas i ett forum efter f�rfattare, antal svar, visningar, och senaste meddelande. 
      Sorteringsordning �r som standard att visa nyaste konversationen �verst. F�r att sortera klicka bara p� en kolumntitel, t ex 
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
      <a name="33"></a><b>Vad betyder �XML�-ikonen l�ngst ned i varje forum?</b>
      <br>
      'XML'-ikonen �r l�nkad till en RSS-kanal f�r forumet. RSS anv�nds f�r att l�ta andra program prenumerera p� forumets meddelanden.
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
      <a name="34"></a><b>Vad betyder den r�da/gr�na ikonen som visas bredvid anv�ndarnamnet i ett meddelande?</b>
      <br>
      Denna ikon indikerar om anv�ndaren �r online. En gr�n ikon visar att anv�ndaren nyligen varit aktiv (vanligtvis de 
      senaste 15 minuterna). En r�d ikon visar att anv�ndaren inte varit aktiv den senaste tiden. Du kan h�lla muspekaren 
      �ver denna ikon f�r att se detaljer om anv�ndarens senaste aktivitet.
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
      <a name="35"></a><b>Jag kommer inte �t ett forum som jag vet existerar.</b>
      <br>
      Om du f�rs�ker komma �t ett forum som du bes�kt f�rut, men nu f�r felmeddelande att forumet inte existerar finns det tv� 
      t�nkbara orsaker. Den ena �r att forumet du f�rs�ker komma �t �r privat och du �r inte inloggad. Den andra �r att forumet 
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
      <a name="36"></a><b>Kan jag anv�nda HTML-kod i meddelanden?</b>
      <br>
      Ja och nej. Du kan inte skriva HTML-kod direkt i editorn. Om du anv�nder Internet Explorer kommer 
      standard editorn att vara 'Rich Text Editor' som automatiskt kommer att formatera dina meddelanden 
      genom att anv�nda HTML-kod. Om du anv�nder en annan internet-l�sare kommer en standard HTML textbox att 
      anv�ndas och du kan anv�nda s.k. BBCode f�r att formatera ditt meddelande.
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
      BBCode �r en speciell syntax f�r att formatera textmeddelanden.
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
      Ja, men det kr�ver att en moderator eller administrat�r har st�llt in r�ttigheter f�r detta i aktuellt forum.
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
      <a name="39"></a><b>Vad �r uttryckssymboler?</b>
      <br>
      Uttryckssymboler �r speciella bilder som kan l�ggas till i meddelandet f�r att visa k�nslor och uttryck i meddelandet. 
      Vanliga exempel p� det �r anv�ndandet av s.k. 'smilies' i ett meddelande. Forumen kommer med en f�rdefinierad m�ngd 
      uttryckssymboler, men administrat�ren kan l�gga till extra uttryckssymboler.
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
      Du kan posta ett meddelande till ett forum p� m�nga olika s�tt beroende p� hur administrat�ren har konfigurerat 
      webbplatsen. N�r du �r inne i ett forum borde du se en knapp d�r det st�r Ny konversation. 
      Om du klickar p� den s� hamnar du i ett formul�r f�r att posta ett meddelande eller ett som ber dig logga in f�rst. 
      Beroende p� hur administrat�ren har konfigurerat forumen kan det h�nda att du ocks� kan posta meddelanden i vissa forum 
      utan att logga in (anonymt). Om du inte ser knappen Ny konversation �ven efter du loggat in, s� beror det f�rmodligen p� 
      att du inte har r�ttigheter att posta meddelanden d�r. D�remot kan du ha r�ttigheter att l�sa meddelanden.
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
      <a name="41"></a><b>Hur svarar jag p� ett befintligt meddelande?</b>
      <br>
      
      Du kan svara p� ett befintligt meddelande genom att anv�nda knapparna Svara och Citera som visas i meddelandet.
      Om du inte kan se knapparna Svara och Citera n�r du l�ser ett meddelande kan det bero p� att du saknar r�ttigheter att svara eller
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
      <a name="42"></a><b>Hur �ndrar jag p� ett av mina meddelanden?</b>
      <br>
      Om administrat�ren eller moderatorn har konfigurerat forumet eller din roll f�r att till�ta �ndringar av meddelanden s� ser du en 
      knapp Redigera bredvid meddelandet du skapat. Klickar du p� denna knapp till�ts du redigera meddelandet.
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
      Om administrat�ren eller moderatorn har konfigurerat forumet eller din roll f�r att till�ta borttagning av meddelanden s� ser du en 
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
      <a name="44"></a><b>Mitt meddelande inneh�ller ord som ersatts med ***?</b>
      <br>
      Administrat�ren kan ha specificerat ordfiltrering for meddelanden. N�r ordfiltrering �r aktiverat klassas vissa ord som st�tande och ers�tts d� med �*� tecken.
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
      <a name="45"></a><b>Hur l�gger jag till en signatur till mina meddelanden?</b>
      <br>
      Se Hur kan jag l�gga till en automatisk signatur till mina meddelanden? under �ndra anv�ndarprofil i Kontrollpanelen.
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
      <a name="46"></a><b>Hur l�gger jag till en avatar till mina meddelanden?</b>
      <br>
      Se Vad �r en Avatar? och Hur st�ller jag in min Avatar? under �ndra anv�ndarprofil i Kontrollpanelen.
      <br>
      <a href="#top">Tillbaka till sidans topp</a>
    </td>
  </tr>

</table>

<P>

<table width="100%" class="tableBorder" cellpadding=3 cellspacing=1>
  <tr>
    <td class="column" height=10>
      Anv�ndarroller & r�ttigheter
    </td>
  <tr>

  <tr>
    <td class="fh2">
      <a name="47"></a><b>Vad �r r�ttigheter?</b>
      <br>
      R�ttigheter kontrollerar vad du till�ts g�ra n�r du utforskar forumen. N�r du anv�nder standardvyer f�r forumen s� visas r�ttigheterna in det nedre h�gra h�rnet av sidan (sidor d�r r�ttigheter anv�nds). Dina r�ttigheter styr vad du ser i forumen.
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
      <a name="48"></a><b>Vad �r en administrat�r?</b>
      <br>
      Administrat�ren har h�gsta r�ttigheterna inom forumen. Som standard s� har en administrat�r fulla r�ttigheter f�r alla aktiviteter inom forumen s� som moderering, godk�nna anv�ndare, skapa nya forum och s� vidare. Administrat�rer tillh�r gruppen Forumadministrat�rer.  
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
      <a name="49"></a><b>Vad �r en moderator?</b>
      <br>
      En moderator har n�st h�gsta niv� av r�ttigheter inom forumen. Som standard kan en moderator utf�ra ett antal uppgifter inom forumen t.ex. godk�nna meddelanden, flytta meddelanden, ta bort meddelanden, redigera meddelanden och utesluta anv�ndare. Om du har problem med ett specifikt forum �r det b�st att v�nda sig till en moderator. Moderatorer tillh�r olika grupper som administrat�ren styr.
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
      <a name="50"></a><b>Vad �r en forumroll?</b>
      <br>
      En roll i ett forum �r en anv�ndare med speciella r�ttigheter. I till�gg till generella r�ttigheter s� kan en forumgrupp ocks� anv�ndas f�r att visa en bild f�r anv�ndare i gruppen. Roller g�r jobbet med att administrera och moderera forumen l�ttare eftersom anv�ndare kan tilldelas olika roller och r�ttigheter f�r de olika forumen.  
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
      Anv�ndare tilldelas en forumroll av administrat�ren eller moderatorn. Om det finns en speciell forumroll du vill ansluta dig till s� skickar du ett privat meddelande eller ett e-postmeddelande till en av dess anv�ndare f�r mer information. 
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
      S�kning
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
      <a name="52"></a><b>Vad �r Community Server :: Forums?</b>
      <br>
      Community Server :: Forums �r ett skalbart, flexibelt och gratis webbaserat diskussionsforum som du kan anv�nda f�r att l�gga till funktionalitet f�r diskussionsforum till din webbplats. 
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
      <a name="53"></a><b>Vilka anv�nder Community Server :: Forums?</b>
      <br>
		Detta diskussionsforum anv�nds av m�nga publika och privata organisationer s� som Microsoft. B�de webbplatsen f�r ASP.NET och Xbox anv�nder systemet f�r sina diskussionsforum. Dessa tv� sajter har tusentals anv�ndare dagligen och k�rs �ver flera servrar s� om du har funderingar p� hur skalbart forumet �r kan du titta p� dessa webbplatser. Du kan anv�nda Google f�r att hitta andra webbplatser som anv�nder Community Server :: Forums. 
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
      Community Server :: Forums �r ett shared source-projekt som �r utvecklat av en olika utvecklare och tillhandah�lls via Microsofts ASP.NET-grupp. L�r dig mer om forumet p� http://forums.asp.net.
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
		Bes�k http://forums.asp.net/builds/ f�r att h�mta senaste version av Community Server :: Forums. L�s filen builds.txt f�r att f� mer information om tillg�ngliga versioner.
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
      <a name="56"></a><b>Hur ser licensen ut f�r att anv�nda forumen?</b>
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
      <a name="57"></a><b>Vad g�r jag men nya funktioner jag skapat eller buggar som jag fixat?</b>
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











 



