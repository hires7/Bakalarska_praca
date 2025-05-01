using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca.UI.ViewModels
{
    public class MaterialsViewModel : BaseViewModel
    {
        private Material? _selectedMaterial;

        public ObservableCollection<Material> Materials { get; set; } = new();
        public bool IsAdmin => UserService.IsAdmin;


        public Material? SelectedMaterial
        {
            get => _selectedMaterial;
            set
            {
                _selectedMaterial = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanEditDelete));
                //Console.WriteLine($"SelectedMaterial zmenené: {(_selectedMaterial != null ? _selectedMaterial.Name : "null")}");
                //Console.WriteLine($"CanEditDelete: {CanEditDelete}");
                ((RelayCommand)EditMaterialCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteMaterialCommand).RaiseCanExecuteChanged();

            }
        }


        public bool CanEditDelete
        {
            get
            {
                bool result = SelectedMaterial != null;
                //Console.WriteLine($"CanEditDelete: {result}");
                return result;
            }
        }

        private bool _showInactive;
        public bool ShowInactive
        {
            get => _showInactive;
            set
            {
                _showInactive = value;
                OnPropertyChanged();
                LoadMaterials();
            }
        }



        public ICommand AddMaterialCommand { get; }
        public ICommand EditMaterialCommand { get; }
        public ICommand DeleteMaterialCommand { get; }

        public MaterialsViewModel()
        {
            LoadMaterials();
            AddMaterialCommand = new RelayCommand(ExecuteAddMaterial);
            EditMaterialCommand = new RelayCommand(ExecuteEditMaterial, _ => CanEditDelete);
            DeleteMaterialCommand = new RelayCommand(ExecuteDeleteMaterial, _ => CanEditDelete);
        }

        private void LoadMaterials()
        {
            Materials.Clear();
            var materials = MaterialService.GetAllMaterials(ShowInactive);
            foreach (var material in materials)
            {
                Materials.Add(material);
            }
        }


        private void ExecuteAddMaterial(object? parameter)
        {
            var addMaterialView = new AddMaterialView();
            if (addMaterialView.ShowDialog() == true)
            {
                LoadMaterials();
            }
        }

        private void ExecuteEditMaterial(object? parameter)
        {
            if (SelectedMaterial == null)
            {
                //Console.WriteLine("Žiadny materiál nie je vybraný!");
                return;
            }

            //Console.WriteLine($"Otváram úpravu materiálu: {SelectedMaterial.Name}");
            var editMaterialView = new EditMaterialView(SelectedMaterial);
            if (editMaterialView.ShowDialog() == true)
            {
                LoadMaterials();
            }
        }


        private void ExecuteDeleteMaterial(object? parameter)
        {
            if (SelectedMaterial == null)
            {
                //Console.WriteLine("Žiadny materiál nie je vybraný!");
                return;
            }

            var result = MessageBox.Show($"Naozaj chcete vymazať materiál {SelectedMaterial.Name}?",
                "Potvrdenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                //Console.WriteLine($"Mažem materiál: {SelectedMaterial.Name}");
                MaterialService.DeleteMaterial(SelectedMaterial.Id);
                LoadMaterials();
            }
        }

    }
}
