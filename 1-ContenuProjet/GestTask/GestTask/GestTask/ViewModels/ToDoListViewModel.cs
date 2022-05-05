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

namespace GestTask.ViewModels
{
    public class ToDoListViewModel : BaseViewModel
    {
        private TaskModel _selectedTask;

        public ObservableCollection<TaskModel> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public Command AddTaskCommand { get; }
        public Command<TaskModel> TaskTapped { get; }

        public ToDoListViewModel()
        {
            Title = "ToDoList";
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());
            TaskTapped = new Command<TaskModel>(OnTaskSelected);
            AddTaskCommand = new Command(OnAddTask);
        }

        async Task ExecuteLoadTasksCommand()
        {
            IsBusy = true;

            try
            {
                Tasks.Clear();
                IEnumerable<TaskModel> tasks = await DataStore.GetTasksAsync(true);
                foreach (TaskModel task in tasks)
                {
                    if (task.InToDoList)
                    {
                        Tasks.Add(task);
                    }
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
            // await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnTaskSelected(TaskModel task)
        {
            if (task == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            // await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(TaskDetailViewModel.ItemId)}={item.Id}");
        }
    }
}