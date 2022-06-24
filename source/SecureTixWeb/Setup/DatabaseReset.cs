using System.Data.SqlClient;
using Dapper;

namespace SecureTixWeb.Setup
{
    public static class DatabaseReset
    {
        private const string MasterConnectionString = "Server=.\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        private const string DbConnectionString = "Server=.\\SQLEXPRESS;Database=StealThisDatabase;Trusted_Connection=True;";

        public static async Task Run()
        {
            try
            {
                var fileContents = await File.ReadAllTextAsync("Setup\\CreateDb.sql");

                using (var connection = new SqlConnection(MasterConnectionString))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(fileContents);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            try
            {
                var fileContents = await File.ReadAllTextAsync("Setup\\CreateTables.sql");

                using (var connection = new SqlConnection(DbConnectionString))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(fileContents);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            try
            {
                var fileContents = await File.ReadAllTextAsync("Setup\\CreateData.sql");

                using (var connection = new SqlConnection(DbConnectionString))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(fileContents);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
