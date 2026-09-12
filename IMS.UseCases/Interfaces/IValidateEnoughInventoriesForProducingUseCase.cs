using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IValidateEnoughInventoriesForProducingUseCase
    {
        Task<bool> ExecuteAsync(Assembly assembly, int quantity);
    }
}