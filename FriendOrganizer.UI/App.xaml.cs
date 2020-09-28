using Autofac;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.Startup;
using FriendOrganizer.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Startup logik mit autofac
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper(); //erstellt bootstrapper instanz
            var container = bootstrapper.Bootstrap();   //holt den Container mit den festgelegten Attributen

            var mainWindow = container.Resolve<MainWindow>(); //Resolver ekennt welche Objekte er erstellen muss, tut das und erstellt das mainWindow =>Autofac
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {


            e.Handled = true;
        }
    }
}
