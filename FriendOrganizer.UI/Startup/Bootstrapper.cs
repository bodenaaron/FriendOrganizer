using Autofac;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().AsSelf();    //Gewährt Zugriff auf MainView innerhalb des Containers
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>(); //Wenn IFriendDataService benötigt => Erstellt FriendDataService

            return builder.Build();
        }
    }
}
