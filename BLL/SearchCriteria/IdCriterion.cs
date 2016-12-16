using System;
using BLL.Interface;
using BLL.Models;

namespace BLL.SearchCriteria
{
    /// <summary>
    /// Id search criterion 
    /// </summary>
    [Serializable]
    public class IdCriterion : ISearchCriteria
    {
        private readonly int id;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="id"></param>
        public IdCriterion(int id)
        {
            this.id = id;
        }

        /// <summary>
        ///     Method search
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Search(BllUser user)
        {
            return user.Id == id;
        }

    }
}