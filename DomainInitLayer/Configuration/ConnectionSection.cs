using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInitLayer.Configuration
{
    public class ConnectionSection : ConfigurationSection
    {
        /// <summary>
        ///     Returns collection of MasterService
        /// </summary>
        [ConfigurationProperty("MasterService")]
        public ServerAppearanceCollection MasterServices => (ServerAppearanceCollection)base["MasterService"];

        /// <summary>
        ///     Gets ServiceConfigSection
        /// </summary>
        /// <returns></returns>
        public static ConnectionSection GetSection()
        {
            return (ConnectionSection)ConfigurationManager.GetSection("ConnectionSection");
        }
    }
}
