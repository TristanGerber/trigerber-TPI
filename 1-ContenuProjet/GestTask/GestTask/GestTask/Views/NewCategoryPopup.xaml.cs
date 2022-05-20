using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCategoryPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        NewCategoryViewModel _viewModel;
        public NewCategoryPopup(FilterViewModel filterViewModel)
        {
            BindingContext = _viewModel = new NewCategoryViewModel(filterViewModel);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}