/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 05.05.2022 */

namespace GestTask.Models
{
    public class TaskModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string PassingDate { get; set; }
        public bool InToDoList { get; set; }
        public bool FkCategory { get; set; }
    }
}