using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        NewTaskViewModel _viewModel;
        public FilterPopup()
        {
            BindingContext = _viewModel = new NewTaskViewModel();
            InitializeComponent();
        }
    }
}