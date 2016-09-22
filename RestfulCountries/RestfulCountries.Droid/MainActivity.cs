using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Utils;

namespace RestfulCountries.Droid
{
    [Activity(Label = "RestfulCountries", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;

            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                // Use the trace writer to log exceptions so HockeyApp finds them
                HockeyApp.MetricsManager.TrackEvent("Caught exception -" + args.Exception);
                args.Handled = true;
            };

            CrashManager.Register(this, App.Keys.ANALYTICS);
            HockeyLog.LogLevel = 4; // Info, show informative or higher log messages

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}