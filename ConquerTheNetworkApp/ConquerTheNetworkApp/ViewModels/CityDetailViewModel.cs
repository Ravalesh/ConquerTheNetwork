﻿using ConquerTheNetworkApp.Data;
using ConquerTheNetworkApp.Views;
using ConquerTheNetworkApp.Services;
using Xamarin.Forms;
using System;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace ConquerTheNetworkApp.ViewModels
{
	class CityDetailViewModel : ViewModelBase
	{
		private CityDetailView _page;

		private City _city;

		private CityScheduleViewModel _schedule;

		public CityDetailViewModel(CityDetailView page, City city)
		{
			_page = page;
			_city = city;
			_rating = 3;

			_schedule = new CityScheduleViewModel(city.Id, city.Name);
		}

		public string Id
		{
			get { return _city.Id; }
		}

		public string Name
		{
			get { return _city.Name; }
		}

		public string ImageUrl
		{
			get { return _city.ImageUrl; }
		}

		private double _rating;
		public double Rating
		{
			get { return _rating; }
			set
			{
				_rating = value;
				OnPropertyChanged();
			}
		}

		public async Task PrefetchSchedule()
		{
			await _schedule.GetSchedule(false);
		}

		private Xamarin.Forms.Command _ratingCommand;
		public Xamarin.Forms.Command RatingCommand
		{
			get
			{
				return _ratingCommand ??
					(_ratingCommand = new Xamarin.Forms.Command(async (o) =>
					{
						if (!CrossConnectivity.Current.IsConnected)
						{
							Notify("You seem to be offline... Try again later.", System.Drawing.Color.Gray, System.Drawing.Color.White);
							return;
						}

						IsLoading = true;

						try
						{
							await ServiceClient.Instance.SendRating(_rating);
						}
						catch (Exception e)
						{
							Notify(e.ToString(), System.Drawing.Color.Red, System.Drawing.Color.White);
							
							// probably want to store the rating somewhere so you can retry later
						}
						finally
						{
							IsLoading = false;
						}
					}, (o) => true));
			}
		}

		private Xamarin.Forms.Command _viewScheduleCommand;
		public Xamarin.Forms.Command ViewScheduleCommand
		{
			get
			{
				return _viewScheduleCommand ??
					(_viewScheduleCommand = new Xamarin.Forms.Command(async (o) =>
					{
						if (_page != null)
						{
							await _page.Navigation.PushAsync(new CityScheduleView(_schedule));
						}
					}, (o) => true));
			}
		}
	}
}
