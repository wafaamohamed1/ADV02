using System;
using System.Collections.Generic;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
       
        List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200, Stock = 15 },
            new Product { Id = 2, Name = "Headphones", Category = "Electronics", Price = 45, Stock = 50 },
            new Product { Id = 3, Name = "T-Shirt", Category = "Clothing", Price = 25, Stock = 100 },
            new Product { Id = 4, Name = "Jeans", Category = "Clothing", Price = 60, Stock = 8 },
            new Product { Id = 5, Name = "Coffee Maker", Category = "Home", Price = 80, Stock = 0 },
            new Product { Id = 6, Name = "Mouse", Category = "Electronics", Price = 20, Stock = 30 }
        };
        Console.WriteLine("================ Task 01: Smart Product Search ================");

        // 1. All Electronics products
        Console.WriteLine("\n--- 1. All Electronics Products ---");
        var electronics = SearchProducts(products, p => p.Category == "Electronics");
        electronics.ForEach(p => Console.WriteLine($"ID: {p.Id} | {p.Name} | {p.Category} | ${p.Price}"));

        // 2. Products cheaper than $50
        Console.WriteLine("\n--- 2. Products Cheaper Than $50 ---");
        var cheapProducts = SearchProducts(products, p => p.Price < 50);
        cheapProducts.ForEach(p => Console.WriteLine($"ID: {p.Id} | {p.Name} | ${p.Price}"));

        // 3. Products that are in stock (Stock > 0)
        Console.WriteLine("\n--- 3. In-Stock Products ---");
        var inStockProducts = SearchProducts(products, p => p.Stock > 0);
        inStockProducts.ForEach(p => Console.WriteLine($"ID: {p.Id} | {p.Name} | Stock: {p.Stock}"));

        // 4. Clothing products under $100
        Console.WriteLine("\n--- 4. Clothing Products Under $100 ---");
        var cheapClothing = SearchProducts(products, p => p.Category == "Clothing" && p.Price < 100);
        cheapClothing.ForEach(p => Console.WriteLine($"ID: {p.Id} | {p.Name} | {p.Category} | ${p.Price}"));


      
        Console.WriteLine("\n\n================ Task 03: Custom Report Generator ================");

        // --- 3.1 Print Reports ---
        Console.WriteLine("\n--- 3.1 Print Reports ---");

        Console.WriteLine("\nScenario 1: Short Report");
        PrintReport(products, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

        Console.WriteLine("\nScenario 2: Detailed Report");
        PrintReport(products, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));

        // --- 3.2 Transform Products ---
        Console.WriteLine("\n--- 3.2 Transform Products ---");

        Console.WriteLine("\nScenario 3: Summary List");
        List<string> summaries = TransformProducts(products, p => $"{p.Name} (${p.Price})");
        summaries.ForEach(Console.WriteLine);

        Console.WriteLine("\nScenario 4: Price Label");
        List<string> priceLabels = TransformProducts(products, p => $"{p.Name}: {(p.Price > 100 ? "Expensive!" : "Affordable")}");
        priceLabels.ForEach(Console.WriteLine);

        // --- 3.3 Filter Products ---
        Console.WriteLine("\n--- 3.3 Filter Products ---");

        Console.WriteLine("\nScenario 5: Low-Stock Alert");
        List<Product> lowStockItems = FilterProducts(products, p => p.Stock < 20);
        lowStockItems.ForEach(p => Console.WriteLine($"[LOW STOCK] {p.Name}: only {p.Stock} left!"));
    }

  

    /*
     * Task 01 Method:
     * We used Func<Product, bool> because we need a custom filter logic that evaluates 
     * each product and returns a boolean value (true if it matches the criteria, false otherwise).
     */
    public static List<Product> SearchProducts(List<Product> products, Func<Product, bool> filter)
    {
        List<Product> result = new List<Product>();
        foreach (var product in products)
        {
            if (filter(product))
            {
                result.Add(product);
            }
        }
        return result;
    }

    /*
     * Task 3.1 Method:
     * We used Action<Product> because the caller only wants to perform an action (printing output)
     * on each item without returning any value (void return type).
     */
    public static void PrintReport(List<Product> products, Action<Product> printAction)
    {
        foreach (var product in products)
        {
            printAction(product);
        }
    }

    /*
     * Task 3.2 Method:
     * We used Func<Product, TResult> because we need to project/transform a Product object 
     * into a completely different representation or data type (e.g., a formatted string).
     */
    public static List<TResult> TransformProducts<TResult>(List<Product> products, Func<Product, TResult> transformer)
    {
        List<TResult> result = new List<TResult>();
        foreach (var product in products)
        {
            result.Add(transformer(product));
        }
        return result;
    }

    /*
     * Task 3.3 Method:
     * We used Predicate<Product> because it is C#'s built-in delegate specifically designed 
     * for criteria evaluation, taking a Product and returning a boolean.
     */
    public static List<Product> FilterProducts(List<Product> products, Predicate<Product> match)
    {
        List<Product> result = new List<Product>();
        foreach (var product in products)
        {
            if (match(product))
            {
                result.Add(product);
            }
        }
        return result;
    }
}