using AkoStoreUpdate.Database;
using AkoStoreUpdate.Enum;
using AkoStoreUpdate.Exceptions;
using AkoStoreUpdate.Models;
using AkoStoreUpdate.Sevices;

Product product1 = new Product
{
    Id = Guid.NewGuid(),
    Name = "Laptop",
    Price = 1200,
    Description = "A high-performance laptop for gaming and work."
};
Product product2 = new Product
{
    Id = Guid.NewGuid(),
    Name = "Smartphone",
    Price = 800,
    Description = "A latest model smartphone with advanced features."
};
Product product3 = new Product
{
    Id = Guid.NewGuid(),
    Name = "Headphones",
    Price = 200,
    Description = "Noise-cancelling over-ear headphones."
};
Product product4 = new Product
{
    Id = Guid.NewGuid(),
    Name = "Smartwatch",
    Price = 300,
    Description = "A smartwatch with fitness tracking features."
};


Order order = new Order
{
    Id = Guid.NewGuid(),
    Products = new List<Product> { product1, product2 },
    TotalPrice = 0
};
Order order2 = new Order
{
    Id = Guid.NewGuid(),
    Products = new List<Product> { product3, product4 },
    TotalPrice = 0
};


Customer customer = new Customer
{
    Id = Guid.NewGuid(),
    Name = "John",
    Surname = "Doe",
    Orders = new List<Order> { order, order2 }
};



AkoStoreDatabase database = new AkoStoreDatabase();
ProductService productService = new ProductService(database);
OrderService orderService = new OrderService(database);
CustomersService customersService = new CustomersService(database);

database.Products.Add(product1);
database.Products.Add(product2);
database.Products.Add(product3);
database.Products.Add(product4);
database.Customers.Add(customer);
database.Orders.Add(order);
database.Orders.Add(order2);


while (true)
{
    try
    {
        bool loggedIn = false;
        string username = "";
        string password = "";
        while (!loggedIn)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the AkoStore");
            Console.WriteLine("=========================================");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Forget Password");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=========================================");
            Console.Write("Select option: ");
            string loginChoice = Console.ReadLine();

            if (loginChoice == "0")
                return;

            if (loginChoice == "3")
            {
                Console.WriteLine("Beta version of Forget Password feature is not available yet.");
                Console.ReadKey(true);
                Console.WriteLine("Press any key to continue..."); continue;
            }

            if (loginChoice == "2")
            {
                Console.Write("Choose a username: ");
                string newUsername = Console.ReadLine();
                Console.Write("Choose a password: ");
                string newPassword = Console.ReadLine();                
                Console.Write("Choose a Email: ");
                string newEmail = Console.ReadLine();
                LoginService loginService = new LoginService();
                RegisterService registerService = new RegisterService();
                var registered = registerService.Register(newUsername, newPassword, newEmail);
                if (registered == RegisterResult.Success)
                {
                    Console.WriteLine("Registration successful. You can now log in.");
                }
                else if (registered == RegisterResult.UsernameTaken)
                {
                    var ex = new UsernameIsAlreadyTakenException();
                    Console.WriteLine(ex.Message);
                }
                else if (registered == RegisterResult.EmailTaken)
                {
                    var ex = new EmailIsAlreadyRegisteredException();
                    Console.WriteLine(ex.Message);
                }
                else if (registered == RegisterResult.Error)
                {
                    var ex = new UsernameIsAlreadyTakenException();
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    UsernameMIghtBeTakenException ex = new UsernameMIghtBeTakenException();
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                continue;
            }

            if (loginChoice == "1")
            {
                Console.Write("Username: ");
                username = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();

                LoginService loginService = new LoginService();
                if (loginService.Login(username, password))
                {
                    Console.WriteLine($"\nWelcome {loginService.GetCurrentUser()}!");
                    loggedIn = true;
                    Console.ReadKey(true);
                    Console.WriteLine("Press any key to continue...");
                }
                else
                {
                    Console.WriteLine("\nInvalid username or password. Please try again.");
                    Console.WriteLine("Press any key to re-enter credentials...");
                    Console.ReadKey(true);
                }
            }
        }
        bool isMenuActive = true;
        while (isMenuActive)
        {
            Console.Clear();
            Console.WriteLine("Welcome StoreApp");
            Console.WriteLine("1. Show all products");
            Console.WriteLine("2. Add product");
            Console.WriteLine("3. Update product");
            Console.WriteLine("4. Delete product");
            Console.WriteLine("5. Show all customers");
            Console.WriteLine("6. Show all orders");
            Console.WriteLine("7. Add order");
            Console.WriteLine("8. Update order");
            Console.WriteLine("9. Delete order");
            Console.WriteLine("0. Logout");
            Console.WriteLine("Please select an option (0-9):");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char choice = keyInfo.KeyChar;

            try
            {
                switch (choice)
                {
                    case '1':
                        Console.WriteLine("\nAll Products:");
                        var allProducts = productService.GetAll();
                        if (allProducts.Count == 0)
                        {
                            Console.WriteLine(new ProductNotFoundException().Message);
                        }
                        else
                        {
                            foreach (var prod in allProducts)
                            {
                                Console.WriteLine($"ID: {prod.Id}");
                                Console.WriteLine($"Name: {prod.Name}");
                                Console.WriteLine($"Price: {prod.Price}");
                                Console.WriteLine($"Description: {prod.Description}");
                                Console.WriteLine(new string('-', 25));
                            }
                        }
                        break;
                    case '2':
                        Console.Write("\nProduct Name: ");
                        string name = Console.ReadLine();
                        float price;
                        while (true)
                        {
                            Console.Write("Price: ");
                            if (float.TryParse(Console.ReadLine(), out price) && price > 0) break;
                            Console.WriteLine(new InvalidPriceException().Message + " Please enter a valid price: ");
                        }
                        Console.Write("Description: ");
                        string desc = Console.ReadLine();

                        productService.Add(new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = name,
                            Price = price,
                            Description = desc
                        });
                        Console.WriteLine("Product added!");
                        break;
                    case '3':
                        Console.Write("\nEnter the ID of the product to update: ");
                        Guid idToUpdate;
                        while (!Guid.TryParse(Console.ReadLine(), out idToUpdate))
                        {
                            Console.WriteLine("Invalid GUID! Try again:");
                        }
                        Product productToUpdate = productService.GetById(idToUpdate);
                        if (productToUpdate == null)
                        {
                            Console.WriteLine(new ProductNotFoundException().Message);
                            break;
                        }
                        Console.Write("New Name: ");
                        string newName = Console.ReadLine();
                        float newPrice;
                        while (true)
                        {
                            Console.Write("New Price: ");
                            if (float.TryParse(Console.ReadLine(), out newPrice) && newPrice > 0) break;
                            Console.WriteLine(new InvalidPriceException().Message + " Please enter a valid price: ");
                        }
                        Console.Write("New Description: ");
                        string newDesc = Console.ReadLine();
                        productToUpdate.Name = newName;
                        productToUpdate.Price = newPrice;
                        productToUpdate.Description = newDesc;
                        productService.Update(productToUpdate);
                        Console.WriteLine("Product updated successfully!");
                        break;
                    case '4':
                        try
                        {
                            Console.Write("\nEnter the ID of the product to delete: ");
                            Guid idToDelete;
                            while (!Guid.TryParse(Console.ReadLine(), out idToDelete))
                            {
                                Console.WriteLine("Invalid GUID! Try again:");
                            }
                            productService.Delete(idToDelete);
                            Console.WriteLine("Product deleted!");
                        }
                        catch (ProductNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case '5':
                        Console.WriteLine("\nAll Customers:");
                        foreach (var cust in customersService.GetAll())
                        {
                            Console.WriteLine($"ID: {cust.Id}");
                            Console.WriteLine($"Name: {cust.Name}");
                            Console.WriteLine($"Surname: {cust.Surname}");
                            Console.WriteLine($"Order Count: {cust.Orders?.Count ?? 0}");
                            Console.WriteLine(new string('-', 25));
                        }
                        break;
                    case '6':
                        Console.WriteLine("\nAll Orders");
                        foreach (var ord in orderService.GetAll())
                        {
                            float totall = 0;
                            foreach (var prod in ord.Products)
                                totall += (float)prod.Price;
                            Console.WriteLine($"Order ID: {ord.Id}");
                            Console.WriteLine($"Total Price: {totall}");
                            Console.WriteLine("Products in this order:");
                            foreach (var prod in ord.Products)
                                Console.WriteLine($"- {prod.Name} (${prod.Price})");
                            Console.WriteLine(new string('-', 25));
                        }
                        break;
                    case '7':
                        Console.WriteLine("\nAdding a new order...\nEnter the IDs of products (min 1 max 10, comma separated): ");
                        string productIdsInput = Console.ReadLine();
                        List<Product> products = new List<Product>();
                        foreach (var id in productIdsInput.Split(','))
                        {
                            if (Guid.TryParse(id.Trim(), out Guid productId))
                            {
                                Product product = productService.GetById(productId);
                                if (product != null)
                                {
                                    products.Add(product);
                                }
                                else
                                {
                                    Console.WriteLine($"Product with ID {productId} not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Invalid GUID: {id.Trim()}");
                            }
                        }

                        if (products.Count < 1)
                        {
                            Console.WriteLine("You must add at least 1 product!");
                            break;
                        }
                        if (products.Count > 10)
                        {
                            Console.WriteLine("You can add at most 10 products to an order!");
                            break;
                        }

                        float total = 0;
                        foreach (var p in products)
                            total += (float)p.Price;

                        Order newOrder = new Order
                        {
                            Id = Guid.NewGuid(),
                            Products = products,
                            TotalPrice = total
                        };
                        orderService.Add(newOrder);
                        Console.WriteLine("Order added successfully!");
                        break;
                    case '8':
                        Console.Write("\nEnter the ID of the order to update: ");
                        Guid orderIdToUpdate;
                        while (!Guid.TryParse(Console.ReadLine(), out orderIdToUpdate))
                        {
                            Console.WriteLine("Invalid GUID! Try again:");
                        }
                        Order orderToUpdate = orderService.GetById(orderIdToUpdate);
                        if (orderToUpdate == null)
                        {
                            Console.WriteLine("Order not found!");
                            break;
                        }
                        Console.WriteLine("Enter new product IDs (min 1 max 10, comma separated): ");
                        string newProductIdsInput = Console.ReadLine();
                        List<Product> newProducts = new List<Product>();
                        foreach (var productIdString in newProductIdsInput.Split(','))
                        {
                            if (Guid.TryParse(productIdString.Trim(), out Guid productId))
                            {
                                Product productItem = productService.GetById(productId);
                                if (productItem != null)
                                {
                                    newProducts.Add(productItem);
                                }
                                else
                                {
                                    Console.WriteLine($"Product with ID {productId} not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Invalid GUID: {productIdString.Trim()}");
                            }
                        }
                        if (newProducts.Count < 1)
                        {
                            Console.WriteLine("You must add at least 1 product!");
                            break;
                        }
                        if (newProducts.Count > 10)
                        {
                            Console.WriteLine("You can add at most 10 products to an order!");
                            break;
                        }
                        orderToUpdate.Products = newProducts;
                        orderToUpdate.TotalPrice = 0;
                        foreach (var prod in newProducts)
                        {
                            orderToUpdate.TotalPrice += prod.Price;
                        }
                        orderService.Update(orderToUpdate);
                        Console.WriteLine("Order updated successfully!");
                        break;
                    case '9':
                        Console.Write("\nEnter the ID of the order to delete: ");
                        Guid orderIdToDelete;
                        while (!Guid.TryParse(Console.ReadLine(), out orderIdToDelete))
                        {
                            Console.WriteLine("Invalid GUID! Try again:");
                        }
                        orderService.Delete(orderIdToDelete);
                        if (orderService.GetById(orderIdToDelete) == null)
                        {
                            Console.WriteLine("Order successfully deleted.");
                        }
                        break;
                    case '0':
                        Console.WriteLine("\nLogging out...");
                        isMenuActive = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option! Please select a number between 0 and 9.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Critical error: {ex.Message}");
        Console.WriteLine("Restarting application...");
        Console.ReadKey(true);
    }
}
