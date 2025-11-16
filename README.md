# NotiCatcher

NotiCatcher is a .NET MAUI app for Android that monitors incoming notifications and alerts the user when a specific keyword is detected in Whatsapp. The alert is delivered via vibration, even when the device is on silent mode.

## Features
- Detects notifications containing a user-defined keyword.
- Vibrates the device for immediate attention.
- Simple interface to set and save your keyword.
- Permissions check with direct link to notification listener settings.

## Usage
- Grant notification listener permission.
- Enter your desired keyword in the input field.
- Save the keyword.
- From now on, notifications containing the keyword will trigger vibration alerts.

## Notes
- Android 8+ requires a foreground service to run reliably in the background.
- App must be opened at least once for the autostart feature to work after reboot.
