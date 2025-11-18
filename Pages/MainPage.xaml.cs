using FlagGame.ViewModels;

namespace FlagGame.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }


    }

}
