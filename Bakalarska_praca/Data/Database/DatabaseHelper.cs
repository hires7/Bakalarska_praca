using System.Data.SQLite;

namespace Bakalarska_praca.Data.Database
{
    class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=weighing.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }

}
