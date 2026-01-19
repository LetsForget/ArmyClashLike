using Cysharp.Threading.Tasks;
using Unity.Collections;

namespace ArmyClashLike.Gameplay.Move
{
    public interface ITargetSearcher
    {
        int[] TargetsForPlayer { get; }
        int[] TargetsForEnemy { get; }

        void Initialize(int playerUnitsCount, int enemyUnitsCount);
        UniTask Search(Unit[] playerUnits, Unit[] enemyUnits);
    }
}