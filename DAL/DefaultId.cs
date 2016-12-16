using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Default id criterion
    /// </summary>
    public class DefaultId : IIdGenerator
    {
        /// <summary>
        /// Current id
        /// </summary>
        public int Current { get; private set; }
        /// <summary>
        /// Id generator
        /// </summary>
        /// <returns></returns>
        public int GenerateId()
        {
            return Current++;
        }
    }
}
