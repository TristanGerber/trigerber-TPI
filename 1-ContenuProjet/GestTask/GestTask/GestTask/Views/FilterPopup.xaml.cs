using GestTask.ViewModels;
using Xamarin.Forms;
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