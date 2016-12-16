using DAL.DTO;

namespace DAL.Interface
{
    /// <summary>
    ///     Interface for user validation
    /// </summary>
    public interface IUserValidation
    {
        /// <summary>
        ///     Validates user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsValid(DalUser user);
    }
}
