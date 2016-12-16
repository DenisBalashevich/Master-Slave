using System.Linq;
using BLL.Models;
using DAL.DTO;

namespace BLL.Mappers
{
    public static class Mappers
    {
        public static BllUser ToBllUser(this DalUser user)
        {
            return new BllUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = (BllGender)user.Gender,
                VisaRecords = user.VisaRecords?.Select(card => new BllVisa
                {
                    Country = card.Country,
                    EndDate = card.EndDate,
                    StartDate = card.StartDate
                })?.ToList()
            };
        }

        public static DalUser ToDalUser(this BllUser user)
        {
            return new DalUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = (DalGender)user.Gender, 
                VisaRecords = user.VisaRecords?.Select(card => new DalVisa
                {
                    Country = card.Country,
                    EndDate = card.EndDate,
                    StartDate = card.StartDate
                })?.ToList()
            };
        }
    }
}
