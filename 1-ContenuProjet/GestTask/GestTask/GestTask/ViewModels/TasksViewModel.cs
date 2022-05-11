/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using GestTask.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using GestTask.Views;
using Rg.Plugins.Popup.Events;

namespace GestTask.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private TaskModel _selectedTask;
        private IPopupNavigation _popup { get; set; }
        private TaskMenuPopup _modalPage;
        private NewTaskPopup _newTaskPage;
        public ObservableCollection<TaskModel> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public Command AddTaskCommand { get; }
        public Command<TaskModel> TaskTappedCommand { get; }

        public TasksViewModel()
        {
            Title = "Tâches";
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());
            TaskTappedCommand = new Command<TaskModel>(OnTaskSelected);
            AddTaskCommand = new Command(OnAddTask);
            _popup = PopupNavigation.Instance;
            _modalPage = new TaskMenuPopup();
            _newTaskPage = new NewTaskPopup();
        }

        async Task ExecuteLoadTasksCommand()
        {
            IsBusy = true;

            try
            {
                Tasks.Clear();
                IEnumerable<TaskModel> tasks = await App.Db.GetTasksAsync(true);
                foreach (TaskModel task in tasks)
                {
                    Tasks.Add(task);
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

        private void Popup_Popped(object sender, PopupNavigationEventArgs e)
        {
            throw new NotImplementedException();
        }

        public TaskModel SelectedTask
        {
            get => _selectedTask;
            set
            {
                SetProperty(ref _selectedTask, value);
                OnTaskSelected(value);
            }
        }

        private async void OnAddTask(object obj)
        {
            await _popup.PushAsync(_newTaskPage, true);
            //await Shell.Current.GoToAsync(nameof(NewTaskPopup));
        }

        async void OnTaskSelected(TaskModel task)
        {
            if (task == null)
                return;
            await _popup.PushAsync(_modalPage, true);
        }
    }
}