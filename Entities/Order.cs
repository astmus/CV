using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Order
	{
		[Required]
		public int CustomerId { get; set; }
		[Required]
		public string Name { get; set; }
		public double Price { get; set; }
	}
}
