using ConquerTheNetworkApp.Data;
using ConquerTheNetworkApp.ViewModels;
using Xamarin.Forms;

namespace ConquerTheNetworkApp.Views
{
	public partial class CitiesView : ContentPage
	{
		private CitiesViewModel ViewModel
		{
			get
			{
				return BindingContext as CitiesViewModel;
			}
			set { BindingContext = value; }
		}

		public CitiesView()
		{
			ViewModel = new CitiesViewModel();
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			ViewModel.GetCities();
		}

		private async void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var city = e.SelectedItem as City;

			await Navigation.PushAsync(new CityDetailView(city));
		}
	}
}
