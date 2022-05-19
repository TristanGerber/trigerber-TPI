using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        FilterViewModel _viewModel;
        public FilterPopup()
        {
            BindingContext = _viewModel = new FilterViewModel();
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