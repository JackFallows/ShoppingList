using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Pages
{
    public partial class Index
    {
        [Inject]
        public DataService DataService { get; set; }

        public string NewStore = string.Empty;
        public string ValidationError = string.Empty;

        public void CreateStore()
        {
            ValidateStore(NewStore, null, () =>
            {
                DataService.CreateStore(NewStore);
                DataService.SaveChanges();

                NewStore = string.Empty;
            });
        }

        public void UpdateStore(Store store, string newName)
        {
            ValidateStore(newName, store, () =>
            {
                store.Name = newName;
                DataService.SaveChanges();
            });
        }

        public void RemoveStore(string name)
        {
            DataService.DeleteStore(name);
            DataService.SaveChanges();
            ClearValidation();
        }

        public void ClearValidation()
        {
            ValidationError = string.Empty;
        }

        private void ValidateStore(string newName, Store store, Action callback)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                ValidationError = "Store must have a name.";
                return;
            }

            if (store == null)
            {
                var existing = DataService.GetStore(newName);
                if (existing != null)
                {
                    ValidationError = "That store already exists.";
                    return;
                }
            }

            if (store == null || (store != null && store.Name != newName))
            {
                callback();
            }
        }
    }
}
