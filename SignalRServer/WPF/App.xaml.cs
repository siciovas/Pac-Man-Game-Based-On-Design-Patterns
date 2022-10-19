using ClassLibrary.Coins;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.Interfaces;
using ClassLibrary.Mobs.StrongMob;
using ClassLibrary.Mobs.WeakMob;
using ClassLibrary.Stores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WPF.Connection;
using WPF.Game.ViewModels;
using WPF.Game.Views;

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

            services.AddTransient<Coin, GoldCoin>();
            services.AddTransient<Coin, BronzeCoin>();
            services.AddTransient<Coin, SilverCoin>();
            services.AddTransient<Mob, WeakGhost>();
            services.AddTransient<Mob, StrongGhost>();
            services.AddTransient<Mob, WeakZombie>();
            services.AddTransient<Mob, StrongZombie>();
            services.AddTransient<Mob, WeakDemogorgon>();
            services.AddTransient<Mob, StrongDemogorgon>();

            services.AddSingleton<IConnectionProvider, ConnectionProvider>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<StartPageViewModel>();
            services.AddSingleton<FirstLevelViewModel>();
            services.AddSingleton<SecondLevelViewModel>();
            services.AddSingleton<ThirdLevelViewModel>();
            services.AddSingleton<FourthLevelViewModel>();
            services.AddSingleton<FifthLevelViewModel>();

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
            services.AddSingleton<SecondLevelView>(s => new SecondLevelView()
            {
                DataContext = s.GetRequiredService<SecondLevelViewModel>()
            });
            services.AddSingleton<ThirdLevelView>(s => new ThirdLevelView()
            {
                DataContext = s.GetRequiredService<ThirdLevelViewModel>()
            });
            services.AddSingleton<FourthLevelView>(s => new FourthLevelView()
            {
                DataContext = s.GetRequiredService<FourthLevelViewModel>()
            });
            services.AddSingleton<FifthLevelView>(s => new FifthLevelView()
            {
                DataContext = s.GetRequiredService<FifthLevelViewModel>()
            });
            services.AddTransient<CoinView>(s => new CoinView()
            {
                DataContext = s.GetRequiredService<Coin>()
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
