using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Customer
	{
		public uint CustomerId { get; set; }
		public string? Name { get; set; }
		public string? CompanyName { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
	}
}
