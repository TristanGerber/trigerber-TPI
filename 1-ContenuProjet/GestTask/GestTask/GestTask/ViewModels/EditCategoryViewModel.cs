/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 25.05.2022 */

using GestTask.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class EditCategoryViewModel : BaseViewModel
    {
        private string name;
        private CategoryModel _category;
        private IPopupNavigation _popup { get; set; }
        private FilterViewModel _baseModel;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public string Name { get => name; set => SetProperty(ref name, value); }

        public EditCategoryViewModel(CategoryModel category, FilterViewModel filterViewModel)
        {
            _baseModel = filterViewModel;
            _category = category;
            Name = category.Name;
            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        private async Task ExecuteSaveCommand()
        {
            _category.Name = name;
            if (!string.IsNullOrWhiteSpace(_category.Name))
            {
                await App.Db.SaveCategoryAsync(_category);
            }
            // Navigate backwards
            _baseModel.ExecuteLoadCategoriesCommand();
            _baseModel.BaseTasksViewModel.ExecuteLoadTasksCommand();
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
