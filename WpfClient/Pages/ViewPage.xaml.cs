using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

using WpfClient.ViewModels;

namespace WpfClient.Pages
{	
	public partial class ViewPage : BasePage
	{
		

		public ViewPage(ViewPageViewModel viewModel) : base(viewModel)
		{
			InitializeComponent();
		}		
	}
}
