using System;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace MySqlOrm
{
    public static class ModelParser
    {
        //private Type info;

        /*public readonly String tableName;
        private readonly IEnumerable<PropertyInfo> properties;*/

        /*public ModelParser()
        {
            this.info = typeof(T);
            this.tableName = info.Name.ToLower() + "s";
            this.properties = info.GetProperties();
        }*/

        private static String EqualsFormat(String s)
        {
            var s1 = s.ToLower().Replace("_", "");
            return s1;
        }

        public static IEnumerable<T> DeparseFrom<T>(MySqlDataReader reader)
        {
            List<T> modelObjects = new List<T>();
            while(reader.Read())
            {
                IEnumerable<PropertyInfo> propertiesInfo = typeof(T).GetProperties();
                dynamic modelObject = Activator.CreateInstance(typeof(T));
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo prop = propertiesInfo
                        .FirstOrDefault(p => EqualsFormat(p.Name).Equals(EqualsFormat(reader.GetName(i))));
                    prop.SetValue(modelObject, reader.GetValue(i));
                }
                modelObjects.Add(modelObject);
            }
            return modelObjects;
        }

        public static String GetTableName<T>()
        {
            return typeof(T).Name.ToLower() + "s";
        }

        public static String GetPrimaryKeyName<T>()
        {
            PropertyInfo prop = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0);
            return EqualsFormat(prop.Name);
        }

        /*public IEnumerable<String> GetPropNames()
        {
            return info.GetProperties().Select(p => p.Name);
        }

        public dynamic GetPropValueByName(String Name)
        {
            foreach(var property in info.GetProperties())
            {
                if (property.Name.ToLower().Equals(Name.ToLower()))
                {
                    dynamic propValue = property.GetValue(this.modelObject);
                    property.SetValue(,);
                    return propValue; //нужен сериализатор (switch)
                }
            }
            return null;
        }*/
    }
}
