using System.Collections.ObjectModel;
using System.Linq;
using MyVet.Common.Helpers;
using MyVet.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;

namespace MyVet.Prism.ViewModels
{
    public class PetsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private OwnerResponse _owner;
        private TokenResponse _token;
        private ObservableCollection<PetItemViewModel> _pets;
        private bool _isRefreshing;

        public PetsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Pets";
            LoadPets();
        }

        public ObservableCollection<PetItemViewModel> Pets
        {
            get => _pets;
            set => SetProperty(ref _pets, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private void LoadPets()
        {
            IsRefreshing = true;

            //_token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            _owner = JsonConvert.DeserializeObject<OwnerResponse>(Settings.Owner);

            Pets = new ObservableCollection<PetItemViewModel>
                (_owner.Pets.Select(p => new PetItemViewModel(_navigationService)
            {
                Born = p.Born,
                Histories = p.Histories,
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                PetType = p.PetType,
                Race = p.Race,
                Remarks = p.Remarks
            }).ToList());

            IsRefreshing = false;
        }
    }
}