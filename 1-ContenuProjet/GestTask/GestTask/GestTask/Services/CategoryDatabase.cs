/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 09.05.2022 */

using GestTask.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestTask.Services
{
    public class CategoryDatabase
    {
        readonly SQLiteAsyncConnection database;

        public CategoryDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<CategoryModel>().Wait();
        }

        public Task<int> UpdateCategoryAsync(CategoryModel cat)
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

        public Task<List<CategoryModel>> GetCategoriesAsync(bool forceRefresh = false)
        {
            return database.Table<CategoryModel>().ToListAsync();
        }
    }
}