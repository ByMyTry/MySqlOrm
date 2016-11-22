using System;
using MySql.Data.MySqlClient;

namespace MySqlOrm
{
    class SimpleConnector : IDisposable
    {
        private MySqlConnection connection = null;

        /*private SimpleConnector()
        {
            throw new NotImplementedException();
        }*/

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

        /*public static SimpleConnector CreateConnection(String server, String userId, String password, String dbName)
        {
            return new SimpleConnector(server, userId, password, dbName);
        }*/

        public bool Add<T>(T modelObject)
        {
            int addRecordsCount = 1;
            return addRecordsCount == 1;
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
