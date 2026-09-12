using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ProduceAssemblyUseCase : IProduceAssemblyUseCase
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly IAssemblyRepository assemblyRepository;
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IAssemblyTransactionRepository assemblyTransactionRepository;

        public ProduceAssemblyUseCase(
            IInventoryRepository inventoryRepository,
            IAssemblyRepository productRepository,
            IInventoryTransactionRepository inventoryTransactionRepository,
            IAssemblyTransactionRepository assemblyTransactionRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.assemblyRepository = productRepository;
            this.inventoryTransactionRepository = inventoryTransactionRepository;
            this.assemblyTransactionRepository = assemblyTransactionRepository;
        }

        public async Task ExecuteAsync(string productionNumber, Assembly assembly, int quantity, string doneBy)
        {
            await this.assemblyTransactionRepository.AssemblyAsync(productionNumber, assembly, quantity, assembly.Price, doneBy);

            assembly.Quantity += quantity;
            await this.assemblyRepository.UpdateAssemblyAsync(assembly);
        }

    }
}

