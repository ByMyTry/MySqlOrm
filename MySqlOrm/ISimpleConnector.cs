using System;
using System.Collections.Generic;

namespace MySqlOrm
{
    interface ISimpleConnector
    {
        T Add<T>(T modelObject);

        bool Update<T>(T modelObject);

        bool Remove<T>(T modelObject);

        bool RemoveById<T>(Object id);

        IEnumerable<T> GetAll<T>();

        T GetById<T>(Object id);
    }
}
