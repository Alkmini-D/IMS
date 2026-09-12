using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class EditAssemblyUseCase : IEditAssemblyUseCase
    {
        private readonly IAssemblyRepository assemblyRepository;

        public EditAssemblyUseCase(IAssemblyRepository assemblyRepository)
        {
            this.assemblyRepository = assemblyRepository;
        }

        public async Task ExecuteAsync(Assembly assembly)
        {
            await this.assemblyRepository.UpdateAssemblyAsync(assembly);
        }
    }
}
