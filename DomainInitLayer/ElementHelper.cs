using System;
using System.Net;


namespace DomainInitLayer
{
    [Serializable]
    public class ElementHelper
    {
        public string ServerType { get; set; }

        public string Path { get; set; }

        public IPEndPoint IpEndPoint { get; set; }
    }
}
