using System.Text.Json;

class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
}

class CatFood : Product
{
    public double WeightPounds { get; set; }
    public bool KittenFood { get; set; }
}

class DogLeash : Product
{
    public int LengthInches { get; set; }
    public string Material { get; set; }
}

class ProductLogic
{
    private List<Product> _products;
    private Dictionary<string, DogLeash> _dogLeashDictionary;
    private Dictionary<string, CatFood> _catFoodDictionary;

    public ProductLogic()
    {
        _products = new List<Product>();
        _dogLeashDictionary = new Dictionary<string, DogLeash>();
        _catFoodDictionary = new Dictionary<string, CatFood>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);

        if (product is DogLeash dogLeash)
        {
            _dogLeashDictionary[dogLeash.Name] = dogLeash;
        }
        else if (product is CatFood catFood)
        {
            _catFoodDictionary[catFood.Name] = catFood;
        }
    }

    public List<Product> GetAllProducts()
    {
        return _products;
    }

    public DogLeash GetDogLeashByName(string name)
    {
        try
        {
            if (_dogLeashDictionary.ContainsKey(name))
            {
                return _dogLeashDictionary[name];
            }
        }
        catch (Exception ex)
        {
            return null;
        }

        if (_dogLeashDictionary.ContainsKey(name))
        {
            return _dogLeashDictionary[name];
        }
        return null; // Return null if the DogLeash is not found by name.
    }
}

class Program
{
    static void Main()
    {
        var productLogic = new ProductLogic();

        Console.WriteLine("Press 1 to add a product");
        Console.WriteLine("Press 2 to get a DogLeash by name");
        Console.WriteLine("Type 'exit' to quit");

        string userInput = Console.ReadLine();

        while (userInput.ToLower() != "exit")
        {
            if (userInput == "1")
            {
                Console.WriteLine("Enter product type (1 for CatFood, 2 for DogLeash):");
                int productType = int.Parse(Console.ReadLine());

                Product product = null;

                if (productType == 1)
                {
                    CatFood catFood = new CatFood();
                    product = catFood;

                    Console.Write("Enter CatFood Name: ");
                    catFood.Name = Console.ReadLine();

                    Console.Write("Enter CatFood Price: ");
                    catFood.Price = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter CatFood Quantity: ");
                    catFood.Quantity = int.Parse(Console.ReadLine());

                    Console.Write("Enter CatFood Description: ");
                    catFood.Description = Console.ReadLine();

                    Console.Write("Enter CatFood Weight (in pounds): ");
                    catFood.WeightPounds = double.Parse(Console.ReadLine());

                    Console.Write("Is it Kitten Food? (true or false): ");
                    catFood.KittenFood = bool.Parse(Console.ReadLine());
                }
                else if (productType == 2)
                {
                    DogLeash dogLeash = new DogLeash();
                    product = dogLeash;

                    Console.Write("Enter DogLeash Name: ");
                    dogLeash.Name = Console.ReadLine();

                    Console.Write("Enter DogLeash Price: ");
                    dogLeash.Price = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter DogLeash Quantity: ");
                    dogLeash.Quantity = int.Parse(Console.ReadLine());

                    Console.Write("Enter DogLeash Description: ");
                    dogLeash.Description = Console.ReadLine();

                    Console.Write("Enter DogLeash Length (in inches): ");
                    dogLeash.LengthInches = int.Parse(Console.ReadLine());

                    Console.Write("Enter DogLeash Material: ");
                    dogLeash.Material = Console.ReadLine();
                }

                productLogic.AddProduct(product);
                Console.WriteLine("Product added.");

            }
            else if (userInput == "2")
            {
                Console.Write("Enter the DogLeash name to retrieve: ");
                string leashName = Console.ReadLine();
                DogLeash retrievedLeash = productLogic.GetDogLeashByName(leashName);

                if (retrievedLeash != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize(retrievedLeash));
                }
                else
                {
                    Console.WriteLine("DogLeash not found.");
                }
            }

            Console.WriteLine("Press 1 to add a product");
            Console.WriteLine("Press 2 to get a DogLeash by name");
            Console.WriteLine("Type 'exit' to quit");

            userInput = Console.ReadLine();
        }
    }
}