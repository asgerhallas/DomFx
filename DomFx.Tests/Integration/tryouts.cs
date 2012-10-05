using System.IO;
using System.Windows.Media;
using DomFx.Api;
using DomFx.Layouters.Specification;
using DomFx.Renderers.iTextSharp;
using DomFx.Tests.Fakes;
using Xunit;
using iTextSharp.text.pdf;

namespace DomFx.Tests.Integration
{
    public class tryouts : input_to_pages_tests
    {
//        string lines = @"Asger
//Asger";
        string longtext = @"I sidste uge skulle vi sige farvel til endnu en au pair pige. Denne gang var det fordi, hun ikke følte, at arbejde var noget for hende. Hun vil hellere til Danmark for at stå i butik. Vi gik på Scotts onsdag aften. Trods mange sjove mennesker blev hyggen dræbt af tanken om, at Anna snart skulle hjem og et par andre au pairs overvejelser om at gøre det samme. Øv også! Jeg troede lige, at vi gik og havde det så godt. Og det har jeg også. Men der er mange, der har svært ved at få tiden til at gå hernede. De synes ikke, der er nok udfordring i hverdagen. Det er der selvfølgelig heller ikke, hvis man ikke kan udfordre sig selv. For så er alternativet at sætte sig foran tv’et.. og det forstår jeg godt bliver kedeligt på et tidspunkt.

Torsdag aften mødtes vi igen på Scotts. Denne gang var det kun Linette, Anna, hendes kæreste og så jeg. Det var utrolig hyggeligt. Vi klingede godt. Og skønt Niels (kæresten) ikke kendte til os, så var han med på jokes’ne med det samme. Stemningen var super… Det blev til en af de bedste aftener hernede. Jeg nød det i fulde drag og smilede hele vejen hjem i bussen. Så kan de andre snakke om, at de keder sig… den aften talte i en fuldstændig anden retning. Modet kom tilbage.

Fredag var Annas sidste aften. Vi mødtes på Pizza Hut. Først 6, så 7 og til sidst 8 mennesker. Det var så kikset. Vi havde så meget bagage med, fordi 4 af dem, skulle køre samme aften til Danmark for at holde ferien der. Det medførte en kæmpe bunke tasker, poser og andet godt midt på gulvet. Vi delte to familiepizzaer og regningen.
På vejen videre ned til Scotts gik 4 af os, mens de andre fik pakket bilen. Det er nok det sjoveste jeg har oplevet hernede. Jeg ved ikke, hvad det er, men Sif og jeg har bare et eller andet sammen. Det er farligt, hvis vi sidder ved siden af hinanden… Vi bryder ud i latter, og jeg er faktisk ikke helt sikker på, hvad vi griner af. På vejen derned gik vi sammen med de to mest talende au pairs. De plaprede løs og hørte knap på hinanden. Vi kørte så nogle af tingene længere ud.. måske lidt for langt. Men nøj, det var så sjovt. Vi passerede flere mennesker, der så på os, som om vi var på stoffer. Det var vi nu ikke. Nærmere høje på grin. Vi grinte så meget, at maverne og kinderne gjorde ondt. Da vi endelig nåede Scotts var der tårer løbende ned af kinderne og to meget tissetrængende blærer. De andre var slet ikke med, men nå ja, det var vi jo heller ikke. Tiden gik alt for hurtigt den aften. Klokken nærmede sig 22.00 og folkene skulle cruse af sted mod det kolde nord. Vi var jo 4 der blev, så vi festede videre. Vi kunne ikke finde ud af, hvad vi skulle. Der er langt til Muko MUKO, især når de vælger små stiletter. Vi prøvede derfor noget nyt. Gik på Melosina (eller noget i den stil)… inden vi nåede ind smuttede den. Hendes date havde fået fri fra arbejde. Det er selvfølgelig også en måde at møde lokalbefolkningen på.

Nå.. lidt som i sangen om de 10 cyklister fortsatte vi ufortrødent. Det var et kæmpe disko med flere etager og tilhørende rum. Trods størrelsen var der SLET ikke tomt. Der var proppet med mennesker. Man kunne ingenting… hverken danse eller sidde og hygge. Vi tog meget trætte hjem ved 4-tiden. En af au pairs’ne kunne ikke komme hjem med natbus eller lignende (det kan hun aldrig), så hun kom med mig hjem. Hun er ikke en, der sover længe, så vi var oppe klokken 9.00. Damn det var tidligt. Tænkte at jeg bare ville sove bagefter, når hun var taget med bussen. Men nej, det kunne jeg ikke.

Ved en 14-tiden tog vi ned til en svensk familie. De søgte en barnepige til nødsituationer og også gerne et par timer, så moren har mulighed for at løbe sig en tur eller lignende. Selvom jeg godt forstod det meste dernede, så skulle jeg dælme koncentrere mig meget. Min au pair mor forstår det og snakker det også.. hun kan bare snakke mange sprog. De ville gerne have mig på prøve, for at se om det gik. Det tror jeg nu det kommer til.

Derefter var jeg træt, men jeg havde lovet Gitte at komme ud til hende. Jeg tog derud efter aftensmaden. Vi så film og hyggede os gevaldigt. Snakkede om, hvad vi skulle i ferien. Vi ville begge gerne af sted, men vi vidste ikke rigtig hvor.

Søndag morgen tjekkede vi internettet for billige rejser, men dem var der ikke rigtig så mange tilbage af. Så i stedet begyndte vi at søge på hostel i Bruxelles. Vi kunne ikke rigtig finde nogen, der havde plads flere dage i streg. Valgte at tage forbi Turist Informationen her i Luxembourg for at søge lidt hjælp. De vidste slet ingenting. De havde ikke engang en brochure. Hmm.. men nu havde vi købt billet til Bruxelles. Så nu tog vi altså bare af sted. I værste fald kunne vi jo bare tage toget hjem den selv samme dag… Vi mødtes med to andre au pairs på Italiano (en skøn isbar midt i byen). De kiggede godt nok på os, da vi fortalte, at vi bare sådan tog af sted. Om aftenen pakkede jeg hurtigt en rygsæk med det allermest nødvendige.

Næste morgen kl. 8.30 ventede jeg på bussen. En ung pige kom forpustet derop. Jeg beroligede hende med, at bussen var forsinket, så hun kunne godt slappe af. Vi faldt i snak og det viste sig, at hun kom fra Malta, men boede nu med fire andre unge i et hus i Hostert. Hun vinkede til mig inden i bussen, og vi snakkede lige indtil hun skulle af.

Jeg stod af ved Gare (togstationen) og fik inden længe fundet Gitte. Vi fandt perronen og ventede nu på toget. 3 timer senere var vi i en regnfuld Bruxelles. Traskede ned mod Grand Place for at finde Informationen. Ekspedienten var helt fantastisk. Han vidste lige, hvad vi havde brug for. Vi fik et kort over de forskellige hostels. Vi valgte at bevæge os hen mod det billigste, der lå forholdsvis tæt på. Og vi var heldige. Der var plads på et ottemands rum. Vi satte vores bagage i ’Luggage Room’ og gik ud for at se os om. Vi gik meget uden brug af kort. Vi gik bare. Så en masse forskellige bygninger. Fandt Grand Place. Den er virkelig smuk. Vi stod længe og kiggede. Gik hele vejen rundt for at for det hele med..

Derefter fandt vi ned mod den tissende dreng, som de er så kendte for. Den var for os, som den lille havfrue er for mange turister… noget mindre, end vi havde forventet. Men nu er den set. Resten af tiden den dag gik vi bare rundt og så på de små gader og de mærkelige butikker. De har en del broderi.. jeg fandt aldrig ud af, om det er noget, de er kendt for, eller om det bare er tilfældigt, at de går så meget op i det. I en af de hyggelige gader kom vi forbi et springvand. Ikke et normalt springvand.. nej nej, et springvand med chokolade. Flydende lækker chokolade. Det så hamrende godt ud! Hvor end man befandt sig i byen var der en konstant duft af hjemmelavede belgiske vafler. Det var alt for lækkert og fristende. Men vi havde lige spist kæmpe treretters menu for at fejre, det hele var gået så godt. Der findes en hel restaurantgade, hvor de har lækre middagstilbud. Så selv fattige tøser som os kunne finde sådan et lækkert måltid til en 10-12 euro.

Vi gik forbi et supermarked og købte lidt pålæg og brød til aftensmad og gik derfor hjem til hostellet. Hentede vores bagage og indlogerede os i et dejligt værelse. Det var ’women only’. Vi købte en hængelås til skuffen, så vi kunne låse vores værdigenstande inde. Så var vi ellers klar til at hvile os en halvtime, inden aftensmaden blev nydt ude i ’opholdsstuen’. Der er fyldt med skandinavers. Danskere og svenskere.. det var så mærkeligt.

Vi gjorde os klar til fest og tog ud i den store by. Vi havde ikke forventet, at der var så meget gang i den. Men der var mange bare og diskoteker, der var åbne og gang i. Folk gik på gade og der var liv og glade dage. Vi gik ind på den første bar og to sekunder efter blev vi tilbudt en øl af en nordmand. Han var egentligt meget fin, men også ret belastende. Så vi gik ud i byen igen og fandt denne gang et diskotek. Begge steder vadede vi bare ind, uden at vise ID eller betale. Det er skønt. Der var ikke så meget gang i den der, men alligevel fik vi folk ud på dansegulvet og fik da også scoret et par drinks der. Vi besluttede os, at vi ville videre og endte det første sted igen. Denne gang var der flere mennesker. Vi så hele baren også ovenpå. Så spillede DJ’en et godt nummer og jeg trak Gitte med ud på gulvet, hvor folk ellers ikke dansede. Men det var vist det, der skulle til. Ikke lang tid efter, var der et par stykker oppe og ryste numsen lidt. Vi måtte have en pause og gik ud til døren. Da vi vendte tilbage var der propfuldt.

Vi dansede med mange forskellige. Jeg klikkede med en neger, der bare kunne føre mig rundt på gulvet. Han var en slags ’konge’ her på baren. Folk så på ham og misundte hans måde at danse på. Det er svært at forklare, men ham og jeg havde gulvet, hele baren og kunne lægge den ned. Det var så fedt. Ikke at jeg dansede med ham hele tiden. Slet ikke. Men selv når jeg dansede med andre, var der fokus. Det var så mærkeligt. Folk troede jeg kom fra UK, fordi mit engelsk var så godt.. og jeg havde den der specielle accent. Men midt i alt dette virvar af mennesker, var der også utroligt mange klamme gamle mænd, som bare ikke fatter et nej. Jeg blev reddet af en hollænder. Hende og hendes 3 venner var super søde. De arbejdede alle i Bruxelles, men skulle møde sent dagen efter.
Resten af aftenen blev tilbragt med dem.. mest på dansegulvet. Til sidst valsede vi hjemad, og efter en mindre omvej fandt vi hostellet. De andre 6 på værelset sov allerede. Så vi listede bare i seng godt kvæstet efter en hel dag.

Om morgenen stod de første i mine øjne tidligt op. Vi trillede ud af sengen kl. 8.15 for at nå ned til morgenmad. Der var cornflakes og brød. Så helt perfekt. Hostellet var så perfekt. Det kostede 17 euro den første nat og derefter var det kun 13,5 pr næse.
Vi pakkede tasken og gik derefter ud i byen. Vi gik efter et Instrument Museum. På vejen faldt vi over katedralen. Den var enorm stor og smuk. Uden for stod der adskillige liggestole, som havde retning mod den. Det var lidt mærkeligt, men også meget dejligt, at man kunne ligge og se på den.
Efter en del forvirring fandt vi frem til museet. Man fik udleveret et sæt hørebøffer og kunne derefter gå rundt fra instrument til instrument og høre, hvordan den lød. Jeg synes, det var vildt spændende, men Gitte fik ikke så meget ud af det. Det var sjovt at se, hvordan klaveret var udviklet fra et orgellignende strengeinstrument til det, som vi kender i dag.
Imens vi gik derinde begyndte det pludseligt at styrte ned. Det havde ellers været flot vejr hele dagen og jeg havde ladet jakken blive hjemme. Så da vi endelig kom op på 7. sal for at nyde udsigten, måtte vi stå ude i silende regn. Øv bøv. Men det var smukt alligevel.";

        [Fact(Skip="")]
        public void text_set_to_null_has_previously_caused_exception_in_text_render()
        {
            Setup(() =>
            {
                var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
                var readFully = ReadFully(manifestResourceStream);
                var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

                Box().Width(10).Borders(1, 1, 1, 1, Colors.Aqua);
                Text().Font(new iTextSharpFont(baseFont, 12, 12)).Text(null).Width(10);
            });

            Layout();

            ShowWithiTextSharp();
        }

        [Fact(Skip = "")]
        public void test_i_text_sharp()
        {
            //Setup(() => Image().Source(iTextSharpVectorImage.FromPdf(GetType().Assembly.GetManifestResourceStream("DomFx.Tests.test.pdf"))).SizeBySource());

            //Layout();

            //ShowWithiTextSharp();
        }

        [Fact]
        public void test_i_text_sharp_image()
        {
            Setup(() =>
            {
                var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
                var readFully = ReadFully(manifestResourceStream);
                var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

                Box().Width(10).Height(20).Borders(1, 1, 1, 1, Colors.Aqua);
                End<Box>();

                Text()
                    .Font(new iTextSharpFont(baseFont, 12, 12))
                    //.Margins(4, 4, 4, 4)
                    //.Borders(1,1,1,1,Colors.Black)
                    .Text(longtext).Width(17);


            });

            Layout();

            ShowWithiTextSharp();
        }

        [Fact(Skip = "")]
        public void test_i_text_sharp_text()
        {
            Setup(() =>
            {
                var manifestResourceStream = GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.DINOffc.ttf");
                var readFully = ReadFully(manifestResourceStream);
                var baseFont = BaseFont.CreateFont("DINOffc.ttf", BaseFont.CP1252, true, true, readFully, null);

                Box().Width(10).Borders(1, 1, 1, 1, Colors.Aqua);
                Text().Font(new iTextSharpFont(baseFont, 12, 12)).Text("Hej\nMed\nDig").Width(10);
            });

            Layout();

            ShowWithiTextSharp();
        }

        [Fact(Skip = "")]
        public void test_itextsharp()
        {
            Setup(() => Image()
                            .Source(iTextSharpVectorImage.FromPdf(GetType().Assembly.GetManifestResourceStream("DomFx.Tests.Resources.test.pdf")))
                            .SizeBySource());

            Layout();

            ShowWithiTextSharp();
        }

        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}