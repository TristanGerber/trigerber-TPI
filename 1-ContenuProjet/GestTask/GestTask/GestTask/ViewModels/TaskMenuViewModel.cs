/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 25.05.2022 */

using GestTask.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class TaskMenuViewModel : TasksViewModel
    {
        private DateTime passingDate;
        private string name;
        private string description;
        private bool inToDoList;
        private bool finished;
        private ObservableCollection<CategoryModel> categories;
        private CategoryModel selectedCategory;
        private TasksViewModel _baseViewModel;
        public DateTime PassingDate { get => passingDate; set => SetProperty(ref passingDate, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public bool InToDoList { get => inToDoList; set => SetProperty(ref inToDoList, value); }
        public bool Finished { get => finished; set => SetProperty(ref finished, value); }
        public ObservableCollection<CategoryModel> Categories { get => categories; set => SetProperty(ref categories, value); }
        public CategoryModel SelectedCategory { get => selectedCategory; set => SetProperty(ref selectedCategory, value); }

        private TaskModel _task;
        private IPopupNavigation _popup { get; set; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command CancelCommand { get; }

        public TaskMenuViewModel(TaskModel task, TasksViewModel tasksViewModel)
        {
            _baseViewModel = tasksViewModel;
            _task = task;
            PassingDate = task.PassingDate;
            Name = task.Name;
            Description = task.Description;
            InToDoList = task.InToDoList;
            Finished = task.Finished;
            Categories = App.Db.GetCategoriesAsync();
            SelectedCategory = App.Db.GetCategoryAsync(task.FkCategory).Result;

            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            DeleteCommand = new Command(async () => await ExecuteDeleteCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        private async Task ExecuteDeleteCommand()
        {
            if (await App.Current.MainPage.DisplayAlert("Confirmation", "Êtes vous sur de vouloir supprimer ?", "Oui", "Non"))
            {
                await App.Db.DeleteTaskAsync(_task);

                // Navigate backwards
                _baseViewModel.ExecuteLoadTasksCommand();
                await _popup.PopAsync();
            }
        }

        private async Task ExecuteSaveCommand()
        {
            _task.PassingDate = passingDate.Date;
            _task.Name = name;
            _task.Description = description;
            _task.InToDoList = inToDoList;
            _task.Finished = finished;
            if (_task.Finished)
            {
                _task.InToDoList = false;
            }
            if (selectedCategory != null)
            {
                _task.FkCategory = selectedCategory.Id;
            }

            if (!string.IsNullOrWhiteSpace(_task.Name))
            {
                await App.Db.SaveTaskAsync(_task);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erreur", "Veuillez remplir le nom de la tâche", "Retour");
            }

            // Navigate backwards
            _baseViewModel.ExecuteLoadTasksCommand();
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