using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BashMeta;
using BashModel.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace BashView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            IoC.Services
                .AddTransient<PolygonReader>()
                .AddTransient<ShapeProcessor>()
                .AddTransient<MainViewModel>()
                .AddTransient<MainWindow>();

            IoC.Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = IoC.Get<MainWindow>();
            mainWindow.DataContext = IoC.Get<MainViewModel>();
            
            Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}
