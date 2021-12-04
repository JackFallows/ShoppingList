using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Pages
{
    public partial class List
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public DataService DataService { get; set; }

        [Parameter]
        public string Name { get; set; }

        public Store Store { get; set; }

        public List<Product> ActiveProducts => Store.Products.Where(p => p.Active).ToList();

        public IEnumerable<IGrouping<string, Product>> CategorisedProducts => Store.Products.GroupBy(p => p.CategoryActual);

        public void UpdateEmailFormat(string format)
        {
            Store.EmailFormat = format;
            DataService.SaveChanges();
        }

        public void ToggleItemActive(Product product)
        {
            product.Active = !product.Active;
            DataService.SaveChanges();
        }

        public void ChangeItemQuantity(Product product, int quantity)
        {
            product.Quantity = quantity;
            DataService.SaveChanges();
        }

        public void AddNewItem(string category = null)
        {
            var queryString = category == null ? string.Empty : $"?category={category}";
            NavManager.NavigateTo($"/list/{Name}/new{queryString}");
        }

        public async void GetList()
        {
            var properties = typeof(Product).GetProperties();
            var propertyNames = properties.Select(p => p.Name).ToList();

            var lines = new List<string>();
            foreach (var product in ActiveProducts)
            {
                var itemStr = Store.EmailFormat;
                foreach (var p in propertyNames)
                {
                    itemStr = itemStr.Replace($"{{{{{p}}}}}", typeof(Product).GetProperty(p).GetValue(product)?.ToString() ?? string.Empty);
                }

                lines.Add(itemStr);
            }

            await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", string.Join(Environment.NewLine, lines));
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Store = DataService.GetStore(Name);
        }
    }
}