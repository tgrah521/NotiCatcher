namespace NotiCatcher
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            App.Current.UserAppTheme = AppTheme.Light;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}