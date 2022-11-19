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
    public class FifthLevelLoader : MapLoader
    {
        protected override void CreateCoins(ref ObservableCollection<Coin> Coins)
        {
            _CoinFactory = new GoldCoinCreator();
            Coins = _CoinMapProvider.GetCoins(10, 800, 50, 600, _CoinFactory);
        }

        protected override void CreateMobs(ref ObservableCollection<Mob> Mobs)
        {
            _MobFactory = new WeakMobFactory();
            var firstDemo = _MobFactory.CreateDemogorgon(500, 600);
            _MobFactory = new StrongMobFactory();
            var secondDemo = _MobFactory.CreateDemogorgon(50, 750);
            var thirdDemo = _MobFactory.CreateDemogorgon(500, 50);
            var fourthDemo = _MobFactory.CreateDemogorgon(300, 300);
            Mobs.Add(firstDemo);
            Mobs.Add(secondDemo);
            Mobs.Add(thirdDemo);
            Mobs.Add(fourthDemo);
        }
    }
}
