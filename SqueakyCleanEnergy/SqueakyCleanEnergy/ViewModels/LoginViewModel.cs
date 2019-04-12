using System;
using System.Windows.Input;
using SqueakyCleanEnergy.Services;
using SqueakyCleanEnergy.Models;
using SqueakyCleanEnergy.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SqueakyCleanEnergy.ViewModels
{
    class LoginViewModel: BaseViewModel
    {
        #region Properties Attributes and Services

        private readonly INavigation _navigation;
        private readonly ApiService _apiService = new ApiService();

        public ICommand LoginCommand { get; set; }
        public ICommand PrivacyPolicyCommand { get; set; }
        public ICommand ShareCommand { get; set; }
        public ICommand ClickCommand { get; set; }

        private string _privacyPoliceImage;

        public string PrivacyPoliceImage
        {
            get => _privacyPoliceImage;
            set { _privacyPoliceImage = value; OnPropertyChanged(); }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set { _isRunning = value; OnPropertyChanged(); }
        }

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion


        public LoginViewModel(INavigation navigation)
        {
            LoginCommand = new Command(Login);
            PrivacyPolicyCommand = new Command(PrivacyPolicy);
            ShareCommand = new Command(ShareApp);
            ClickCommand = new Command(Click);

            Preferences.Set("policyAccepted", true);
            PrivacyPoliceImage = "ic_check_box.png";
            IsEnabled = true;
            IsRunning = false;

            _navigation = navigation;
        }

        private async void ShareApp(object obj)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "Text to Share",
                Title = "Clean Energy!"
            });
        }

        private void Click()
        {
            Device.OpenUri(new Uri("https://www.squeaky.energy/privacy-policy"));
        }

        private void PrivacyPolicy()
        {
            var politicaEstado = Preferences.Get("policyAccepted", false);
            if (politicaEstado)
            {
                PrivacyPoliceImage = "ic_check_box_outline_blank.png";
                Preferences.Set("policyAccepted", false);
            }
            else
            {
                PrivacyPoliceImage = "ic_check_box.png";
                Preferences.Set("policyAccepted", true);
            }
        }

        private async void Login()
        {


            if (!Preferences.Get("policyAccepted", false))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "You MUST accept the privacy policy.", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Insert your Email.", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Insert your Password.", "Ok");
                return;
            }

            var request = new TokenRequest()
            {
                Email = Email,
                Password = Password
            };

            IsEnabled = false;
            IsRunning = true;

            var response = await _apiService.GetTokenAsync("/api", "/account/login", request);

            IsRunning = false;
            IsEnabled = true;

            //If authentication is succeeded
            if (response.IsSuccess)
            {
                var token = (TokenResponse) response.Result;

                // Token is stores in Preferences -> This works across Android, iOS and UWP
                Preferences.Set("Token", token.Token);

                Application.Current.MainPage = new NavigationPage(new HomePage());
                await _navigation.PopToRootAsync();
            }

            await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Ok");


        }
    }
}
