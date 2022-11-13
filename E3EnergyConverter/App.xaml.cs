namespace E3EnergyConverter
{
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.SetCultureToGerman();
            base.OnStartup(e);
            
            MainWindow window = new MainWindow
            {
                DataContext = new VmMain()
            };
            window.Show();
        }

        private void SetCultureToGerman()
        {
            var germanCulture = new CultureInfo("de-DE");

            Thread.CurrentThread.CurrentCulture = germanCulture;
            Thread.CurrentThread.CurrentUICulture = germanCulture;
            CultureInfo.DefaultThreadCurrentCulture = germanCulture;
            CultureInfo.DefaultThreadCurrentUICulture = germanCulture;

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}