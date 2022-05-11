using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTaskPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NewTaskPopup()
        {
            InitializeComponent();
        }
        async void OnSaveButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }
        async void OnCancelButtonClicked()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}