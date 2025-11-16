using Microsoft.Maui;
using Microsoft.Maui.Controls;
#if ANDROID
using Android.Content;
using Android.App;
using AndroidX.Core.App;
#endif


namespace NotiCatcher
{

    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            string savedKeyword = Preferences.Get("codeword", "");
            UpdatePermissionLabel();
            KeywordEntry.Text = savedKeyword;
        }

        public bool IsNotificationAccessEnabled()
        {
            #if ANDROID
                var context = Android.App.Application.Context;
                var enabledPackages = NotificationManagerCompat.GetEnabledListenerPackages(context);

                return enabledPackages.Contains(context.PackageName);
            #else
                return false;
            #endif
        }

        void RefreshPermissions(object sender, EventArgs e)
        {
            UpdatePermissionLabel();
        }

        private void UpdatePermissionLabel()
        {
            bool isEnabled = IsNotificationAccessEnabled();

            PermissionStatusLabel.Text = isEnabled
                ? "Berechtigung: Aktiv"
                : "Berechtigung: Nicht erteilt";
            PermissionStatusLabel.TextColor = isEnabled ? Colors.Green : Colors.Red;
        }

        private async void BtnEnable_Clicked(object sender, EventArgs e)
        {
#if ANDROID
            try
            {
                var intent = new Intent("android.settings.ACTION_NOTIFICATION_LISTENER_SETTINGS");

                intent.SetFlags(ActivityFlags.NewTask);

                Android.App.Application.Context.StartActivity(intent);

                await Task.Delay(1000);

                UpdatePermissionLabel();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Fehler beim Öffnen der Notification Listener Settings: " + ex.Message);
            }
#endif
        }

        async void Save(object sender, EventArgs e)
        {
            string text = KeywordEntry.Text;
            Console.WriteLine(text);
            if(!string.IsNullOrWhiteSpace(text))
            {
                Preferences.Set("codeword", text);
                await DisplayAlert("Erfolg", "Ihr Codewort wurde gesetzt", "Ok");
                Console.WriteLine("Keyword gespeichert: " + text);
            }
            else
            {
                await DisplayAlert("Fehler", "Bitte geben Sie einen Wert an", "Ok");
            }
        }

    }

}
