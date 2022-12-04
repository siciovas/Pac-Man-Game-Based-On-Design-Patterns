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
    public class SecondLevelLoader : MapLoader
    {
        protected override void CreateCoins(ref ObservableCollection<Coin> Coins)
        {
            _CoinMapProviderAdapter = new CoinMapProviderAdapter(_CoinMapProvider);
            _CoinFactory = new BronzeCoinCreator();
            var firstHalf = _CoinMapProviderAdapter.GetFirstHalfCoins(_CoinFactory);
            _CoinFactory = new SilverCoinCreator();
            Coins = _CoinMapProviderAdapter.GetSecondHalfCoins(_CoinFactory, firstHalf);
        }

        protected override void CreateMobs(ref ObservableCollection<Mob> Mobs)
        {
            _MobFactory = new WeakMobFactory();
            var firstGhost = _MobFactory.CreateGhost(500, 600);
            _MobFactory = new StrongMobFactory();
            var secondGhost = _MobFactory.CreateGhost(50, 750);
            var thirdGhost = _MobFactory.CreateGhost(500, 50);
            var fourthGhost = _MobFactory.CreateGhost(300, 300);
            Mobs.Add(firstGhost);
            Mobs.Add(secondGhost);
            Mobs.Add(thirdGhost);
            Mobs.Add(fourthGhost);
        }
    }
}
