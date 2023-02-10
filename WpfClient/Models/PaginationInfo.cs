using WpfClient.ViewModels;

namespace WpfClient.Models
{
	public class PaginationInfo : BindableBase
	{
		public int ItemsCount { get => Get<int>(); set => Set(value); }
		public int PagesCount { get => Get<int>(); set => Set(value); }
		public int ItemsPerPage { get => Get(10); set => Set(value); }
		public int PageNumber { get => Get(0); set => Set(value); }
	}
}
