
using System.Timers;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace RentACar.Droid
{
    [Activity(Label = "Rent A Car", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Timer timer = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Splash);

            timer = new Timer();
            timer.Interval = 2000;
            timer.Elapsed += new ElapsedEventHandler(t_Elapsed);
            timer.Start();
        }

        protected async void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();

            var intent = new Android.Content.Intent(this, typeof(MainActivity));
            if (this.Intent.Extras != null)
                intent.PutExtras(this.Intent.Extras);

            StartActivity(intent);
            OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
        }
    }
}