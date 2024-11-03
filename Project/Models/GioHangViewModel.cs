namespace Project.Models
{
	public class GioHangViewModel
	{
		public IEnumerable<GioHang> DsGioHang { get; set; }
		public HoaDon HoaDon { get; set; }

		// Nếu bạn muốn tính tổng tại đây, có thể dùng thuộc tính sau
		public double TotalPrice
		{
			get
			{
				return DsGioHang?.Sum(item => item.Quantity * item.SanPham.price) ?? 0;
			}
		}
	}
}
