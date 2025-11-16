using Android.App;
using Android.Content;
using Android.OS;

namespace NotiCatcher.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == Intent.ActionBootCompleted)
            {
                var serviceIntent = new Intent(context, typeof(ForegroundNotificationService));
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    context.StartForegroundService(serviceIntent);
                else
                    context.StartService(serviceIntent);
            }
        }

    }
}
