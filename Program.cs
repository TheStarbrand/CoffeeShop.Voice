// See https://aka.ms/new-console-template for more information
using Microsoft.CognitiveServices.Speech;
using Spectre.Console;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;


var products = new List<Product>
{
	new Product(1, "Expresso", 2.50m),
	new Product(2, "Americano", 3.00m),
	new Product(3, "Cappuccino", 2.50m),
	new Product(4, "Latte", 3.00m),
	new Product(5, "Mocha", 3.00m),
	new Product(6, "Macciato", 3.00m),
	new Product(7, "Flat White", 3.00m),
	new Product(8, "Cortado", 3.00m),
	new Product(9, "Affogato", 3.00m),
	new Product(10, "Cold Brew", 3.00m)
};

await GetOrder();

async Task GetOrder() 
{
	bool continueOrdering = true;
	while (continueOrdering)
	{
		ShowProducts();
		Console.WriteLine("Which coffee would you like?");

		var config = SpeechConfig.FromSubscription("cdd3048a4f2549fcb0a88b49af91083a", "eastus");
		using var recognizer = new SpeechRecognizer(config);
		var result = await recognizer.RecognizeOnceAsync();

		if (result.Reason == ResultReason.RecognizedSpeech)
		{
			Console.WriteLine(result.Text);
		}

		if (result.Text.StartsWith("stop"))
		{
			continueOrdering = false;
		}
	}
}
void ShowProducts()
{
	var table = new Table();
	table.AddColumn("Name");
	table.AddColumn("Price");

	foreach (var product in products)
	{
		table.AddRow(product.Name, product.Price.ToString("C"));
	}
	AnsiConsole.Write(table);
}
record Product(int id, string Name, decimal Price);
