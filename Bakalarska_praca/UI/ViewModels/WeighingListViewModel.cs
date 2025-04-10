using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using System;
using System.Collections.ObjectModel;

namespace Bakalarska_praca.UI.ViewModels
{
    public class WeighingListViewModel : BaseViewModel
    {
        public ObservableCollection<WeighingDisplayModel> Weighings { get; set; } = new();

        public WeighingListViewModel()
        {
            LoadTodaysWeighings();
        }

        private void LoadTodaysWeighings()
        {
            Weighings.Clear();
            var data = WeighingService.GetAllWeighings();

            foreach (var w in data)
            {
                Weighings.Add(new WeighingDisplayModel
                {
                    Id = w.Id,
                    IsIncoming = w.IsIncoming,
                    BruttoTime = w.BruttoTime,
                    Partner = PartnerService.GetPartnerById(w.Partner_Id)?.Name ?? "",
                    Truck = TruckService.GetTruckById(w.Truck_Id)?.LicensePlate ?? "",
                    User = UserService.GetUserById(w.User_Id)?.Username ?? "",
                    Material = MaterialService.GetMaterialById(w.Material_Id)?.Name ?? "",
                    Brutto = w.Brutto,
                    Tara = w.Tara,
                    Note = w.Note
                });

            }
        }
    }
}
