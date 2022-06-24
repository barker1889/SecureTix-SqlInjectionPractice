using Dapper;
using SecureTixWeb.DataAccess.Utils;

namespace SecureTixWeb.DataAccess
{
    public interface IUserRepo
    {
        Task<UserModel> TryResolveUser(string username, string passwordMd5);
        Task<UserModel> GetById(int userId);
        Task CreateUser(UserModel userModel, string passwordMd5);
        UserModel GetByUsername(string emailAddress);
    }

    public class UserRepo : IUserRepo
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public UserRepo(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<UserModel> TryResolveUser(string username, string passwordMd5)
        {
            var sql = $@"
SELECT [UserId],[UserName], [Password], [Role] 
    FROM [Users] 
    WHERE [UserName] = '{username}' AND [Password] = '{passwordMd5}'";

            using (var con = _dbConnectionFactory.New())
            {
                return con.QueryFirstOrDefault<UserModel>(sql);
            }
        }

        public async Task<UserModel> GetById(int userId)
        {
            var sql = @"
SELECT [UserId],[UserName], [Password], [Role] 
    FROM [Users] 
    WHERE [UserId] = @userId";

            using (var con = _dbConnectionFactory.New())
            {
                return con.QueryFirstOrDefault<UserModel>(sql, new {userId});
            }
        }

        public async Task CreateUser(UserModel userModel, string passwordMd5)
        {
            var role = string.IsNullOrEmpty(userModel.Role)
                ? "Standard"
                : userModel.Role;

            var sql = @"
INSERT INTO [dbo].[Users] ([UserName], [Password], [Role])
     VALUES (@username, @passwordMd5, @role)
";

            using (var con = _dbConnectionFactory.New())
            {
                var userId = await con.ExecuteAsync(sql, new { userModel.Username, passwordMd5, role });
                userModel.UserId = userId;
            }
        }

        public UserModel GetByUsername(string emailAddress)
        {
            var sql = @"
SELECT [UserId],[UserName], [Password], [Role] 
    FROM [Users] 
    WHERE [UserName] = @emailAddress";

            using (var con = _dbConnectionFactory.New())
            {
                return con.QueryFirstOrDefault<UserModel>(sql, new { emailAddress });
            }
        }
    }

    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
