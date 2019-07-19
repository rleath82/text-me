using System;
using SQLite;

namespace TextMe.Models
{
    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
