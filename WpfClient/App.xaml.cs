using DataAccess;

using Domain;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

using WpfClient.Pages;
using WpfClient.ViewModels;

namespace WpfClient
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public IServiceProvider Services { get; set; }
		public IConfiguration AppConfig { get; set; }
		public App()
		{

		}
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			 var configBuilde = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Development.json",false,true);
			AppConfig = configBuilde.Build();
			var section = AppConfig.GetSection("ApiAddress");
			ServiceCollection services = new ServiceCollection();
			services.AddDomainCore();
			services.AddRestDataAccess(section.Value);
			services.AddSingleton<MainWindow>();
			services.AddSingleton<MainWindowViewModel>();
			services.AddTransient<EditPage>();
			services.AddTransient<EditPageViewModel>();
			services.AddTransient<ViewPage>();
			services.AddTransient<ViewPageViewModel>();
			services.AddSingleton<BackgroundTask>();
			services.AddSingleton<IPageNavigateProvider, PageNavigateProvider>();
			Services = services.BuildServiceProvider();			
			var window = Services.GetRequiredService<MainWindow>();

			window.Show();

			//var pageProvider = Services.GetRequiredService<IPageNavigateProvider>();
			//window.ContentFrame.Navigate(pageProvider.NavigateToPage<ViewPage>());
			//var viewPage = pageProvider.GetPage<ViewPage>();

			//pageProvider.Navigation = NavigationService.GetNavigationService(viewPage);
			//pageProvider.Navigation.Navigate(viewPage);
		}
		
	}
}
