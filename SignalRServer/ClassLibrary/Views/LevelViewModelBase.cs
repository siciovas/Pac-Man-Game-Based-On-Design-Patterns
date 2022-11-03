using ClassLibrary.Commands;
using System.Threading.Tasks;

namespace ClassLibrary.Views
{
    public abstract class LevelViewModelBase : ViewModelBase
    {
        public abstract void OnRightClick();
        public abstract int score { get; set; }
        public abstract int opponentScore { get; set; }
        public abstract void OnDownClick();
        public abstract void OnUpClick();
        public abstract void OnLeftClick();
        public abstract void SendOponmentCoordinates(string serializedObject);

        public abstract void RemoveApple(RemoveAppleAtIndexCommand command);

        public abstract void RottenApple(RemoveRottenAppleAtIndexCommand command);

        public abstract Task RemoveCoin(RemoveCoinAtIndexCommand command);

        public abstract void RemoveCherry(RemoveCherryAtIndexCommand command);
        public abstract void UpdateOpScore(GivePointsToOpponentCommand command);

        public abstract void Move(string pos);

        public abstract void DamagePacman(int damage);

    }
}
