using System;

namespace DAL.DTO
{
    /// <summary>
    ///     Gender of the user
    /// </summary>
    [Serializable]
    public enum DalGender
    {
        /// <summary>
        ///     Not assigned
        /// </summary>
        None,

        /// <summary>
        ///     Male
        /// </summary>
        Male,

        /// <summary>
        ///     Female
        /// </summary>
        Female
    }
}
