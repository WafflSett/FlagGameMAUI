using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlagGame.Classes;
using FlagGame.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagGame.ViewModels
{
    [QueryProperty(nameof(SelectedRegion), "region")]
    public partial class GameViewModel : ObservableObject
    {
        private bool counting = false;
        private bool gotWrong = false;
        private HashSet<Country> usedCountries = new HashSet<Country>();
        [ObservableProperty]
        string? selectedRegion;
        [ObservableProperty]
        List<Country> countries = new List<Country>();
        [ObservableProperty]
        ObservableCollection<Country> selectedCountries = new ObservableCollection<Country>();
        [ObservableProperty]
        int secondsLeft;
        [ObservableProperty]
        Country correctAnswer;
        [ObservableProperty]
        int score = 0;
        [ObservableProperty]
        int question = 1;
        [RelayCommand]
        public async Task OptionClicked(Button tappedBtn) {
            await Task.Yield();
            if (tappedBtn.Text == CorrectAnswer.name)
            {
                counting = false;
                tappedBtn.BackgroundColor = Colors.Green;
                await Task.Delay(1500);
                tappedBtn.BackgroundColor = (Color)Application.Current.Resources["Secondary"];
                await EndOfRound();
                
            }
            else {
                gotWrong = true;
                tappedBtn.BackgroundColor = Colors.Red;
                await Task.Delay(1500);
                tappedBtn.BackgroundColor = (Color)Application.Current.Resources["Secondary"];
            }
        }

        private async Task EndOfRound() {
            if (!gotWrong)
            {
                Score++;
            }
            Question++;
            gotWrong = false;
            if (Question > 10)
            {
                if (await Shell.Current.DisplayAlert("End of game", $"Your score was: {Score}", "Play Again", "Quit"))
                {
                    ResetGame();
                }
                else
                {
                    await Shell.Current.GoToAsync("//MainPage");
                }
            }
            else
            {
                SelectFlag();
            }
        }

        private void ResetGame() {
            Score = 0;
            Question = 1;
            usedCountries.Clear();
            SelectFlag();
        }

        [RelayCommand]
        public void Appearing() {
            Task.Run(() => ResetGame());
        }

        partial void OnSelectedRegionChanged(string? value){
            if (value!=null)
            {
                Countries = App.Countries.Where(x => x.region.Contains(value)).ToList();
            }
            ResetGame();
        }

        private void SelectFlag() {
            Random r = new Random();
            List<Country> selectedCountries = new List<Country>();
            for (int i = 0; i < 4; i++)
            {
                int index = r.Next(Countries.Count);
                if (usedCountries.Contains(Countries[index]))
                {
                    i--;
                }
                else
                {
                    selectedCountries.Add(Countries[index]);
                    usedCountries.Add(Countries[index]);
                }
            }
            SelectedCountries = new(selectedCountries);
            CorrectAnswer = new(SelectedCountries[r.Next(4)]);
            if (!counting)
            {
                StartCountdown();
            }
            else {
                SecondsLeft = 15;
            }
        }

        private async Task StartCountdown() {
            counting = true;
            SecondsLeft = 15;
            while (SecondsLeft>0 && counting)
            {
                await Task.Delay(1000);
                SecondsLeft--;
            }
            counting = false;
            if (SecondsLeft <= 0)
            {
                gotWrong = true;
                await EndOfRound();
            }
        }
    }
}