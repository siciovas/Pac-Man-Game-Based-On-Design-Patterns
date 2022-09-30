using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF.Connection;
using WPF.Levels;
using WPF.Stores;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IConnectionProvider, ConnectionProvider>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<StartPageViewModel>();
            services.AddSingleton<FirstLevelViewModel>();
            services.AddSingleton<NavigationStore>();

            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });
            services.AddSingleton<StartPageView>(s => new StartPageView()
            {
                DataContext = s.GetRequiredService<StartPageViewModel>()
            });
            services.AddSingleton<FirstLevelView>(s => new FirstLevelView()
            {
                DataContext = s.GetRequiredService<FirstLevelViewModel>()
            });
            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
           /* INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();
*/
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
