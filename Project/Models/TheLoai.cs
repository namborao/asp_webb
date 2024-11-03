using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class TheLoai
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Không được để trống tên thể loại!!!")]
        [Display(Name = "Thể Loại")]
        public string Name { get; set; } = string.Empty; // Khởi tạo mặc định để tránh cảnh báo CS8618

        [Required(ErrorMessage = "Không được để trống ngày tạo!!!")]
        [Display(Name = "Ngày Tạo")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
