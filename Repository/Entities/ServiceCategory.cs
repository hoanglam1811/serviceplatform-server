using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class ServiceCategory
	{
		public Guid? Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }	
		public string? Icon { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
