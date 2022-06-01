/* Developper : Tristan Gerber
 * Place : ETML, N501
 * Project creation date : 05.05.2022
 * Last updated : 01.06.2022 */

using GestTask.Services;
using System;
using System.IO;
using Xamarin.Forms;

namespace GestTask
{
    /// <summary>
    /// Main application
    /// </summary>
    public partial class App : Application
    {
        private static Database db;

        // Create the database connection as a singleton.
        public static Database Db
        {
            get
            {
                if (db == null)
                {
                    db = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TaskDb.db3"));
                }
                return db;
            }
        }
        /// <summary>
        /// Constructor, initialize components, Register the database for SQLite and set the AppShell
        /// </summary>
        public App()
        {
            InitializeComponent();

            DependencyService.Register<Database>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
