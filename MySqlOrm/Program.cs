using System;
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
                /*IEnumerable<User> users = sc.GetAll<User>();
                foreach (var user in users)
                    Console.WriteLine(user.Id + "\t " + user.Name + "\t " + user.Region_Id);*/

                //GetById//
                /*User user1 = sc.GetById<User>(users.ElementAt(2).Id);
                 Console.WriteLine(user1.Id + "\t " + user1.Name + "\t " + user1.Region_Id);*/

                //PrimaryKey//
                //Console.WriteLine(ModelParser.GetPrimaryKeyName<User>());

                //Remove//
                /*for(int i = 10; i< 19;i++)
                    Console.WriteLine(sc.RemoveById<User>(i));
                
                users = sc.GetAll<User>();
                foreach (var user in users)
                    Console.WriteLine(user.Id + " " + user.Name + " " + user.Region_Id);*/

                //Insert//
                /*user1 = sc.Add<User>(new User { Name = "nw", Region_Id = 2 });
                Console.WriteLine(user1.Id + " " + user1.Name + " " + user1.Region_Id);*/

                //Update//
                /* user1.Name = "lololo";
                 Console.WriteLine(sc.Update<User>(user1));
                 //Console.WriteLine(CommandCreator.UpdateCommand<User>(user1).CommandText);
                 users = sc.GetAll<User>();
                 foreach (var user in users)
                     Console.WriteLine(user.Id + " " + user.Name + " " + user.Region_Id);*/
                User u = new User() {Name = "test", Region_Id = 2 };
                u = sc.GetById<User>(19);
                sc.RemoveById<User>(2);
            }
            Console.ReadKey();
        }
    }
}
