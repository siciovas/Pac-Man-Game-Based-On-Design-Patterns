using ClassLibrary.Adapter;
using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.StrongMob;
using ClassLibrary.Mobs.WeakMob;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.TemplateMethod
{
    public class FourthLevelLoader : MapLoader
    {
        protected override void CreateCoins(ref ObservableCollection<Coin> Coins)
        {
            _CoinMapProviderAdapter = new CoinMapProviderAdapter(_CoinMapProvider);
            _CoinFactory = new SilverCoinCreator();
            var firstHalf = _CoinMapProviderAdapter.GetFirstHalfCoins(_CoinFactory);
            _CoinFactory = new GoldCoinCreator();
            Coins = _CoinMapProviderAdapter.GetSecondHalfCoins(_CoinFactory, firstHalf);
        }

        protected override void CreateMobs(ref ObservableCollection<Mob> Mobs)
        {
            _MobFactory = new StrongMobFactory();
            var secondZombie = _MobFactory.CreateZombie(50, 750);
            _MobFactory = new WeakMobFactory();
            var thirdZombie = _MobFactory.CreateZombie(500, 50);
            var fourthZombie = _MobFactory.CreateZombie(300, 300);
            var firstDemo = _MobFactory.CreateDemogorgon(500, 600);
            Mobs.Add(secondZombie);
            Mobs.Add(thirdZombie);
            Mobs.Add(fourthZombie);
            Mobs.Add(firstDemo);
        }
    }
}
