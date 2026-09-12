using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IAssemblyTransactionRepository
    {
        Task AssemblyAsync(string productionNumber, Assembly assembly, int quantity, double price, string doneBy);
        Task SellAssemblyAsync(string salesOrderNumber, Assembly assembly, int quantity, double price, string doneBy);
        Task<IEnumerable<AssemblyTransaction>> GetAssemblyTransactionsAsync(string assemblyName, DateTime? dateFrom, DateTime? dateTo, AssemblyTransactionType? transactionType);
    }
}