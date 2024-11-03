using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class SanPham
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // Khởi tạo mặc định để tránh cảnh báo CS8618

        [Required]
        public double Price { get; set; }

        public string? Description { get; set; }

        public string ImageUrl { get; set; } = string.Empty; // Khởi tạo mặc định để tránh cảnh báo CS8618

        [Required]
        public int TheLoaiId { get; set; }

        [ForeignKey("TheLoaiId")]
        [ValidateNever]
        public TheLoai TheLoai { get; set; } = new TheLoai(); // Khởi tạo mặc định để tránh cảnh báo CS8618
    }
}
