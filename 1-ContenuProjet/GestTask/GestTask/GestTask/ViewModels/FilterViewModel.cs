/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using GestTask.Models;
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
        public ObservableCollection<CategoryModel> Categories { get; }
        public Command LoadCategoriesCommand { get; }
        public Command AddCategoryCommand { get; }
        public Command DeleteCategoryCommand { get; }
        public Command<CategoryModel> CategoryTappedCommand { get; }

        public FilterViewModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<CategoryModel>();
            LoadCategoriesCommand = new Command(ExecuteLoadCategoriesCommand);
            CategoryTappedCommand = new Command<CategoryModel>(OnCategorySelected);
            AddCategoryCommand = new Command(OnAddCategory);
            DeleteCategoryCommand = new Command<CategoryModel>(ExecuteDeleteCategoryCommand);
        }

        private void ExecuteLoadCategoriesCommand()
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

        private void OnAddCategory(object obj)
        {
            CategoryModel newCategory = new CategoryModel();
            newCategory.Id = 0;
            newCategory.Name = "new";
            newCategory.Color = "new";
            Categories.Add(newCategory);
        }
        private async void ExecuteDeleteCategoryCommand(CategoryModel cat)
        {
            Categories.Remove(cat);
            await App.Db.DeleteCategoryAsync(cat);
        }


        async void OnCategorySelected(CategoryModel category)
        {
            if (category == null)
                return;
            
        }
        private async Task ExecuteAddCategory(string name)
        {
            CategoryModel cat = new CategoryModel();
            cat.Id = 0;
            cat.Name = name;
            cat.Color = "blue";
            if (!string.IsNullOrWhiteSpace(cat.Name))
            {
                await App.Db.SaveCategoryAsync(cat);
            }
        }
    }
}