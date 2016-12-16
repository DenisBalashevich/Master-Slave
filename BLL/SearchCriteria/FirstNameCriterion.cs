using System;
using BLL.Interface;
using BLL.Models;

namespace BLL.SearchCriteria
{
    /// <summary>
    ///     First Name serarch Criterion
    /// </summary>
    [Serializable]
    public class FirstNameCriterion : ISearchCriteria 
    {
        private readonly string firstName;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="firstName"></param>
        public FirstNameCriterion(string firstName)
        {
            this.firstName = firstName;
        }

        /// <summary>
        ///     Method search
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Search(BllUser user)
        {
            return user.FirstName == firstName;
        }

    }
}