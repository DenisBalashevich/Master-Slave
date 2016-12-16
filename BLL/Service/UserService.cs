using BLL.Interface;
using BLL.Mappers;
using BLL.Models;
using DAL;
using DAL.DTO;
using DAL.Interface;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    /// <summary>
    /// represent base abstract class for Master and Slave
    /// </summary>
    public abstract class UserService : MarshalByRefObject
    {
        protected static readonly Logger Logger;

        protected static readonly BooleanSwitch LoggerSwitch;

        private Communicator communicator;

        static UserService()
        {
            Logger = LogManager.GetCurrentClassLogger();
            LoggerSwitch = new BooleanSwitch("Data", "Module");
        }


        protected UserService() : this(new UserRepository())
        {
        }

        protected UserService(IUserRepository repository)
        {
            if (ReferenceEquals(repository, null))
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(nameof(repository));
                }

                throw new ArgumentNullException();
            }

            Repository = repository;
        }

        protected IUserRepository Repository { get; set; }

        public virtual Communicator Communicator
        {
            get
            {
                return communicator;
            }

            set
            {
                if (ReferenceEquals(value, null))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                communicator = value;
            }
        }

        public int Add(BllUser user)
        {
            try
            {
                return AddUser(user);
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

        public void Delete(BllUser user)
        {
            try
            {
                DeleteUser(user);
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


        public virtual int SearchForUsers(ISearchCriteria criteria)
        {
            try
            {
                Func<BllUser, bool> predicate = new Func<BllUser, bool>(criteria.Search);
                return SearchForUsers(new Func<BllUser, bool>[] { predicate }).First();
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

        public virtual int[] SearchForUsers(Func<BllUser, bool>[] criteria)
        {
            try
            {
                var dalCriteria = new Func<DalUser, bool>[criteria.Length];
                for (var i = 0; i < dalCriteria.Length; i++)
                {
                    var k = i;
                    dalCriteria[k] = user => criteria[k].Invoke(user.ToBllUser());
                }

                return Repository.GetByPredicate(dalCriteria);
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

        public abstract void Save();

        public abstract void Load();

        protected abstract int AddUser(BllUser user);

        protected abstract void DeleteUser(BllUser user);


    }
}
