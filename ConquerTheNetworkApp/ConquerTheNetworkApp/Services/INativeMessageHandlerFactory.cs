using System.Net.Http;

namespace ConquerTheNetworkApp.Services
{
	public interface INativeMessageHandlerFactory
	{
		HttpMessageHandler GetNativeHandler();
	}
}
