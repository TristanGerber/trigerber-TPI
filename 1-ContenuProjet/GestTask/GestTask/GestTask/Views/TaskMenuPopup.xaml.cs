/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 25.05.2022 */

using GestTask.Models;
using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskMenuPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        TaskMenuViewModel _viewModel;
        public TaskMenuPopup(TaskModel task, TasksViewModel tasksViewModel)
        {
            BindingContext = _viewModel = new TaskMenuViewModel(task, tasksViewModel);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}