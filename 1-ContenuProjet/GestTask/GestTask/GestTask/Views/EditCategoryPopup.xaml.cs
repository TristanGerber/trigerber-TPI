using GestTask.Models;
using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCategoryPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        EditCategoryViewModel _viewModel;
        public EditCategoryPopup(CategoryModel cat, FilterViewModel filterViewModel)
        {
            BindingContext = _viewModel = new EditCategoryViewModel(cat, filterViewModel);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}