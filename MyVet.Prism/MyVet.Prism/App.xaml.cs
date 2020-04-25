﻿using Prism;
using Prism.Ioc;
using MyVet.Prism.ViewModels;
using MyVet.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyVet.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyVet.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.
            RegisterLicense("MjQzMzMzQDMxMzgyZTMxMmUzME5kQndSRGo4ejdWeFJOTlpFRWowNkNYUWFORklrM3NETEdYT0pnWHJnd1E9");
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PetsPage, PetsPageViewModel>();
            containerRegistry.RegisterForNavigation<PetPage, PetPageViewModel>();
            containerRegistry.RegisterForNavigation<HistoryPage, HistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<HistoriesPage, HistoriesPageViewModel>();
            containerRegistry.RegisterForNavigation<PetTabbedPage, PetTabbedPageViewModel>();
        }
    }
}
