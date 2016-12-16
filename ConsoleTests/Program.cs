using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BLL.Models;
using BLL.Service;
using DomainInitLayer;

namespace ConsoleTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DomainInitialization.Load();
            MasterUserService master = DomainInitialization.Master as MasterUserService;
            var slaves = DomainInitialization.Slaves.ToList().Select(a=>a as SlaveUserService);
            RunMaster(master);
            RunSlaves(slaves);
            while (true)
            {
                var quit = Console.ReadKey();
                if (quit.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static void RunMaster(MasterUserService master)
        {
            if (object.ReferenceEquals(master, null))
            {
                return;
            }

            Random rand = new Random();

            ThreadStart masterSearch = () =>
            {
                while (true)
                {
                    var serachresult = master.SearchForUsers(new Func<BllUser, bool>[] { u => u.FirstName != null });
                    Console.Write("Master search results: ");
                    foreach (var result in serachresult)
                    {
                        Console.Write(result + " ");
                    }

                    Console.WriteLine();
                    Thread.Sleep(rand.Next(1000, 5000));
                }
            };

            ThreadStart masterAddDelete = () =>
            {
                var users = new List<BllUser>
                {
                    new BllUser { FirstName = "User1", LastName = "User1" },
                    new BllUser { FirstName = "User2", LastName = "User2" },
                    new BllUser { FirstName = "User3", LastName = "User3" }
                };
                BllUser userToDelete = null;

                while (true)
                {
                    foreach (var user in users)
                    {
                        int addChance = rand.Next(0, 5);
                        if (addChance == 0)
                        {
                            master.Add(user);
                        }

                        Thread.Sleep(rand.Next(1000, 5000));
                        if (userToDelete != null)
                        {
                            int deleteChance = rand.Next(0, 5);
                            if (deleteChance == 0)
                            {
                                master.Delete(userToDelete);
                            }
                        }

                        userToDelete = user;
                        Thread.Sleep(rand.Next(1000, 5000));
                        Console.WriteLine();
                    }
                }
            };

            Thread masterSearchThread = new Thread(masterSearch) { IsBackground = true };
            Thread masterAddThread = new Thread(masterAddDelete) { IsBackground = true };
            Thread masterSearchThread1 = new Thread(masterSearch) { IsBackground = true };
            Thread masterAddThread1 = new Thread(masterAddDelete) { IsBackground = true };
            masterAddThread.Start();
            masterSearchThread.Start();
            masterAddThread1.Start();
            masterSearchThread1.Start();
        }

        private static void RunSlaves(IEnumerable<SlaveUserService> slaves)
        {
            Random rand = new Random();

            foreach (var slave in slaves)
            {
                var slaveThread = new Thread(() =>
                {
                    while (true)
                    {
                        var userIds = slave.SearchForUsers(new Func<BllUser, bool>[]
                        {
                            u => u.FirstName != null
                        });

                        Console.Write(" Slave search results: ");
                        foreach (var user in userIds)
                        {
                            Console.Write(user + " ");
                        }

                        Console.WriteLine();
                        Thread.Sleep((int)(rand.NextDouble() * 5000));
                    }
                })
                {
                    IsBackground = true
                };
                slaveThread.Start();
            }
        }
    }
}
