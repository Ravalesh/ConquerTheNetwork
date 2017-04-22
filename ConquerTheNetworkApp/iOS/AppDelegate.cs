using ConquerTheNetworkApp.iOS.Services;
using ConquerTheNetworkApp.Services;
using Foundation;
using UIKit;

namespace ConquerTheNetworkApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			ServiceClient.Init(new iOSMessageHandlerFactory());

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
