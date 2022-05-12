using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaskPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        NewTaskViewModel _viewModel;
        public NewTaskPopup()
        {
            BindingContext = _viewModel = new NewTaskViewModel();
            InitializeComponent();
        }
    }
}