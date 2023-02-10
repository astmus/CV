using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using WpfClient.ViewModels;

namespace WpfClient.Pages
{
	public class PageNavigateProvider : IPageNavigateProvider
	{
		IServiceProvider services;
		ModernWpf.Controls.Frame contentFrame
			=> services.GetRequiredService<MainWindow>().ContentFrame;
		MainWindowViewModel rooViewModel
			=> services.GetRequiredService<MainWindowViewModel>();
		public PageNavigateProvider(IServiceProvider serviceProvider)
		{
			services = serviceProvider;
		}

		public T NavigateToPage<T>() where T : BasePage
		{
			T Page = services.GetRequiredService<T>();
			rooViewModel.PageViewModel = Page.ViewModel;
			var navigationTarget = contentFrame.Navigate(Page);
			return Page;
		}

		public BasePage NavigateToPage(Type pageType)
		{
			if (!pageType.IsSubclassOf(typeof(BasePage)))
				throw new InvalidOperationException();
			var Page = (BasePage)services.GetRequiredService(pageType);
			rooViewModel.PageViewModel = Page.ViewModel;
			contentFrame.Navigate(Page);
			return Page;
		}

	}
}
