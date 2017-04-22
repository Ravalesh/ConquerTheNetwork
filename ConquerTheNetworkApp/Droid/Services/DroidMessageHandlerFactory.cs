using System.Net.Http;
using ConquerTheNetworkApp.Services;
using Xamarin.Android.Net;

namespace ConquerTheNetworkApp.Droid.Services
{
	[Preserve]
	public class DroidMessageHandlerFactory : INativeMessageHandlerFactory
	{
		public HttpMessageHandler GetNativeHandler()
		{
			return new AndroidClientHandler();
		}
	}
}
