/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaskPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        NewTaskViewModel _viewModel;
        /// <summary>
        /// Constructor, set the connection with the ViewModel and initialize components
        /// </summary>
        /// <param name="tasksViewModel"></param>
        public NewTaskPopup(TasksViewModel tasksViewModel)
        {
            BindingContext = _viewModel = new NewTaskViewModel(tasksViewModel);
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