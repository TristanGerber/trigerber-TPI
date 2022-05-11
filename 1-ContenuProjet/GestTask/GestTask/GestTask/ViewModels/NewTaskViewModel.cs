using GestTask.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class NewTaskViewModel : BaseViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewTaskViewModel()
        {
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        private async Task ExecuteSaveCommand()
        {
            TaskModel task = new TaskModel();
            task.Id = 0;
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

        private async Task ExecuteCancelCommand()
        {
            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }
    }
}
