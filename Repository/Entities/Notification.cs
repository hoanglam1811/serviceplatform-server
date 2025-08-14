﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Notification
	{
		public Guid? Id { get; set; }
		public string? Title { get; set; }
		public string? Message { get; set; }
		public string? Status { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid? UserId { get; set; }
		public User User { get; set; }
	}
}
