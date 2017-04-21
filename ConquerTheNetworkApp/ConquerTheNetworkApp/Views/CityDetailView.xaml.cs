using System;
using ConquerTheNetworkApp.Data;
using ConquerTheNetworkApp.ViewModels;
using Xamarin.Forms;

namespace ConquerTheNetworkApp.Views
{
	public partial class CityDetailView : ContentPage
	{
		private CityDetailViewModel ViewModel
		{
			get
			{
				return BindingContext as CityDetailViewModel;
			}
			set { BindingContext = value; }
		}

		public CityDetailView(City city)
		{
			ViewModel = new CityDetailViewModel(this, city);
			InitializeComponent();
		}

		void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
		{
			var newStep = Math.Round(e.NewValue / 1.0);
			RatingSlider.Value = newStep * 1.0;
		}
	}
}