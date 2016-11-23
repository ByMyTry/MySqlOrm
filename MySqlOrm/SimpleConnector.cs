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

        public bool Remove<T>(T modelObject)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
            //MySqlDataReader rdr = null;
            /*try
            {
                MySqlCommand command = new MySqlCommand();
                command.Connection = this.connection;
            }*/
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
