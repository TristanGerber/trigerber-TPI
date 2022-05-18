using GestTask.Models;
using GestTask.ViewModels;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskMenuPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        TaskMenuViewModel _viewModel;
        public TaskMenuPopup(TaskModel task)
        {
            BindingContext = _viewModel = new TaskMenuViewModel(task);
            InitializeComponent();
        }
    }
}