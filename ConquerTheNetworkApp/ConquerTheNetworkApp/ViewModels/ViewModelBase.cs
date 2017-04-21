using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;

namespace ConquerTheNetworkApp.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private bool _isLoading;
		public bool IsLoading
		{
			get
			{
				return _isLoading;
			}
			set
			{
				_isLoading = value;
				OnPropertyChanged();
			}
		}

		protected void Notify(string message, System.Drawing.Color backgroundColor, System.Drawing.Color textColor)
		{
			var config = new ToastConfig(message)
				.SetDuration(2000)
				.SetBackgroundColor(backgroundColor)
				.SetMessageTextColor(textColor);

			UserDialogs.Instance.Toast(config);
		}
	}
}
