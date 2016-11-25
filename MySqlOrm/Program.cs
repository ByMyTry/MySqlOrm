using System;
using System.Data.SqlClient;
using MySqlOrm.Models;
using System.Collections.Generic;

namespace MySqlOrm
{
    class C
    {
        public int x = 2;

        public string s = "3";

        public override string ToString()
        {
            return this.x + this.s;
        }
    }

    class Program1
    {
        static void Main(string[] args)
        {
            /*Type t = typeof(String);
            Console.WriteLine(Type.GetType(""));
            Object o = new C();
            Console.WriteLine(o.ToString());*/

            //sasmorotrun
            /*Type t = typeof(SqlDataReader);
            foreach (var f in t.GetMethods())
                if(f.Name.EndsWith(typeof(Int32).Name)  && f.Name.StartsWith("Get")) Console.WriteLine(f.Name);*/
            //var o1 = Activator.CreateInstance(typeof(String),o);

            //Console.WriteLine(o1);
            SimpleConnector sc = new SimpleConnector(
                 "localhost",
                 "root",
                 "1111",
                 "test_db1"
                 );
             IEnumerable<User> users = sc.GetAll<User>();
             foreach (var user in users)
                 Console.WriteLine(user.id + " " + user.name + " " + user.region_id);
            Console.ReadKey();
        }
    }
}
