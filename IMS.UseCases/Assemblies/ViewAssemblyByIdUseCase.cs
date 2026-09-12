using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ViewAssemblyByIdUseCase : IViewAssemblyByIdUseCase
    {
        private readonly IAssemblyRepository assemblyRepository;

        public ViewAssemblyByIdUseCase(IAssemblyRepository assemblyRepository)
        {
            this.assemblyRepository = assemblyRepository;
        }

        public async Task<Assembly> ExecuteAsync(int assemblyId)
        {
            return await this.assemblyRepository.GetAssemblyByIdAsync(assemblyId);
        }
    }
}

