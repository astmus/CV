﻿using Microsoft.Extensions.DependencyInjection;

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfClient
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			ServiceCollection services = new ServiceCollection();			
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			int i = 0; 
		}
	}
}
