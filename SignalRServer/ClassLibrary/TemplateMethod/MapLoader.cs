using ClassLibrary.Adapter;
using ClassLibrary.Bridge;
using ClassLibrary.CoinMapping;
using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Fruits;
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
    public abstract class MapLoader
    {
        protected MobFactory _MobFactory;
        protected CoinFactory _CoinFactory;
        protected CoinMapProvider _CoinMapProvider = new CoinMapProvider();
        protected CoinMapProviderAdapter _CoinMapProviderAdapter;

        public void LoadMap(ref ObservableCollection<Apple> Apples, ref ObservableCollection<RottenApple> RottenApples, ref ObservableCollection<Cherry> Cherries,
            ref ObservableCollection<Strawberry> Strawberries, ref ObservableCollection<Spike> Spikes, ref ObservableCollection<Mob> Mobs,
            ref ObservableCollection<Coin> Coins, ref ObservableCollection<Wall> Walls)
        {
            CreateCoins(ref Coins);
            CreateMobs(ref Mobs);
            CreateFruits(ref Apples, ref RottenApples, ref Cherries, ref Strawberries);
            CreateWalls(ref Walls);
            CreateSpikes(ref Spikes);
        }

        private void CreateFruits(ref ObservableCollection<Apple> Apples, ref ObservableCollection<RottenApple> RottenApples, ref ObservableCollection<Cherry> Cherries, ref ObservableCollection<Strawberry> Strawberries)
        {
            Apples = Utils.Utils.CreateApples();
            RottenApples = Utils.Utils.CreateRottenApples();
            Cherries = Utils.Utils.CreateCherries();
            Strawberries = Utils.Utils.CreateStrawberries();
        }
        protected abstract void CreateCoins(ref ObservableCollection<Coin> Coins);
        protected abstract void CreateMobs(ref ObservableCollection<Mob> Mobs);
        private void CreateSpikes(ref ObservableCollection<Spike> Spikes)
        {
            for (int i = 250; i < 450; i += 30)
            {
                var temp = new Spike(new LethalFeature());
                temp.SetDamage();
                temp.Left = i;
                temp.Top = 150;
                Spikes.Add(temp);
            }

        }
        private void CreateWalls(ref ObservableCollection<Wall> Walls)
        {
            for (int i = 200; i < 500; i += 30)
            {
                var temp = new Wall(new StandardFeature());
                temp.SetDamage();
                temp.Left = 600;
                temp.Top = i;
                Walls.Add(temp);
            }
            for (int i = 200; i < 500; i += 30)
            {
                var temp = new Wall(new StandardFeature());
                temp.SetDamage();
                temp.Left = 200;
                temp.Top = i;
                Walls.Add(temp);
            }
        }

    }
}
