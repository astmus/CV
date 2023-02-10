using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfClient.Validations
{
	
	public class EmailRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var res = Regex.IsMatch(value.ToString(), @"^([\w-\.\+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
			// Test if date is valid
			if (DateTime.TryParse(value.ToString(), out DateTime date))
			{
				// Date is not in the future, fail
				if (DateTime.Now > date)
					return new ValidationResult(false, "Please enter a date in the future.");
			}
			else
			{
				// Date is not a valid date, fail
				return new ValidationResult(false, "Value is not a valid date.");
			}

			// Date is valid and in the future, pass
			return ValidationResult.ValidResult;
		}
	}
}
