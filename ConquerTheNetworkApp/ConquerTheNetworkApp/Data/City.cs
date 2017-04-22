﻿using System;
using Newtonsoft.Json;
using ConquerTheNetworkApp.Services;

namespace ConquerTheNetworkApp.Data
{
	public class City
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Logo { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTime LastUpdated { get; set; }

		[JsonIgnore]
		public string ImageUrl
		{
			get { return ServiceClient.ApiBaseAddress + Logo; }
		}
	}
}
