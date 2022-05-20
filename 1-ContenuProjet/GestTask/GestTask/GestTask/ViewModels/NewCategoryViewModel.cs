using GestTask.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class NewCategoryViewModel : BaseViewModel
    {
        private string name;
        private IPopupNavigation _popup { get; set; }
        private FilterViewModel _baseModel;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public string Name { get => name; set => SetProperty(ref name, value); }

        public NewCategoryViewModel(FilterViewModel filterViewModel)
        {
            _baseModel = filterViewModel;
            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        private async Task ExecuteSaveCommand()
        {
            CategoryModel cat = new CategoryModel
            {
                Id = 0,
                Name = name
            };

            if (!string.IsNullOrWhiteSpace(cat.Name))
            {
                await App.Db.SaveCategoryAsync(cat);
            }
            // Navigate backwards
            _baseModel.ExecuteLoadCategoriesCommand();
            await _popup.PopAsync();
        }

        private async Task ExecuteCancelCommand()
        {
            // Navigate backwards
            await _popup.PopAsync();
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
