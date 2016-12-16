using System;
using BLL.Mappers;
using BLL.Models;
using DAL;
using DAL.Interface;
using BLL.Interface;

namespace BLL.Service
{
    /// <summary>
    /// Slave service
    /// </summary>
    public class SlaveUserService : UserService, ISlave
    {
        public SlaveUserService(IUserRepository repository) : base(repository)
        {
        }

        public SlaveUserService() : this(new UserRepository())
        {
        }

        public override Communicator Communicator
        {
            get
            {
                return base.Communicator;
            }

            set
            {
                base.Communicator = value;
                Communicator.UserAdded += OnAdded;
                Communicator.UserDeleted += OnDeleted;
            }
        }

        public override void Save()
        {
            throw new NotSupportedException();
        }

        public override void Load()
        {
            Repository.Load();
        }

        protected override int AddUser(BllUser user)
        {
            throw new NotSupportedException();
        }

        protected override void DeleteUser(BllUser user)
        {
            throw new NotSupportedException();
        }

        private void OnAdded(object sender, BllUser args)
        {
            try
            {
                Repository.Add(args.ToDalUser());
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

        private void OnDeleted(object sender, BllUser args)
        {
            try
            {
                Repository.Delete(args.ToDalUser());
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
    }
}