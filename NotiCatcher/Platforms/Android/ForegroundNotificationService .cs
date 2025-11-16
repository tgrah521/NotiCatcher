using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace NotiCatcher.Platforms.Android
{
    [Service(Exported = true)]
    public class ForegroundNotificationService : Service
    {
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 1001;
        public const string CHANNEL_ID = "foreground_service_channel";

        public override void OnCreate()
        {
            base.OnCreate();
            CreateNotificationChannel();
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, BuildNotification());
        }

        private Notification BuildNotification()
        {
            var builder = new NotificationCompat.Builder(this, CHANNEL_ID)
                .SetContentTitle("Notification Listener Active")
                .SetContentText("Der Listener läuft im Hintergrund")
                .SetOngoing(true);

            return builder.Build();
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(
                    CHANNEL_ID,
                    "Foreground Service Channel",
                    NotificationImportance.Low);
                var manager = (NotificationManager)GetSystemService(NotificationService);
                manager.CreateNotificationChannel(channel);
            }
        }

        public override IBinder OnBind(Intent intent) => null;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }
    }
}
