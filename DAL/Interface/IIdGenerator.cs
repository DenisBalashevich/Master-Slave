using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    /// <summary>
    /// Generator for id
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        /// Current id
        /// </summary>
        int Current { get; }
        /// <summary>
        /// Method for id generating
        /// </summary>
        /// <returns></returns>
        int GenerateId();
    }
}
