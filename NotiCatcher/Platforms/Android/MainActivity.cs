using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Maui;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
        {
            // Removed this #### ugly statusbar
            Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
        }
    }
}
