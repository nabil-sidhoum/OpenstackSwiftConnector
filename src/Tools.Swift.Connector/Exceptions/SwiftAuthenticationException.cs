using System;

namespace Tools.Swift.Connector.Exceptions
{

    public class SwiftAuthenticationException : Exception
    {
        public SwiftAuthenticationException(string message) : base(message) { }
    }
}
