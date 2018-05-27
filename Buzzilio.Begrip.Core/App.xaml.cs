using Buzzilio.Begrip.Miner.Helpers;
using Buzzilio.Begrip.Core.Factories;
using Buzzilio.Begrip.Core.Helpers;
using Buzzilio.Begrip.Core.ViewModels;
using Buzzilio.Begrip.Core.Views;
using System.Windows;
using Buzzilio.Begrip.Utilities.Logging;
using System;
using Buzzilio.Begrip.Core.Settings;

namespace Buzzilio.Begrip.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Initialize logger
            InstanceStore.Logger = new BzLogger(RuntimeSettings._logDirectory);

            // Initialize database
            try
            {
                DatabaseHelper.InitializeDatabase(InstanceStore.DbConnectionString);
            }
            catch (Exception ex)
            {
                InstanceStore.Logger.LogException("App::OnStartup: DatabaseHelper.InitializeDatabase(...)", ex);
            }

            base.OnStartup(e);

            // Open app
            try
            {
                InstanceStore.ViewFactory.OpenView<MainView, MainViewModel>();
            }
            catch (Exception ex)
            {
                InstanceStore.Logger.LogException("App::OnStartup: InstanceStore.ViewFactory.OpenView(...)", ex);
            }
        }
    }
}
