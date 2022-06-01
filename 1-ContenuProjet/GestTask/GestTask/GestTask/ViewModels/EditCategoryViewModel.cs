/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using GestTask.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    /// <summary>
    /// ViewModel of the EditCategory Popup
    /// </summary>
    public class EditCategoryViewModel : BaseViewModel
    {
        private string name;
        private CategoryModel _category;
        private IPopupNavigation _popup { get; set; }
        private FilterViewModel _baseModel;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public string Name { get => name; set => SetProperty(ref name, value); }

        /// <summary>
        /// Constructor, get values and set commands
        /// </summary>
        /// <param name="category"></param>
        /// <param name="filterViewModel"></param>
        public EditCategoryViewModel(CategoryModel category, FilterViewModel filterViewModel)
        {
            _baseModel = filterViewModel;
            _category = category;
            Name = category.Name;
            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        /// <summary>
        /// Save the changes of a category in database
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteSaveCommand()
        {
            _category.Name = name;
            if (!string.IsNullOrWhiteSpace(_category.Name))
            {
                await App.Db.SaveCategoryAsync(_category);
            }
            // Navigate backwards and reload the list
            _baseModel.ExecuteLoadCategoriesCommand();
            _baseModel.BaseTasksViewModel.ExecuteLoadTasksCommand();
            await _popup.PopAsync();
        }

        /// <summary>
        /// Go back to the categories list
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteCancelCommand()
        {
            // Navigate backwards
            await _popup.PopAsync();
        }

        /// <summary>
        /// On appearing, set useful values for the program
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}
