/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using GestTask.Models;
using GestTask.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksView : ContentPage
    {
        TasksViewModel _viewModel;
        public TasksView()
        {
            BindingContext = _viewModel = new TasksViewModel();
            InitializeComponent();
        }
        /// <summary>
        /// On appearing, calls the OnAppearing method of the ViewModel
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
            TasksListView.ItemsSource = await App.Db.GetTasksAsync();
        }
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                TaskModel task = (TaskModel)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(TaskMenuPopup)}"/*?{nameof(TaskMenuPopup.TaskId)}={task.Id}"*/);
            }
        }
    }
}