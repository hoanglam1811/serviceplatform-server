using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class ServiceDTO
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid CategoryId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Type { get; set; }
		public string? Duration { get; set; }
		public string? ServiceArea { get; set; }
		public decimal? OriginalPrice { get; set; }
		public decimal? DiscountPrice { get; set; }
		public string? Status { get; set; }
		public string? ImageUrl { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	// DTO khi tạo mới
	public class CreateServiceDTO
	{
		public Guid UserId { get; set; }
		public Guid CategoryId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Type { get; set; }
		public string? Duration { get; set; }
		public string? ServiceArea { get; set; }
		public decimal? OriginalPrice { get; set; }
		public decimal? DiscountPrice { get; set; }
		public string? Status { get; set; }
		public string? ImageUrl { get; set; }
	}

	// DTO khi cập nhật
	public class UpdateServiceDTO
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Type { get; set; }
		public string? Duration { get; set; }
		public string? ServiceArea { get; set; }
		public decimal? OriginalPrice { get; set; }
		public decimal? DiscountPrice { get; set; }
		public string? Status { get; set; }
		public string? ImageUrl { get; set; }
	}
}
