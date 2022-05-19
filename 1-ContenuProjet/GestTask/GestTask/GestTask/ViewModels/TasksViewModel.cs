/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using GestTask.Models;
using GestTask.Views;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Events;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private TaskModel _selectedTask;
        private IPopupNavigation _popup { get; set; }
        private TaskMenuPopup _modalPage;
        private readonly NewTaskPopup _newTaskPage;
        private readonly FilterPopup _filterPage;
        public ObservableCollection<TaskModel> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public Command AddTaskCommand { get; }
        public Command FilterCommand { get; }
        public Command<TaskModel> TaskTappedCommand { get; }

        public TasksViewModel()
        {
            Title = "Tâches";
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasksCommand = new Command(async () => ExecuteLoadTasksCommand());
            TaskTappedCommand = new Command<TaskModel>(OnTaskSelected);
            AddTaskCommand = new Command(OnAddTask);
            FilterCommand = new Command(OnFilter);
            _popup = PopupNavigation.Instance;
            _newTaskPage = new NewTaskPopup();
            _filterPage = new FilterPopup();
        }

        private void ExecuteLoadTasksCommand()
        {
            IsBusy = true;
            try
            {
                Tasks.Clear();
                ObservableCollection<TaskModel> tasks = App.Db.GetTasksAsync(true);
                tasks = new ObservableCollection<TaskModel>(tasks.OrderBy(i => i.PassingDate));
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
        }
        private async void OnFilter(object obj)
        {
            await _popup.PushAsync(_filterPage, true);
        }

        private async void OnTaskSelected(TaskModel task)
        {
            if (task == null)
            {
                return;
            }

            _modalPage = new TaskMenuPopup(task);
            await _popup.PushAsync(_modalPage, true);
        }
    }
}