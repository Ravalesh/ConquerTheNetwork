﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ConquerTheNetworkApp.Data;
using Refit;
using System.Net;
using System.Net.Http;
using System;

namespace ConquerTheNetworkApp.Services
{
	public class ServiceClient
	{
		public static string ApiBaseAddress = "http://vslivesampleservice.azurewebsites.net";

		private IConferenceApi _client;

		public ServiceClient()
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri(ApiBaseAddress)
			};

			_client = RestService.For<IConferenceApi>(client);
		}

		public async Task<List<City>> GetCities()
		{
			return await _client.GetCities();
		}

		public async Task<Schedule> GetScheduleForCity(string id)
		{
			try
			{
				return await _client.GetScheduleForCity(id);
			}
			catch (ApiException ex)
			{
				if (ex.StatusCode == HttpStatusCode.NotFound)
				{
					return null;
				}
				throw;
			}
		}

		public async Task SendRating(double rating)
		{
			await _client.Rate(rating);
		}
	}
}
