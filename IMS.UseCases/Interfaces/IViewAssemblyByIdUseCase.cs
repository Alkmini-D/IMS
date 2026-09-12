using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IViewAssemblyByIdUseCase
    {
        Task<Assembly> ExecuteAsync(int assemblyId);
    }
}