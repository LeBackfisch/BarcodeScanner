using System;
using SQLite;

namespace SQLTest.Model
{
    public class ScannedItemModel
    {
        
        public string Name { get; set; }
        public Attribute Attributes { get; set; }
        public DateTime DateSearched { get; set; }
    }

    public class Attribute
    {
        public string Brand { get; set; }
    }

    public class ItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime DateSearched { get; set; }

        public override string ToString()
        {
            return $"Name : {Name}, Brand : {Brand}, Date Searched: {DateSearched.Date}";
        }
    }
}
