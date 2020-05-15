namespace Taasks
{
    using Caliburn.Micro;

    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper() => Initialize();

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            _ = DisplayRootViewFor<RootViewModel>();
        }
    }
}