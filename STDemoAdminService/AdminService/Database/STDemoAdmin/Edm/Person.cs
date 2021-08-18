using System;
using System.Collections.Generic;

#nullable disable

namespace AdminService.Database.STDemoAdmin.Edm
{
	public partial class Person
	{
		public Person()
		{
			Banks = new HashSet<Bank>();
		}

		public int Id { get; set; }

		public string AccountNumber { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public string DisplayName { get; set; }

		public string Ssn { get; set; }

		public bool? IsActive { get; set; }

		public DateTime? TerminationDate { get; set; }

		public string CreatedBy { get; set; }

		public DateTime CreatedDate { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public string ModifiedBy { get; set; }

		public virtual ICollection<Bank> Banks { get; set; }
	}
}
