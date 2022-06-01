/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using SQLite;
using System;

namespace GestTask.Models
{
    /// <summary>
    /// Task table class
    /// </summary>
    public class TaskModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Finished { get; set; }
        public DateTime PassingDate { get; set; }
        public bool InToDoList { get; set; }
        public int FkCategory { get; set; }
        public string CatName { get; set; }
        public string CatColor { get; set; }
        public string BackColor { get; set; }
    }
}