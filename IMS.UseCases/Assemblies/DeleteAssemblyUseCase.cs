using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class DeleteAssemblyUseCase : IDeleteAssemblyUseCase
    {
        private readonly IAssemblyRepository assemblyRepository;

        public DeleteAssemblyUseCase(IAssemblyRepository assemblyRepository)
        {
            this.assemblyRepository = assemblyRepository;
        }

        public async Task ExecuteAsync(int assemblyId)
        {
            await this.assemblyRepository.DeleteAssemblyAsync(assemblyId);
        }
    }
}

