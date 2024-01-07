using System.Globalization;
using CsvHelper;

namespace AlgorithmicTrading.Logic.Services;

public class CsvService : ICsvService
{
    public string GetCSV<T>(IEnumerable<T> data)
    {
        using var writer = new StringWriter();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteRecords(data);

        return writer.ToString();
    }
}