using System.Collections.Generic;
using System.Threading.Tasks;
using ConquerTheNetworkApp.Data;
using Fusillade;
using Refit;
using System.Net;
using System.Net.Http;
using System;
using Xamarin.Forms;
using Polly;
using System.Diagnostics;

namespace ConquerTheNetworkApp.Services
{
	public class ServiceClient
	{
		public static string ApiBaseAddress = "http://vslivesampleservice.azurewebsites.net";

		private readonly Lazy<IConferenceApi> _background;
 		private readonly Lazy<IConferenceApi> _userInitiated;
 		private readonly Lazy<IConferenceApi> _speculative;
		private static INativeMessageHandlerFactory _factory;

		public static void Init(INativeMessageHandlerFactory factory)
		{
			_factory = factory;
		}

		private static Lazy<ServiceClient> _instance = new Lazy<ServiceClient>(() => new ServiceClient());
		public static ServiceClient Instance => _instance.Value;

		private ServiceClient()
		{
			Func<HttpMessageHandler, IConferenceApi> createClient = messageHandler =>
						  {
							  var client = new HttpClient(messageHandler)
							  {
								  BaseAddress = new Uri(ApiBaseAddress)
							  };

							  return RestService.For<IConferenceApi>(client);
						  };

			var nativeHandler = _factory.GetNativeHandler();

			_background = new Lazy<IConferenceApi>(() => createClient(
				new RateLimitedHttpMessageHandler(nativeHandler, Priority.Background)));

			_userInitiated = new Lazy<IConferenceApi>(() => createClient(
				new RateLimitedHttpMessageHandler(nativeHandler, Priority.UserInitiated)));

			_speculative = new Lazy<IConferenceApi>(() => createClient(
				new RateLimitedHttpMessageHandler(nativeHandler, Priority.Speculative)));
		}

		public IConferenceApi Background
		{
			get { return _background.Value; }
		}
 
		public IConferenceApi UserInitiated
		{
			get { return _userInitiated.Value; }
		}
 
		public IConferenceApi Speculative
		{
			get { return _speculative.Value; }
		}

		public async Task<List<City>> GetCities(bool isUserInitiated)
		{
			var client = isUserInitiated ? UserInitiated : Background;

			return await Policy
				 .Handle<ApiException>(ex => ex.StatusCode != HttpStatusCode.NotFound)
				 .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 2, durationOfBreak: TimeSpan.FromMinutes(1))
				 .ExecuteAsync(async () =>
				 {
					 Debug.WriteLine("Trying cities service call...");
					 return await client.GetCities();
				 });
		}

		public async Task<Schedule> GetScheduleForCity(string id, bool isUserInitiated)
		{
			try
			{
				var client = isUserInitiated ? UserInitiated : Speculative;

				return await Policy
						 .Handle<ApiException>(ex => ex.StatusCode != HttpStatusCode.NotFound)
						 .WaitAndRetryAsync
						 (
							 retryCount: 3,
							 sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
						 )
						 .ExecuteAsync(async () =>
						 {
							 Debug.WriteLine("Trying schedule service call...");
							 return await client.GetScheduleForCity(id);
						 });
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
			await Policy
				.Handle<ApiException>(ex => ex.StatusCode != HttpStatusCode.NotFound)
				.WaitAndRetryAsync
				(
					retryCount: 5,
					sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
				)
				.ExecuteAsync(async () =>
				{
					Debug.WriteLine("Trying rating service call...");
					await UserInitiated.Rate(rating);
				});
		}
	}
}
