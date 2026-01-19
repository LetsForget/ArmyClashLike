using System;

namespace ArmyClashLike.GameStates.States
{
    public interface IFormationSelectUI
    {
        event Action ToFightPressed;

        event Action LinePressed;
        event Action SquarePressed;
        event Action CirclePressed;

        void ShowToFightButton();
    }
}