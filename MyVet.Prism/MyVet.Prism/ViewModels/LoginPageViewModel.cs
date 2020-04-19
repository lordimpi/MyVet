﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyVet.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string _password;
        private bool _isRunning;
        private bool _isEneabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(
            INavigationService navigationService) : base(navigationService)
        {
            Title = "Login";
            IsEneable = true;
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

        public bool IsEneable
        {
            get => _isEneabled;
            set => SetProperty(ref _isEneabled, value);
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an email.", "Accep");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a password.", "Accept");
                return;
            }
            await App.Current.MainPage.DisplayAlert("Ok", "Fuck yeaaaah!!!.", "Accept");
        }

    }
}
