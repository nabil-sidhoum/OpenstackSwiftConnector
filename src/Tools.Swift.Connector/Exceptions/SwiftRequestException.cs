using System;

namespace Tools.Swift.Connector.Exceptions
{

    public class SwiftRequestException : Exception
    {
        public SwiftRequestException(string message) : base(message) { }
    }
}
