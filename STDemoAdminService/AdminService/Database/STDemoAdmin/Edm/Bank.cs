using System;
using System.Collections.Generic;

#nullable disable

namespace AdminService.Database.STDemoAdmin.Edm
{
	public partial class Bank
	{
		public int Id { get; set; }

		public int PersonId { get; set; }

		public string Name { get; set; }

		public string AccountNumber { get; set; }

		public string RoutingNumber { get; set; }

		public string AccountType { get; set; }

		public bool? IsActive { get; set; }

		public string CreatedBy { get; set; }

		public DateTime CreatedDate { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public string ModifiedBy { get; set; }

		public virtual Person Person { get; set; }
	}
}
