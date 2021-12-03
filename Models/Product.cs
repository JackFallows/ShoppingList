using System.Collections.Generic;

namespace ShoppingList.Models
{
    public class Product
    {
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string Preferred { get; set; }
        public List<string> Alternatives { get; set; }
    }
}
