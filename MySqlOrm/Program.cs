using System;
using System.Data.SqlClient;
using MySqlOrm.Models;
using System.Collections.Generic;
using System.Linq;

namespace MySqlOrm
{
    class Program1
    {
        static void Main(string[] args)
        {
            using (SimpleConnector sc = new SimpleConnector(
                 "localhost",
                 "root",
                 "1111",
                 "test_db1"
                 )
            )
            {
                //GetAll//
                IEnumerable<User> users = sc.GetAll<User>();
                foreach (var user in users)
                    Console.WriteLine(user.Id + " " + user.Name + " " + user.RegionId);

                //GetById//
                User user1 = sc.GetById<User>(users.ElementAt(3).Id);
                Console.WriteLine(user1.Id + " " + user1.Name + " " + user1.RegionId);

                //PrimaryKey//
                Console.WriteLine(ModelParser.GetPrimaryKeyName<User>());

                //Remove//
                Console.WriteLine(sc.RemoveById<User>(4));

                //GetAll//
                users = sc.GetAll<User>();
                foreach (var user in users)
                    Console.WriteLine(user.Id + " " + user.Name + " " + user.RegionId);
            }
            Console.ReadKey();
        }
    }
}
