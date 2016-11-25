using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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
            try
            {
                String commandText = "SELECT * FROM {0}";
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
                command.CommandText = String.Format(commandText, typeof(T).Name.ToLower() + "s");
                reader = command.ExecuteReader();
                res = new ModelParser<T>().ParseFrom(reader);
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
            return res;
        }

        public T GetById<T>()
        {
            throw new NotImplementedException();
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
