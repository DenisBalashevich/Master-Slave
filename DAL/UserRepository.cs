using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DAL.DTO;
using DAL.Interface;
using NLog;

namespace DAL
{
    /// <summary>
    ///     User repository
    /// </summary>
    [Serializable]
    public class UserRepository : MarshalByRefObject, IUserRepository
    {

        private static readonly Logger Logger;
        private static readonly BooleanSwitch LoggerSwitch;

        private readonly List<DalUser> users;
        private readonly IUserValidation validator;
        private IIdGenerator generator;
        static UserRepository()
        {
            LoggerSwitch = new BooleanSwitch("Data", "Module");
            Logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        ///     ctor
        /// </summary>
        public UserRepository() : this(new DefaultId(), new UserValidator())
        {
        }

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="generator"></param>
        /// <param name="validator"></param>
        public UserRepository(IIdGenerator generator, IUserValidation validator)
        {
            if (object.ReferenceEquals(generator, null))
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(nameof(generator) + " is null!");
                }

                throw new ArgumentNullException();
            }

            if (object.ReferenceEquals(validator, null))
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(nameof(validator) + " is null!");
                }

                throw new ArgumentNullException();
            }

            this.generator = generator;
            this.validator = validator;
            this.users = new List<DalUser>();
        }

        /// <summary>
        ///     Add
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Add(DalUser user)
        {
            try
            {
                var isValid = this.validator.IsValid(user);
                if (!isValid)
                {
                    throw new ArgumentException(nameof(user));
                }

                this.generator.GenerateId();
                user.Id = this.generator.Current;
            }
            catch (ArgumentNullException exception)
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(exception.Message);
                }

                throw;
            }
            catch (InvalidOperationException exception)
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(exception.Message);
                }

                throw;
            }

            this.users.Add(user);
            if (LoggerSwitch.Enabled)
            {
                Logger.Info($"User: {user} Added");
            }

            return user.Id;
        }

        /// <summary>
        ///     Delete 
        /// </summary>
        /// <param name="user"></param>
        public void Delete(DalUser user)
        {
            if (object.ReferenceEquals(user, null))
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(nameof(user));
                }

                throw new ArgumentNullException();
            }

            this.users.Remove(user);
            if (LoggerSwitch.Enabled)
            {
                Logger.Info($"User {user} Deleted");
            }
        }

        /// <summary>
        ///     Search by predicates
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public int[] GetByPredicate(Func<DalUser, bool>[] predicates)
        {
            try
            {
                return this.users.AsParallel().TakeWhile(p => predicates.All(a => a(p))).Select(u => u.Id).ToArray();
            }
            catch (ArgumentNullException exception)
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(exception.Message);
                }

                throw exception;
            }
        }

        /// <summary>
        ///     Searches by criteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public int[] SearchForUsers(IDalCriterion<DalUser>[] criteria)
        {
            Func<DalUser, bool>[] predicates = new Func<DalUser, bool>[criteria.Length];
            for (int i = 0; i < criteria.Length; i++)
                predicates[i] = (criteria[i].ApplyCriterion);
            return GetByPredicate(predicates);
        }

        /// <summary>
        ///     Saves user to xml
        /// </summary>
        public void Save()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<DalUser>));
            string path;
            try
            {
                path = ConfigurationManager.AppSettings["Path"];
            }
            catch (ConfigurationErrorsException exception)
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(exception.Message);
                }

                throw;
            }

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, this.users);
            }

            if (LoggerSwitch.Enabled)
            {
                Logger.Info($"saved ");
            }
        }

        /// <summary>
        ///     Loads user from xml
        /// </summary>
        public void Load()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<DalUser>));
            string path;
            try
            {
                path = ConfigurationManager.AppSettings["Path"];
            }
            catch (ConfigurationErrorsException ex)
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(ex.Message);
                }

                throw;
            }

            using (StreamReader sr = new StreamReader(path))
            {
                List<DalUser> users = (List<DalUser>)formatter.Deserialize(sr);
                foreach (var user in users)
                {
                    this.users.Add(user);
                }

                if (users.Count != 0)
                {
                    this.generator = new DefaultId();
                }
            }

            if (LoggerSwitch.Enabled)
            {
                Logger.Info($"loaded ");
            }
        }
    }
}
