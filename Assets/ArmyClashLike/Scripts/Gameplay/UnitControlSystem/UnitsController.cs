using System;
using ArmyClashLike.Gameplay.Move;
using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public class UnitsController : IUnitsController
    {
        public event Action<Team> BattleOver;
        
        private readonly ITargetSearcher targetSearcher;
        private readonly IUnitMover unitMover;
        private readonly IUnitAttacker unitAttacker;
        
        private UnitSet playerUnitSet;
        private UnitSet enemyUnitSet;
        
        private bool battleOver;
        
        public UnitsController(ITargetSearcher targetSearcher, IUnitMover unitMover, IUnitAttacker unitAttacker)
        {
            this.targetSearcher = targetSearcher;
            this.unitMover = unitMover;
            this.unitAttacker = unitAttacker;
        }
        
        public void Initialize(UnitSet playerUnitSet, UnitSet enemyUnitSet)
        {
            targetSearcher.Initialize(playerUnitSet.units.Length, enemyUnitSet.units.Length);
            
            unitMover.Initialize(playerUnitSet);
            unitMover.Initialize(enemyUnitSet);
            
            this.playerUnitSet = playerUnitSet;
            this.enemyUnitSet = enemyUnitSet;
        }

        public void Update(float deltaTime)
        {
            if (battleOver)
            {
                return;
            }
            
            targetSearcher.Search(playerUnitSet.units, enemyUnitSet.units);

            Attack(playerUnitSet, targetSearcher.TargetsForPlayer, enemyUnitSet);
            Attack(enemyUnitSet, targetSearcher.TargetsForEnemy, playerUnitSet);

            if (CheckAllDead(playerUnitSet))
            {
                BattleOver?.Invoke(Team.Enemy);
                battleOver = true;
            }

            if (CheckAllDead(enemyUnitSet))
            {
                BattleOver?.Invoke(Team.Player);
                battleOver = true;
            }
            
            void Attack(UnitSet attackSet, int[] targets, UnitSet targetSet)
            {
                for (var i = 0; i < attackSet.units.Length; i++)
                {
                    var unit = attackSet.units[i];

                    if (unit.isDead)
                    {
                        continue;
                    }
                    
                    var targetIndex = targets[i];

                    if (targetIndex < 0)
                    {
                        continue;
                    }
                    
                    var targetUnit = targetSet.units[targetIndex];

                    if (targetUnit.isDead)
                    {
                        continue;
                    }
                    
                    unitAttacker.TryAttack(ref unit, ref targetUnit, deltaTime);
                    
                    attackSet.units[i] = unit;
                    targetSet.units[targetIndex] = targetUnit;
                }
            }

            bool CheckAllDead(UnitSet unitSet)
            {
                var allDead = true;
                
                for (var i = 0; i < unitSet.units.Length; i++)
                {
                    if (unitSet.units[i].isDead)
                    {
                        unitSet.units[i].container.PlayDead();
                    }
                    else
                    {
                        allDead = false;
                    }
                }
                
                return allDead;
            }
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            if (battleOver)
            {
                return;
            }
            
            MakeMove(playerUnitSet, targetSearcher.TargetsForPlayer, enemyUnitSet);
            MakeMove(enemyUnitSet, targetSearcher.TargetsForEnemy, playerUnitSet);
            
            void MakeMove(UnitSet movingSet, int[] targets, UnitSet targetSet)
            {
                for (var i = 0; i < movingSet.units.Length; i++)
                {
                    var unit = movingSet.units[i];

                    if (unit.isDead)
                    {
                        continue;
                    }
                    
                    var targetIndex = targets[i];
                    var targetUnit = targetSet.units[targetIndex];

                    if (targetUnit.isDead)
                    {
                        continue;
                    }
                    
                    var targetPos = targetUnit.container.Position;
                    unitMover.Move(unit, targetPos, fixedDeltaTime);
                }
            }
        }
    }
}