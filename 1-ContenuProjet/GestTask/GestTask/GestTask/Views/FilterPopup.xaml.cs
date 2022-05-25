/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 25.05.2022 */

using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        FilterViewModel _viewModel;
        public FilterPopup(TasksViewModel tasksViewModel)
        {
            BindingContext = _viewModel = new FilterViewModel(tasksViewModel);
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void OnEditButtonClicked(object sender, System.EventArgs e)
        {

        }
    }
}