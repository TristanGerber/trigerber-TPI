/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using SQLite;

namespace GestTask.Models
{
    /// <summary>
    /// Category table class
    /// </summary>
    public class CategoryModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
