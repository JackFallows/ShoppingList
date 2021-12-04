using System.Collections.Generic;
using Newtonsoft.Json;

namespace ShoppingList.Models
{
    public class Product
    {
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public int Quantity { get; set; } = 1;
        public string Preferred { get; set; }
        public List<string> Alternatives { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public string QuantityStr => Quantity == 1 ? string.Empty : $" x{Quantity}";
    }
}
