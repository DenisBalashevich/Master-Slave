using System.Configuration;

namespace DomainInitLayer.Configuration
{
    public class Element : ConfigurationElement
    {
        /// <summary>
        ///     Property for slave service type
        /// </summary>
        [ConfigurationProperty("type", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string ServiceType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        /// <summary>
        /// Property for slave service name
        /// </summary>
        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }

        /// <summary>
        ///     Property for slave service ip address
        /// </summary>
        [ConfigurationProperty("ip", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string IpAddress
        {
            get { return (string)base["ip"]; }
            set { base["ip"] = value; }
        }

        /// <summary>
        ///     Property for slave service port
        /// </summary>
        [ConfigurationProperty("port", DefaultValue = 0, IsKey = false, IsRequired = false)]
        public int Port
        {
            get { return (int)base["port"]; }
            set { base["port"] = value; }
        }
    }
}
