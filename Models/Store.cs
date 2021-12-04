using System.Collections.Generic;

namespace ShoppingList.Models
{
    public class Store
    {
        public string Name { get; set; }

        public string EmailFormat { get; set; }

        public List<Product> Products { get; set; }
    }
}