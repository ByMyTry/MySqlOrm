using System;
using System.Collections.Generic;

namespace MySqlOrm
{
    interface ISimpleConnector
    {
        T Add<T>(T modelObject);//id?

        bool Update<TModel>(TModel modelObject);//id через атрибут

        bool RemoveById<T>();

        IEnumerable<T> GetAll<T>();

        T GetById<T>(Object id);
    }
}
