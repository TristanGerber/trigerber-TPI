﻿using GestTask.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class TaskMenuViewModel : BaseViewModel
    {
        private DateTime passingDate;
        private string name;
        private string description;
        private bool inToDoList;
        private bool active;
        private List<CategoryModel> categories;
        private CategoryModel selectedCategory;
        public DateTime PassingDate { get => passingDate; set => SetProperty(ref passingDate, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public bool InToDoList { get => inToDoList; set => SetProperty(ref inToDoList, value); }
        public bool Active { get => active; set => SetProperty(ref active, value); }
        public List<CategoryModel> Categories { get => categories; set => SetProperty(ref categories, value); }
        public CategoryModel SelectedCategory { get => selectedCategory; set => SetProperty(ref selectedCategory, value); }

        private TaskModel _task;
        private IPopupNavigation _popup { get; set; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command CancelCommand { get; }

        public TaskMenuViewModel(TaskModel task)
        {
            _task = task;
            passingDate = task.PassingDate;
            name = task.Name;
            description = task.Description;
            inToDoList = task.InToDoList;
            active = task.Active;
            categories = App.Db.GetCategoriesAsync().Result;
            selectedCategory = App.Db.GetCategoryAsync(task.FkCategory).Result;

            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            DeleteCommand = new Command(async () => await ExecuteDeleteCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        private async Task ExecuteDeleteCommand()
        {
            await App.Db.DeleteTaskAsync(_task);

            // Navigate backwards
            await _popup.PopAsync();
        }

        private async Task ExecuteSaveCommand()
        {
            _task.PassingDate = passingDate.Date;
            _task.Name = name;
            _task.Description = description;
            _task.InToDoList = inToDoList;
            _task.Active = active;
            _task.FkCategory = SelectedCategory.Id;

            if (!string.IsNullOrWhiteSpace(_task.Name))
            {
                await App.Db.SaveTaskAsync(_task);
            }

            // Navigate backwards
            await _popup.PopAsync();
        }

        private async Task ExecuteCancelCommand()
        {
            // Navigate backwards
            await _popup.PopAsync();
        }
        public Task<List<CategoryModel>> GetCategories()
        {
            return App.Db.GetCategoriesAsync();
        }
    }
}