using BLL.Interface;
using BLL.Service;
using DomainInitLayer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DomainInitLayer
{
    public class DomainInitialization
    {
        public static MasterUserService Master { get; private set; }

        public static List<ISlave> Slaves { get; private set; }

        public static void Load()
        {
            Slaves = Slaves ?? new List<ISlave>();

            var section = ConnectionSection.GetSection();


            var servers = GetSection();
            if (ReferenceEquals(servers, null))
                throw new ArgumentNullException();
            Slaves = new List<ISlave>();
            for (int i = 0; i < servers.Count; i++)
            {
                AppDomain domain = AppDomain.CreateDomain(servers[i].ServiceType + i);
                var type = typeof(DomainLoader);
                var loader = (DomainLoader)domain.CreateInstanceAndUnwrap(Assembly.GetAssembly(type).FullName, type.FullName);
                var element = new ElementHelper
                {
                    IpEndPoint = new IPEndPoint(IPAddress.Parse(servers[i].IpAddress), servers[i].Port),
                    ServerType = servers[i].ServiceType
                };
                var service = loader.LoadService(element);
                if (servers[i].ServiceType == "slave")
                {
                    Slaves.Add(service as ISlave);
                }
                else if (servers[i].ServiceType == "master")
                {
                    Master = service as MasterUserService;
                }
                else throw new ArgumentException("Incorrect server type");
            }
            InitializeServices();
        }

        public static void InitializeServices()
        {
            var tempList = new List<SlaveUserService>();
            var iPoints = new List<IPEndPoint>();
            for (int i = 0; i < Slaves.Count; i++)
            {
                tempList.Add(Slaves[i] as SlaveUserService);
            }

            var servers = GetSection();
            for (int i = 0; i < servers.Count; i++)
            {
                if (servers[i].ServiceType == "slave")
                    iPoints.Add(new IPEndPoint(IPAddress.Parse(servers[i].IpAddress), servers[i].Port));
            }
            Master.Communicator.Connect(iPoints);
            for (int i = 0; i < Slaves.Count; i++)
            {
                tempList[i].Communicator.RunReceiver();
            }
        }
        private static List<Element> GetSection()
        {
            List<Element> list = new List<Element>();
            var section = ConnectionSection.GetSection();
            for (int i = 0; i < section.MasterServices.Count; i++)
            {
                list.Add(section.MasterServices[i]);
            }
            return list;
        }
    }
}
