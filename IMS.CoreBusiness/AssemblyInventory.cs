using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// sql server product inventory table and add product page
namespace IMS.CoreBusiness
{
    public class AssemblyInventory
    {
        public int AssemblyId { get; set; }
        public Assembly? Assembly { get; set; }

        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }

        public int InventoryQuantity { get; set; }
    }
}

