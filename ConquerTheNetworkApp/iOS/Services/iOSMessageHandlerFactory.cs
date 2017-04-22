using System.Net.Http;
using ConquerTheNetworkApp.iOS.Services;
using ConquerTheNetworkApp.Services;
using Xamarin.Forms;

namespace ConquerTheNetworkApp.iOS.Services
{
	public class iOSMessageHandlerFactory : INativeMessageHandlerFactory
	{
		public HttpMessageHandler GetNativeHandler()
		{
			return new NSUrlSessionHandler();
		}
	}
}
