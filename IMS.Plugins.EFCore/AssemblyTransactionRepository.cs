using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//assembly transaction table sql server
namespace IMS.Plugins.EFCore
{
    public class AssemblyTransactionRepository : IAssemblyTransactionRepository
    {
        private readonly IMSContext db;
        private readonly IAssemblyRepository assemblyRepository;

        public AssemblyTransactionRepository(IMSContext db, IAssemblyRepository assemblyRepository)
        {
            this.db = db;
            this.assemblyRepository = assemblyRepository;
        }

        public async Task<IEnumerable<AssemblyTransaction>> GetAssemblyTransactionsAsync(
            string assemblyName,
            DateTime? dateFrom,
            DateTime? dateTo,
            AssemblyTransactionType? transactionType)
        {
            if (dateTo.HasValue) dateTo = dateTo.Value.AddDays(1);
            var query = from pt in db.AssemblyTransactions
                        join prod in db.Assemblies on pt.AssemblyId equals prod.AssemblyId
                        where
                            (string.IsNullOrWhiteSpace(assemblyName) || prod.AssemblyName.ToLower().IndexOf(assemblyName.ToLower()) >= 0) &&
                            (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date) &&
                            (!transactionType.HasValue || pt.ActivityType == transactionType)
                        select pt;

            return await query.Include(x => x.Assembly).ToListAsync();
        }

        public async Task AssemblyAsync(string productionNumber, Assembly assembly, int quantity, double price, string doneBy)
        {
            
            var prod = await this.assemblyRepository.GetAssemblyByIdAsync(assembly.AssemblyId);
            if (prod != null)
            {
                foreach (var pi in prod.AssemblyInventories)
                {
                    int qtyBefore = pi.Inventory.Quantity;
                    pi.Inventory.Quantity -= quantity * pi.InventoryQuantity;                   

                    this.db.InventoryTransactions.Add(new InventoryTransaction
                    {
                        ProductionNumber = productionNumber,
                        InventoryId = pi.Inventory.InventoryId,
                        QuantityBefore = qtyBefore,
                        ActivityType = InventoryTransactionType.ProduceAssembly,
                        QuantityAfter = pi.Inventory.Quantity,
                        TransactionDate = DateTime.Now,
                        DoneBy = doneBy,
                        UnitPrice = price
                    });
                }
            }

            this.db.AssemblyTransactions.Add(new AssemblyTransaction
            {
                ProductionNumber = productionNumber,
                AssemblyId = assembly.AssemblyId,
                QuantityBefore = assembly.Quantity,
                ActivityType = AssemblyTransactionType.ProduceAssembly,
                QuantityAfter = assembly.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });
            await this.db.SaveChangesAsync();
        }

        public async Task SellAssemblyAsync(string salesOrderNumber, Assembly assembly, int quantity, double price, string doneBy)
        {
            this.db.AssemblyTransactions.Add(new AssemblyTransaction
            {
                SalesOrderNumber = salesOrderNumber,
                AssemblyId = assembly.AssemblyId,
                QuantityBefore = assembly.Quantity,
                QuantityAfter = assembly.Quantity - quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price,
                ActivityType = AssemblyTransactionType.SellAssembly
            });
            await this.db.SaveChangesAsync();
        }
    }
}

