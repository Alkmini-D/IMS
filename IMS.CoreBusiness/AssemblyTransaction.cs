using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//sql server product transactions table and sell product page
namespace IMS.CoreBusiness
{
    public class AssemblyTransaction
    {
        public int AssemblyTransactionId { get; set; }

        [Required]
        public int AssemblyId { get; set; }

        [Required]
        public int QuantityBefore { get; set; }

        //action taken (purchase or product product)
        [Required]
        public AssemblyTransactionType ActivityType { get; set; }

        [Required]
        public int QuantityAfter { get; set; }

        public string? ProductionNumber { get; set; }
        public string? SalesOrderNumber { get; set; }

        public double? UnitPrice { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string DoneBy { get; set; }

        //Navigation Properties
        public Assembly Assembly { get; set; }
    }
}
