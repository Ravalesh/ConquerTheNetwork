﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Acr.UserDialogs;
using ConquerTheNetworkApp.Services;
using ConquerTheNetworkApp.Droid.Services;

namespace ConquerTheNetworkApp.Droid
{
	[Activity(Label = "ConquerTheNetworkApp.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			UserDialogs.Init(this);
			ServiceClient.Init(new DroidMessageHandlerFactory());

			LoadApplication(new App());
		}
	}
}
