using SecureTixWeb.DataAccess;

namespace SecureTixWeb.Services
{
    public interface ISessionService
    {
        UserSession CreateNewUserSession(UserModel user);
        UserModel? ResolveUser(Guid sessionId);
    }

    public class SessionService : ISessionService
    {
        private readonly List<UserSession> _activeSessions = new();

        public UserSession CreateNewUserSession(UserModel user)
        {
            var session = new UserSession
            {
                SessionId = Guid.NewGuid(),
                User = user
            };

            _activeSessions.Add(session);
            return session;
        }

        public UserModel? ResolveUser(Guid sessionId)
        {
            return _activeSessions.FirstOrDefault(s => s.SessionId == sessionId)?.User;
        }
    }

    public class UserSession
    {
        public Guid SessionId { get; set; }
        public UserModel User { get; set; }
    }
}
