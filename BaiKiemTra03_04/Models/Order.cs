using System;
using System.ComponentModel.DataAnnotations;

namespace BaiKiemTra03_04.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int SupplierId { get; set; } // Khóa ngoại

        public string OrderStatus { get; set; }

        // Thuộc tính điều hướng
        public virtual Supplier Supplier { get; set; }
    }
}
