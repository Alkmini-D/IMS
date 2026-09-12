using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Reports
{
    public class SearchAssemblyTransactionsUseCase : ISearchAssemblyTransactionsUseCase
    {
        private readonly IAssemblyTransactionRepository assemblyTransactionRepository;

        public SearchAssemblyTransactionsUseCase(IAssemblyTransactionRepository assemblyTransactionRepository)
        {
            this.assemblyTransactionRepository = assemblyTransactionRepository;
        }

        public async Task<IEnumerable<AssemblyTransaction>> ExecuteAsync(
            string assemblyName,
            DateTime? dateFrom,
            DateTime? dateTo,
            AssemblyTransactionType? transactionType)
        {
            return await this.assemblyTransactionRepository.GetAssemblyTransactionsAsync(
                assemblyName,
                dateFrom,
                dateTo,
                transactionType);
        }
    }
}

