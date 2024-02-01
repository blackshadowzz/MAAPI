using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string CategoryName { get; set; }=string.Empty;
        [Column(TypeName = "varchar(100)")]
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }=DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }=DateTime.Now;
        public string? UpdatedBy { get; set; }
    }
}
