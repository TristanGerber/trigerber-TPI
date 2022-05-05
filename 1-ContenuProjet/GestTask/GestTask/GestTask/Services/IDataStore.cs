/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestTask.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddTaskAsync(T task);
        Task<bool> UpdateTaskAsync(T task);
        Task<bool> DeleteTaskAsync(string id);
        Task<T> GetTaskAsync(string id);
        Task<IEnumerable<T>> GetTasksAsync(bool forceRefresh = false);
    }
}
