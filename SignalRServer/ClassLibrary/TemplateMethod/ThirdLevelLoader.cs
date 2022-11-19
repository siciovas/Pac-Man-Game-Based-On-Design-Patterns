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
    public class ThirdLevelLoader : MapLoader
    {
        protected override void CreateCoins(ref ObservableCollection<Coin> Coins)
        {
            _CoinFactory = new SilverCoinCreator();
            Coins = _CoinMapProvider.GetCoins(10, 800, 50, 600, _CoinFactory);
        }

        protected override void CreateMobs(ref ObservableCollection<Mob> Mobs)
        {
            _MobFactory = new StrongMobFactory();
            var firstGhost = _MobFactory.CreateGhost(500, 600);
            var secondGhost = _MobFactory.CreateGhost(50, 750);
            _MobFactory = new WeakMobFactory();
            var firstZombie = _MobFactory.CreateZombie(500, 50);
            var secondZombie = _MobFactory.CreateZombie(300, 300);
            Mobs.Add(firstGhost);
            Mobs.Add(secondGhost);
            Mobs.Add(firstZombie);
            Mobs.Add(secondZombie);
        }
    }
}
