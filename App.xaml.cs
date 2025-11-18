using FlagGame.Classes;

namespace FlagGame
{
    public partial class App : Application
    {
        public static List<Country>? Countries { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            Countries = Task.Run(async ()=> await HttpCommunication<List<Country>>.Get("https://restcountries.com/v2/all?fields=flag,name,region")).Result;
        }

    }
}