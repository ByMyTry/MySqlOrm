using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Data;

namespace MySqlOrm
{
    public static class CommandCreator
    {
        public static MySqlCommand AddCommand<T>(T modelObject)
        {
            String commandText = "INSERT INTO {0} {1} VALUES {2};";  
            String tableName = ModelParser.GetTableName<T>();
            var propDict = ModelParser.Parse<T>(modelObject);
            String propNames = "(" + String.Join(", ", propDict.Keys) + ")";
            String propParams = "(" + String.Join(", ", propDict.Keys.Select(n => "@" + n)) + ")";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, tableName, propNames, propParams);

            foreach(var prop in propDict)
            {
                MySqlParameter param = new MySqlParameter("@" + prop.Key, prop.Value);
                command.Parameters.Add(param);
            }

            return command;
        }
        
        public static MySqlCommand UpdateCommand<T>(T modelObject)
        {
            if (ModelParser.GetPrimaryKeyName<T>() != null)
            {
                String commandText = "UPDATE {0} SET {1} WHERE {2};";
                String tableName = ModelParser.GetTableName<T>();
                var propDict = ModelParser.Parse<T>(modelObject);
                var propNames = propDict.Keys.Select(n => n + " = @" + n);
                String propParams = String.Join(", ", propNames);
                String pkName = ModelParser.GetPrimaryKeyName<T>();

                MySqlCommand command = new MySqlCommand();
                command.CommandText = String.Format(commandText, tableName, propParams, pkName + " = @" + pkName);
                foreach (var prop in propDict)
                    command.Parameters.AddWithValue("@" + prop.Key, prop.Value);
                command.Parameters.AddWithValue("@" + pkName, ModelParser.GetPrimaryKeyValue<T>(modelObject));

                return command;
            }
            else
                throw new Exception(String.Format("{0} has no Primary Key", typeof(T).Name));
        }

        public static MySqlCommand RemoveCommand<T>(T modelObject)
        {
            String commandText = "DELETE FROM {0} WHERE {1};";
            String tableName = ModelParser.GetTableName<T>();
            var propDict = ModelParser.Parse<T>(modelObject);
            var propNames = propDict.Keys.Select(n => n + " = @" + n);
            String propParams = String.Join(" AND ", propNames);

            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, tableName, propParams);
            foreach (var prop in propDict)
                command.Parameters.AddWithValue("@" + prop.Key, prop.Value);

            return command;
        }

        public static MySqlCommand RemoveByIdCommand<T>(Object id)
        {
            if (ModelParser.GetPrimaryKeyName<T>() != null)
            {
                String commandText = "DELETE FROM {0} WHERE {1} = {2};";
                String tableName = ModelParser.GetTableName<T>();
                String pkName = ModelParser.GetPrimaryKeyName<T>();

                MySqlCommand command = new MySqlCommand();
                command.CommandText = String.Format(commandText, tableName, pkName, "@" + pkName);

                MySqlParameter param = new MySqlParameter("@" + pkName, id);
                command.Parameters.Add(param);

                return command;
            }
            else
                throw new Exception(String.Format("{0} has no Primary Key",typeof(T).Name));
        }

        public static MySqlCommand SelectCommand<T>()
        {
            String commandText = "SELECT {0} FROM {1};";
            var namesList = ModelParser.Parse<T>();
            String props = String.Join(", ", namesList);
            String tableName = ModelParser.GetTableName<T>();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, props, tableName);

            return command;
        }

        public static MySqlCommand SelectByIdCommand<T>(Object id)
        {
            if (ModelParser.GetPrimaryKeyName<T>() != null)
            {
                String commandText = "SELECT {0} FROM {1} WHERE {2} = {3};";
                var namesList = ModelParser.Parse<T>();
                String props = String.Join(", ", namesList);
                String tableName = ModelParser.GetTableName<T>();
                String pkName = ModelParser.GetPrimaryKeyName<T>();

                MySqlCommand command = new MySqlCommand();
                command.CommandText = String.Format(commandText, props, tableName, pkName, "@" + pkName);

                MySqlParameter param = new MySqlParameter("@" + pkName, id);
                command.Parameters.Add(param);

                return command;
            }
            else
                throw new Exception(String.Format("{0} has no Primary Key", typeof(T).Name));
        }
    }
}
