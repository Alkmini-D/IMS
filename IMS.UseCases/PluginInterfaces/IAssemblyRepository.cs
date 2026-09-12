using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IAssemblyRepository
    {
        Task AddAssemblyAsync(Assembly assembly);
        Task<List<Assembly>> GetAssembliesByNameAsync(string name);
        Task<Assembly> GetAssemblyByIdAsync(int assemblyId);
        Task UpdateAssemblyAsync(Assembly assembly);
        Task DeleteAssemblyAsync(int assemblyId);
    }
}
