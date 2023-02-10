using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.ViewModels
{
	public abstract class BindableBase : INotifyPropertyChanged, INotifyPropertyChanging, System.IEquatable<BindableBase>
	{
		private readonly ConcurrentDictionary<string, object> _properties = new ConcurrentDictionary<string, object>();

		public event PropertyChangingEventHandler PropertyChanging;
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanging([CallerMemberName] string propertyName = default)
		{
			PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = default)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected T Get<T>(T defValue = default, [CallerMemberName] string name = default)
		{
			if (string.IsNullOrEmpty(name))
				return defValue;

			return (T)_properties.GetOrAdd(name, defValue);
		}

		protected bool Set(object value, [CallerMemberName] string name = default)
		{
			if (string.IsNullOrEmpty(name))
				return false;

			var isFound = _properties.TryGetValue(name, out var oldValue);
			if (isFound && Equals(value, oldValue))
				return false;

			OnPropertyChanging(name);

			_properties.AddOrUpdate(name, value, (s, o) => value);

			OnPropertyChanged(name);

			return true;
		}

		public bool Equals(BindableBase other)
		{
			return _properties.Equals(other._properties);
		}
	}
}
