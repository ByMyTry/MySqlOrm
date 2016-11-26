using System;
using System.Linq;
using MySql.Data.MySqlClient;

namespace MySqlOrm
{
    public static class CommandCreator
    {
        public static MySqlCommand AddCommand<T>(T modelObject)
        {
            String commandText = "INSERT INTO {0} {1} VALUES {2};";// SET @id=SCOPE_IDENTITY();";   
            String tableName = ModelParser.GetTableName<T>();
            var propDict = ModelParser.Parse<T>(modelObject);
            String fieldsNames = "(" + String.Join(", ", propDict.Keys) + ")";
            String fieldsParams = "(" + String.Join(", ", propDict.Keys.Select(n => "@" + n)) + ")";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, tableName, fieldsNames, fieldsParams);

            foreach(var prop in propDict)
            {
                MySqlParameter param = new MySqlParameter("@" + prop.Key, prop.Value);
                command.Parameters.Add(param);
            }

            return command;
        }

        /*
        public static MySqlCommand UpdateCommand<T>()
        {
            
        }*/

        public static MySqlCommand RemoveByIdCommand<T>(Object id)
        {
            String commandText = "DELETE FROM {0} WHERE {1} = {2};";            
            String tableName = ModelParser.GetTableName<T>();
            String pkName = ModelParser.GetPrimaryKeyName<T>();
            String paramName = "@id";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, tableName, pkName, paramName);

            MySqlParameter param = new MySqlParameter(paramName, id);
            command.Parameters.Add(param);

            return command;
        }

        public static MySqlCommand SelectCommand<T>()
        {
            String commandText = "SELECT * FROM {0};";
            String tableName = ModelParser.GetTableName<T>();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, tableName);

            return command;
        }

        public static MySqlCommand SelectByIdCommand<T>(Object id)
        {
            String commandText = "SELECT * FROM {0} WHERE {1} = {2};";
            String tableName = ModelParser.GetTableName<T>();
            String pkName = ModelParser.GetPrimaryKeyName<T>();
            String paramName = "@id";

            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, tableName, pkName, paramName);

            MySqlParameter param = new MySqlParameter(paramName, id);
            command.Parameters.Add(param);

            return command;
        }
    }
}
