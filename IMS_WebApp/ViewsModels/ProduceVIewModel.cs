using System.ComponentModel.DataAnnotations;

namespace IMS.WebApp.ViewModels
{
    public class ProduceViewModel
    {
        [Required]
        public string ProductionNumber { get; set; }

        [Required]
        public int AssemblyId { get; set; }

        [Required]
        public string AssemblyName { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Quantity has to be greater than 1.")]
        public int QuantityToProduce { get; set; }

        public double AssemblyPrice { get; set; }
    }
}
