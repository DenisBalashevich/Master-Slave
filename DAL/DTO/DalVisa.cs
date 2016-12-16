using System;

namespace DAL.DTO
{
    /// <summary>
    ///     Class for Visa card description
    /// </summary>
    [Serializable]
    public struct DalVisa
    {
        /// <summary>
        ///     Country of visa
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Start Date 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     End Date 
        /// </summary>
        public DateTime EndDate { get; set; }

        
    }
}
