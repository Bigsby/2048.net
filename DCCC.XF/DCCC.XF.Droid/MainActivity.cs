using Android.App;
using Android.Content.PM;
using Android.OS;

namespace DCCC.XF.Droid
{
    [Activity(Label = "DCCC.XF", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            App.SetSize(new Xamarin.Forms.Size(
                Application.ApplicationContext.Resources.DisplayMetrics.WidthPixels / Application.ApplicationContext.Resources.DisplayMetrics.ScaledDensity,
                Application.ApplicationContext.Resources.DisplayMetrics.HeightPixels / Application.ApplicationContext.Resources.DisplayMetrics.ScaledDensity));
            LoadApplication(new App());


        }
    }
}

