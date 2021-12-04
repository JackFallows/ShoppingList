using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ShoppingList.Models;

namespace ShoppingList.Services
{
    public class DataService
    {
        private string filePath = @"C:\Users\jack_\Desktop\shoppinglist.json";

        public DataService()
        {
            ReadFromFile();
        }

        public List<Store> Stores { get; set; }

        public Store GetStore(string name)
        {
            return Stores.FirstOrDefault(s => s.Name == name);
        }

        public Store CreateStore(string name)
        {
            var store = new Store { Name = name };
            Stores.Add(store);

            return store;
        }

        public void DeleteStore(string name)
        {
            Stores.Remove(Stores.First(s => s.Name == name));
        }

        public void SaveChanges()
        {
            WriteToFile();
        }

        private void ReadFromFile()
        {
            try
            {
                var content = File.ReadAllText(filePath);
                Stores = JsonConvert.DeserializeObject<List<Store>>(content);
            }
            catch
            {
                Stores = new();
            }
        }

        private void WriteToFile()
        {
            var json = GetJson();
            if (json == null)
            {
                try
                {
                    File.Delete(filePath);
                }
                catch
                {
                    // do nothing
                }
            }
            else
            {
                File.WriteAllText(filePath, json);
            }
        }

        private string GetJson()
        {
            if (Stores.Count == 0)
            {
                return null;
            }

            return JsonConvert.SerializeObject(Stores, Formatting.Indented);
        }
    }
}
