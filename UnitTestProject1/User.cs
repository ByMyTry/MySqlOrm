using MySqlOrm;

namespace UnitTestProject1
{
    class User
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Region_Id { get; set; }
    }
}
