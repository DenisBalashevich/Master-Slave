using BLL;
using BLL.Models;
using BLL.Service;
using DAL;
using DAL.Interface;
using NetworkConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInitLayer
{
    public class DomainLoader : MarshalByRefObject
    {
        /// <summary>
        /// Creates service according to config info
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        public UserService LoadService(ElementHelper configInfo)
        {
            UserService service;
            Communicator communicator;
            IUserRepository repository = new UserRepository();
            if (configInfo.ServerType == "master")
            {
                Sender<BllUser> sender = new Sender<BllUser>();
                communicator = new Communicator(sender);
                service = new MasterUserService(repository);
            }

            else if (configInfo.ServerType == "slave")
            {
                Receiver<BllUser> receiver = new Receiver<BllUser>(configInfo.IpEndPoint.Address, configInfo.IpEndPoint.Port);
                communicator = new Communicator(receiver);
                service = new SlaveUserService(repository);
            }
            else
                throw new ArgumentException("Wrong service type!");

            service.Communicator = communicator;

            return service;
        }
    }
}
