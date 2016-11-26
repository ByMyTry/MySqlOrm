using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;

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

        public T Add<T>(T modelObject)//id?
        {
            MySqlCommand command = CommandCreator.AddCommand<T>(modelObject);
            /*MySqlParameter idParam = new MySqlParameter
            {
                ParameterName = "@id",
                MySqlDbType = MySqlDbType.Int32, 
                Direction = ParameterDirection.Output // параметр выходной
            };
            command.Parameters.Add(idParam);*/
            command.Connection = this.connection;
            command.ExecuteNonQuery();
            //ModelParser.SetPrimaryKey<T>(modelObject,idParam);

            return modelObject;
        }

        public bool Update<T>(T modelObject)//id через атрибут
        {
            throw new NotImplementedException();
        }

        public bool RemoveById<T>(Object id)
        {
            MySqlCommand command = CommandCreator.RemoveByIdCommand<T>(id);
            command.Connection = this.connection;
            int cod = command.ExecuteNonQuery();
            return cod == 1;
        }

        public IEnumerable<T> GetAll<T>()
        {
            MySqlDataReader reader = null;
            IEnumerable<T> modelObjects = null;
            try
            {
                MySqlCommand command = CommandCreator.SelectCommand<T>();
                command.Connection = this.connection;
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
                MySqlCommand command = CommandCreator.SelectByIdCommand<T>(id);
                command.Connection = this.connection;
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
                this.connection.Dispose(); // вызовет Close; Чистый Close нужен чтобы потом Open
                this.connection = null;
            }
        }
        
    }
}
