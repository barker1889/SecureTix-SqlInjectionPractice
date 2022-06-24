using Dapper;
using SecureTixWeb.DataAccess.Models;
using SecureTixWeb.DataAccess.Utils;

namespace SecureTixWeb.DataAccess
{
    public interface IEventsRepo
    {
        Task<IEnumerable<EventDataModel>> List(string searchTerm);
        Task<EventDataModel> Get(int eventId);
    }

    public class EventsRepo : IEventsRepo
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public EventsRepo(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<EventDataModel>> List(string searchTerm)
        {
            var sql = @"
SELECT [Id], [Name], [Description], [Price], [SoldOut], [OnSale]
    FROM [Events]
    WHERE [OnSale] = 1
";

            if (!string.IsNullOrEmpty(searchTerm))
            {
                sql += " AND [Name] LIKE '%" + searchTerm + "%'";
            }

            using (var con = _dbConnectionFactory.New())
            {
                return await con.QueryAsync<EventDataModel>(sql);
            }
        }

        public async Task<EventDataModel> Get(int id)
        {
            var sql = @"
SELECT [Id], [Name], [Description], [Price], [SoldOut], [OnSale]
    FROM [Events]
    WHERE [Id] = " + id;

            using (var con = _dbConnectionFactory.New())
            {
                return await con.QueryFirstOrDefaultAsync<EventDataModel>(sql);
            }
        }
    }
}
