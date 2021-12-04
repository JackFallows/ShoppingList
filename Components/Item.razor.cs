using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Components
{
    public partial class Item
    {
        [Inject]
        public DataService DataService { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public string StoreName { get; set; }

        [Parameter]
        public string ItemName { get; set; }

        public Store Store { get; set; }

        public Product Product { get; set; }

        public bool IsNew { get; set; }

        public string ValidationError { get; set; }

        public void Save()
        {
            if (string.IsNullOrWhiteSpace(Product.ProductName))
            {
                ValidationError = "Product must have a name.";
                return;
            }

            if (IsNew)
            {
                if (Store.Products == null)
                {
                    Store.Products = new List<Product>();
                }
                else if (Store.Products.Any(p => p.ProductName == Product.ProductName))
                {
                    ValidationError = "A product with that name has already been added.";
                    return;
                }
                
                Store.Products.Add(Product);
            }

            DataService.SaveChanges();

            NavManager.NavigateTo($"/list/{StoreName}");
        }

        public void ClearValidation()
        {
            ValidationError = null;
        }

        protected override void OnParametersSet()
        {
            Store = DataService.GetStore(StoreName);
            var existing =Store.Products?.FirstOrDefault(p => p.ProductName == ItemName);
            if (existing == null)
            {
                IsNew = true;
            }

            Product = existing ?? new Product();
        }
    }
}