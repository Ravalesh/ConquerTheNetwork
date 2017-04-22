using Xamarin.Forms;
using ConquerTheNetworkApp.Views;
using Plugin.Connectivity;
using Acr.UserDialogs;

namespace ConquerTheNetworkApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			CrossConnectivity.Current.ConnectivityChanged += (sender, e) =>
			{
				string not = "not ";
				Notify($"Your connection has changed. You are {e.IsConnected ? string.Empty : not} connected.",
				       System.Drawing.Color.Gray,
				       System.Drawing.Color.White); 
			};

			MainPage = new NavigationPage(new CitiesView());
		}

		protected void Notify(string message, System.Drawing.Color backgroundColor, System.Drawing.Color textColor)
		{
			var config = new ToastConfig(message)
				.SetDuration(2000)
				.SetBackgroundColor(backgroundColor)
				.SetMessageTextColor(textColor);

			UserDialogs.Instance.Toast(config);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
