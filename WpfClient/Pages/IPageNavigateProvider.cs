using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfClient.Pages
{
	public interface IPageNavigateProvider
	{
		T NavigateToPage<T>() where T : BasePage;

		BasePage NavigateToPage(Type pageType);
	}
}
