namespace AlgorithmicTrading.Logic.Services;

public interface ICsvService
{
    public string GetCSV<T>(IEnumerable<T> data);
}