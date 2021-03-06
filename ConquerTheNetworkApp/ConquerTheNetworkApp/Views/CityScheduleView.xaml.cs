﻿using ConquerTheNetworkApp.ViewModels;
using Xamarin.Forms;

namespace ConquerTheNetworkApp.Views
{
	public partial class CityScheduleView : ContentPage
	{
		private CityScheduleViewModel ViewModel
		{
			get
			{
				return BindingContext as CityScheduleViewModel;
			}
			set { BindingContext = value; }
		}

		public CityScheduleView(CityScheduleViewModel viewModel)
		{
			ViewModel = viewModel;
			InitializeComponent();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			await ViewModel.GetSchedule(false);
		}

		public void Schedule_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;
		}
	}
}
