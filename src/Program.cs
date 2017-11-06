using System;
using System.IO;
using CsvHelper;

namespace CsvReaderSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            var filePath = Path.Combine(currentDirectory, "Files\\catalogo.csv");          

            using (var textReader = File.OpenText(filePath))
            {
                var csv = new CsvReader(textReader);
                csv.Configuration.Delimiter = ";";
                // csv.Configuration.HasHeaderRecord = false;

                // Prepare Header
                csv.Configuration.PrepareHeaderForMatch = header => header?.Trim();
                csv.Configuration.PrepareHeaderForMatch = header => header.Replace(" ", string.Empty);
                csv.Configuration.PrepareHeaderForMatch = header => header.Replace("_", string.Empty);
                csv.Configuration.PrepareHeaderForMatch = header => header.ToLower();

                var records = csv.GetRecords<Product>();

                foreach (var product in records)
                {
                    var isDecimal = decimal.TryParse(product.Price, out decimal result);

                    Console.WriteLine($"{product.Id} - {product.Name} - {product.Price} - {isDecimal}");
                }
            }

            Console.ReadKey();
        }
    }
}
