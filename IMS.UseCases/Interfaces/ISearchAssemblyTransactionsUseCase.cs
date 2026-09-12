using IMS.CoreBusiness;

namespace IMS.UseCases.Reports
{
    public interface ISearchAssemblyTransactionsUseCase
    {
        Task<IEnumerable<AssemblyTransaction>> ExecuteAsync(string assemblyName, DateTime? dateFrom, DateTime? dateTo, AssemblyTransactionType? transactionType);
    }
}