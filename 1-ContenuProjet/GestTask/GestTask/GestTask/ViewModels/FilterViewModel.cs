/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using GestTask.Models;
using GestTask.Views;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    /// <summary>
    /// ViewModel of the Filter Popup
    /// </summary>
    public class FilterViewModel : BaseViewModel
    {
        private CategoryModel _selectedCategory;
        public NewCategoryPopup _newCategoryPage;
        private EditCategoryPopup _editCategoryPage;
        private IPopupNavigation _popup { get; set; }
        public TasksViewModel BaseTasksViewModel;
        public ObservableCollection<CategoryModel> Categories { get; }
        public Command LoadCategoriesCommand { get; }
        public Command AddCategoryCommand { get; }
        public Command DeleteCategoryCommand { get; }
        public Command EditCategoryCommand { get; }
        public Command<CategoryModel> CategoryTappedCommand { get; }

        /// <summary>
        /// Constructor, get values and set commands
        /// </summary>
        /// <param name="category"></param>
        /// <param name="filterViewModel"></param>
        public FilterViewModel(TasksViewModel tasksViewModel)
        {
            BaseTasksViewModel = tasksViewModel;
            Title = "Categories";
            Categories = new ObservableCollection<CategoryModel>();
            _popup = PopupNavigation.Instance;
            _newCategoryPage = new NewCategoryPopup(this);
            EditCategoryCommand = new Command<CategoryModel>(ExecuteEditCategoryCommand);
            LoadCategoriesCommand = new Command(ExecuteLoadCategoriesCommand);
            CategoryTappedCommand = new Command<CategoryModel>(OnCategorySelected);
            AddCategoryCommand = new Command(OnAddCategoryAsync);
            DeleteCategoryCommand = new Command<CategoryModel>(ExecuteDeleteCategoryCommand);
        }

        /// <summary>
        /// Load categories in the list from database
        /// </summary>
        public void ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;

            try
            {
                Categories.Clear();

                // Getting categories from database
                ObservableCollection<CategoryModel> categories = App.Db.GetCategoriesAsync(true);
                categories = new ObservableCollection<CategoryModel>(categories.OrderBy(i => i.Name));

                // Adding them in the list
                foreach (CategoryModel category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// On appearing, set useful values for the program
        /// </summary>
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedTask = null;
        }

        public CategoryModel SelectedTask
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                OnCategorySelected(value);
            }
        }

        /// <summary>
        /// Open the NewCategory Popup
        /// </summary>
        private async void OnAddCategoryAsync()
        {
            await _popup.PushAsync(_newCategoryPage);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="cat"></param>
        private async void ExecuteDeleteCategoryCommand(CategoryModel cat)
        {
            if (await App.Current.MainPage.DisplayAlert("Confirmation", "Êtes vous sur de vouloir supprimer ?", "Oui", "Non"))
            {
                Categories.Remove(cat);
                await App.Db.DeleteCategoryAsync(cat);
            }
        }

        /// <summary>
        /// Set the filter to the selected category
        /// </summary>
        /// <param name="category"></param>
        private async void OnCategorySelected(CategoryModel category)
        {
            if (category == null)
                return;


            BaseTasksViewModel.FilterCategory = category;
            BaseTasksViewModel.FilterOn = true;

            // Navigate backwards and reload the list
            BaseTasksViewModel.ExecuteLoadTasksCommand();
            await _popup.PopAsync();
        }

        /// <summary>
        /// Open the EditCategory Popup
        /// </summary>
        /// <param name="category"></param>
        private async void ExecuteEditCategoryCommand(CategoryModel category)
        {
            if (category == null)
                return;

            _editCategoryPage = new EditCategoryPopup(category, this);
            await _popup.PushAsync(_editCategoryPage, true);
        }
    }
}