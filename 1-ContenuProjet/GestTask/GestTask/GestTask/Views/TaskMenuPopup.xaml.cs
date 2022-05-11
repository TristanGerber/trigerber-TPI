using GestTask.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskMenuPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public TaskMenuPopup()
        {
            InitializeComponent();
            LoadNote("0");
        }
        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                TaskModel task = await App.Db.GetTaskAsync(id);
                BindingContext = task;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)BindingContext;
            task.PassingDate = DateTime.UtcNow;
            task.Name = "test";
            task.InToDoList = true;
            task.Active = true;
            task.FkCategory = Convert.ToInt32("1");

            if (!string.IsNullOrWhiteSpace(task.Name))
            {
                await App.Db.SaveTaskAsync(task);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            TaskModel task = (TaskModel)BindingContext;
            await App.Db.DeleteTaskAsync(task);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}