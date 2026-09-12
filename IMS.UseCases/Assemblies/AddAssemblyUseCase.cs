using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class AddAssemblyUseCase : IAddAssemblyUseCase
    {
        private readonly IAssemblyRepository assemblyInventory;

        public AddAssemblyUseCase(IAssemblyRepository assemblyInventory)
        {
            this.assemblyInventory = assemblyInventory;
        }

        public async Task ExecuteAsync(Assembly assembly)
        {
            if (assembly == null) return;

            await assemblyInventory.AddAssemblyAsync(assembly);
        }
    }
}

