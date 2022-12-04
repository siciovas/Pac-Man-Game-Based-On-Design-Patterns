using ClassLibrary.Bridge;
using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.WeakMob;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.TemplateMethod
{
    public class FirstLevelLoader : MapLoader
    {
        protected override void CreateCoins(ref ObservableCollection<Coin> Coins)
        {
            _CoinFactory = new BronzeCoinCreator();
            Coins = _CoinMapProvider.GetCoins(10, 800, 50, 600, _CoinFactory);
        }

        protected override void CreateMobs(ref ObservableCollection<Mob> Mobs)
        {
            _MobFactory = new WeakMobFactory();

            var firstGhost = _MobFactory.CreateGhost(500, 600);
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
