using System.Collections.Generic;

namespace MySqlOrm
{
    interface ISimpleConnector
    {
        T Add<T>(T modelObject);//id?

        bool Update<TModel>(TModel modelObject);//id через атрибут

        bool Remove<T>(T modelObject);

        IEnumerable<T> GetAll<T>();

        T GetById<T>();
    }
}
