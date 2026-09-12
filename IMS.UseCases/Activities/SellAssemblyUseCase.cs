using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class SellAssemblyUseCase : ISellAssemblyUseCase
    {
        private readonly IAssemblyTransactionRepository assemblyTransactionRepository;
        private readonly IAssemblyRepository assemblyRepository;

        public SellAssemblyUseCase(
            IAssemblyTransactionRepository assemblyTransactionRepository,
            IAssemblyRepository assemblyRepository
            )
        {
            this.assemblyTransactionRepository = assemblyTransactionRepository;
            this.assemblyRepository = assemblyRepository;
        }

        public async Task ExecuteAsync(string salesOrderNumber, Assembly assembly, int quantity, string doneBy)
        {
            await this.assemblyTransactionRepository.SellAssemblyAsync(salesOrderNumber, assembly, quantity, assembly.Price, doneBy);

            assembly.Quantity -= quantity;
            await this.assemblyRepository.UpdateAssemblyAsync(assembly);
        }
    }
}