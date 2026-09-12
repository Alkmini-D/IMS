using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ValidateEnoughInventoriesForProducingUseCase : IValidateEnoughInventoriesForProducingUseCase
    {
        private readonly IAssemblyRepository assemblyRepository;

        public ValidateEnoughInventoriesForProducingUseCase(IAssemblyRepository assemblyRepository)
        {
            this.assemblyRepository = assemblyRepository;
        }

        public async Task<bool> ExecuteAsync(Assembly assembly, int quantity)
        {
            var prod = await assemblyRepository.GetAssemblyByIdAsync(assembly.AssemblyId);
            foreach (var pi in prod.AssemblyInventories)
            {
                if (pi.InventoryQuantity * quantity > pi.Inventory.Quantity)
                    return false;
            }

            return true;
        }
    }
}
