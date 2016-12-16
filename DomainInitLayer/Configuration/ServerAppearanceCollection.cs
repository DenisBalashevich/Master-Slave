using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInitLayer.Configuration
{
    public class ServerAppearanceCollection : ConfigurationElementCollection
    {
        /// <summary>
        ///     Indexer for slave user services
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Element this[int idx] => (Element)BaseGet(idx);

        /// <summary>
        ///     Creates new SlaveServiceElement
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new Element();
        }

        /// <summary>
        ///     Gets SlaveServiceElement name
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Element)element).Path;
        }
    }
}
