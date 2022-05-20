using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaskPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        NewTaskViewModel _viewModel;
        public NewTaskPopup(TasksViewModel tasksViewModel)
        {
            BindingContext = _viewModel = new NewTaskViewModel(tasksViewModel);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}