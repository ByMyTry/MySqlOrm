using System;
using MySqlOrm;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (SimpleConnector sc = new SimpleConnector(
                 "localhost",
                 "root",
                 "1111",
                 "test_db1"
                 )
            )
            {
                User u = new User() { Name = "lol"};
                var u1 = sc.GetAll<User>();
                foreach (var user in u1)
                    Console.WriteLine(user.Name);
                //Console.WriteLine(u1.Name);
                //u = sc.GetById<User>(22);
                /*u.Name = "loh";
                sc.Update<User>(u);*/
                //sc.RemoveById<User>(22);
                //sc.Remove<User>(u);
            }
        }
    }
}
