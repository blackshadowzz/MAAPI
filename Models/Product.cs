using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string ProductName { get; set; }=string.Empty;
        [Column(TypeName = "varchar(200)")]
        public string? Description { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? Barcode { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? Qty { get; set; }

        public decimal? UnitPriceIn { get; set; } = 0;
        public decimal? UnitPriceOut { get; set; } = 0;
        public string? ProductImage { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
