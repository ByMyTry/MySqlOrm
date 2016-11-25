using System;
using MySql.Data.MySqlClient;

namespace MySqlOrm
{
    public static class CommandCreator
    {
        /*public static MySqlCommand AddCommand<T>()
        {

        }

        public static MySqlCommand UpdateCommand<T>()
        {

        }*/

        public static MySqlCommand RemoveByIdCommand<T>(Object id)
        {
            String commandText = "DELETE FROM {0} WHERE {1} = {2};";
            MySqlCommand command = new MySqlCommand();
            String pkName = ModelParser.GetPrimaryKeyName<T>();
            String paramName = "@id";
            command.CommandText = String.Format(commandText, ModelParser.GetTableName<T>(), pkName, paramName);
            MySqlParameter param = new MySqlParameter(paramName, id);
            command.Parameters.Add(param);
            return command;
        }

        public static MySqlCommand SelectCommand<T>()
        {
            String commandText = "SELECT * FROM {0};";
            MySqlCommand command = new MySqlCommand();
            command.CommandText = String.Format(commandText, ModelParser.GetTableName<T>());
            return command;
        }

        public static MySqlCommand SelectByIdCommand<T>(Object id)
        {
            String commandText = "SELECT * FROM {0} WHERE {1} = {2};";
            MySqlCommand command = new MySqlCommand();
            String pkName = ModelParser.GetPrimaryKeyName<T>();
            String paramName = "@id";
            command.CommandText = String.Format(commandText, ModelParser.GetTableName<T>(), pkName, paramName);
            MySqlParameter param = new MySqlParameter(paramName, id);
            command.Parameters.Add(param);
            return command;
        }
    }
}
