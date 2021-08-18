using System;

namespace AdminService.V1.Models.Request
{
	/// <summary>
	/// 
	/// </summary>
	public class BankRequest
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string AccountNumber { get; set; }

		public string AccountType { get; set; }

		public bool? IsActive { get; set; }

		public string CreatedBy { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}
