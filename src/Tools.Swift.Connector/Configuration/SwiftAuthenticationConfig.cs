namespace Tools.Swift.Connector.Configuration
{
    public class SwiftAuthenticationConfig
    {
        public string Authurl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Catalog
        {
            get { return "swift"; }
        }
        public string Region { get; set; }
    }
}
