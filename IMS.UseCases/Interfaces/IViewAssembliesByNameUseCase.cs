using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IViewAssembliesByNameUseCase
    {
        Task<List<Assembly>> ExecuteAsync(string name = "");
    }
}