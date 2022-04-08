using System;
using System.Windows;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for CopEscapeGame.xaml
    /// </summary>
    public partial class CopEscapeGame : Window
    {
        Random rng = new Random();
        int scenario;
        public CopEscapeGame()
        {
            InitializeComponent();
        }

        public CopEscapeGame(int LocationCaught) : base()
        {
            InitializeComponent();
            bool aggressiveCops = false;
            if (rng.Next(2) < 2)
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
                    break;
                default: //generic
                    break;
            }
            LoadScenario();
        }

        public void LoadScenario() // just pretend there are images ok i have no internet
        {
            switch (scenario)
            {
                case 1: //school chase
                    Background = Brushes.Gray;
                    lblLocation.Content = "school";
                    lblTitle.Content = "Achtervolging aan de School";
                    tbScenario.Text = "Je loopt zo snel als je kan, maar je hoort de agent roepen. Hij is niet ver achter jou. Je keert de hoek om en ziet een grote drukte voor de poort van de lokale school. Misschien kan je ervan gebruik maken om de agent te ontlopen.";
                    btnSolution1.Content = "Verstop je in de massa. Als de agent je even kwijt is kan je ontsnappen.";
                    btnSolution2.Content = "Sluip de school binnen. Daar kan je wachten tot de kust veilig is.";
                    break;
                case 2: //school search
                    Background = Brushes.Gray;
                    lblLocation.Content = "school";
                    lblTitle.Content = "Opsporing aan de School";
                    tbScenario.Text = "Je hebt de politie even afgeschud, maar als je geen verstopplaats vindt zullen ze je gauw te pakken hebben. Je weet dat net om de hoek de ingang van de school is, die nu verlaten zou moeten zijn. Je weet ook dat de steegjes voorbij de school een echt labyrint zijn.";
                    btnSolution1.Content = "Kruip over het schoolhek. Daar kan je je wel enkele uren verschansen.";
                    btnSolution2.Content = "Loop de steegjes in. De politie zal je daar nooit te pakken krijgen.";
                    break;
                case 3: //park chase
                    Background = Brushes.Green;
                    lblLocation.Content = "park";
                    lblTitle.Content = "Achtervolging in het Park";
                    tbScenario.Text = "Met een agent die jou op de hielen zit ren je het park in. De vele bomen en struiken zouden je kans kunnen zijn om te ontsnappen.";
                    btnSolution1.Content = "Ren door het struikgewas. Zo zal je de agent wel kwijtraken.";
                    btnSolution2.Content = "Gooi een steen naar een groep duiven. Dan zal de agent wel even bezighouden";
                    break;
                case 4: //park search
                    Background = Brushes.Green;
                    lblLocation.Content = "park";
                    lblTitle.Content = "Opsporing in het Park";
                    tbScenario.Text = "Je komt aan in het park, maar de politie zal hier snel zijn. De vele paden, begroeien en zelf de parkgangers zijn mogelijkheden om definitief aan je achtervolgers te ontkomen.";
                    btnSolution1.Content = "Kruip in een boom. Daar gaan ze toch niet zoeken.";
                    btnSolution2.Content = "Er is toevallig een protestactie! Ideaal om in de massa te verdwijnen.";
                    break;
                case 5: //square chase
                    Background = Brushes.LightGray;
                    lblLocation.Content = "square";
                    lblTitle.Content = "Achtervolging op het Plein";
                    tbScenario.Text = "Je stormt het plein met een agent in volle achtervolging. Misschien kunnen de brasseries of de winkels je een kans geven om de agent te ontlopen.";
                    btnSolution1.Content = "Loop door het terras van een brasserie en gooi wat tafels en stoelen omver.";
                    btnSolution2.Content = "Duik het winkelcentrum in en verdwijn tussen de winkelaars";
                    break;
                case 6: //square search
                    Background = Brushes.LightGray;
                    lblLocation.Content = "square";
                    lblTitle.Content = "Opsporing op het Plein";
                    tbScenario.Text = "Je bent aan de rand van het plein, en je weet dat je weinig tijd hebt. Er zijn verschillende winkels en brasseries om je in te verschuilen, of misschien moet je toch gewoon nog verder lopen.";
                    btnSolution1.Content = "Tijd om aan de toog te hangen! Hiding in plain sight noemen ze dat.";
                    btnSolution2.Content = "Beter nog wat lopen. Als je het plein af bent voor de politie er is zal je ze wel kwijtspelen.";
                    break;
                case 7: //narrow streets chase
                    Background = Brushes.DarkGray;
                    lblLocation.Content = "streets";
                    lblTitle.Content = "Achtervolging in de Steegjes";
                    tbScenario.Text = "";
                    btnSolution1.Content = "";
                    btnSolution2.Content = "";
                    break;
                case 8: //narrow streets search
                    Background = Brushes.DarkGray;
                    lblLocation.Content = "streets";
                    lblTitle.Content = "Opsporing in de Steegjes";
                    tbScenario.Text = "";
                    btnSolution1.Content = "";
                    btnSolution2.Content = "";
                    break;
            }
        }
    }
}
