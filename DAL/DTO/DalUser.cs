using System;
using System.Collections.Generic;
using DAL.Interface;
using System.Linq;

namespace DAL.DTO
{
    /// <summary>
    ///     User class in DAL
    /// </summary>
    [Serializable]
    public class DalUser : IEquatable<DalUser>, IEntity
    {

        /// <summary>
        ///     Constructor
        /// </summary>
        public DalUser()
        {
            VisaRecords = new List<DalVisa>();
        }

        /// <summary>
        ///     id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///    First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///      Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Birth Date
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public DalGender Gender { get; set; }

        /// <summary>
        ///     User's Visa cards
        /// </summary>
        public List<DalVisa> VisaRecords { get; set; }


        /// <summary>
        ///     Static Equals
        /// </summary>
        /// <param name="lhs">first user</param>
        /// <param name="rhs">second user</param>
        /// <returns>true, if objects are equal, otherwise false</returns>
        public static bool Equals(DalUser first, DalUser second)
        {
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return string.Equals(first.FirstName, second.FirstName, StringComparison.OrdinalIgnoreCase)
                && string.Equals(first.LastName, second.LastName, StringComparison.OrdinalIgnoreCase)
                 && (first.BirthDate == second.BirthDate) && (first.VisaRecords.SequenceEqual(second.VisaRecords)) &&
                (first.Gender == second.Gender);
        }

        /// <summary>
        ///     Instance Equals
        /// </summary>
        /// <param name="other">other user</param>
        /// <returns>true, if objects are equal, otherwise false</returns>
        public bool Equals(DalUser other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Equals(this, other);
        }

        /// <summary>
        ///     Overriden Equals
        /// </summary>
        /// <param name="obj">other user</param>
        /// <returns>true, if objects are equal, otherwise false</returns>
        public override bool Equals(object obj)
        {
            var other = obj as DalUser;
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Equals(this, other);
        }

        /// <summary>
        ///      GetHashCode
        /// </summary>
        /// <returns>hash code of the user</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        ///     Overriden ToString
        /// </summary>
        /// <returns>string representation of the user</returns>
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
