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
            //throw new NotImplementedException();
            MySqlDataReader reader = null;
            IEnumerable<T> res = null;
            ModelParser<T> mp = new ModelParser<T>();
            try
            {
                //
                String commandText = "SELECT * FROM {0};";
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
                command.CommandText = String.Format(commandText, mp.tableName);
                //
                reader = command.ExecuteReader();
                res = mp.DeparseFrom(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
            return res;
        }

        public T GetById<T>(Object id)
        {
            //throw new NotImplementedException();
            MySqlDataReader reader = null;
            IEnumerable<T> res = null;
            ModelParser<T> mp = new ModelParser<T>();
            try
            {
                //
                String commandText = "SELECT * FROM {0} WHERE {1} = {2};";
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
                String pkName = "id";   //сделать поиск по атрибуту [PrimaryKey]
                String paramName = "@id";
                command.CommandText = String.Format(commandText, mp.tableName, pkName, paramName);
                MySqlParameter param = new MySqlParameter(paramName, id);
                command.Parameters.Add(param);
                 //
                reader = command.ExecuteReader();
                res = mp.DeparseFrom(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
            return res.FirstOrDefault<T>();
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
