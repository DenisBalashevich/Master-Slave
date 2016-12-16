using System;
using BLL.Interface;
using BLL.Models;

namespace BLL.SearchCriteria
{
    /// <summary>
    /// Last Name search criterion 
    /// </summary>
    [Serializable]
    public class LastNameCriterion : ISearchCriteria
    {
        private readonly string lastName;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="lastName"></param>
        public LastNameCriterion(string lastName)
        {
            this.lastName = lastName;
        }


        /// <summary>
        ///     Method search
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Search(BllUser user)
        {
            return user.LastName == lastName;
        }
    }
}
