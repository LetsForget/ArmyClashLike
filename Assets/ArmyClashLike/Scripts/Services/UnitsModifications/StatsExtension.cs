namespace ArmyClashLike.Gameplay
{
    public static class StatsExtension
    {
        public static Stats Apply(this Stats value, Modification modification)
        {
            switch (modification.statType)
            {
                case StatType.Attack:
                {
                    value.attack = ApplyValue(modification.applyType, modification.applyValue, value.attack);
                    break;
                }
                case StatType.AttackSpeed:
                {
                    value.attackSpeed = ApplyValue(modification.applyType, modification.applyValue, value.attackSpeed);
                    break;
                }
                case StatType.Health:
                {
                    value.health = ApplyValue(modification.applyType, modification.applyValue, value.health);
                    break;
                }
                case StatType.Speed:
                {
                    value.speed = ApplyValue(modification.applyType, modification.applyValue, value.speed);
                    break;
                }
            }
            return value;
        }

        private static float ApplyValue(ModifyType modifyType, float modifier, float value)
        {
            switch (modifyType)
            {
                case ModifyType.Add:
                {
                    return value + modifier;
                }
                case ModifyType.Multiply:
                {
                    return value * modifier;
                }
            }
            
            return value;
        }
    }
}