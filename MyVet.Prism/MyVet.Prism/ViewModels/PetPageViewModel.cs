using System.Collections.ObjectModel;
using System.Linq;
using MyVet.Common.Models;
using Prism.Navigation;

namespace MyVet.Prism.ViewModels
{
    public class PetPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PetResponse _pet;
        private ObservableCollection<HistoryItemViewModel> _histories;

        public PetPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        public PetResponse Pet
        {
            get => _pet;
            set => SetProperty(ref _pet, value);
        }

        public ObservableCollection<HistoryItemViewModel> Histories
        {
            get => _histories;
            set => SetProperty(ref _histories, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("pet"))
            {
                Pet = parameters.GetValue<PetResponse>("pet");
                Title = Pet.Name;
                Histories = new ObservableCollection<HistoryItemViewModel>(Pet.Histories.Select(
                    h => new HistoryItemViewModel(_navigationService)
                {
                    Date = h.Date,
                    Description = h.Description,
                    Id = h.Id,
                    Remarks = h.Remarks,
                    ServiceType = h.ServiceType
                }).ToList());
            }
        }
    }
}

