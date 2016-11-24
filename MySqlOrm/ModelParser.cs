using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace MySqlOrm
{
    class ModelParser<T>
    {
        private Type info;
        private T modelObject;

        public ModelParser(/*T modelObject*/)
        {
            this.info = typeof(T);
            this.modelObject = modelObject;
        }

        public String GetTableName()
        {
            return info.Name.ToLower() + "s";//typeof(T).Name.ToLower() + "s"
        }

        public IEnumerable<String> GetPropNames()
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
                    return propValue; //нужен сериализатор (switch)
                }
            }
            return null;
        }
    }
}
