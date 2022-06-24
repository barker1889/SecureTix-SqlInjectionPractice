using Dapper;
using SecureTixWeb.DataAccess.Utils;

namespace SecureTixWeb.DataAccess
{
    public interface IMailingListRepo
    {
        Task Insert(string emailAddress);
    }

    public class MailingListRepo : IMailingListRepo
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public MailingListRepo(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task Insert(string emailAddress)
        {
            var sql = @"INSERT INTO [MailingList] ([EmailAddress]) VALUES ('" + emailAddress + "')";

            using (var con = _dbConnectionFactory.New())
            {
                await con.ExecuteAsync(sql);
            }
        }
    }
}
