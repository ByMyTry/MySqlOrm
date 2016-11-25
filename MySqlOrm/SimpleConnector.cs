using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace MySqlOrm
{
    class SimpleConnector : IDisposable, ISimpleConnector
    {
        private MySqlConnection connection = null;

        public SimpleConnector(String server, String userId, String password,String dbName)
        {
            String connectionString = String.Format(
                @"server={0};userid={1};password={2};database={3}", 
                server, 
                userId, 
                password, 
                dbName
            );
            this.connection = new MySqlConnection(connectionString);
            this.connection.Open();
        }

        public T Add<T>(T modelObject)
        {
            throw new NotImplementedException();
            int addRecordsCount = 1;
            //return addRecordsCount == 1;
        }

        public bool Update<TModel>(TModel modelObject)
        {
            throw new NotImplementedException();
        }

        public bool RemoveById<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
        {
            MySqlDataReader reader = null;
            IEnumerable<T> modelObjects = null;
            try
            {
                //
                String commandText = "SELECT * FROM {0};";
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
                command.CommandText = String.Format(commandText, ModelParser.GetTableName<T>());
                //
                reader = command.ExecuteReader();
                modelObjects = ModelParser.DeparseFrom<T>(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
            return modelObjects;
        }

        public T GetById<T>(Object id)
        {
            MySqlDataReader reader = null;
            IEnumerable<T> modelObjects = null;
            try
            {
                //
                String commandText = "SELECT * FROM {0} WHERE {1} = {2};";
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
                String pkName = ModelParser.GetPrimaryKeyName<T>();   //сделать поиск по атрибуту [PrimaryKey]
                String paramName = "@id";
                command.CommandText = String.Format(commandText, ModelParser.GetTableName<T>(), pkName, paramName);
                MySqlParameter param = new MySqlParameter(paramName, id);
                command.Parameters.Add(param);
                //CommandCreator.GetSelectByIdCommand<User>(id);
                //
                reader = command.ExecuteReader();
                modelObjects = ModelParser.DeparseFrom<T>(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
            return modelObjects.FirstOrDefault<T>();
        }

        public void Dispose()
        {
            if (this.connection != null)
            {
                this.connection.Dispose(); // вызовет Close
                this.connection = null;
            }
        }
        
    }
}
