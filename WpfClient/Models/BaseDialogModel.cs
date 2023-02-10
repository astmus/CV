using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Models
{
	public abstract class BaseDialogModel
	{
		public string Title { get; set; }

		public string Message { get; init; }

		public string CloseButtonText { get; init; }
	}
}
