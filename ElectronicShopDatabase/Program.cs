using ElectronicShopDatabase.Data;
using ElectronicShopDatabase.Models;

namespace ElectronicShopDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the database context
            ShopContext context = new ShopContext();

            // Create the initial products
            List<Product> products = new List<Product>
            {
                new Product("Gaming Laptop", "Computer", 12500m),
                new Product("Office Laptop", "Computer", 7500m),
                new Product("Gaming Mus", "Tilbehør", 650m),
                new Product("Keyboard", "Tilbehør", 1100m),
                new Product("4K Skærm", "Skærm", 4500m),
                new Product("Gaming Headset", "Tilbehør", 1500m),
                new Product("27\" Gaming Skærm", "Skærm", 3500m),
                new Product("USB-C Dock", "Tilbehør", 1800m),
                new Product("MacBook Air", "Computer", 9500m),
                new Product("Gaming PC", "Computer", 15000m),
                new Product("Webkamera", "Tilbehør", 850m),
                new Product("32\" 4K Skærm", "Skærm", 5500m)
            };

            // Only runs if there's no products in the database // We don't want duplicates
            if (!context.Products.Any())
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            // Find all computers
            var computers = context.Products.Where(product => product.Category == "Computer").ToList();

            Console.WriteLine("\n=== Computers ===");

            foreach (Product product in computers)
            {
                Console.WriteLine($"{product.Name} - {product.Price} kr.");
            }


            // Find all products above 5000 kr.
            var expensiveProducts = context.Products.Where(product => product.Price > 5000m).ToList();

            Console.WriteLine("\n=== Products above 5000 kr. ===");

            foreach (Product product in expensiveProducts)
            {
                Console.WriteLine($"{product.Name} - {product.Price} kr.");
            }


            // Find all products between 1000 and 5000 kr.
            var midRangeProducts = context.Products.Where(product => product.Price >= 1000m && product.Price <= 5000m).ToList();

            Console.WriteLine("\n=== Products between 1000 and 5000 kr. ===");

            foreach (Product product in midRangeProducts)
            {
                Console.WriteLine($"{product.Name} - {product.Price} kr.");
            }


            // Find all accessories above 1000 kr.
            var expensiveAccessories = context.Products.Where(product => product.Category == "Tilbehør" && product.Price > 1000m).ToList();

            Console.WriteLine("\n=== Accessories above 1000 kr. ===");

            foreach (Product product in expensiveAccessories)
            {
                Console.WriteLine($"{product.Name} - {product.Price} kr.");
            }


            // Find all products with "Gaming" in the name
            var gamingProducts = context.Products.Where(product => product.Name.Contains("Gaming")).ToList();

            Console.WriteLine("\n=== Gaming products ===");

            foreach (Product product in gamingProducts)
            {
                Console.WriteLine($"{product.Name} - {product.Price} kr.");
            }

            // Create
            Product gamingKeyboard = new Product("Gaming Keyboard", "Tilbehør", 1200m);

            context.Products.Add(gamingKeyboard);

            context.SaveChanges();

            // Read
            List<Product> allProducts = context.Products.ToList();

            Console.WriteLine("\n=== All Products ===");

            foreach (Product product in allProducts)
            {
                Console.WriteLine($"{product.Id}: {product.Name} - {product.Category} - {product.Price} kr.");
            }

            // Update
            Product? gamingKeyboardToUpdate = context.Products.FirstOrDefault(product => product.Name == "Gaming Keyboard");

            if (gamingKeyboardToUpdate != null)
            {
                gamingKeyboardToUpdate.Price = 1350m;

                context.SaveChanges();
            }

            // Delete
            Product? gamingKeyboardToDelete = context.Products.FirstOrDefault(product => product.Name == "Gaming Keyboard");

            if (gamingKeyboardToDelete != null)
            {
                context.Products.Remove(gamingKeyboardToDelete);
                context.SaveChanges();
            }

            List<Product> productsAfterDelete = context.Products.ToList();

            Console.WriteLine("\n=== Products After Delete ===");

            foreach (Product product in productsAfterDelete)
            {
                Console.WriteLine($"{product.Id}: {product.Name} - {product.Category} - {product.Price} kr.");
            }

            // Add UnitsSold values to the products
            List<Product> productsToUpdate = context.Products.ToList();

            foreach (Product product in productsToUpdate)
            {
                if (product.Name == "Gaming Laptop")
                    product.UnitsSold = 18;

                else if (product.Name == "Office Laptop")
                    product.UnitsSold = 24;

                else if (product.Name == "Gaming Mus")
                    product.UnitsSold = 65;

                else if (product.Name == "Keyboard")
                    product.UnitsSold = 42;

                else if (product.Name == "4K Skærm")
                    product.UnitsSold = 27;

                else if (product.Name == "Gaming Headset")
                    product.UnitsSold = 38;

                else if (product.Name == "27\" Gaming Skærm")
                    product.UnitsSold = 31;

                else if (product.Name == "USB-C Dock")
                    product.UnitsSold = 45;

                else if (product.Name == "MacBook Air")
                    product.UnitsSold = 21;

                else if (product.Name == "Gaming PC")
                    product.UnitsSold = 14;

                else if (product.Name == "Webkamera")
                    product.UnitsSold = 52;

                else if (product.Name == "32\" 4K Skærm")
                    product.UnitsSold = 19;
            }

            context.SaveChanges();

            // Group products by category and calculate total units sold
            var salesByCategory = context.Products.GroupBy(product => product.Category).Select(group => new { Category = group.Key, TotalUnitsSold = group.Sum(product => product.UnitsSold) }).ToList();

            Console.WriteLine("\n=== Units Sold By Category ===");

            foreach (var category in salesByCategory)
            {
                Console.WriteLine($"{category.Category}: {category.TotalUnitsSold} units sold");
            }

            // Display units sold as a bar chart // 1 per 5th product
            Console.WriteLine("\n=== Sales Chart ===");

            foreach (var category in salesByCategory)
            {
                int barLength = category.TotalUnitsSold / 5;

                Console.Write($"{category.Category}: ");

                for (int i = 0; i < barLength; i++)
                {
                    Console.Write("#");
                }

                Console.WriteLine($" {category.TotalUnitsSold}");
            }

            // Add schema-flexible specifications to laptops and screens

            // Laptops
            Product? gamingLaptop = context.Products.FirstOrDefault(product => product.Name == "Gaming Laptop");

            if (gamingLaptop != null)
            {
                gamingLaptop.Specifications = new Dictionary<string, string>
                {
                    { "brand", "Lenovo" },
                    { "processor", "Intel i7" },
                    { "ram", "16 GB" },
                    { "storage", "1 TB" }
                };
            }

            Product? officeLaptop = context.Products.FirstOrDefault(product => product.Name == "Office Laptop");

            if (officeLaptop != null)
            {
                officeLaptop.Specifications = new Dictionary<string, string>
                {
                    { "brand", "Dell" },
                    { "processor", "Intel i5" },
                    { "ram", "16 GB" },
                    { "storage", "512 GB" }
                };
            }

            Product? macBookAir = context.Products.FirstOrDefault(product => product.Name == "MacBook Air");

            if (macBookAir != null)
            {
                macBookAir.Specifications = new Dictionary<string, string>
                {
                    { "brand", "Apple" },
                    { "processor", "Apple M3" },
                    { "ram", "16 GB" },
                    { "storage", "512 GB" }
                };
            }


            // Screens
            Product? screen4K = context.Products.FirstOrDefault(product => product.Name == "4K Skærm");

            if (screen4K != null)
            {
                screen4K.Specifications = new Dictionary<string, string>
                {
                    { "brand", "Samsung" },
                    { "resolution", "3840x2160" },
                    { "size", "27 inches" },
                    { "refreshRate", "60 Hz" }
                };
            }

            Product? gamingScreen = context.Products.FirstOrDefault(product => product.Name == "27\" Gaming Skærm");

            if (gamingScreen != null)
            {
                gamingScreen.Specifications = new Dictionary<string, string>
                {
                    { "brand", "Lenovo" },
                    { "resolution", "2560x1440" },
                    { "size", "27 inches" },
                    { "refreshRate", "165 Hz" }
                };
            }

            Product? screen32 = context.Products.FirstOrDefault(product => product.Name == "32\" 4K Skærm");

            if (screen32 != null)
            {
                screen32.Specifications = new Dictionary<string, string>
                {
                    { "brand", "LG" },
                    { "resolution", "3840x2160" },
                    { "size", "32 inches" },
                    { "refreshRate", "144 Hz" }
                };
            }

            context.SaveChanges();

            // Find products where the brand specification is Lenovo
            var lenovoProducts = context.Products.AsEnumerable().Where(product => product.Specifications != null && product.Specifications.ContainsKey("brand") && product.Specifications["brand"] == "Lenovo").ToList();

            Console.WriteLine("\n=== Lenovo Products ===");

            foreach (Product product in lenovoProducts)
            {
                Console.WriteLine($"{product.Name} - {product.Specifications!["brand"]}");
            }
        }
    }
}
