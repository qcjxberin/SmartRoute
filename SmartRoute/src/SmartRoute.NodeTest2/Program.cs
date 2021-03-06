﻿using SmartRoute.NodeTest.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRoute.NodeTest2
{
    public class Program
    {
        static long mCount;
        public static void Main(string[] args)
        {
            INode node = NodeFactory.Default;
            node.Loger.Type = LogType.ALL;
            node.AddLogHandler(new SmartRoute.ConsoleLogHandler(LogType.ALL));
            node.Open();
            EventSubscriber ken = node.Register<EventSubscriber>("ken");
            ken.Register<User>(OnUser);
            ken.Register<Employee>(OnEmployees);
            EventSubscriber mark = node.Register<EventSubscriber>("mark");
            mark.Register<User>(OnUser);
            mark.Register<Employee>(OnEmployees);
            node.SubscriberRegisted = (n, s) =>
            {
                if (s.Name == "henry")
                {
                    ken.Publish("henry", Employee.GetEmployee());
                    mark.Publish("henry", Employee.GetEmployee());
                }
            };

            while (true)
            {
                Console.WriteLine(mCount);
                System.Threading.Thread.Sleep(1000);
            }
            Console.Read();
        }
       
        private static void OnEmployees(Message msg, Employee emp)
        {
            System.Threading.Interlocked.Increment(ref mCount);
            msg.Reply(Employee.GetEmployee());
        }
        private static void OnUser(Message msg, User user)
        {
            System.Threading.Interlocked.Increment(ref mCount);
            msg.Reply(Employee.GetEmployee());
        }
    }
}
