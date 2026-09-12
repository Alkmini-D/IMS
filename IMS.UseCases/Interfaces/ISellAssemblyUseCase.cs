using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface ISellAssemblyUseCase
    {
        Task ExecuteAsync(string salesOrderNumber, Assembly assembly, int quantity, string doneBy);
    }
}