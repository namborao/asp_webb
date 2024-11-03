using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class HoaDon
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty; // Khởi tạo mặc định với chuỗi rỗng

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser(); // Khởi tạo mặc định

        public double Total { get; set; }
        public DateTime OrderDate { get; set; }

        public string? OrderStatus { get; set; } // Đánh dấu là nullable vì có thể không cần thiết

        [Required]
        public string PhoneNumber { get; set; } = string.Empty; // Khởi tạo mặc định để tránh cảnh báo CS8618

        [Required]
        public string Name { get; set; } = string.Empty; // Khởi tạo mặc định để tránh cảnh báo CS8618

        [Required]
        public string Address { get; set; } = string.Empty; // Khởi tạo mặc định để tránh cảnh báo CS8618
    }
}
