namespace MySqlOrm.Models
{
    class User
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }
    }
}
