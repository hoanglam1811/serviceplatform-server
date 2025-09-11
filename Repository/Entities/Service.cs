using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Services
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Type {  get; set; }
		public int? Duration { get; set; }
		public string? ServiceArea {  get; set; }
		public decimal? OriginalPrice { get; set; }
		public decimal DiscountPrice { get; set; }
		public string? Status { get; set; }
		public string? ImageUrl { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get;set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
		public Guid CategoryId { get; set; }
		public ServiceCategory Category { get; set; }
	}
}
