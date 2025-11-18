using FlagGame.ViewModels;

namespace FlagGame.Pages;

public partial class GamePage : ContentPage
{
	public GamePage(GameViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}