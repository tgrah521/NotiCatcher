#if ANDROID
using Android.Service.Notification;
using Android.Media;
using Android.App;
using Android.Content;
using Android.OS;
#endif


namespace NotiCatcher.Platforms.Android
{
    [Service(
        Name = "com.noticatcher.notificationlistener",
        Permission = "android.permission.BIND_NOTIFICATION_LISTENER_SERVICE",
        Exported = true)]
    [IntentFilter(new[] { "android.service.notification.NotificationListenerService" })]
    public class NotificationListener : NotificationListenerService
    {
        private const string CHANNEL_ID = "alert_channel";

        public override void OnNotificationPosted(StatusBarNotification sbn)
        {
            Console.WriteLine("Received Message!");
            var package = sbn.PackageName;
            var extras = sbn.Notification.Extras;
            var text = extras.GetCharSequence("android.text")?.ToString();
            string keyword = Preferences.Get("codeword", "");
            Console.WriteLine("Message: " + text);
            if (!string.IsNullOrEmpty(keyword) &&
                !string.IsNullOrEmpty(text) &&
                text.Contains(keyword, StringComparison.OrdinalIgnoreCase) && package.Equals("com.whatsapp", StringComparison.OrdinalIgnoreCase))
            {
                TriggerVibrationAlarm();
            }
        }

        private void TriggerVibrationAlarm()
        {
#if ANDROID
            var manager = (NotificationManager)GetSystemService(NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = manager.GetNotificationChannel(CHANNEL_ID);
                if (channel == null)
                {
                    channel = new NotificationChannel(CHANNEL_ID, "Alarm", NotificationImportance.High)
                    {
                        Description = "Alarmbenachrichtigung nur mit vibration"
                    };

                    channel.EnableVibration(true);
                    channel.SetVibrationPattern(new long[] { 0, 1500, 250, 1500 }); 
                    channel.SetSound(null, null);

                    manager.CreateNotificationChannel(channel);
                }
            }
#if ANDROID
            var notification = new Notification.Builder(this, CHANNEL_ID)
                .SetContentTitle("Alarm!")
                .SetContentText("Keyword erkant!")
                #if ANDROID
                .SetSmallIcon(Resource.Mipmap.appicon)
#endif
                .SetAutoCancel(true)
                .Build();


            manager.Notify(999, notification);
#endif
            var vibrator = (Vibrator)GetSystemService(VibratorService);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                vibrator.Vibrate(VibrationEffect.CreateWaveform(new long[] { 0, 1500, 250, 1500 }, -1));
            }
            else
            {
#pragma warning disable CS0618
                vibrator.Vibrate(new long[] { 0, 1500, 250, 1500 }, -1);
#pragma warning restore CS0618
            }
#endif
        }
    }
}
