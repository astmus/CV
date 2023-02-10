using System.Windows;
using System.Windows.Navigation;

using WpfClient.Pages;
using WpfClient.ViewModels;
using WpfClient.Windows.Base;

namespace WpfClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : BaseWindow
	{
		public MainWindow(MainWindowViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();	
			
		}
	}
}
