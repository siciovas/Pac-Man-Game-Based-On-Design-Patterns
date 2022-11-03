using ClassLibrary.Coins;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.StrongMob;
using ClassLibrary.Mobs.WeakMob;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WPF.Connection;
using WPF.Game.Stores;
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

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<StartPageViewModel>();
            services.AddTransient<FirstLevelViewModel>();
            services.AddTransient<SecondLevelViewModel>();
            services.AddTransient<ThirdLevelViewModel>();
            services.AddTransient<FourthLevelViewModel>();
            services.AddTransient<FifthLevelViewModel>();
            services.AddTransient<GameFinishedViewModel>();

            services.AddTransient<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });
            services.AddTransient<StartPageView>(s => new StartPageView()
            {
                DataContext = s.GetRequiredService<StartPageViewModel>()
            });
            services.AddTransient<FirstLevelView>(s => new FirstLevelView()
            {
                DataContext = s.GetRequiredService<FirstLevelViewModel>()
            });
            services.AddTransient<SecondLevelView>(s => new SecondLevelView()
            {
                DataContext = s.GetRequiredService<SecondLevelViewModel>()
            });
            services.AddTransient<ThirdLevelView>(s => new ThirdLevelView()
            {
                DataContext = s.GetRequiredService<ThirdLevelViewModel>()
            });
            services.AddTransient<FourthLevelView>(s => new FourthLevelView()
            {
                DataContext = s.GetRequiredService<FourthLevelViewModel>()
            });
            services.AddTransient<FifthLevelView>(s => new FifthLevelView()
            {
                DataContext = s.GetRequiredService<FifthLevelViewModel>()
            });
            services.AddTransient<GameFinishedView>(s => new GameFinishedView()
            {
                DataContext = s.GetRequiredService<GameFinishedViewModel>()
            });
            services.AddTransient<CoinView>(s => new CoinView()
            {
                DataContext = s.GetRequiredService<Coin>()
            });

            services.AddSingleton<NavigationFacade>();

            _serviceProvider = services.BuildServiceProvider();


        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
