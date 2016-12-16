using System;
using BLL.Mappers;
using BLL.Models;
using DAL;
using DAL.Interface;
using BLL.Interface;

namespace BLL.Service
{
    /// <summary>
    /// Master user service
    /// </summary>
    public class MasterUserService : UserService, IMaster
    {
        public MasterUserService(IUserRepository repository) : base(repository)
        {
        }

        public MasterUserService() : this(new UserRepository())
        {
        }

        public override void Save()
        {
            Repository.Save();
        }

        public override void Load()
        {
            Repository.Load();
        }

        protected virtual void OnUserDeleted(object sender, BllUser args)
        {
            Communicator?.SendDelete(args);
        }

        protected virtual void OnUserAdded(object sender, BllUser args)
        {
            Communicator?.SendAdd(args);
        }

        protected override int AddUser(BllUser user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException();
            }

            try
            {
                Repository.Add(user.ToDalUser());
            }
            catch (ArgumentException exception)
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(exception.Message);
                }

                throw exception;
            }

            OnUserAdded(this, user);
            return user.Id;
        }

        protected override void DeleteUser(BllUser user)
        {
            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException();
            }

            try
            {
                Repository.Delete(user.ToDalUser());
            }
            catch (ArgumentNullException exception)
            {
                if (LoggerSwitch.Enabled)
                {
                    Logger.Error(exception.Message);
                }

                throw exception;
            }

            OnUserDeleted(this, user);
        }

    }
}
