﻿using MyVet.Common.Helpers;
using MyVet.Common.Models;
using MyVet.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace MyVet.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Login";
            IsEnabled = true;
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an email.", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a password.", "Accept");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnection(url);
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var response = await _apiService.GetTokenAsync(url, "/Account", "/CreateToken", request);

            if (!response.IsSuccess)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "User or password incorrect.", "Accept");
                Password = string.Empty;
                return;
            }

            var token = response.Result;

            var response2 = await _apiService.GetOwnerByEmailAsync(
                url,
                "/api",
                "/Owners/GetOwnerByEmail",
                "bearer",
                token.Token,
                Email);

            var owner = response2.Result;

            Settings.Owner = JsonConvert.SerializeObject(owner);
            //Settings.Owner = JsonConvert.SerializeObject(token);

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync("/VeterinaryMasterDetailPage/NavigationPage/PetsPage");
            //await _navigationService.NavigateAsync("PetsPage");
            Password = string.Empty;

        }

    }
}
