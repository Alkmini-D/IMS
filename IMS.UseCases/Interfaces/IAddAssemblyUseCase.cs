using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IAddAssemblyUseCase
    {
        Task ExecuteAsync(Assembly assembly);
    }
}