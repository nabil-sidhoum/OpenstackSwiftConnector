using System.Net.Http;

namespace Tools.Swift.Connector.Authentication
{
    internal static class SwiftClientExtension
    {
        internal static HttpRequestMessage AddToken(this HttpRequestMessage request, string token)
        {
            request.Headers.Add("X-Auth-Token", token);
            return request;
        }
    }
}
