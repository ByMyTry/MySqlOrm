using System;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace MySqlOrm
{
    class ModelParser<T>
    {
        private Type info;

        public readonly String tableName;
        private readonly IEnumerable<PropertyInfo> properties;

        public ModelParser()
        {
            this.info = typeof(T);
            this.tableName = info.Name.ToLower() + "s";
            this.properties = info.GetProperties();
        }

        public IEnumerable<T> ParseFrom(MySqlDataReader reader)
        {
            List<T> modelObjects = new List<T>();
            while(reader.Read())
            {
                var modelObject = Activator.CreateInstance(typeof(T));
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    PropertyInfo prop = this.properties.First(p => p.Name.ToLower().Equals(reader.GetName(i).ToLower()));
                    prop.SetValue(modelObject, reader.GetValue(i));
                }
                dynamic kostil = modelObject;
                modelObjects.Add(kostil);
            }
            return modelObjects;
        }

        /*public String GetTableName()
        {
            return info.Name.ToLower() + "s";//typeof(T).Name.ToLower() + "s"
        }*/

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
