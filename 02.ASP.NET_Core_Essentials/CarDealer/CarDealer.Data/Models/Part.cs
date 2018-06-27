using System.Collections.Generic;

namespace CarDealer.Data.Models
{
    public class Part
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Quantity { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}