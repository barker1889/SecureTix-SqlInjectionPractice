using System.Data.SqlClient;

namespace SecureTixWeb.DataAccess.Utils
{
    public interface IDbConnectionFactory
    {
        SqlConnection New();
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private const string ConnectionString = "Server=.\\SQLEXPRESS;Database=StealThisDatabase;Trusted_Connection=True;";

        public SqlConnection New()
        {
            var con = new SqlConnection(ConnectionString);
            con.Open();

            return con;
        }
    }
}
