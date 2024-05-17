using System;
using System.Collections.Generic;

namespace Global.Managers.Game
{
    using Global.Components.UserInterface;
    using Datas;
    using UserInterface;

    public static class GameSetter
    {
        private static Dictionary<GameData.GameStage, Action<GameData>> gameSetterFromLoading = new Dictionary<GameData.GameStage, Action<GameData>>()
        {
            { GameData.GameStage.Main, OnStageHomeLoad }
        };

        public static void SetGameFrom(GameData gameData)
        {
            gameSetterFromLoading[gameData.currentStageData](gameData);
        }

        #region setting functions

        private static void OnStageHomeLoad(GameData gameData)
        {
            Services.GetManager<UIManager>().ShowWindow<MainWindow>();
        }

        #endregion
    }
}
