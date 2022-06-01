/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using GestTask.Models;
using SQLite;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GestTask.Services
{
    /// <summary>
    /// Connects with database and execute different methods using database
    /// </summary>
    public class Database
    {
        readonly SQLiteAsyncConnection database;
        /// <summary>
        /// Constructor, connect with database and creates the two tables
        /// </summary>
        /// <param name="dbPath"></param>
        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TaskModel>().Wait();
            database.CreateTableAsync<CategoryModel>().Wait();
        }
        /// <summary>
        /// Save a task in database
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete a task from database
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Task<int> DeleteTaskAsync(TaskModel task)
        {
            return database.DeleteAsync(task);
        }
        /// <summary>
        /// Get a task from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TaskModel> GetTaskAsync(int id)
        {
            return database.Table<TaskModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Get all tasks from database
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public ObservableCollection<TaskModel> GetTasksAsync(bool forceRefresh = false)
        {
            return new ObservableCollection<TaskModel>(database.Table<TaskModel>().ToListAsync().Result);
        }
        /// <summary>
        /// Save a category in database
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Delete a category from database
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public Task<int> DeleteCategoryAsync(CategoryModel cat)
        {
            return database.DeleteAsync(cat);
        }
        /// <summary>
        /// Get a category from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<CategoryModel> GetCategoryAsync(int id)
        {
            return database.Table<CategoryModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Get all categories in database
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public ObservableCollection<CategoryModel> GetCategoriesAsync(bool forceRefresh = false)
        {
            return new ObservableCollection<CategoryModel>(database.Table<CategoryModel>().ToListAsync().Result);
        }
    }
}