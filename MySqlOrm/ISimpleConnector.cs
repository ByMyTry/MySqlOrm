using System;
using System.Collections.Generic;

namespace MySqlOrm
{
    interface ISimpleConnector
    {
        bool Add<T>(T modelObject);

        bool Update<TId, TModel>(TId id, TModel modelObject);

        bool Remove<T>(T modelObject);

        IEnumerable<T> GetAll<T>();
    }
}
