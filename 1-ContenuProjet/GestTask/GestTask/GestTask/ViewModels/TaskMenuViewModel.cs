using GestTask.Models;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestTask.ViewModels
{
    public class TaskMenuViewModel : BaseViewModel
    {
        private TaskModel _task;
        private IPopupNavigation _popup { get; set; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command CancelCommand { get; }

        public TaskMenuViewModel(TaskModel task)
        {
            _task = task;
            _popup = PopupNavigation.Instance;
            SaveCommand = new Command(async () => await ExecuteSaveCommand());
            DeleteCommand = new Command(async () => await ExecuteDeleteCommand());
            CancelCommand = new Command(async () => await ExecuteCancelCommand());
        }

        private async Task ExecuteDeleteCommand()
        {
            await App.Db.DeleteTaskAsync(_task);

            // Navigate backwards
            await _popup.PopAsync();
        }

        private async Task ExecuteSaveCommand()
        {
         
            _task.PassingDate = DateTime.UtcNow;
            _task.Name = "Modifié";

            if (!string.IsNullOrWhiteSpace(_task.Name))
            {
                await App.Db.SaveTaskAsync(_task);
            }

            // Navigate backwards
            await _popup.PopAsync();
        }

        private async Task ExecuteCancelCommand()
        {
            // Navigate backwards
            await _popup.PopAsync();
        }
    }
}