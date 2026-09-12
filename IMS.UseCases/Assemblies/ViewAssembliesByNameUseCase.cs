using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ViewAssembliesByNameUseCase : IViewAssembliesByNameUseCase
    {
        private readonly IAssemblyRepository assemblyRepository;

        public ViewAssembliesByNameUseCase(IAssemblyRepository assemblyRepository)
        {
            this.assemblyRepository = assemblyRepository;
        }

        public async Task<List<Assembly>> ExecuteAsync(string name = "")
        {
            return await this.assemblyRepository.GetAssembliesByNameAsync(name);
        }
    }
}

