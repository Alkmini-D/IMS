using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IEditAssemblyUseCase
    {
        Task ExecuteAsync(Assembly assembly);
    }
}