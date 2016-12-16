using System;

namespace BLL.Models
{
    /// <summary>
    ///     Class for Visa 
    /// </summary>
    [Serializable]
    public class BllVisa
    {

        /// <summary>
        ///    Country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Start date visa
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     End date visa
        /// </summary>
        public DateTime EndDate { get; set; }

    }
}
