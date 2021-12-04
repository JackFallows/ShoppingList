using Microsoft.AspNetCore.Components;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Pages
{
    public partial class List
    {
        [Inject]
        public NavigationManager NavManager { get; set; }

        [Inject]
        public DataService DataService { get; set; }

        [Parameter]
        public string Name { get; set; }

        public Store Store { get; set; }

        public void UpdateEmailFormat(string format)
        {
            Store.EmailFormat = format;
            DataService.SaveChanges();
        }

        public void AddNewItem()
        {
            NavManager.NavigateTo($"/list/{Name}/new");
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Store = DataService.GetStore(Name);
        }
    }
}