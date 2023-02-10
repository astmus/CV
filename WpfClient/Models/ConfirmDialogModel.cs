using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Models
{
	public class ConfirmDialogModel : BaseDialogModel
	{
		public string ApplyButtonText { get; init; }

		public Action ApplyCallback { get; init; }

		public Action CancelCallback { get; init; }

		public static ConfirmDialogModel Confirm(string message, Action applyCallback, Action cancelCallback = null) =>
			new()
			{
				Message = message,
				ApplyButtonText = "YES",
				ApplyCallback = applyCallback,
				CancelCallback = cancelCallback,
				CloseButtonText = "NO"
			};
	}
}
