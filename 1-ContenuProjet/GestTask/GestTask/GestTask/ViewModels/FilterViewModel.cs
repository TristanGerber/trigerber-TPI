/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using GestTask.Models;
using GestTask.Views;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private CategoryModel _selectedCategory;
        private NewCategoryPopup _newCategoryPage;
        private EditCategoryPopup _editCategoryPage;
        private IPopupNavigation _popup { get; set; }
        private TasksViewModel _baseViewModel;
        public ObservableCollection<CategoryModel> Categories { get; }
        public Command LoadCategoriesCommand { get; }
        public Command AddCategoryCommand { get; }
        public Command DeleteCategoryCommand { get; }
        public Command EditCategoryCommand { get; }
        public Command<CategoryModel> CategoryTappedCommand { get; }

        public FilterViewModel(TasksViewModel tasksViewModel)
        {
            _baseViewModel = tasksViewModel;
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

        public void ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;

            try
            {
                Categories.Clear();
                ObservableCollection<CategoryModel> categories = App.Db.GetCategoriesAsync(true);
                categories = new ObservableCollection<CategoryModel>(categories.OrderBy(i => i.Name));
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

        private async void OnAddCategoryAsync()
        {
            await _popup.PushAsync(_newCategoryPage);
        }
        private async void ExecuteDeleteCategoryCommand(CategoryModel cat)
        {
            Categories.Remove(cat);
            await App.Db.DeleteCategoryAsync(cat);
        }


        private async void OnCategorySelected(CategoryModel category)
        {
            if (category == null)
                return;


            _baseViewModel.FilterCategory = category;
            _baseViewModel.FilterOn = true;
            _baseViewModel.ExecuteLoadTasksCommand();
            await _popup.PopAsync();
        }
        private async void ExecuteEditCategoryCommand(CategoryModel category)
        {
            if (category == null)
                return;

            _editCategoryPage = new EditCategoryPopup(category, this);
            await _popup.PushAsync(_editCategoryPage, true);
        }
    }
}