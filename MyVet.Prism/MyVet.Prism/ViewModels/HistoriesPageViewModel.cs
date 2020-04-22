﻿using MyVet.Common.Helpers;
using MyVet.Common.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyVet.Prism.ViewModels
{
    public class HistoriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ObservableCollection<HistoryItemViewModel> _histories;
        private PetResponse _pet;

        public HistoriesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Histories";
            Pet = JsonConvert.DeserializeObject<PetResponse>(Settings.Pet);
            LoadHistories();

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

        private void LoadHistories()
        {
            Histories = new ObservableCollection<HistoryItemViewModel>(Pet.Histories.Select(h => new HistoryItemViewModel(_navigationService)
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