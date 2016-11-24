using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

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
            Type t = typeof(SqlDataReader);
            foreach (var f in t.GetMethods())
                if(f.Name.EndsWith(typeof(Int32).Name)  && f.Name.StartsWith("Get")) Console.WriteLine(f.Name);
            //var o1 = Activator.CreateInstance(typeof(String),o);

            //Console.WriteLine(o1);
            Console.ReadKey();
        }
    }
}
