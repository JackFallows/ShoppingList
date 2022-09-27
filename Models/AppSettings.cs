using Microsoft.Extensions.Configuration;

namespace ShoppingList.Models
{
    public static class AppSettings
    {
        static AppSettings()
        {
            FileLocation = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("FileLocation");
        }

        public static string FileLocation { get; set; }
    }
}