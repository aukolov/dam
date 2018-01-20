using System.IO;
using Dam.Infrastructure.DataAccess;

namespace Dam.UnitTests.Infrastructure.DataAccess
{
    public static class DataAccessTestHelper
    {
        public static void Initialize()
        {
            TryDeleteDatabase();
            var databaseInitializer = new DatabaseInitializer();
            databaseInitializer.Initialize();
        }

        public static void TryDeleteDatabase()
        {
            if (File.Exists(DataContext.DbFilePath))
            {
                File.Delete(DataContext.DbFilePath);
            }
        }
    }
}