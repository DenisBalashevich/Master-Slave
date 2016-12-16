using DAL.DTO;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// User validation
    /// </summary>
    public class UserValidator : IUserValidation
    {
        /// <summary>
        /// Method for valid user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsValid(DalUser user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
                return false;
            return true;
        }
    }
}
