using GestTask.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class NewTaskViewModel : BaseViewModel
    {
        private DateTime passingDate;
        private string name;
        private List<CategoryModel> categories;
        private CategoryModel selectedCategory;
        private string description;
        private bool inToDoList;
        private bool active;
        private IPopupNavigation _popup { get; set; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public DateTime PassingDate { get => passingDate; set => SetProperty(ref passingDate, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public List<CategoryModel> Categories { get => categories; set => SetProperty(ref categories, value); }
        public CategoryModel SelectedCategory { get => selectedCategory; set => SetProperty(ref selectedCategory, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public bool InToDoList { get => inToDoList; set => SetProperty(ref inToDoList, value); }
        public bool Active { get => active; set => SetProperty(ref active, value); }

        public NewTaskViewModel()
        {
            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
            Categories = App.Db.GetCategoriesAsync().Result;
        }

        private async Task ExecuteSaveCommand()
        {
            TaskModel task = new TaskModel();
            task.Id = 0;
            task.PassingDate = passingDate.Date;
            task.Name = name;
            task.Description = description;
            task.InToDoList = inToDoList;
            task.Active = active;
            task.FkCategory = selectedCategory.Id;

            if (!string.IsNullOrWhiteSpace(task.Name))
            {
                await App.Db.SaveTaskAsync(task);
            }
            // Navigate backwards
            await _popup.PopAsync();
        }

        private async Task ExecuteCancelCommand()
        {
            // Navigate backwards
            await _popup.PopAsync();
        }
    }
}
