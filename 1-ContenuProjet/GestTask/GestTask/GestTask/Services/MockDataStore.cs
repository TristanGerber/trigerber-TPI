/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using GestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestTask.Services
{
    public class MockDataStore : IDataStore<TaskModel>
    {
        private readonly List<TaskModel> tasks;

        public MockDataStore()
        {
            tasks = new List<TaskModel>()
            {
                new TaskModel { Id = Guid.NewGuid().ToString(), Name = "First item", InToDoList=true},
                new TaskModel { Id = Guid.NewGuid().ToString(), Name = "Second item", InToDoList=false},
                new TaskModel { Id = Guid.NewGuid().ToString(), Name = "Third item", InToDoList=false},
                new TaskModel { Id = Guid.NewGuid().ToString(), Name = "Fourth item", InToDoList=true},
                new TaskModel { Id = Guid.NewGuid().ToString(), Name = "Fifth item", InToDoList=false},
                new TaskModel { Id = Guid.NewGuid().ToString(), Name = "Sixth item", InToDoList=true}
            };
        }

        public async Task<bool> AddTaskAsync(TaskModel task)
        {
            tasks.Add(task);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateTaskAsync(TaskModel task)
        {
            TaskModel oldTask = tasks.FirstOrDefault((TaskModel arg) => arg.Id == task.Id);
            tasks.Remove(oldTask);
            tasks.Add(task);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            TaskModel oldTask = tasks.FirstOrDefault((TaskModel arg) => arg.Id == id);
            tasks.Remove(oldTask);

            return await Task.FromResult(true);
        }

        public async Task<TaskModel> GetTaskAsync(string id)
        {
            return await Task.FromResult(tasks.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<TaskModel>> GetTasksAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(tasks);
        }
    }
}