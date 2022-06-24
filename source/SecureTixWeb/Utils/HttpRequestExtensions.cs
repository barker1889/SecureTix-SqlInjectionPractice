namespace SecureTixWeb.Utils;

public static class HttpRequestExtensions
{
    public static bool TryGetSessionId(this HttpRequest request, out Guid? sessionId)
    {
        sessionId = null;
        if (!request.Cookies.ContainsKey("SessionId"))
        {
            return false;
        }

        sessionId = Guid.Parse(request.Cookies["SessionId"]);
        return true;
    }
}