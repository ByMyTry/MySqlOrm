# MySqlOrm Tutorial
#### Введение
**MySqlOrm** – библиотека написанная мной для упрощения использования базы данных **MySQL** из кода **C#**, по причине запрета на использование стандартных реализаций **ORM**. В сущности, представляет из себя простейшую реализацию **ORM**, ориентированную на работу с **MVC** приложениями. Среди её основных функций стоит отметить _парсинг_ (в SQL) и _депарсинг_ (из SQL) моделей, а также _создание простейших SQL скриптов_ для работы с данными в соответствующих таблицах БД. Однако, напрямую использовать их не придется, поскольку основную логику работы инкапсулирует класс `SimpleConnector`. Он же отвечает за подключения к базе данных.
#### Установка
Используемая IDE - Microsoft Visual Studio 2015. 
Рекомендуемая версия .NET Framework -  4.5.2. Корректность работы на других версиях .NET не установлена. После создания проекта сделайте следующее:
1) Для началу необходимо установить NuGet пакет для работы c базой данных MySQL через функционал предоставляемый ADO.NET. `Project -> Manage NuGet pakages... -> Browse -> MySql.Data -> Install`
2) Далее необходимо добавить в проект ссылку на MySqlOrm.dll в ваш проект. `Project -> Add Reference... -> Browse -> ...\MySqlOrm.dll -> Ok`
#### Подготовка
Данная библиотека предоставляет интерфейс, позволяющий работать с данными в БД, но не с её структурой. Другими словами, саму _базу_ и _таблицы_ в ней _придется создавать вручную_.
###### Создание БД
Давайте создадим базу с именем _test_db_ и добавим в неё таблицу _users_ с полями _id_, _name_ и _age_.
```sql
create database test_db;
use test_db;
CREATE TABLE users(
	id INT PRIMARY KEY AUTO_INCREMENT,
	name VARCHAR(10),
	age INT
) ENGINE INNODB;
```
![](https://github.com/ByMyTry/MySqlOrm/blob/master/create.png?raw=true)
###### Создание Моделей
Создадим в проекте папку _Models_ и добавим в неё модель таблицы базы данных. Названия свойств модели и полей таблицы должны совпадать с точность до регистра символов.  Атрибутом `PrimaryKey` необходимо помечать соответствующие свойства класса модели, если такие имеются.
```cs
class User
{
    [PrimaryKey]
    public int Id {get; set;}
    
    public string Name {get; set;}
    
    public int Age {get; set;}
}
```
#### Использование
 Cоздим экземпляр класса `SimpleConnector`, который установит соединения с базой данных. Данный класс имеет четыре параметра конструктора, а именно:
* имя сервера бд
* имя пользователя
* пароль
* имя базы данных
```cs
using MySqlOrm;
using ProjectName.Models;
...
SimpleConnector simpleConnector = new SimpleConnector(
     "localhost",
     "root",
     "1111",
    "test_db"
);
...
```
###### Добавление
Добавим запись в таблицу users.
```cs
User user = new User() { Name = "user", Age = 19};
User userWithId = simpleConnector.Add<User>(user);
```
После добавления в таблицу записи метод вернет объект класса `User` с полем `Id`, равным значению присвоенному базой данных.
###### Удаление
Удалим только что добавленного user'а. Обратите внимание, что полю `Id` значение не присвоено.
```cs
User user = new User() { Name = "user", Age = 19};
bool success = simpleConnector.Remove<User>(user);
```
Сделать это можно и по-другому, используя `Id` user'a.
```cs
User user = new User() { Name = "user", Age = 19};
User userWithId = simpleConnector.Add<User>(user);
bool success = simpleConnector.RemoveById<User>(userWithId.Id);
```
Если запись была удалена, метод вернет `true`.
###### Обновление
С обновлением немного сложнее, тут поле `Id` должно обязательно присутствовать.
```cs
User user = new User() { Name = "user", Age = 19};
User userWithId = simpleConnector.Add<User>(user);
userWithId.Age = 20;
bool success = simpleConnector.Update<User>(userWithId);
```
При успешном обновлении метод вернет `true`.
###### Поиск
Получим список всех user'ов.
```cs
IEnumerable<User> users = simpleConnector.GetAll<User>();
```
Либо же, можно получить конкретного user'а по `Id`.
```cs
User user = new User() { Name = "user", Age = 19};
User userWithId = simpleConnector.Add<User>(user);
User sameUser = simpleConnector.GetById<User>(userWithId.Id);
```
###### Завершение работы
После последнего использования необходимо вызвать метод `Dispose` у объекта `SimpleConnector`.
```cs
simpleConnector.Dispose();
```
Либо можно использовать конструкцию `using`.
#### Ссылки
Ознакомиться с кодом можно [здесь](https://github.com/ByMyTry/MySqlOrm).
Вопросы, замечания, предложения пишите мне на почту anton.taleckij@gmail.com.