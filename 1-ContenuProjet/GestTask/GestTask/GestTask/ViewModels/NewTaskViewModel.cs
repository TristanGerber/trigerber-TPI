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
    public class NewTaskViewModel : BaseViewModel
    {
        private DateTime passingDate;
        private string name;
        private ObservableCollection<CategoryModel> categories;
        private CategoryModel selectedCategory;
        private string description;
        private bool inToDoList;
        private bool finished;
        private TasksViewModel _baseViewModel;
        private IPopupNavigation _popup { get; set; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public DateTime PassingDate { get => passingDate; set => SetProperty(ref passingDate, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public ObservableCollection<CategoryModel> Categories { get => categories; set => SetProperty(ref categories, value); }
        public CategoryModel SelectedCategory { get => selectedCategory; set => SetProperty(ref selectedCategory, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public bool InToDoList { get => inToDoList; set => SetProperty(ref inToDoList, value); }
        public bool Finished { get => finished; set => SetProperty(ref finished, value); }

        public NewTaskViewModel(TasksViewModel tasksViewModel)
        {
            _baseViewModel = tasksViewModel;
            _popup = PopupNavigation.Instance;
            PassingDate = DateTime.Now;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
            Categories = App.Db.GetCategoriesAsync();
        }

        private async Task ExecuteSaveCommand()
        {
            TaskModel task = new TaskModel();
            task.Id = 0;
            task.PassingDate = passingDate.Date;
            task.Name = name;
            task.Description = description;
            task.InToDoList = inToDoList;
            task.Finished = finished;
            if (task.Finished)
            {
                task.InToDoList = false;
            }
            if (selectedCategory != null)
            {
                task.FkCategory = selectedCategory.Id;
            }

            if (!string.IsNullOrWhiteSpace(task.Name))
            {
                await App.Db.SaveTaskAsync(task);
                _baseViewModel.ExecuteLoadTasksCommand();
                await _popup.PopAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erreur", "Veuillez remplir le nom de la tâche", "Retour");
            }


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
