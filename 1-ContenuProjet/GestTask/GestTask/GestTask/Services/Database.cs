/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using GestTask.Models;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GestTask.Services
{
    public class Database
    {
        readonly SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TaskModel>().Wait();
            database.CreateTableAsync<CategoryModel>().Wait();
        }

        public Task<int> SaveTaskAsync(TaskModel task)
        {
            if (task.Id != 0)
            {
                return database.UpdateAsync(task);
            }
            else
            {
                return database.InsertAsync(task);
            }
        }

        public Task<int> DeleteTaskAsync(TaskModel task)
        {
            return database.DeleteAsync(task);
        }

        public Task<TaskModel> GetTaskAsync(int id)
        {
            return database.Table<TaskModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public ObservableCollection<TaskModel> GetTasksAsync(bool forceRefresh = false)
        {
            return new ObservableCollection<TaskModel>(database.Table<TaskModel>().ToListAsync().Result);
        }
        public Task<int> SaveCategoryAsync(CategoryModel cat)
        {
            if (cat.Id != 0)
            {
                return database.UpdateAsync(cat);
            }
            else
            {
                return database.InsertAsync(cat);
            }
        }

        public Task<int> DeleteCategoryAsync(CategoryModel cat)
        {
            return database.DeleteAsync(cat);
        }

        public Task<CategoryModel> GetCategoryAsync(int id)
        {
            return database.Table<CategoryModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public ObservableCollection<CategoryModel> GetCategoriesAsync(bool forceRefresh = false)
        {
            return new ObservableCollection<CategoryModel>(database.Table<CategoryModel>().ToListAsync().Result);
        }
    }
}