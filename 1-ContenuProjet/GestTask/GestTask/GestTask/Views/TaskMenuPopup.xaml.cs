/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using GestTask.Models;
using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskMenuPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        TaskMenuViewModel _viewModel;
        /// <summary>
        /// Constructor, set the connection with the ViewModel and initialize components
        /// </summary>
        /// <param name="task"></param>
        /// <param name="tasksViewModel"></param>
        public TaskMenuPopup(TaskModel task, TasksViewModel tasksViewModel)
        {
            BindingContext = _viewModel = new TaskMenuViewModel(task, tasksViewModel);
            InitializeComponent();
        }
        /// <summary>
        /// On appearing, calls the OnAppearing method of the ViewModel
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}