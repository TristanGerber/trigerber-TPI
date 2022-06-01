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
    /// ViewModel of the NewCategory Popup
    /// </summary>
    public class NewCategoryViewModel : BaseViewModel
    {
        private string name;
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
        public NewCategoryViewModel(FilterViewModel filterViewModel)
        {
            _baseModel = filterViewModel;
            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        /// <summary>
        /// Get values from the View and add the new category in database
        /// </summary>
        /// <returns></returns>
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
            // Navigate backwards and reload the list
            _baseModel.ExecuteLoadCategoriesCommand();
            _baseModel.BaseTasksViewModel.ExecuteLoadTasksCommand();
            await _popup.PopAsync();
        }

        /// <summary>
        /// Goes back to the categories page
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
