using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public readonly struct SpawnInfo
    {
        public readonly int count;
        public readonly Vector3 spawnPoint;
        public readonly Vector3 direction;
        public readonly FormationType formationType;
        public readonly Team team;

        public SpawnInfo(int count, Vector3 spawnPoint, Vector3 direction, FormationType formationType, Team team)
        {
            this.count = count;
            this.spawnPoint = spawnPoint;
            this.direction = direction;
            this.formationType = formationType;
            this.team = team;
        }
    }
}