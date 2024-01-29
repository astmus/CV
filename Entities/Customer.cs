using System.ComponentModel;

namespace Entities
{
	public class Customer
	{
		public int CustomerId { get; set; }
		public string? Name { get; set; }
		[DisplayName("Company Name")]
		public string? CompanyName { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
	}
}
