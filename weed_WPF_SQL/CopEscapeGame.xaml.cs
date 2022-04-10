using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for CopEscapeGame.xaml
    /// </summary>
    public partial class CopEscapeGame : Window
    {
        Random rng = new Random();
        int scenario;
        bool pathChosen = false;
        bool goodEnding;
        private BitmapImage imgSchool = new BitmapImage();
        private BitmapImage imgPark = new BitmapImage();
        private BitmapImage imgAlley = new BitmapImage();
        private BitmapImage imgSquare = new BitmapImage();
        public CopEscapeGame()
        {
            InitializeComponent();
        }

        public CopEscapeGame(int LocationCaught) : base()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            bool aggressiveCops = false;
            if (rng.Next(2) == 1)
            {
                aggressiveCops = true;
            }
            switch (LocationCaught)
            {
                case 1: //school
                    if (aggressiveCops)
                    {
                        scenario = 1;
                    }
                    else
                    {
                        scenario = 2;
                    }
                    imgSchool.BeginInit();
                    imgSchool.UriSource = new Uri("/Assets/img/school.png", UriKind.Relative);
                    imgSchool.EndInit();
                    imgBackground.Source = imgSchool;
                    break;
                case 2: //park
                    if (aggressiveCops)
                    {
                        scenario = 3;
                    }
                    else
                    {
                        scenario = 4;
                    }
                    imgPark.BeginInit();
                    imgPark.UriSource = new Uri("/Assets/img/park.png", UriKind.Relative);
                    imgPark.EndInit();
                    imgBackground.Source = imgPark;
                    break;
                case 3: //square
                    if (aggressiveCops)
                    {
                        scenario = 5;
                    }
                    else
                    {
                        scenario = 6;
                    }
                    imgSquare.BeginInit();
                    imgSquare.UriSource = new Uri("/Assets/img/townsquare.png", UriKind.Relative);
                    imgSquare.EndInit();
                    imgBackground.Source = imgSquare;
                    break;
                case 4: //narrow streets
                    if (aggressiveCops)
                    {
                        scenario = 7;
                    }
                    else
                    {
                        scenario = 8;
                    }
                    imgAlley.BeginInit();
                    imgAlley.UriSource = new Uri("/Assets/img/alley.png", UriKind.Relative);
                    imgAlley.EndInit();
                    imgBackground.Source = imgAlley;
                    break;
            }
            lblTitle.FontFamily = MediaManager.Instance().FntTitleFont;
            tbScenario.FontFamily = MediaManager.Instance().FntMainFont;
            btnSolution1.FontFamily = MediaManager.Instance().FntMainFont;
            btnSolution2.FontFamily = MediaManager.Instance().FntMainFont;
            LoadScenario();
        }

        public void LoadScenario() 
        {
            switch (scenario)
            {
                case 1: //school chase
                    
                    lblTitle.Content = "Achtervolging aan de School";
                    tbScenario.Text = "Je loopt zo snel als je kan, maar je hoort de agent roepen.\n Hij is niet ver achter jou. Je keert de hoek om en ziet een grote drukte voor de poort van de lokale school. Misschien kan je ervan gebruik maken om de agent te ontlopen.";
                    btnSolution1.Content = "Verstop je in de massa. Als de agent je even kwijt is kan je ontsnappen.";
                    btnSolution2.Content = "Sluip de school binnen. Daar kan je wachten tot de kust veilig is.";
                    break;
                case 2: //school search

                    lblTitle.Content = "Opsporing aan de School";
                    tbScenario.Text = "Je hebt de politie even afgeschud, maar als je geen verstopplaats vindt zullen ze je gauw te pakken hebben.\n Je weet dat net om de hoek de ingang van de school is, die nu verlaten zou moeten zijn. Je weet ook dat de steegjes voorbij de school een echt labyrint zijn.";
                    btnSolution1.Content = "Kruip over het schoolhek. Daar kan je je wel enkele uren verschansen.";
                    btnSolution2.Content = "Loop de steegjes in. De politie zal je daar nooit te pakken krijgen.";
                    break;
                case 3: //park chase

                    lblTitle.Content = "Achtervolging in het Park";
                    tbScenario.Text = "Met een agent die jou op de hielen zit ren je het park in.\n De vele bomen en struiken zouden je kans kunnen zijn om te ontsnappen.";
                    btnSolution1.Content = "Ren door het struikgewas. Zo zal je de agent wel kwijtraken.";
                    btnSolution2.Content = "Gooi een steen naar een groep duiven. Dan zal de agent wel even bezighouden";
                    break;
                case 4: //park search

                    lblTitle.Content = "Opsporing in het Park";
                    tbScenario.Text = "Je komt aan in het park, maar de politie zal hier snel zijn.\n De vele paden, begroeiing en zelf de parkgangers zijn mogelijkheden om definitief aan je achtervolgers te ontkomen.";
                    btnSolution1.Content = "Kruip in een boom. Daar gaan ze toch niet zoeken.";
                    btnSolution2.Content = "Er is toevallig een protestactie! Ideaal om in de massa te verdwijnen.";
                    break;
                case 5: //square chase

                    lblTitle.Content = "Achtervolging op het Plein";
                    tbScenario.Text = "Je stormt het plein op met een agent in volle achtervolging.\n Misschien kunnen de brasseries of de winkels je een kans geven om de agent te ontlopen.";
                    btnSolution1.Content = "Loop door het terras van een brasserie en gooi wat tafels en stoelen omver.";
                    btnSolution2.Content = "Duik het winkelcentrum in en verdwijn tussen de winkelaars";
                    break;
                case 6: //square search

                    lblTitle.Content = "Opsporing op het Plein";
                    tbScenario.Text = "Je bent aan de rand van het plein, en je weet dat je weinig tijd hebt.\n Er zijn verschillende winkels en brasseries om je in te verschuilen, of misschien moet je nog verder lopen.";
                    btnSolution1.Content = "Tijd om aan de toog te hangen! Hiding in plain sight noemen ze dat.";
                    btnSolution2.Content = "Beter nog wat lopen. Als je het plein af bent voor de politie er is zal je ze wel kwijtspelen.";
                    break;
                case 7: //narrow streets chase
                    Background = Brushes.DarkGray;
                    lblTitle.Content = "Achtervolging in de Steegjes";
                    tbScenario.Text = "De agent achtervolgt je tot in een nauw steegje.\n In deze donkere locatie zou je toch op een of andere manier moeten kunnen ontsnappen.";
                    btnSolution1.Content = "Probeer de agent af te schudden in de wirwar van straatjes.";
                    btnSolution2.Content = "Spring je in de vuilniscontainer. Zo is zelfs je geur gemaskeerd!";
                    break;
                case 8: //narrow streets search
                    Background = Brushes.DarkGray;
                    lblTitle.Content = "Opsporing in de Steegjes";
                    tbScenario.Text = "Je duikt een steegje in, en je hebt nog wat tijd voor de politie hier zal komen.\nToch zal je snel iets moeten verzinnen om de lange arm van de wet te ontlopen.";
                    btnSolution1.Content = "Kruip in de riolen via een groot putdeksel. Met wat geluk kan je zo helemaal naar huis lopen";
                    btnSolution2.Content = "Maak een barricade met vuilbakken en andere troep. Tegen dat ze daar voorbij zijn ben je zeker ontsnapt.";
                    break;
            }
        }

        private void btnSolution1_Click(object sender, RoutedEventArgs e)
        {
            int currentRandom = rng.Next(100);
            currentRandom -= GameManager.Instance().MyCharacter.Stress/10;
            switch (scenario)
            {
                case 1: // school chase in the masses
                    if (currentRandom < 40)
                    {
                        tbScenario.Text = "Je duikt de mensenmassa in, maar het hindert je meer dan het helpt.\n" +
                            "De agent heeft je gauw te pakken, en hij begeleidt je naar het politiekantoor.";

                        goodEnding = false;
                    }
                    else
                    {
                        tbScenario.Text = "Je verdwijnt in de massa, en het geroep van de agent wordt gedempt door joelende kinderen.\n" +
                            "Je ontsnapt, en beslist dat het maar tijd is om naar huis te keren met je buit.";
                        goodEnding = (true);
                    }
                    break;
                case 2: // school search jump school fence
                    if (currentRandom < 60)
                    {
                        tbScenario.Text = "Je doet je best om het hek over te klimmen, maar het gaat moeizamer dan gedacht.\n" +
                            "Opeens zie je meerdere agenten achter je, en je beseft dat je ontsnapping gefaald heeft.";
                        goodEnding = (false);
                    }
                    else
                    {
                        tbScenario.Text = "Met een elegante sprong en wat geklauter dring je de lege school binnen.\n" +
                            "Na je snel verstopt te hebben in een gang, hoor je de politie langslopen. Je bent ontsnapt.";
                        goodEnding=(true);
                    }
                    break;
                case 3: // park chase bushes
                    if (currentRandom < 40)
                    {
                        tbScenario.Text = "Je duikt vol vertrouwen de struiken in, maar je ondervindt dat ook jij gehinderd wordt.\n" +
                            "Door die vertraging is je voorsprong snel verdwenen, en de agent slaat je snel in de boeien.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je kent dit park door en door, en je weet perfect welke struiken je door kan en welke niet.\n" +
                            "Al gauw hoor je het gevloek van de agent stiller worden, en je kan simpelweg het park uitwandelen.";
                        goodEnding=(true);
                    }
                    break;
                case 4: // park search tree
                    if (currentRandom < 60)
                    {
                        tbScenario.Text = "Je klimt de boom moeizaam in, maar de politie heeft je gezien. Je zit helemaal vast.\n" +
                            "Hoe komisch het ook is om de agenten te zien klimmen, je weet dat dit niet goed afloopt voor jou.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je weet direct welke boom je wilt beklimmen, en je klimt vlot naar boven tot je helemaal verborgen bent.\n" +
                            "Je amuseert je een tijdje door de politiezoektocht te volgen, en dan ga je op je gemakje naar huis.";
                        goodEnding=(true);
                    }
                    break;
                case 5: // square chase terrace
                    if (currentRandom < 80)
                    {
                        tbScenario.Text = "Je loopt langs een terrasje en duwt wat tafels en stoelen omver, maar de agent omzeilt de obstakels moeiteloos.\n" +
                            "De extra inspanning heeft je uitgeput, en al snel merk je dat de agent je inhaalt.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je laat je even inlopen aan een terrasje, en dan duw je tafels en stoelen naar de agent.\n" +
                            "De agent kan ze niet meer ontwijken, en het geluid en gevloek laat weinig aan de verbeelding over.";
                        goodEnding=(true);
                    }
                    break;
                case 6: // square seach tavern
                    if (currentRandom < 20)
                    {
                        tbScenario.Text = "Je stormt de brasserie binnen, en probeert natuurlijk over te komen, maar het pakt niet.\n" +
                            "Als de politie dan even later binnenkomt, wordt je snel verklikt door het personeel.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je wandelt de brasserie binnen, geeft de ober een fooi en een knipoog en neemt plaats.\n" +
                            "Als de politie even later binnenkomt, informeert de ober ze dat er niemand is binnengekomen.";
                        goodEnding=(true);
                    }
                    break;
                case 7: // alley chase run
                    if (currentRandom < 40)
                    {
                        tbScenario.Text = "Je spurt door de vele nauwe straatjes, hopende dat je de agent kan verwarren en afschudden.\n" +
                            "Maar hij blijft je bij, en de vermoeidheid wordt je snel te veel. Een spurtje van de agent en je bent gevat.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je kent deze steegjes door en door, en al snel merk je dat je de agent aan het verliezen bent.\n" +
                            "Je maakt er je met een spurtje van af en keert op je gemakje naar huis terug, terwijl de agent tevergeefs verder zoekt.";
                        goodEnding=(true);
                    }
                    break;
                case 8: // alley search sewers
                    if (currentRandom < 20)
                    {
                        tbScenario.Text = "Je probeert het riooldeksel op te heffen, maar het is zwaar en moeilijk zonder iets van gereedschap.\n" +
                            "Je ziet meerdere agenten verschijnen terwijl je nog aan het sleuren bent, en je weet dat je kans verkeken is.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je opent een rond putdeskel en daalt af in de riolen. Daar loop je snel verder om het deksel achter je te laten.\n" +
                            "Zelfs als ze in de riolen komen zoeken, dan ben je al lang verdwenen. Je zal wel een douche moeten nemen.";
                        goodEnding=(true);
                    }
                    break;
            }
            tbScenario.Text += "\nDruk op een toets om verder te gaan.";
            btnSolution1.Visibility = Visibility.Hidden;
            btnSolution2.Visibility = Visibility.Hidden;
            pathChosen = true;
        }

        private void btnSolution2_Click(object sender, RoutedEventArgs e)
        {
            int currentRandom = rng.Next(101);
            currentRandom -= GameManager.Instance().MyCharacter.Stress;
            switch (scenario)
            {
                case 1: // school chase hide in school
                    if (currentRandom < 80)
                    {
                        tbScenario.Text = "Je probeert door de schoolpoort te glippen, maar de toezichter spreekt je aan en houdt je tegen.\n" +
                            "Als er zich dan nog wat ouders mee moeien, zit je klem en kan de agent je simpelweg arresteren.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je ziet dat de toezichter afgeleid is door een enthousiaste ouder, en wandelt de speelplaats op.\n" +
                            "Na enige tijd wandel je simpelweg weer naar buiten, en is er geen agent meer te zien.";
                        goodEnding=(true);
                    }
                    break;
                case 2: // school search run in alley
                    if (currentRandom < 40)
                    {
                        tbScenario.Text = "Je loopt het straatje achter de school in, hopende dat dat de politie je niet direct zal volgen.\n" +
                            "Helaas hoor je gauw meerdere voetstappen achter je, en na een korte klopjacht nemen ze je gevangen.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Met de vele kinderen en ouders als afleiding sluip je het steegje in zonder veel aandacht te trekken\n" +
                            "Van daaruit keer je terug naar huis via een grote omweg, en je komt geen politie meer tegen.";
                        goodEnding=(true);
                    }
                    break;
                case 3: // park chase birds
                    if (currentRandom < 80)
                    {
                        tbScenario.Text = "Je gooit een steen naar een groep duiven, maar de vogels vliegen gewoon weg.\n" +
                            "Met je aandacht afgeleid, slaagt de agent erin om je te grijpen en tegen de grond te werken.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je loopt in een boogje rond de vogels en gooit een steen, waardoor ze recht naar de agent vliegen.\n" +
                            "De agent wordt overrompeld door een massa veren, en tegen dat hij ervan bekomen is ben je verdwenen.";
                        goodEnding=(true);
                    }
                    break;
                case 4: // park search protests
                    if (currentRandom < 20)
                    {
                        tbScenario.Text = "Het klonk als een goed idee om je tussen de actievoerders te verstoppen.\n" +
                            "Helaas worden alle herrieschoppers opgepakt, en kom je zo toch in de cel terecht.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "De grote massa is een ideale situatie om je achtervolgers af te schudden.\n" +
                            "Eens je terug thuis bent kan je zelf zeggen dan je sociaal geëngageerd bent.";
                        goodEnding=(true);
                    }
                    break;
                case 5: // square chase mall
                    if (currentRandom < 20)
                    {
                        tbScenario.Text = "Je stormt het winkelcentrum binnen, maar helaas loop je direct een winkelaar omver.\n" +
                            "Dit trekt veel aandacht, en even later valt er niet meer te ontsnappen aan de politie.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Het winkelcentrum heeft oneindige mogelijkheden om je te verstoppen of weg te lopen.\n" +
                            "Terwijl je een oogje houd op de agenten, kan je nog een beetje shoppen.";
                        goodEnding=(true);
                    }
                    break;
                case 6: // square search run
                    if (currentRandom < 60)
                    {
                        tbScenario.Text = "Je sprint het plein over, maar je merkt dat de vele terrasgangers je vreemd bekijken.\n" +
                            "Het is dan ook normaal dat ze de politie helpen, waardoor je even later gevat wordt.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Je doet alsof je een jogger bent, en loopt naar de ander kant van het plein.\n" +
                            "Je vermoedt dat je de politie bent kwijtgeraakt, want op de terugweg zie je ze niet.";
                        goodEnding=(true);
                    }
                    break;
                case 7: // alley chase garbage
                    if (currentRandom < 40)
                    {
                        tbScenario.Text = "Je ziet een grote groene metalen vuilniscontainer en zonder nadenken spring je erin.\n" +
                            "Tot jouw ongenoegen blijkt het een glasbak te zijn, en je gekerm geeft je positie weg.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Na wat vuilbakken te bekijken, verstop je je in een restafvalcontainer zonder veel scherp afval.\n" +
                            "Vanuit je onaangename schuilplaats hoor je de agent toekomen en weer vertrekken.";
                        goodEnding=(true);
                    }
                    break;
                case 8: // alley search barricade
                    if (currentRandom < 60)
                    {
                        tbScenario.Text = "Je begint snel wat zakken en dozen op elkaar te stapelen, maar het lijkt weinig stabiliteit te hebben.\n" +
                            "Als de politie toekomt, lijk je meer op een vuilnisman. Het lijkt ze niet te overtuigen.";
                        goodEnding=(false);
                    }
                    else
                    {
                        tbScenario.Text = "Met enkele containers, wat oude palletten en vuilzakken maak je vrij snel een solide barriere.\n" +
                            "Het laatste dat je hoort voor je de scene verlaat is het gezucht van agenten die het allemaal moeten opruimen.";
                        goodEnding=(true);
                    }
                    break;
            }
            tbScenario.Text += "\nDruk op een toets om verder te gaan.";
            btnSolution1.Visibility = Visibility.Hidden;
            btnSolution2.Visibility = Visibility.Hidden;
            pathChosen = true;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (pathChosen)
            {
                this.DialogResult = goodEnding;
            }
        }


    }
}
