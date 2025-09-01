using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
	public class ServiceCategoryDTO
	{
		public Guid? Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Icon { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}

	// DTO khi tạo mới
	public class CreateServiceCategoryDTO
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Icon { get; set; }
	}

	// DTO khi cập nhật
	public class UpdateServiceCategoryDTO
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Icon { get; set; }
	}
}
