using System;
using System.Reflection;

namespace MySqlOrm
{
    class ModelParser<T>
    {
        private Type info;
        private T modelObject;

        public ModelParser(T modelObject)
        {
            this.info = typeof(T);
            this.modelObject = modelObject;
        }

        public String GetModelName()
        {
            return info.Name;
        }

        public String GetPropValueByName(String Name)
        {
            foreach(var property in info.GetProperties())
            {
                if (property.Name.ToLower().Equals(Name.ToLower()))
                    return property.GetValue(this.modelObject).ToString(); //нужен сериализатор 
            }
            return null;
        }
    }
}
