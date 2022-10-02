using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WPF.Connection;
using WPF.Game.AbstractFactory.Classes.StrongMob;
using WPF.Game.AbstractFactory.Classes.WeakMob;
using WPF.Game.AbstractFactory.Interfaces;
using WPF.Game.Factory.Classes;
using WPF.Game.Factory.Interfaces;
using WPF.Game.ViewModels;
using WPF.Game.Views;
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

            services.AddTransient<ICoin, GoldCoin>();
            services.AddTransient<ICoin, BronzeCoin>();
            services.AddTransient<ICoin, SilverCoin>();
            services.AddTransient<IGhost, WeakGhost>();
            services.AddTransient<IGhost, StrongGhost>();
            services.AddTransient<IZombie, WeakZombie>();
            services.AddTransient<IZombie, StrongZombie>();
            services.AddTransient<IDemogorgon, WeakDemogorgon>();
            services.AddTransient<IDemogorgon, StrongDemogorgon>();

            services.AddSingleton<IConnectionProvider, ConnectionProvider>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<StartPageViewModel>();
            services.AddSingleton<FirstLevelViewModel>();
            services.AddSingleton<SecondLevelViewModel>();
            services.AddSingleton<ThirdLevelView>();

            services.AddSingleton<NavigationStore>();

            services.AddTransient<CoinViewModel>();


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
            services.AddSingleton<SecondLevelView>(s => new SecondLevelView()
            {
                DataContext = s.GetRequiredService<SecondLevelViewModel>()
            });
            services.AddSingleton<ThirdLevelView>(s => new ThirdLevelView()
            {
                DataContext = s.GetRequiredService<ThirdLevelViewModel>()
            });
            services.AddTransient<CoinView>(s => new CoinView()
            {
                DataContext = s.GetRequiredService<ICoin>()
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
