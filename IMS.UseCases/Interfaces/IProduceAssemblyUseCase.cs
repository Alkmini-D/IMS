using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IProduceAssemblyUseCase
    {
        Task ExecuteAsync(string productionNumber, Assembly assembly, int quantity, string doneBy);
    }
}