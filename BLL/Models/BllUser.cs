using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Models
{
    /// <summary>
    ///     User class in BLL
    /// </summary>
    [Serializable]
    public class BllUser
    {

        /// <summary>
        ///     Constructor
        /// </summary>
        public BllUser()
        {
            VisaRecords = new List<BllVisa>();
        }


        /// <summary>
        ///     Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Birth Date
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        public BllGender Gender { get; set; }

        /// <summary>
        ///     Gets of sets a User's Visa cards
        /// </summary>
        public List<BllVisa> VisaRecords { get; set; }


        /// <summary>
        ///     Static Equals
        /// </summary>
        /// <param name="lhs">first user</param>
        /// <param name="rhs">second user</param>
        /// <returns>true, if objects are equal, otherwise false</returns>
        public static bool Equals(BllUser first, BllUser second)
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
        public bool Equals(BllUser other)
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
            var other = obj as BllUser;
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            return Equals(this, other);
        }

        /// <summary>
        ///     Override GetHashCode
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
