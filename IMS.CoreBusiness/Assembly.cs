using IMS.CoreBusiness.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//association with product page for the errors and sql server
namespace IMS.CoreBusiness
{
    public class Assembly
    {
        public int AssemblyId { get; set; }

        [Required]
        public string AssemblyName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to {0}")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to {0}")]
        [Product_EnsurePriceIsGreaterThanInventoriesPrice]
        public double Price { get; set; }

        public bool IsActive { get; set; } = true;

        public List<AssemblyInventory>? AssemblyInventories { get; set; }

        public double TotalInventoryCost()
        {
            return this.AssemblyInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0);
        }

        public bool ValidatePricing()
        {
            if (AssemblyInventories == null || AssemblyInventories.Count <= 0) return true;

            if (this.TotalInventoryCost() > Price) return false;

            return true;
        }
    }
}

