using System;

namespace Tools.Swift.Connector.Authentication
{
    public class SwiftAuthenticationToken
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresOn { get; set; }
    }
}
