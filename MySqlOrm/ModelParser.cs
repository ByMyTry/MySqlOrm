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
            return s.ToLower().Replace("_", "");
        }


        public static String GetTableName<T>()
        {
            return EqualsFormat(typeof(T).Name) + "s";
        }

        public static String GetPrimaryKeyName<T>()
        {
            PropertyInfo prop = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0);
            if (prop != null)
                return EqualsFormat(prop.Name);
            else
                return null;
        }

        public static Object GetPrimaryKeyValue<T>(T modelObject)
        {
            PropertyInfo prop = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0);
            if (prop != null)
                return prop.GetValue(modelObject);
            else
                return null;
        }

        public static T SetPrimaryKey<T>(T modelObject,Object value)
        {
            PropertyInfo prop = typeof(T)
                .GetProperties()
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Length > 0);
            prop.SetValue(modelObject, value);
            return modelObject;
        }

        public static IEnumerable<T> Deparse<T>(MySqlDataReader reader)
        {
            //List<T> modelObjects = new List<T>();
            object modelObjects = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]{typeof(T)}));
            while (reader.Read())
            {
                IEnumerable<PropertyInfo> propertiesInfo = typeof(T).GetProperties();
                dynamic modelObject = Activator.CreateInstance(typeof(T));
                for (int i = 0; i < reader.FieldCount; i++) //переделать 
                {
                    PropertyInfo prop = propertiesInfo
                        .FirstOrDefault(p => EqualsFormat(p.Name).Equals(EqualsFormat(reader.GetName(i))));
                    prop.SetValue(modelObject, reader.GetValue(i));
                }
                //modelObjects.Add(modelObject);
                modelObjects.GetType().GetMethod("Add").Invoke(modelObjects, new[] { modelObject });
            }
            return (IEnumerable<T>)modelObjects;
        }

        public static Dictionary<String, Object> Parse<T>(T modelObject)//for insert/update/remove
        {
            Dictionary<String, Object> namesValuesDict = new Dictionary<String, Object>();

            IEnumerable<PropertyInfo> propertiesInfo = typeof(T).GetProperties();
            if (GetPrimaryKeyName<T>() != null)
            {
                foreach (var propInfo in propertiesInfo)
                    if (!EqualsFormat(propInfo.Name).Equals(GetPrimaryKeyName<T>()))
                        namesValuesDict.Add(propInfo.Name, propInfo.GetValue(modelObject));
            }
            else
                foreach (var propInfo in propertiesInfo)
                    namesValuesDict.Add(propInfo.Name, propInfo.GetValue(modelObject));

            return namesValuesDict;
        }

        public static IEnumerable<String> Parse<T>()//for select
        {
            List<String> namesList = new List<String>();

            IEnumerable<PropertyInfo> propertiesInfo = typeof(T).GetProperties();
            foreach (var propInfo in propertiesInfo)
                    namesList.Add(propInfo.Name);

            return namesList;
        }
    }
}
