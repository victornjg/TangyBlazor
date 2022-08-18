using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models
{
    public class ProductPriceDTO
    {
        public long Id { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public double Price { get; set; }
    }
}
