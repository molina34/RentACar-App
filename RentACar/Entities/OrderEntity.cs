using System;
using SQLite;

namespace RentACar.Entities
{
    public class OrderEntity
    {
        [PrimaryKey, AutoIncrement] 
        public int? Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string Price { get; set; }
    }
}