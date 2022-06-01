/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

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
    /// <summary>
    /// ViewModel of the TasksView and ToDoList Pages
    /// </summary>
    public class TasksViewModel : BaseViewModel
    {
        private TaskModel _selectedTask;
        private IPopupNavigation _popup { get; set; }
        private TaskMenuPopup _modalPage;
        private NewTaskPopup _newTaskPage;
        private FilterPopup _filterPage;
        public CategoryModel FilterCategory;
        public bool FilterOn = false;
        public bool ToDoListOn = false;
        public ObservableCollection<TaskModel> Tasks { get; }
        public Command LoadTasksCommand { get; }
        public Command RemoveFiltersCommand { get; }
        public Command AddTaskCommand { get; }
        public Command FilterCommand { get; }
        public Command<TaskModel> TaskTappedCommand { get; }

        /// <summary>
        /// Constructor, get values and set commands
        /// </summary>
        /// <param name="category"></param>
        /// <param name="filterViewModel"></param>
        public TasksViewModel()
        {
            Title = "Tâches";
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasksCommand = new Command(async () => ExecuteLoadTasksCommand());
            TaskTappedCommand = new Command<TaskModel>(OnTaskSelected);
            AddTaskCommand = new Command(ExecuteAddTaskCommand);
            FilterCommand = new Command(ExecuteFilterCommand);
            RemoveFiltersCommand = new Command(ExecuteRemoveFiltersCommand);
            _popup = PopupNavigation.Instance;
        }

        /// <summary>
        /// Load all tasks from database in the tasks list
        /// </summary>
        public void ExecuteLoadTasksCommand()
        {
            IsBusy = true;
            try
            {
                Tasks.Clear();
                ObservableCollection<TaskModel> tasks = App.Db.GetTasksAsync(true);

                // Get the right color according to the passing date
                foreach (TaskModel task in tasks)
                {
                    if (task.PassingDate.Day == DateTime.Now.Day)
                    {
                        task.BackColor = "OrangeRed";
                    }
                    else if (task.PassingDate < DateTime.Now)
                    {
                        task.BackColor = "Red";
                    }
                    else if (task.PassingDate < DateTime.Now + new TimeSpan(3, 0, 0, 0))
                    {
                        task.BackColor = "Orange";
                    }
                    else if (task.PassingDate < DateTime.Now + new TimeSpan(7, 0, 0, 0))
                    {
                        task.BackColor = "Goldenrod";
                    }
                    else if (task.PassingDate < DateTime.Now + new TimeSpan(30, 0, 0, 0))
                    {
                        task.BackColor = "LightGoldenrodYellow";
                    }
                    else
                    {
                        task.BackColor = "White";
                    }

                    if (task.Finished)
                    {
                        task.PassingDate = DateTime.MaxValue;
                        task.BackColor = "GreenYellow";
                    }
                    CategoryModel cat = App.Db.GetCategoryAsync(task.FkCategory).Result;
                    if (cat != null)
                    {
                        task.CatName = cat.Name;
                    }
                }

                // Add tasks in the list according to current filters / ToDoList
                tasks = new ObservableCollection<TaskModel>(tasks.OrderBy(i => i.PassingDate));
                if (FilterOn)
                {
                    if (ToDoListOn)
                    {
                        foreach (TaskModel task in tasks)
                        {
                            if (task.InToDoList && task.FkCategory == FilterCategory.Id)
                            {
                                Tasks.Add(task);
                            }
                        }
                    }
                    else
                    {
                        foreach (TaskModel task in tasks)
                        {
                            if (task.FkCategory == FilterCategory.Id)
                            {
                                Tasks.Add(task);
                            }
                        }
                    }
                }
                else
                {
                    if (ToDoListOn)
                    {
                        foreach (TaskModel task in tasks)
                        {
                            if (task.InToDoList)
                            {
                                Tasks.Add(task);
                            }
                        }
                    }
                    else
                    {
                        foreach (TaskModel task in tasks)
                        {
                            Tasks.Add(task);
                        }
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

        /// <summary>
        /// On appearing, set useful values for the program
        /// </summary>
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

        /// <summary>
        /// Open the NewTask Popup
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecuteAddTaskCommand(object obj)
        {
            _newTaskPage = new NewTaskPopup(this);
            await _popup.PushAsync(_newTaskPage, true);
        }

        /// <summary>
        /// Open the Filter Popup
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecuteFilterCommand(object obj)
        {
            _filterPage = new FilterPopup(this);
            await _popup.PushAsync(_filterPage, true);
        }

        /// <summary>
        /// Open the TaskMenu Popup
        /// </summary>
        /// <param name="task"></param>
        private async void OnTaskSelected(TaskModel task)
        {
            if (task == null)
            {
                return;
            }

            _modalPage = new TaskMenuPopup(task, this);
            await _popup.PushAsync(_modalPage, true);
        }

        /// <summary>
        /// Remove filter and load again
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteRemoveFiltersCommand(object obj)
        {
            FilterOn = false;
            ExecuteLoadTasksCommand();
        }
    }
}